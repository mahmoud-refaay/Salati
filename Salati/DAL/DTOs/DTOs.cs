using DAL.Enums;

namespace DAL.DTOs;

// ═══════════════════════════════════════════════════════════════
//  DTOs — C# Records (immutable + auto-generated Equals/ToString)
//  كل record بيطابق نتيجة Stored Procedure محدد.
//  Dapper بيعمل mapping تلقائي: اسم Column = اسم Property.
// ═══════════════════════════════════════════════════════════════


/// <summary>
/// ذكر/حديث واحد — يتوافق مع SP_GetRandomAdhkar + SP_GetAdhkarByCategory
/// ⚠️ Dapper يحتاج parameterless constructor — لذلك class مع init
/// </summary>
public class AdhkarDTO
{
    public int AdhkarID { get; init; }
    public eAdhkarCategory Category { get; init; }
    public string TextAr { get; init; } = "";
    public string? TextEn { get; init; }
    public string? Source { get; init; }
    public byte RepeatCount { get; init; } = 1;
    public byte SortOrder { get; init; }
}


/// <summary>
/// مواعيد صلوات يوم واحد — يتوافق مع جدول DailyPrayerTimes
/// </summary>
public record PrayerTimeDTO(
    DateTime Date,
    TimeSpan FajrTime,
    TimeSpan DhuhrTime,
    TimeSpan AsrTime,
    TimeSpan MaghribTime,
    TimeSpan IshaTime,
    TimeSpan SunriseTime
);


/// <summary>
/// حالة صلاة واحدة في اليوم — يتوافق مع SP_GetTodayTracking
/// </summary>
public record PrayerTrackingDTO(
    ePrayer Prayer,
    TimeSpan PrayerTime,
    ePrayerStatus Status,
    DateTime? MarkedAt
);


/// <summary>
/// إحصائيات يوم واحد — يتوافق مع SP_GetMonthlyTrackingStats
/// </summary>
public record DailyStatsDTO(
    DateTime Date,
    int OnTime,
    int Late,
    int Missed,
    int NotMarked,
    int TotalPrayers
);


/// <summary>
/// إحصائيات أسبوعية لصلاة واحدة — يتوافق مع SP_GetWeeklyTrackingStats
/// </summary>
public record WeeklyPrayerStatsDTO(
    ePrayer Prayer,
    int OnTime,
    int Late,
    int Missed,
    int TotalPrayers
);
