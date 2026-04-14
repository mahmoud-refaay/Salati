using UI.Core.Theme;
using UI.Core.Language;

namespace UI.Controls.Settings
{
    /// <summary>
    /// تاب إعدادات عام — toggles + reset + about.
    /// </summary>
    public partial class ucSettingsGeneral : UserControl, IThemeable, ILocalizable
    {
        // ═══════════════════════════════════════
        //  Events
        // ═══════════════════════════════════════

        /// <summary>يُطلق عند الضغط على إعادة ضبط</summary>
        public event EventHandler? ResetClicked;

        /// <summary>يُطلق عند الضغط على فتح المجلد</summary>
        public event EventHandler? OpenFolderClicked;

        /// <summary>يُطلق عند تغيير أي toggle</summary>
        public event EventHandler? SettingChanged;

        // ═══════════════════════════════════════
        //  Constructor
        // ═══════════════════════════════════════

        public ucSettingsGeneral()
        {
            InitializeComponent();

            // TODO: BLL — btnResetDefaults → clsSettingsStore.ResetToDefaults() + إعادة تحميل الإعدادات
            btnResetDefaults.Click += (s, e) => ResetClicked?.Invoke(this, EventArgs.Empty);
            // TODO: BLL — btnOpenFolder → Process.Start(مسار مجلد الإعدادات)
            btnOpenFolder.Click += (s, e) => OpenFolderClicked?.Invoke(this, EventArgs.Empty);

            // TODO: BLL — كل toggle يربط بـ clsSettingsStore (Registry read/write)
            // togStartWithWindows → clsRegistryManager.SetStartWithWindows()
            // togMinimizeOnStart → clsSettingsStore.MinimizeOnStart
            // togShowInTray → clsSettingsStore.ShowInTray
            // togCloseToTray → clsSettingsStore.CloseToTray
            togStartWithWindows.CheckedChanged += (s, e) => SettingChanged?.Invoke(this, EventArgs.Empty);
            togMinimizeOnStart.CheckedChanged += (s, e) => SettingChanged?.Invoke(this, EventArgs.Empty);
            togShowInTray.CheckedChanged += (s, e) => SettingChanged?.Invoke(this, EventArgs.Empty);
            togCloseToTray.CheckedChanged += (s, e) => SettingChanged?.Invoke(this, EventArgs.Empty);
        }

        // ═══════════════════════════════════════
        //  Properties
        // ═══════════════════════════════════════

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool StartWithWindows
        {
            get => togStartWithWindows.Checked;
            set => togStartWithWindows.Checked = value;
        }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool MinimizeOnStart
        {
            get => togMinimizeOnStart.Checked;
            set => togMinimizeOnStart.Checked = value;
        }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool ShowInTray
        {
            get => togShowInTray.Checked;
            set => togShowInTray.Checked = value;
        }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool CloseToTray
        {
            get => togCloseToTray.Checked;
            set => togCloseToTray.Checked = value;
        }

        // ═══════════════════════════════════════
        //  IThemeable
        // ═══════════════════════════════════════

        public void ApplyTheme(ThemeColors t)
        {
            lblSectionTitle.ForeColor = t.TextPrimary;

            // Toggle labels
            foreach (var lbl in new[] { lblStartup, lblMinOnStart, lblTray, lblCloseToTray })
                lbl.ForeColor = t.TextPrimary;

            // Toggle switches
            foreach (var tog in new[] { togStartWithWindows, togMinimizeOnStart, togShowInTray, togCloseToTray })
            {
                tog.CheckedState.FillColor = t.Accent1;
                tog.CheckedState.BorderColor = t.Accent1;
                tog.UncheckedState.FillColor = ThemeColorUtils.Darken(t.BgSurface, 5);
                tog.UncheckedState.BorderColor = t.TextMuted;
            }

            // Separator
            pnlSeparator.BackColor = ThemeColorUtils.WithAlpha(t.TextMuted, 30);

            // Buttons
            foreach (var btn in new[] { btnResetDefaults, btnOpenFolder })
            {
                btn.ForeColor = t.TextSecondary;
                btn.BorderColor = t.BorderDefault;
                btn.HoverState.ForeColor = t.TextPrimary;
                btn.HoverState.BorderColor = t.Accent1;
            }

            // About
            lblVersion.ForeColor = t.TextMuted;
            lblCredits.ForeColor = t.TextMuted;
        }

        // ═══════════════════════════════════════
        //  ILocalizable
        // ═══════════════════════════════════════

        public void ApplyLanguage(ILanguagePack lang)
        {
            lblSectionTitle.Text = "⚙️ " + lang.SettingsTabGeneral;
            lblStartup.Text = lang.SettingsStartWithWindows;
            lblMinOnStart.Text = lang.SettingsMinimizeOnStart;
            lblTray.Text = lang.SettingsShowInTray;
            lblCloseToTray.Text = lang.SettingsCloseToTray;
            btnResetDefaults.Text = "🔄 " + lang.SettingsResetDefaults;
            btnOpenFolder.Text = "📂 " + lang.SettingsOpenFolder;
            lblVersion.Text = lang.SettingsAboutVersion + " 1.0.0";
            lblCredits.Text = lang.SettingsAboutCredits;
        }
    }
}
