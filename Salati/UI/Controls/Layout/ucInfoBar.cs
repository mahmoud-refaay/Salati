using System.Globalization;
using UI.Core.Theme;
using UI.Core.Language;

namespace UI.Controls.Layout
{
    /// <summary>
    /// شريط المعلومات السفلي — التاريخ + المدينة + زر Tray.
    /// 
    /// ┌─────────────────────────────────────────────┐
    /// │ 📅 Saturday, April 12   📍 Cairo    ▼ Tray  │
    /// └─────────────────────────────────────────────┘
    /// </summary>
    public partial class ucInfoBar : UserControl, IThemeable, ILocalizable
    {
        // ═══════════════════════════════════════
        //  Events
        // ═══════════════════════════════════════

        /// <summary>▼ المستخدم ضغط Minimize to Tray</summary>
        public event EventHandler? TrayClicked;

        // ═══════════════════════════════════════
        //  Constructor
        // ═══════════════════════════════════════

        public ucInfoBar()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            lnkTray.Click += (s, e) => TrayClicked?.Invoke(this, EventArgs.Empty);
        }

        // ═══════════════════════════════════════
        //  Public Methods
        // ═══════════════════════════════════════

        /// <summary>يحدّث التاريخ المعروض (ميلادي + هجري)</summary>
        public void UpdateDate(DateTime date, bool isArabic)
        {
            string gregorian = isArabic
                ? date.ToString("📅 dddd، d MMMM", new CultureInfo("ar-EG"))
                : date.ToString("📅 dddd, MMMM d", CultureInfo.InvariantCulture);

            string hijri = GetHijriDate(isArabic);

            lblDate.Text = $"{gregorian}  ·  {hijri}";
        }

        /// <summary>يحسب التاريخ الهجري من System.Globalization</summary>
        private static string GetHijriDate(bool isArabic)
        {
            var hijri = new HijriCalendar();
            var today = DateTime.Today;

            int hDay = hijri.GetDayOfMonth(today);
            int hMonth = hijri.GetMonth(today);
            int hYear = hijri.GetYear(today);

            string[] monthsAr = ["", "محرم", "صفر", "ربيع الأول", "ربيع الثاني",
                "جمادى الأولى", "جمادى الآخرة", "رجب", "شعبان",
                "رمضان", "شوال", "ذو القعدة", "ذو الحجة"];

            string[] monthsEn = ["", "Muharram", "Safar", "Rabi I", "Rabi II",
                "Jumada I", "Jumada II", "Rajab", "Sha'ban",
                "Ramadan", "Shawwal", "Dhul-Qi'dah", "Dhul-Hijjah"];

            string monthName = isArabic ? monthsAr[hMonth] : monthsEn[hMonth];
            return $"🌙 {hDay} {monthName} {hYear}";
        }

        /// <summary>يحدّث اسم المدينة</summary>
        public void UpdateCity(string city)
        {
            lblCity.Text = $"📍 {city}";
        }

        // ═══════════════════════════════════════
        //  IThemeable
        // ═══════════════════════════════════════

        public void ApplyTheme(ThemeColors t)
        {
            pnlBackground.FillColor = t.BgSecondary;
            lblDate.ForeColor = t.TextMuted;
            lblCity.ForeColor = t.TextMuted;
            lnkTray.LinkColor = t.TextMuted;
            lnkTray.ActiveLinkColor = t.Accent1;
        }

        // ═══════════════════════════════════════
        //  ILocalizable
        // ═══════════════════════════════════════

        public void ApplyLanguage(ILanguagePack lang)
        {
            lnkTray.Text = "▼ " + lang.LayoutMinimizeToTray;
            UpdateDate(DateTime.Now, lang.IsRtl);
        }
    }
}
