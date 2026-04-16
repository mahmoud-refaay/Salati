using DAL.Enums;

namespace BLL.Extensions;

/// <summary>
/// Extension Methods — بتضيف methods على الـ Enums والـ DTOs.
/// بدل: GetEmojiForCategory(category)
/// بتكتب: category.GetEmoji() ← أنظف وأسهل
/// </summary>
public static class EnumExtensions
{
    // ═══════════════════════════════════════
    //  ePrayer Extensions
    // ═══════════════════════════════════════

    /// <summary>اسم الصلاة بالعربي</summary>
    public static string ToArabicName(this ePrayer prayer) => prayer switch
    {
        ePrayer.Fajr => "الفجر",
        ePrayer.Dhuhr => "الظهر",
        ePrayer.Asr => "العصر",
        ePrayer.Maghrib => "المغرب",
        ePrayer.Isha => "العشاء",
        _ => "غير معروف"
    };

    /// <summary>اسم الصلاة بالإنجليزي</summary>
    public static string ToEnglishName(this ePrayer prayer) => prayer switch
    {
        ePrayer.Fajr => "Fajr",
        ePrayer.Dhuhr => "Dhuhr",
        ePrayer.Asr => "Asr",
        ePrayer.Maghrib => "Maghrib",
        ePrayer.Isha => "Isha",
        _ => "Unknown"
    };

    /// <summary>إيموجي الصلاة</summary>
    public static string GetEmoji(this ePrayer prayer) => prayer switch
    {
        ePrayer.Fajr => "🌅",
        ePrayer.Dhuhr => "☀️",
        ePrayer.Asr => "🌤️",
        ePrayer.Maghrib => "🌅",
        ePrayer.Isha => "🌙",
        _ => "🕌"
    };

    // ═══════════════════════════════════════
    //  ePrayerStatus Extensions
    // ═══════════════════════════════════════

    /// <summary>نص الحالة بالعربي</summary>
    public static string ToArabicText(this ePrayerStatus status) => status switch
    {
        ePrayerStatus.OnTime => "صليت في الوقت ✅",
        ePrayerStatus.Late => "صليت متأخراً ⚠️",
        ePrayerStatus.Missed => "لم أصلِّ ❌",
        ePrayerStatus.NotMarked => "لم أحدد بعد",
        _ => ""
    };

    /// <summary>لون الحالة (لعرضه في الـ UI)</summary>
    public static System.Drawing.Color GetColor(this ePrayerStatus status) => status switch
    {
        ePrayerStatus.OnTime => System.Drawing.Color.FromArgb(27, 138, 107),
        ePrayerStatus.Late => System.Drawing.Color.FromArgb(200, 169, 110),
        ePrayerStatus.Missed => System.Drawing.Color.FromArgb(224, 108, 117),
        _ => System.Drawing.Color.FromArgb(139, 148, 158)
    };

    // ═══════════════════════════════════════
    //  eAdhkarCategory Extensions
    // ═══════════════════════════════════════

    /// <summary>إيموجي التصنيف</summary>
    public static string GetEmoji(this eAdhkarCategory category) => category switch
    {
        eAdhkarCategory.Hadith => "📖",
        eAdhkarCategory.Dhikr => "📿",
        eAdhkarCategory.Salawat => "🤲",
        eAdhkarCategory.Dua => "🕌",
        eAdhkarCategory.MorningAdhkar => "🌅",
        eAdhkarCategory.EveningAdhkar => "🌇",
        _ => "📿"
    };

    /// <summary>اسم التصنيف بالعربي</summary>
    public static string ToArabicName(this eAdhkarCategory category) => category switch
    {
        eAdhkarCategory.Hadith => "أحاديث",
        eAdhkarCategory.Dhikr => "أذكار عامة",
        eAdhkarCategory.Salawat => "صلاة على النبي",
        eAdhkarCategory.Dua => "أدعية",
        eAdhkarCategory.MorningAdhkar => "أذكار الصباح",
        eAdhkarCategory.EveningAdhkar => "أذكار المساء",
        _ => ""
    };
}
