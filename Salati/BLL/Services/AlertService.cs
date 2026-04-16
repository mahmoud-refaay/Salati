using BLL.Common;
using DAL.DTOs;
using DAL.Enums;
using DAL.Repositories;

namespace BLL.Services;

/// <summary>
/// Service التنبيهات — يقرأ إعدادات التنبيه ويقرر متى ينبّه.
/// </summary>
public class AlertService
{
    private readonly AlertRepository _alertRepo = new();
    private readonly PrayerTimesRepository _timesRepo = new();

    /// <summary>جيب إعدادات كل صلاة</summary>
    public async Task<Result<List<AlertConfigDTO>>> GetAllConfigsAsync()
    {
        var list = (await _alertRepo.GetAllConfigsAsync()).ToList();
        return list.Count > 0
            ? Result<List<AlertConfigDTO>>.Success(list)
            : Result<List<AlertConfigDTO>>.Failure("لا توجد إعدادات تنبيهات");
    }

    /// <summary>
    /// فحص — هل في تنبيه لازم يطلع دلوقتي؟
    /// بترجع (Prayer, AlertType, MinutesBefore) لو في تنبيه.
    /// بتترجع لكل صلاة enabledAlert + adhanAlert.
    /// </summary>
    public async Task<(ePrayer Prayer, byte AlertType, int MinutesBefore, TimeSpan PrayerTime)?> 
        CheckForAlertAsync()
    {
        // 1. جيب مواعيد النهارده
        var times = await _timesRepo.GetTodayTimesAsync();
        if (times == null) return null;

        // 2. جيب إعدادات التنبيهات
        var configs = (await _alertRepo.GetAllConfigsAsync()).ToList();
        if (configs.Count == 0) return null;

        var now = DateTime.Now.TimeOfDay;

        // 3. لكل صلاة — شوف لو وقت التنبيه
        var prayerTimes = new[]
        {
            (ePrayer.Fajr,    times.FajrTime),
            (ePrayer.Dhuhr,   times.DhuhrTime),
            (ePrayer.Asr,     times.AsrTime),
            (ePrayer.Maghrib, times.MaghribTime),
            (ePrayer.Isha,    times.IshaTime),
        };

        foreach (var (prayer, prayerTime) in prayerTimes)
        {
            var config = configs.FirstOrDefault(c => c.Prayer == (byte)prayer);
            if (config == null || !config.IsEnabled) continue;

            // ── تنبيه قبل الصلاة ──
            if (config.MinutesBefore > 0)
            {
                var alertTime = prayerTime.Subtract(TimeSpan.FromMinutes(config.MinutesBefore));
                if (IsWithinWindow(now, alertTime))
                {
                    return (prayer, config.AlertType, config.MinutesBefore, prayerTime);
                }
            }

            // ── تنبيه عند وقت الأذان ──
            if (config.AlertAtAdhanTime && IsWithinWindow(now, prayerTime))
            {
                return (prayer, config.AlertType, 0, prayerTime);
            }
        }

        return null;
    }

    /// <summary>سجّل تنبيه</summary>
    public async Task LogAlertAsync(ePrayer prayer, TimeSpan prayerTime, byte alertType, int minutesBefore)
    {
        await _alertRepo.LogAlertAsync((byte)prayer, prayerTime, alertType, minutesBefore);
    }

    /// <summary>
    /// هل الوقت الحالي في نافذة ±30 ثانية من الوقت المحدد?
    /// (علشان الـ Timer بيشتغل كل 30 ثانية)
    /// </summary>
    private static bool IsWithinWindow(TimeSpan now, TimeSpan target)
    {
        var diff = (now - target).TotalSeconds;
        return diff >= 0 && diff < 35; // نافذة 35 ثانية
    }
}
