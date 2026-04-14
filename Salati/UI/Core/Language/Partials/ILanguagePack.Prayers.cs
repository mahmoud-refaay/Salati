namespace UI.Core.Language
{
    // أسماء الصلوات الخمس
    public partial interface ILanguagePack
    {
        string PrayerFajr { get; }
        string PrayerDhuhr { get; }
        string PrayerAsr { get; }
        string PrayerMaghrib { get; }
        string PrayerIsha { get; }

        string PrayerFajrEn { get; }
        string PrayerDhuhrEn { get; }
        string PrayerAsrEn { get; }
        string PrayerMaghribEn { get; }
        string PrayerIshaEn { get; }
    }
}
