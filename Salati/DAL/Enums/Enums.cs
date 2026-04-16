namespace DAL.Enums;

/// <summary>
/// تصنيف الأذكار — يتوافق مع Column: Category (TINYINT) في جدول Adhkar.
/// </summary>
public enum eAdhkarCategory : byte
{
    Hadith = 1,          // 📖 حديث
    Dhikr = 2,           // 📿 ذكر عام
    Salawat = 3,         // 🤲 صلاة على النبي
    Dua = 4,             // 🕌 دعاء
    MorningAdhkar = 5,   // 🌅 أذكار الصباح
    EveningAdhkar = 6    // 🌇 أذكار المساء
}

/// <summary>
/// حالة تتبع الصلاة — يتوافق مع Column: Status (TINYINT) في جدول PrayerTracking.
/// </summary>
public enum ePrayerStatus : byte
{
    NotMarked = 0,   // لم أحدد بعد
    OnTime = 1,      // صليت في الوقت ✅
    Late = 2,        // صليت متأخراً ⚠️
    Missed = 3       // لم أصلِّ ❌
}

/// <summary>
/// الصلوات الخمس — يتوافق مع Column: Prayer (TINYINT) في جدول PrayerTracking.
/// </summary>
public enum ePrayer : byte
{
    Fajr = 1,      // 🌅 الفجر
    Dhuhr = 2,     // ☀️ الظهر
    Asr = 3,       // 🌤️ العصر
    Maghrib = 4,   // 🌅 المغرب
    Isha = 5       // 🌙 العشاء
}
