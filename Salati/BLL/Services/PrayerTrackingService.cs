using BLL.Common;
using BLL.Extensions;
using DAL.DTOs;
using DAL.Enums;
using DAL.Repositories;

namespace BLL.Services;

/// <summary>
/// Service تتبع الصلاة — يتعامل مع Mark/Unmark/Stats.
/// 
/// ═══ الاستخدام ═══
///   var service = new PrayerTrackingService();
///   
///   // تعليم صلاة
///   var result = await service.MarkPrayerAsync(ePrayer.Fajr, ePrayerStatus.OnTime);
///   
///   // إحصائيات
///   var stats = await service.GetTodayTrackingAsync();
/// </summary>
public class PrayerTrackingService
{
    private readonly PrayerTrackingRepository _repo = new();

    // ═══════════════════════════════════════
    //  Mark / Unmark — الأهم!
    // ═══════════════════════════════════════

    /// <summary>تعليم صلاة كـ "صليت"</summary>
    public async Task<Result> MarkPrayerAsync(ePrayer prayer, ePrayerStatus status)
    {
        // ── Validation ──
        if (status == ePrayerStatus.NotMarked)
            return Result.Failure("لازم تحدد الحالة (في الوقت / متأخر)");

        // ── DAL ──
        int spResult = await _repo.MarkPrayerAsync(prayer, status);

        // ── Result mapping (switch expression!) ──
        return spResult switch
        {
            0 => Result.Success(),
            -1 => Result.Failure($"لم يحن وقت {prayer.ToArabicName()} بعد"),
            -2 => Result.Failure("لا توجد مواعيد صلاة لليوم — تأكد من تحديث المواعيد"),
            _ => Result.Failure("خطأ غير متوقع في تعليم الصلاة")
        };
    }

    /// <summary>إلغاء تعليم صلاة</summary>
    public async Task<Result> UnmarkPrayerAsync(ePrayer prayer)
    {
        int rows = await _repo.UnmarkPrayerAsync(prayer);

        return rows > 0
            ? Result.Success()
            : Result.Failure($"تعذّر إلغاء تعليم {prayer.ToArabicName()}");
    }

    // ═══════════════════════════════════════
    //  Today's Tracking
    // ═══════════════════════════════════════

    /// <summary>جيب حالة الـ 5 صلوات النهارده</summary>
    public async Task<Result<List<PrayerTrackingDTO>>> GetTodayTrackingAsync()
    {
        var list = (await _repo.GetTodayTrackingAsync()).ToList();

        return list.Count > 0
            ? Result<List<PrayerTrackingDTO>>.Success(list)
            : Result<List<PrayerTrackingDTO>>.Failure("لا توجد بيانات تتبع لليوم");
    }

    // ═══════════════════════════════════════
    //  Fill Missed Days
    // ═══════════════════════════════════════

    /// <summary>ملء الأيام الفائتة = Missed (يتنادى عند فتح البرنامج)</summary>
    public async Task FillMissedDaysAsync()
    {
        await _repo.FillMissedDaysAsync();
    }

    // ═══════════════════════════════════════
    //  Statistics
    // ═══════════════════════════════════════

    /// <summary>إحصائيات الشهر</summary>
    public async Task<Result<List<DailyStatsDTO>>> GetMonthlyStatsAsync(int month, int year)
    {
        if (month < 1 || month > 12)
            return Result<List<DailyStatsDTO>>.Failure("الشهر لازم يكون بين 1 و 12");

        if (year < 2020 || year > 2030)
            return Result<List<DailyStatsDTO>>.Failure("السنة غير صالحة");

        var list = (await _repo.GetMonthlyStatsAsync(month, year)).ToList();

        return list.Count > 0
            ? Result<List<DailyStatsDTO>>.Success(list)
            : Result<List<DailyStatsDTO>>.Failure("لا توجد إحصائيات لهذا الشهر");
    }

    /// <summary>إحصائيات أسبوعية</summary>
    public async Task<Result<List<WeeklyPrayerStatsDTO>>> GetWeeklyStatsAsync()
    {
        var list = (await _repo.GetWeeklyStatsAsync()).ToList();

        return list.Count > 0
            ? Result<List<WeeklyPrayerStatsDTO>>.Success(list)
            : Result<List<WeeklyPrayerStatsDTO>>.Failure("لا توجد إحصائيات أسبوعية");
    }

    /// <summary>عدد أيام الالتزام المتتالية 🔥</summary>
    public async Task<int> GetStreakCountAsync()
    {
        return await _repo.GetStreakCountAsync();
    }

    /// <summary>نسبة الالتزام الكلية (%)</summary>
    public async Task<decimal> GetOverallPercentageAsync()
    {
        return await _repo.GetOverallPercentageAsync();
    }
}
