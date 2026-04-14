namespace UI.Core.Language.Languages
{
    /// <summary>English Language Pack</summary>
    public class EnglishPack : ILanguagePack
    {
        // ═══ Base ═══
        public string LanguageCode => "en";
        public string LanguageNameAr => "الإنجليزية";
        public string LanguageNameEn => "English";
        public bool IsRtl => false;

        // ═══ Common ═══
        public string CommonSave => "Save";
        public string CommonCancel => "Cancel";
        public string CommonClose => "Close";
        public string CommonDismiss => "Dismiss";
        public string CommonMute => "Mute";
        public string CommonRefresh => "Refresh";
        public string CommonReset => "Reset";
        public string CommonError => "Error";
        public string CommonSuccess => "Success";
        public string CommonLoading => "Loading";
        public string CommonOk => "OK";
        public string CommonWarning => "Warning";
        public string CommonInfo => "Info";

        // ═══ Layout ═══
        public string LayoutAppTitle => "Salati";
        public string LayoutMinimizeToTray => "Minimize to Tray";
        public string LayoutSettings => "Settings";

        // ═══ Dashboard ═══
        public string DashboardRemaining => "Remaining";
        public string DashboardNextPrayer => "Next Prayer";
        public string DashboardPassed => "Passed";
        public string DashboardUpcoming => "Upcoming";
        public string DashboardNoInternet => "No Internet Connection";

        // ═══ Prayers ═══
        public string PrayerFajr => "Fajr";
        public string PrayerDhuhr => "Dhuhr";
        public string PrayerAsr => "Asr";
        public string PrayerMaghrib => "Maghrib";
        public string PrayerIsha => "Isha";
        public string PrayerFajrEn => "FAJR";
        public string PrayerDhuhrEn => "DHUHR";
        public string PrayerAsrEn => "ASR";
        public string PrayerMaghribEn => "MAGHRIB";
        public string PrayerIshaEn => "ISHA";

        // ═══ Settings ═══
        public string SettingsTitle => "Settings";
        public string SettingsTabPrayer => "Prayer Times";
        public string SettingsTabAlerts => "Alerts";
        public string SettingsTabAppearance => "Appearance";
        public string SettingsTabGeneral => "General";

        public string SettingsPrayerSource => "Time Source";
        public string SettingsPrayerAuto => "Auto from Internet";
        public string SettingsPrayerManual => "Manual Input";
        public string SettingsPrayerCountry => "Country";
        public string SettingsPrayerCity => "City";
        public string SettingsPrayerMethod => "Calculation Method";

        public string SettingsAlertMinutes => "Minutes Before";
        public string SettingsAlertSound => "Adhan Sound";
        public string SettingsAlertVolume => "Volume";
        public string SettingsAlertAtAdhan => "Alert at Adhan Time";
        public string SettingsAlertType => "Alert Type";

        public string SettingsTheme => "Theme";
        public string SettingsLanguage => "Language";

        public string SettingsStartWithWindows => "Start with Windows";
        public string SettingsMinimizeOnStart => "Minimize on Start";
        public string SettingsShowInTray => "Show in System Tray";
        public string SettingsCloseToTray => "Close to Tray";
        public string SettingsResetDefaults => "Reset to Defaults";
        public string SettingsOpenFolder => "Open Settings Folder";
        public string SettingsAboutVersion => "Version";
        public string SettingsAboutCredits => "Made with ❤️ by Team Salati";

        // ═══ Alert ═══
        public string AlertTimeFor => "Time for {0} prayer";
        public string AlertMinutesUntil => "{0} minutes until {1} prayer";
        public string AlertAutoDismiss => "Auto-dismiss in {0} seconds";
        public string AlertHayaAlaSalah => "Hayya Alas Salah, Hayya Alal Falah";
        public string AlertConfirm => "Confirm";
        public string AlertCancel => "Cancel";
        public string AlertOk => "OK";
        public string AlertYes => "Yes";
        public string AlertNo => "No";
    }
}
