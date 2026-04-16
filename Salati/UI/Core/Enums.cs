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
        Passed = 1,
        Next = 2,
        Upcoming = 3,
        Disabled = 4
    }

    /// <summary>حالة تتبع الصلاة</summary>
    public enum eTrackingStatus
    {
        NotMarked = 0,
        OnTime = 1,
        Late = 2,
        Missed = 3
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

    /// <summary>Helper — يرجع Emoji/اسم الصلاة</summary>
    public static class PrayerHelper
    {
        public static string GetEmoji(ePrayer prayer) => prayer switch
        {
            ePrayer.Fajr => "🌅",
            ePrayer.Dhuhr => "☀️",
            ePrayer.Asr => "🌤️",
            ePrayer.Maghrib => "🌇",
            ePrayer.Isha => "🌙",
            _ => "🕐"
        };

        public static string GetStatusEmoji(ePrayerStatus status) => status switch
        {
            ePrayerStatus.Passed => "✅",
            ePrayerStatus.Next => "⏳",
            ePrayerStatus.Upcoming => "🔜",
            ePrayerStatus.Disabled => "⛔",
            _ => ""
        };

        public static string GetTrackingEmoji(eTrackingStatus status) => status switch
        {
            eTrackingStatus.OnTime => "✅",
            eTrackingStatus.Late => "⚠️",
            eTrackingStatus.Missed => "❌",
            eTrackingStatus.NotMarked => "⬜",
            _ => "⬜"
        };

        public static string GetName(ePrayer prayer, Language.ILanguagePack lang) => prayer switch
        {
            ePrayer.Fajr => lang.PrayerFajr,
            ePrayer.Dhuhr => lang.PrayerDhuhr,
            ePrayer.Asr => lang.PrayerAsr,
            ePrayer.Maghrib => lang.PrayerMaghrib,
            ePrayer.Isha => lang.PrayerIsha,
            _ => ""
        };

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
