using UI.Core;
using UI.Core.Theme;
using UI.Core.Language;

namespace UI.Controls.Settings
{
    /// <summary>
    /// تاب مواعيد الصلاة — Auto (API) أو Manual.
    /// يُحمّل داخل ucSettingsPanel.pnlContent.
    /// </summary>
    public partial class ucSettingsPrayer : UserControl, IThemeable, ILocalizable
    {
        // ═══════════════════════════════════════
        //  Events
        // ═══════════════════════════════════════

        /// <summary>يُطلق عند تغيير أي إعداد</summary>
        public event EventHandler? SettingChanged;

        // ═══════════════════════════════════════
        //  Constructor
        // ═══════════════════════════════════════

        public ucSettingsPrayer()
        {
            InitializeComponent();
            WireEvents();
            LoadDefaultData();
        }

        // ═══════════════════════════════════════
        //  Properties
        // ═══════════════════════════════════════

        /// <summary>true = Auto (API), false = Manual</summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool IsAutoMode => radAuto.Checked;

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string SelectedCountry => cboCountry.SelectedItem?.ToString() ?? "";

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string SelectedCity => cboCity.SelectedItem?.ToString() ?? "";

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string SelectedMethod => cboMethod.SelectedItem?.ToString() ?? "";

        /// <summary>يرجع مواعيد الصلاة اليدوية [Fajr, Dhuhr, Asr, Maghrib, Isha]</summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public TimeOnly[] GetManualTimes() => new TimeOnly[]
        {
            TimeOnly.FromDateTime(dtpFajr.Value),
            TimeOnly.FromDateTime(dtpDhuhr.Value),
            TimeOnly.FromDateTime(dtpAsr.Value),
            TimeOnly.FromDateTime(dtpMaghrib.Value),
            TimeOnly.FromDateTime(dtpIsha.Value),
        };

        /// <summary>يضبط مواعيد الصلاة اليدوية</summary>
        public void SetManualTimes(TimeOnly[] times)
        {
            if (times.Length != 5) return;
            dtpFajr.Value = DateTime.Today.Add(times[0].ToTimeSpan());
            dtpDhuhr.Value = DateTime.Today.Add(times[1].ToTimeSpan());
            dtpAsr.Value = DateTime.Today.Add(times[2].ToTimeSpan());
            dtpMaghrib.Value = DateTime.Today.Add(times[3].ToTimeSpan());
            dtpIsha.Value = DateTime.Today.Add(times[4].ToTimeSpan());
        }

        // ═══════════════════════════════════════
        //  Private Setup
        // ═══════════════════════════════════════

        private void WireEvents()
        {
            radAuto.CheckedChanged += (s, e) =>
            {
                pnlAutoFields.Visible = radAuto.Checked;
                pnlManualFields.Visible = !radAuto.Checked;
                SettingChanged?.Invoke(this, EventArgs.Empty);
            };

            cboCountry.SelectedIndexChanged += (s, e) =>
            {
                UpdateCities();
                SettingChanged?.Invoke(this, EventArgs.Empty);
            };
            cboCity.SelectedIndexChanged += (s, e) => SettingChanged?.Invoke(this, EventArgs.Empty);
            cboMethod.SelectedIndexChanged += (s, e) => SettingChanged?.Invoke(this, EventArgs.Empty);

            // Manual DTPs
            foreach (var dtp in new[] { dtpFajr, dtpDhuhr, dtpAsr, dtpMaghrib, dtpIsha })
                dtp.ValueChanged += (s, e) => SettingChanged?.Invoke(this, EventArgs.Empty);
        }

        private void LoadDefaultData()
        {
            // TODO: BLL — استبدال بـ clsLocationManager.GetCountries() من الـ DAL
            // الحالي: بيانات مؤقتة — لحين بناء طبقة الـ BLL
            cboCountry.Items.AddRange(new object[] { "Egypt", "Saudi Arabia", "UAE", "Jordan", "Morocco", "Turkey", "Malaysia", "Indonesia" });
            cboCountry.SelectedIndex = 0;

            // TODO: BLL — استبدال بـ clsPrayerTimeManager.GetMethods() من الـ DAL
            cboMethod.Items.AddRange(new object[] {
                "Egyptian General Authority",
                "University of Islamic Sciences, Karachi",
                "Islamic Society of North America",
                "Muslim World League",
                "Umm Al-Qura University",
            });
            cboMethod.SelectedIndex = 0;
        }

