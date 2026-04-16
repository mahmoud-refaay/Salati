using System.Globalization;
using System.Net.Http.Json;
using DAL.Config;
using DAL.DTOs;
using DAL.Enums;
using DAL.Logging;

namespace DAL.Repositories;

/// <summary>
/// Repository مواعيد الصلاة — يجمع بين:
///   1️⃣ Aladhan API (جلب من الإنترنت)
///   2️⃣ Database (cache — قراءة سريعة)
/// 
/// ═══ Flow ═══
///   HasTodayTimes? → Yes → GetFromDB
///                  → No  → FetchFromAPI → SaveToDB → Return
/// </summary>
public class PrayerTimesRepository : BaseRepository
{
    // ═══════════════════════════════════════
    //  Constants
    // ═══════════════════════════════════════

    private const string API_BASE = "https://api.aladhan.com/v1/timingsByCity";
    private static readonly HttpClient _httpClient = new()
    {
        Timeout = TimeSpan.FromSeconds(10)
    };

    // ═══════════════════════════════════════
    //  Database — Read
    // ═══════════════════════════════════════

    /// <summary>هل في مواعيد النهارده في الـ DB؟</summary>
    public async Task<bool> HasTodayTimesAsync()
    {
        var result = await ExecuteScalarAsync<bool>("SP_HasPrayerTimesForToday");
        return result;
    }

    /// <summary>جيب مواعيد النهارده من الـ DB</summary>
    public async Task<PrayerTimeDTO?> GetTodayTimesAsync()
    {
        return await QuerySingleAsync<PrayerTimeDTO>("SP_GetTodayPrayerTimes");
    }

    /// <summary>جيب الموقع الافتراضي</summary>
    public async Task<LocationDTO?> GetDefaultLocationAsync()
    {
        return await QuerySingleAsync<LocationDTO>("SP_GetDefaultLocation");
    }

    // ═══════════════════════════════════════
    //  Database — Write (Cache)
    // ═══════════════════════════════════════

    /// <summary>خزّن مواعيد في الـ DB (Insert or Update)</summary>
    public async Task SavePrayerTimesAsync(
        int? locationId, DateTime date,
        TimeSpan fajr, TimeSpan sunrise, TimeSpan dhuhr,
        TimeSpan asr, TimeSpan maghrib, TimeSpan isha,
        ePrayerSource source, string? hijriDate = null)
    {
        await ExecuteAsync("SP_InsertOrUpdatePrayerTimes", new
        {
            LocationID = locationId,
            Date = date.Date,
            FajrTime = fajr,
            SunriseTime = sunrise,
            DhuhrTime = dhuhr,
            AsrTime = asr,
            MaghribTime = maghrib,
            IshaTime = isha,
            Source = (byte)source,
            HijriDate = hijriDate
        });
    }

    // ═══════════════════════════════════════
    //  API — Aladhan 🌐
    // ═══════════════════════════════════════

    /// <summary>
    /// جلب مواعيد الصلاة من Aladhan API.
    /// 
    /// مثال URL:
    /// https://api.aladhan.com/v1/timingsByCity?city=Cairo&amp;country=Egypt&amp;method=5&amp;date=16-04-2026
    /// </summary>
    public async Task<(PrayerTimeDTO? Times, string? HijriDate)> FetchFromApiAsync(
        string city, string country, int method = 5)
    {
        try
        {
            string dateStr = DateTime.Today.ToString("dd-MM-yyyy");
            string url = $"{API_BASE}?city={city}&country={country}&method={method}&date={dateStr}";

            clsLogger.Info($"[API] Fetching prayer times: {url}");

            var response = await _httpClient.GetFromJsonAsync<AladhanResponse>(url);

            if (response?.Data?.Timings == null)
            {
                clsLogger.Warn("[API] Response was null or empty", new { city, country });
                return (null, null);
            }

            var t = response.Data.Timings;
            var hijri = response.Data.Date?.Hijri;

            string? hijriDate = hijri != null
                ? $"{hijri.Day} {hijri.Month.Ar} {hijri.Year}"
                : null;

            var dto = new PrayerTimeDTO(
                Date: DateTime.Today,
                FajrTime: ParseTime(t.Fajr),
                DhuhrTime: ParseTime(t.Dhuhr),
                AsrTime: ParseTime(t.Asr),
                MaghribTime: ParseTime(t.Maghrib),
                IshaTime: ParseTime(t.Isha),
                SunriseTime: ParseTime(t.Sunrise)
            );

            clsLogger.Info($"[API] Success — Fajr:{t.Fajr} Dhuhr:{t.Dhuhr} Asr:{t.Asr} Maghrib:{t.Maghrib} Isha:{t.Isha}");

            return (dto, hijriDate);
        }
        catch (HttpRequestException ex)
        {
            clsLogger.Error("[API] Network error — لا يوجد اتصال بالإنترنت", ex, new { city, country });
            return (null, null);
        }
        catch (TaskCanceledException ex)
        {
            clsLogger.Error("[API] Timeout — API بطيء", ex, new { city, country });
            return (null, null);
        }
        catch (Exception ex)
        {
            clsLogger.Error("[API] Unexpected error", ex, new { city, country });
            return (null, null);
        }
    }

    // ═══════════════════════════════════════
    //  Helper — Parse "HH:mm" → TimeSpan
    // ═══════════════════════════════════════

    private static TimeSpan ParseTime(string time)
    {
        // API يرجع "04:15" أو "04:15 (EET)"
        string clean = time.Split(' ')[0].Trim();
        return TimeSpan.ParseExact(clean, "hh\\:mm", CultureInfo.InvariantCulture);
    }
}
