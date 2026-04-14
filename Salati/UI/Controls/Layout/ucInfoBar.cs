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

        /// <summary>يحدّث التاريخ المعروض</summary>
        public void UpdateDate(DateTime date, bool isArabic)
        {
            string formatted = isArabic
                ? date.ToString("📅 dddd، d MMMM yyyy", new System.Globalization.CultureInfo("ar-EG"))
                : date.ToString("📅 dddd, MMMM d, yyyy", System.Globalization.CultureInfo.InvariantCulture);

            lblDate.Text = formatted;
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
