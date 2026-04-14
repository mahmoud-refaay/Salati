using UI.Core.Theme;
using UI.Core.Language;

namespace UI.Controls.Settings
{
    /// <summary>
    /// تاب المظهر - اختيار الثيم + اللغة.
    /// ThemeCards و LanguageCards معرفين في Designer.cs.
    /// </summary>
    public partial class ucSettingsAppearance : UserControl, IThemeable, ILocalizable
    {
        // ===== Events =====

        public event EventHandler? ThemeChanged;
        public event EventHandler? LanguageChanged;

        // ===== Constructor =====

        public ucSettingsAppearance()
        {
            InitializeComponent();
            SetupCards();
            WireEvents();
        }

        // ===== Setup (Cards are pre-created in Designer) =====

        private void SetupCards()
        {
            // Theme cards - set data only (controls exist in Designer)
            _cardMidnight.ThemeDef = BuiltInThemes.MidnightSerenity;
            _cardMidnight.IsSelected = clsThemeManager.IsDark;

            _cardGolden.ThemeDef = BuiltInThemes.DesertSand;
            _cardGolden.IsSelected = !clsThemeManager.IsDark;

            // Language cards - set data only
            _cardArabic.SetLanguage("ar", "\ud83c\uddf8\ud83c\udde6", "\u0627\u0644\u0639\u0631\u0628\u064a\u0629", "Arabic");
            _cardArabic.IsSelected = clsLanguageManager.Code == "ar";

            _cardEnglish.SetLanguage("en", "\ud83c\uddfa\ud83c\uddf8", "English", "\u0627\u0644\u0625\u0646\u062c\u0644\u064a\u0632\u064a\u0629");
            _cardEnglish.IsSelected = clsLanguageManager.Code == "en";
        }

        private void WireEvents()
        {
            _cardMidnight.ThemeSelected += (s, e) => SelectTheme(BuiltInThemes.MidnightSerenity);
            _cardGolden.ThemeSelected += (s, e) => SelectTheme(BuiltInThemes.DesertSand);
            _cardArabic.LanguageSelected += (s, e) => SelectLanguage("ar");
            _cardEnglish.LanguageSelected += (s, e) => SelectLanguage("en");
        }

        // ===== Selection Logic =====

        private void SelectTheme(ThemeDefinition theme)
        {
            clsThemeManager.ApplyTheme(theme);
            _cardMidnight.IsSelected = (theme.Name == BuiltInThemes.MidnightSerenity.Name);
            _cardGolden.IsSelected = (theme.Name == BuiltInThemes.DesertSand.Name);
            ThemeChanged?.Invoke(this, EventArgs.Empty);
        }

        private void SelectLanguage(string code)
        {
            clsLanguageManager.ApplyLanguage(code);
            _cardArabic.IsSelected = (code == "ar");
            _cardEnglish.IsSelected = (code == "en");
            LanguageChanged?.Invoke(this, EventArgs.Empty);
        }

        // ===== IThemeable =====

        public void ApplyTheme(ThemeColors t)
        {
            lblThemeTitle.ForeColor = t.TextPrimary;
            lblLangTitle.ForeColor = t.TextPrimary;

            _cardMidnight.ApplyTheme(t);
            _cardGolden.ApplyTheme(t);
            _cardArabic.ApplyTheme(t);
            _cardEnglish.ApplyTheme(t);
        }

        // ===== ILocalizable =====

        public void ApplyLanguage(ILanguagePack lang)
        {
            lblThemeTitle.Text = "\ud83c\udfa8 " + lang.SettingsTheme;
            lblLangTitle.Text = "\ud83c\udf10 " + lang.SettingsLanguage;
        }
    }
}
