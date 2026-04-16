using System.Data;
using Dapper;
using DAL.DTOs;
using DAL.Enums;

namespace DAL.Repositories;

/// <summary>
/// Repository تتبع الصلاة — يتعامل مع جدول PrayerTracking.
/// 
/// SPs المستخدمة:
///   - SP_MarkPrayer             → تعليم صلاة (مع Output parameter)
///   - SP_UnmarkPrayer           → إلغاء تعليم
///   - SP_GetTodayTracking       → حالة صلوات اليوم
///   - SP_FillMissedDays         → ملء الأيام الفائتة
///   - SP_GetMonthlyTrackingStats → إحصائيات شهرية
///   - SP_GetWeeklyTrackingStats  → إحصائيات أسبوعية
///   - SP_GetStreakCount          → عدد أيام الالتزام المتتالية
///   - SP_GetOverallPercentage   → نسبة الالتزام الكلية
/// </summary>
public class PrayerTrackingRepository : BaseRepository
{
    // ═══════════════════════════════════════
    //  Mark / Unmark
    // ═══════════════════════════════════════

    /// <summary>
    /// تعليم صلاة كـ "صليت".
    /// يرجع: 0 = نجح, -1 = الصلاة لسه, -2 = مفيش مواعيد.
    /// </summary>
    public async Task<int> MarkPrayerAsync(ePrayer prayer, ePrayerStatus status)
    {
        var p = new DynamicParameters();
        p.Add("@Prayer", (byte)prayer);
        p.Add("@Status", (byte)status);
        p.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

        await ExecuteWithOutputAsync("SP_MarkPrayer", p);

        return p.Get<int>("@Result");
    }

    /// <summary>إلغاء تعليم صلاة (يرجعها NotMarked)</summary>
    public async Task<int> UnmarkPrayerAsync(ePrayer prayer)
    {
        return await ExecuteAsync(
            "SP_UnmarkPrayer",
            new { Prayer = (byte)prayer }
        );
    }

    // ═══════════════════════════════════════
    //  Get Today
    // ═══════════════════════════════════════

    /// <summary>جيب حالة الـ 5 صلوات النهارده</summary>
    public async Task<IEnumerable<PrayerTrackingDTO>> GetTodayTrackingAsync()
    {
        return await QueryAsync<PrayerTrackingDTO>("SP_GetTodayTracking");
    }

    // ═══════════════════════════════════════
    //  Fill Missed Days
    // ═══════════════════════════════════════

    /// <summary>ملء الأيام اللي مافتحش فيها البرنامج = Missed</summary>
    public async Task FillMissedDaysAsync()
    {
        await ExecuteAsync("SP_FillMissedDays");
    }

    // ═══════════════════════════════════════
    //  Statistics
    // ═══════════════════════════════════════

    /// <summary>إحصائيات الشهر (كل يوم: OnTime, Late, Missed)</summary>
    public async Task<IEnumerable<DailyStatsDTO>> GetMonthlyStatsAsync(int month, int year)
    {
        return await QueryAsync<DailyStatsDTO>(
            "SP_GetMonthlyTrackingStats",
            new { Month = month, Year = year }
        );
    }

    /// <summary>إحصائيات أسبوعية (لكل صلاة: OnTime, Late, Missed)</summary>
    public async Task<IEnumerable<WeeklyPrayerStatsDTO>> GetWeeklyStatsAsync()
    {
        return await QueryAsync<WeeklyPrayerStatsDTO>("SP_GetWeeklyTrackingStats");
    }

    /// <summary>عدد أيام الالتزام المتتالية (Streak 🔥)</summary>
    public async Task<int> GetStreakCountAsync()
    {
        var p = new DynamicParameters();
        p.Add("@StreakDays", dbType: DbType.Int32, direction: ParameterDirection.Output);

        await ExecuteWithOutputAsync("SP_GetStreakCount", p);

        return p.Get<int>("@StreakDays");
    }

    /// <summary>نسبة الالتزام الكلية (%)</summary>
    public async Task<decimal> GetOverallPercentageAsync()
    {
        var p = new DynamicParameters();
        p.Add("@Percentage", dbType: DbType.Decimal, direction: ParameterDirection.Output,
              precision: 5, scale: 2);

        await ExecuteWithOutputAsync("SP_GetOverallPercentage", p);

        return p.Get<decimal>("@Percentage");
    }
}
