using UI.Core;
using UI.Core.Theme;
using UI.Core.Language;

namespace UI.Controls.Card
{
    /// <summary>
    /// كارت صلاة واحدة — يعرض الإيموجي + الاسم + الوقت + الحالة.
    /// يُستخدم 5 مرات في الداشبورد (Fajr → Isha).
    /// 
    /// ┌────────────┐
    /// │     🌅      │
    /// │   الفجر     │
    /// │  04:35 AM   │
    /// │     ✅      │
    /// └────────────┘
    /// </summary>
    public partial class ucPrayerCard : UserControl, IThemeable, ILocalizable
    {
        // ═══════════════════════════════════════
        //  Fields
        // ═══════════════════════════════════════

        private ePrayer _prayer = ePrayer.Fajr;
        private ePrayerStatus _status = ePrayerStatus.Upcoming;
        private TimeOnly _prayerTime = new(4, 35);

        // ═══════════════════════════════════════
        //  Constructor
        // ═══════════════════════════════════════

        public ucPrayerCard()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        // ═══════════════════════════════════════
        //  Properties
        // ═══════════════════════════════════════

        /// <summary>أي صلاة (Fajr..Isha)</summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public ePrayer Prayer
        {
            get => _prayer;
            set
            {
                _prayer = value;
                lblEmoji.Text = PrayerHelper.GetEmoji(_prayer);
            }
        }

        /// <summary>وقت الصلاة</summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public TimeOnly PrayerTime
        {
            get => _prayerTime;
            set
            {
                _prayerTime = value;
                lblTime.Text = _prayerTime.ToString("hh:mm tt");
            }
        }

        /// <summary>حالة الصلاة (Passed/Next/Upcoming)</summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public ePrayerStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                lblStatus.Text = PrayerHelper.GetStatusEmoji(_status);
                // إعادة تطبيق الثيم عشان الألوان تتغير
                ApplyTheme(clsThemeManager.Colors);
            }
        }

        // ═══════════════════════════════════════
        //  IThemeable
        // ═══════════════════════════════════════

        public void ApplyTheme(ThemeColors t)
        {
            switch (_status)
            {
                case ePrayerStatus.Passed:
                    // خافت — الصلاة فاتت
                    pnlCard.FillColor = ThemeColorUtils.WithAlpha(t.BgSurface, 150);
                    pnlCard.BorderColor = Color.Transparent;
                    pnlCard.ShadowDecoration.Enabled = false;
                    lblEmoji.ForeColor = t.TextMuted;
                    lblPrayerName.ForeColor = t.TextMuted;
                    lblTime.ForeColor = t.TextMuted;
                    break;

                case ePrayerStatus.Next:
                    // مميزة — الصلاة القادمة (border أخضر + shadow)
                    pnlCard.FillColor = t.BgSurface;
                    pnlCard.BorderColor = t.Accent1;
                    pnlCard.BorderThickness = 2;
                    pnlCard.ShadowDecoration.Enabled = true;
                    pnlCard.ShadowDecoration.Color = t.ShadowColorCard;
                    pnlCard.ShadowDecoration.Depth = t.ShadowDepthCard;
                    lblEmoji.ForeColor = t.Accent1;
                    lblPrayerName.ForeColor = t.TextPrimary;
                    lblTime.ForeColor = t.Accent2;
                    break;

                case ePrayerStatus.Upcoming:
                default:
                    // عادية
                    pnlCard.FillColor = t.BgSurface;
                    pnlCard.BorderColor = t.BorderDefault;
                    pnlCard.BorderThickness = 1;
                    pnlCard.ShadowDecoration.Enabled = true;
                    pnlCard.ShadowDecoration.Color = t.ShadowColorCard;
                    pnlCard.ShadowDecoration.Depth = Math.Max(1, t.ShadowDepthCard - 2);
                    lblEmoji.ForeColor = t.TextSecondary;
                    lblPrayerName.ForeColor = t.TextSecondary;
                    lblTime.ForeColor = t.TextSecondary;
                    break;
            }

            lblStatus.ForeColor = t.TextMuted;
        }

        // ═══════════════════════════════════════
        //  ILocalizable
        // ═══════════════════════════════════════

        public void ApplyLanguage(ILanguagePack lang)
        {
            lblPrayerName.Text = PrayerHelper.GetName(_prayer, lang);
        }
    }
}
