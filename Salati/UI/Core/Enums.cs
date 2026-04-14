namespace UI.Core
{
    /// <summary>الصلوات الخمس</summary>
    public enum ePrayer
    {
        Fajr = 1,
        Dhuhr = 2,
        Asr = 3,
        Maghrib = 4,
        Isha = 5
    }

    /// <summary>حالة الصلاة في الداشبورد</summary>
    public enum ePrayerStatus
    {
        /// <summary>الصلاة فاتت</summary>
        Passed = 1,
        /// <summary>الصلاة القادمة (مميزة)</summary>
        Next = 2,
        /// <summary>صلاة لاحقة</summary>
        Upcoming = 3,
        /// <summary>معطلة</summary>
        Disabled = 4
    }

    /// <summary>مصدر المواعيد</summary>
    public enum ePrayerSource
    {
        API = 1,
        Manual = 2
    }

    /// <summary>نوع التنبيه</summary>
    public enum eAlertType
    {
        AdhanSound = 1,
        SimpleBeep = 2,
        WindowsNotification = 3
    }

    /// <summary>
    /// Helper — يرجع Emoji الصلاة ولونها.
    /// مركزي عشان كل الكنترولز تستخدمه.
    /// </summary>
    public static class PrayerHelper
    {
        /// <summary>إيموجي كل صلاة</summary>
        public static string GetEmoji(ePrayer prayer) => prayer switch
        {
            ePrayer.Fajr => "🌅",
            ePrayer.Dhuhr => "☀️",
            ePrayer.Asr => "🌤️",
            ePrayer.Maghrib => "🌇",
            ePrayer.Isha => "🌙",
            _ => "🕐"
        };

        /// <summary>إيموجي حالة الصلاة</summary>
        public static string GetStatusEmoji(ePrayerStatus status) => status switch
        {
            ePrayerStatus.Passed => "✅",
            ePrayerStatus.Next => "⏳",
            ePrayerStatus.Upcoming => "🔜",
            ePrayerStatus.Disabled => "⛔",
            _ => ""
        };

        /// <summary>اسم الصلاة من ILanguagePack</summary>
        public static string GetName(ePrayer prayer, Language.ILanguagePack lang) => prayer switch
        {
            ePrayer.Fajr => lang.PrayerFajr,
            ePrayer.Dhuhr => lang.PrayerDhuhr,
            ePrayer.Asr => lang.PrayerAsr,
            ePrayer.Maghrib => lang.PrayerMaghrib,
            ePrayer.Isha => lang.PrayerIsha,
            _ => ""
        };

        /// <summary>اسم الصلاة بالإنجليزي</summary>
        public static string GetEnglishName(ePrayer prayer, Language.ILanguagePack lang) => prayer switch
        {
            ePrayer.Fajr => lang.PrayerFajrEn,
            ePrayer.Dhuhr => lang.PrayerDhuhrEn,
            ePrayer.Asr => lang.PrayerAsrEn,
            ePrayer.Maghrib => lang.PrayerMaghribEn,
            ePrayer.Isha => lang.PrayerIshaEn,
            _ => ""
        };
    }
}
