using BLL.Common;
using DAL.DTOs;
using DAL.Enums;
using DAL.Repositories;

namespace BLL.Services;

/// <summary>
/// Service مواعيد الصلاة — يدير الـ caching بين API و DB.
/// 
/// ═══ Smart Flow ═══
///   1. هل في مواعيد النهارده في الـ DB؟
///      ├─ ✅ أيوا → اقرأها (سريع)
///      └─ ❌ لأ  → اجلبها من API → خزّنها → اقرأها
/// 
///   2. لو الـ API فشل (مفيش نت):
///      → يرجع آخر مواعيد متخزنة (أفضل من لا شيء)
///      → أو يرجع Failure مع رسالة واضحة
/// 
/// ═══ الاستخدام ═══
///   var service = new PrayerTimesService();
///   var result = await service.GetTodayTimesAsync();
///   if (result.IsSuccess)
///       // result.Data.FajrTime, result.Data.DhuhrTime, ...
/// </summary>
public class PrayerTimesService
{
    private readonly PrayerTimesRepository _repo = new();

    // ═══════════════════════════════════════
    //  Main API — GetTodayTimes (Smart Cache)
    // ═══════════════════════════════════════

    /// <summary>
    /// جيب مواعيد الصلاة النهارده — DB أولاً، API لو مش لاقي.
    /// </summary>
    public async Task<Result<PrayerTimeDTO>> GetTodayTimesAsync()
    {
        // ── 1. حاول من الـ DB (سريع) ──
        bool hasToday = await _repo.HasTodayTimesAsync();

        if (hasToday)
        {
            var cached = await _repo.GetTodayTimesAsync();
            if (cached != null)
                return Result<PrayerTimeDTO>.Success(cached);
        }

        // ── 2. مفيش في الـ DB → جيب من API ──
        return await FetchAndCacheAsync();
    }

    /// <summary>
    /// إجبار جلب من الـ API (حتى لو في cache).
    /// مفيد لو المستخدم غيّر الموقع.
    /// </summary>
    public async Task<Result<PrayerTimeDTO>> RefreshFromApiAsync()
    {
        return await FetchAndCacheAsync();
    }

    // ═══════════════════════════════════════
    //  Internal — Fetch + Cache
    // ═══════════════════════════════════════

    private async Task<Result<PrayerTimeDTO>> FetchAndCacheAsync()
    {
        // ── جيب الموقع الافتراضي ──
        var location = await _repo.GetDefaultLocationAsync();

        if (location == null)
            return Result<PrayerTimeDTO>.Failure(
                "لا يوجد موقع افتراضي — اذهب للإعدادات وحدد مدينتك");

        // ── اجلب من API ──
        var (times, hijriDate) = await _repo.FetchFromApiAsync(
            location.City, location.Country, location.CalculationMethod);

        if (times == null)
            return Result<PrayerTimeDTO>.Failure(
                "تعذّر جلب مواعيد الصلاة — تأكد من اتصال الإنترنت");

        // ── خزّن في DB (cache) ──
        await _repo.SavePrayerTimesAsync(
            location.LocationID,
            DateTime.Today,
            times.FajrTime,
            times.SunriseTime,
            times.DhuhrTime,
            times.AsrTime,
            times.MaghribTime,
            times.IshaTime,
            ePrayerSource.API,
            hijriDate
        );

        return Result<PrayerTimeDTO>.Success(times);
    }
}
