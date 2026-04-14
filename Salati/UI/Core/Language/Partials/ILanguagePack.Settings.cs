namespace UI.Core.Language
{
    // نصوص الإعدادات — 4 tabs
    public partial interface ILanguagePack
    {
        // Headers
        string SettingsTitle { get; }
        string SettingsTabPrayer { get; }
        string SettingsTabAlerts { get; }
        string SettingsTabAppearance { get; }
        string SettingsTabGeneral { get; }

        // Tab: Prayer Times
        string SettingsPrayerSource { get; }
        string SettingsPrayerAuto { get; }
        string SettingsPrayerManual { get; }
        string SettingsPrayerCountry { get; }
        string SettingsPrayerCity { get; }
        string SettingsPrayerMethod { get; }

        // Tab: Alerts
        string SettingsAlertMinutes { get; }
        string SettingsAlertSound { get; }
        string SettingsAlertVolume { get; }
        string SettingsAlertAtAdhan { get; }
        string SettingsAlertType { get; }

        // Tab: Appearance
        string SettingsTheme { get; }
        string SettingsLanguage { get; }

        // Tab: General
        string SettingsStartWithWindows { get; }
        string SettingsMinimizeOnStart { get; }
        string SettingsShowInTray { get; }
        string SettingsCloseToTray { get; }
        string SettingsResetDefaults { get; }
        string SettingsOpenFolder { get; }

        // About (inside General tab)
        string SettingsAboutVersion { get; }
        string SettingsAboutCredits { get; }
    }
}
