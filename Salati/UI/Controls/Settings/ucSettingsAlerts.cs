using UI.Core;
using UI.Core.Theme;
using UI.Core.Language;

namespace UI.Controls.Settings
{
    /// <summary>
    /// تاب التنبيهات - 5 صفوف ucAlertRow + Volume slider + Alert type.
    /// كل الـ controls معرفة في Designer.cs.
    /// </summary>
    public partial class ucSettingsAlerts : UserControl, IThemeable, ILocalizable
    {
        // ===== Constructor =====

        public ucSettingsAlerts()
        {
            InitializeComponent();
            SetupAlertRows();
            LoadAlertTypes();

            trackVolume.ValueChanged += (s, e) =>
                lblVolumeValue.Text = $"{trackVolume.Value}%";
        }

        // ===== Properties =====

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public int Volume
        {
            get => trackVolume.Value;
            set
            {
                trackVolume.Value = Math.Clamp(value, 0, 100);
                lblVolumeValue.Text = $"{trackVolume.Value}%";
            }
        }

        /// <summary>Alert rows array for BLL access</summary>
        public ucAlertRow[] AlertRows => new[] { alertRowFajr, alertRowDhuhr, alertRowAsr, alertRowMaghrib, alertRowIsha };

        // ===== Setup (controls exist in Designer) =====

        private void SetupAlertRows()
        {
            // Set prayer type and defaults (controls pre-created in Designer)
            var prayers = new[] { ePrayer.Fajr, ePrayer.Dhuhr, ePrayer.Asr, ePrayer.Maghrib, ePrayer.Isha };
            var rows = AlertRows;

            for (int i = 0; i < 5; i++)
            {
                rows[i].Prayer = prayers[i];
                rows[i].IsEnabled = true;
                rows[i].MinutesBefore = 10;
                rows[i].IsAltRow = (i % 2 == 1);
            }
        }

        private void LoadAlertTypes()
        {
            // TODO: BLL - clsSoundPlayer.GetAvailableSounds()
            cboAlertType.Items.Clear();
            cboAlertType.Items.AddRange(new object[]
            {
                "\ud83d\udd4c \u0635\u0648\u062a \u0627\u0644\u0623\u0630\u0627\u0646",
                "\ud83d\udd14 \u062a\u0646\u0628\u064a\u0647 \u0628\u0633\u064a\u0637",
                "\ud83d\udcac \u0625\u0634\u0639\u0627\u0631 Windows",
            });
            cboAlertType.SelectedIndex = 0;
        }

        // ===== IThemeable =====

        public void ApplyTheme(ThemeColors t)
        {
            lblSectionTitle.ForeColor = t.TextPrimary;
            lblVolume.ForeColor = t.TextSecondary;
            lblVolumeValue.ForeColor = t.Accent2;
            lblAlertType.ForeColor = t.TextSecondary;

            trackVolume.ThumbColor = t.Accent1;
            trackVolume.FillColor = ThemeColorUtils.WithAlpha(t.Accent1, 40);

            cboAlertType.FillColor = t.InputBg;
            cboAlertType.ForeColor = t.InputText;
            cboAlertType.FocusedColor = t.Accent1;
            cboAlertType.BorderColor = t.BorderDefault;

            foreach (var row in AlertRows)
                row.ApplyTheme(t);
        }

        // ===== ILocalizable =====

        public void ApplyLanguage(ILanguagePack lang)
        {
            lblSectionTitle.Text = "\ud83d\udd14 " + lang.SettingsTabAlerts;
            lblVolume.Text = "\ud83d\udd0a " + lang.SettingsAlertVolume;
            lblAlertType.Text = lang.SettingsAlertType;

            foreach (var row in AlertRows)
                row.ApplyLanguage(lang);
        }
    }
}
