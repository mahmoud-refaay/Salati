namespace DAL.DTOs;

/// <summary>
/// استجابة Aladhan API — الجزء اللي نحتاجه بس.
/// System.Text.Json بيعمل mapping تلقائي.
/// </summary>
public record AladhanResponse(AladhanData Data);
public record AladhanData(AladhanTimings Timings, AladhanDateInfo Date);
public record AladhanTimings(string Fajr, string Sunrise, string Dhuhr, string Asr, string Maghrib, string Isha);
public record AladhanDateInfo(AladhanHijri Hijri);
public record AladhanHijri(string Day, AladhanMonth Month, string Year);
public record AladhanMonth(string Ar);

/// <summary>
/// موقع المستخدم — من جدول Locations
/// </summary>
public record LocationDTO(
    int LocationID,
    string Country,
    string City,
    int CalculationMethod,
    decimal? Latitude,
    decimal? Longitude,
    bool IsDefault
);