        private void UpdateCities()
        {
            // TODO: BLL — استبدال بـ clsLocationManager.GetCitiesByCountry(country) من الـ DAL
            // الحالي: بيانات مؤقتة hardcoded — لحين ربط DB.Locations
            cboCity.Items.Clear();
            string country = cboCountry.SelectedItem?.ToString() ?? "";

            string[] cities = country switch
            {
                "Egypt" => new[] { "Cairo", "Alexandria", "Giza", "Luxor", "Aswan" },
                "Saudi Arabia" => new[] { "Makkah", "Madinah", "Riyadh", "Jeddah" },
                "UAE" => new[] { "Dubai", "Abu Dhabi", "Sharjah" },
                "Jordan" => new[] { "Amman", "Irbid", "Zarqa" },
                "Morocco" => new[] { "Rabat", "Casablanca", "Fes" },
                "Turkey" => new[] { "Istanbul", "Ankara", "Izmir" },
                "Malaysia" => new[] { "Kuala Lumpur", "Penang", "Johor" },
                "Indonesia" => new[] { "Jakarta", "Surabaya", "Bandung" },
                _ => new[] { "Select City" }
            };

            cboCity.Items.AddRange(cities);
            if (cboCity.Items.Count > 0)
                cboCity.SelectedIndex = 0;
        }

        // ═══════════════════════════════════════
        //  IThemeable
        // ═══════════════════════════════════════

        public void ApplyTheme(ThemeColors t)
        {
            lblSectionTitle.ForeColor = t.TextPrimary;
            radAuto.ForeColor = t.TextPrimary;
            radManual.ForeColor = t.TextPrimary;
            radAuto.CheckedState.FillColor = t.Accent1;
            radAuto.CheckedState.BorderColor = t.Accent1;
            radManual.CheckedState.FillColor = t.Accent1;
            radManual.CheckedState.BorderColor = t.Accent1;

            lblCountry.ForeColor = t.TextSecondary;
            lblCity.ForeColor = t.TextSecondary;
            lblMethod.ForeColor = t.TextSecondary;

            foreach (var cbo in new[] { cboCountry, cboCity, cboMethod })
            {
                cbo.FillColor = t.InputBg;
                cbo.ForeColor = t.InputText;
                cbo.FocusedColor = t.Accent1;
                cbo.BorderColor = t.BorderDefault;
            }

            // Manual DTP labels
            foreach (var lbl in new[] { lblManFajr, lblManDhuhr, lblManAsr, lblManMaghrib, lblManIsha })
                lbl.ForeColor = t.TextPrimary;

            foreach (var dtp in new[] { dtpFajr, dtpDhuhr, dtpAsr, dtpMaghrib, dtpIsha })
            {
                dtp.BackColor = t.InputBg;
                dtp.ForeColor = t.InputText;
                dtp.CalendarForeColor = t.TextPrimary;
                dtp.CalendarMonthBackground = t.BgSurface;
            }
        }

        // ═══════════════════════════════════════
        //  ILocalizable
        // ═══════════════════════════════════════

        public void ApplyLanguage(ILanguagePack lang)
        {
            lblSectionTitle.Text = "🕐 " + lang.SettingsTabPrayer;
            radAuto.Text = lang.SettingsPrayerAuto;
            radManual.Text = lang.SettingsPrayerManual;
            lblCountry.Text = lang.SettingsPrayerCountry;
            lblCity.Text = lang.SettingsPrayerCity;
            lblMethod.Text = lang.SettingsPrayerMethod;

            // Manual labels
            lblManFajr.Text = PrayerHelper.GetEmoji(Core.ePrayer.Fajr) + " " + PrayerHelper.GetName(Core.ePrayer.Fajr, lang);
            lblManDhuhr.Text = PrayerHelper.GetEmoji(Core.ePrayer.Dhuhr) + " " + PrayerHelper.GetName(Core.ePrayer.Dhuhr, lang);
            lblManAsr.Text = PrayerHelper.GetEmoji(Core.ePrayer.Asr) + " " + PrayerHelper.GetName(Core.ePrayer.Asr, lang);
            lblManMaghrib.Text = PrayerHelper.GetEmoji(Core.ePrayer.Maghrib) + " " + PrayerHelper.GetName(Core.ePrayer.Maghrib, lang);
            lblManIsha.Text = PrayerHelper.GetEmoji(Core.ePrayer.Isha) + " " + PrayerHelper.GetName(Core.ePrayer.Isha, lang);
        }
    }
}
