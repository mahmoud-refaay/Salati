using DAL.DTOs;

namespace DAL.Repositories;

/// <summary>
/// Repository التنبيهات — إعدادات التنبيه + سجل التنبيهات.
/// </summary>
public class AlertRepository : BaseRepository
{
    /// <summary>جيب كل إعدادات التنبيهات (5 صلوات)</summary>
    public async Task<IEnumerable<AlertConfigDTO>> GetAllConfigsAsync()
        => await QueryAsync<AlertConfigDTO>("SP_GetAllAlertConfigs");

    /// <summary>حدّث إعدادات تنبيه لصلاة</summary>
    public async Task UpdateConfigAsync(
        byte prayer, bool isEnabled, int minutesBefore,
        int soundId, byte alertType, bool alertAtAdhanTime, int volume)
    {
        await ExecuteAsync("SP_UpdateAlertConfig", new
        {
            Prayer = prayer,
            IsEnabled = isEnabled,
            MinutesBefore = minutesBefore,
            SoundID = soundId,
            AlertType = alertType,
            AlertAtAdhanTime = alertAtAdhanTime,
            Volume = volume
        });
    }

    /// <summary>سجّل تنبيه في الـ Log</summary>
    public async Task LogAlertAsync(byte prayer, TimeSpan prayerTime, byte alertType, int minutesBefore)
    {
        await ExecuteAsync("SP_InsertAlertLog", new
        {
            Prayer = prayer,
            PrayerTime = prayerTime,
            AlertType = alertType,
            MinutesBefore = minutesBefore
        });
    }
}
