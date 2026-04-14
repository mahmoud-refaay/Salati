using UI.Core;
using UI.Core.Theme;
using UI.Core.Language;
using UI.Core.Animation;

namespace UI.Forms
{
    /// <summary>
    /// نافذة التنبيه — تظهر عند وقت الصلاة.
    /// TopMost, CenterScreen, Borderless.
    /// تعرض اسم الصلاة + الوقت + آية + أزرار (إغلاق / إيقاف صوت).
    /// 
    /// ── الاستخدام ──
    /// var alert = new frmAlert();
    /// alert.SetPrayer(ePrayer.Fajr, new TimeOnly(4, 35));
    /// alert.Show();
    /// </summary>
    public partial class frmAlert : Form, IThemeable, ILocalizable
    {
        // ═══════════════════════════════════════
        //  Fields
        // ═══════════════════════════════════════

        private ePrayer _prayer = ePrayer.Fajr;
        private TimeOnly _prayerTime = new(4, 35);
        private bool _isSoundStopped;

        // ═══════════════════════════════════════
        //  Constructor
        // ═══════════════════════════════════════

        public frmAlert()
        {
            InitializeComponent();
            this.Opacity = 0;

            btnDismiss.Click += (s, e) => DismissAlert();
            btnStop.Click += (s, e) => StopSound();
        }

        // ═══════════════════════════════════════
        //  Public API
        // ═══════════════════════════════════════

        /// <summary>يضبط الصلاة ويبدأ التنبيه</summary>
        public void SetPrayer(ePrayer prayer, TimeOnly time)
        {
            _prayer = prayer;
            _prayerTime = time;
            _isSoundStopped = false;

            lblEmoji.Text = PrayerHelper.GetEmoji(prayer);
            lblTime.Text = time.ToString("hh:mm tt");

            // تطبيق الثيم واللغة
            ApplyTheme(clsThemeManager.Colors);
            ApplyLanguage(clsLanguageManager.Current);
        }

        // ═══════════════════════════════════════
        //  Lifecycle
        // ═══════════════════════════════════════

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // TODO: BLL — clsSoundPlayer.PlayAdhan()

            // Fade in + Scale pulse
            var fadeIn = new System.Windows.Forms.Timer { Interval = 12 };
            fadeIn.Tick += (s, ev) =>
            {
                if (this.Opacity < 1)
                {
                    this.Opacity += 0.06;
                }
                else
                {
                    this.Opacity = 1;
                    fadeIn.Stop();
                    fadeIn.Dispose();

                    // Pulse animation على الـ emoji
                    clsAnimationManager.Pulse(lblEmoji, 800, 1.15f);
                }
            };
            fadeIn.Start();
        }

        // ═══════════════════════════════════════
        //  Actions
        // ═══════════════════════════════════════

        private void DismissAlert()
        {
            // TODO: BLL — clsSoundPlayer.StopAdhan()

            // Fade out
            var fadeOut = new System.Windows.Forms.Timer { Interval = 12 };
            fadeOut.Tick += (s, e) =>
            {
                if (this.Opacity > 0)
                {
                    this.Opacity -= 0.08;
                }
                else
                {
                    fadeOut.Stop();
                    fadeOut.Dispose();
                    this.Close();
                }
            };
            fadeOut.Start();
        }

        private void StopSound()
        {
            if (_isSoundStopped) return;
            _isSoundStopped = true;

            // TODO: BLL — clsSoundPlayer.StopAdhan()
            btnStop.Text = "🔇 ✓";
            btnStop.ForeColor = Color.FromArgb(27, 138, 107);
            btnStop.BorderColor = Color.FromArgb(27, 138, 107);
        }

        // ═══════════════════════════════════════
        //  IThemeable
        // ═══════════════════════════════════════

        public void ApplyTheme(ThemeColors t)
        {
            pnlBg.FillColor = t.BgPrimary;
            pnlBg.FillColor2 = t.BgSecondary;

            lblEmoji.ForeColor = t.Accent1;
            lblPrayerName.ForeColor = t.TextPrimary;
            lblPrayerNameEn.ForeColor = t.TextMuted;
            lblTime.ForeColor = t.Accent2;
            lblQuran.ForeColor = t.TextMuted;

            btnDismiss.FillColor = t.GradientBtn1;
            btnDismiss.FillColor2 = t.GradientBtn2;
            btnDismiss.HoverState.FillColor = t.GradientBtnHover2;
            btnDismiss.HoverState.FillColor2 = t.GradientBtnHover1;

            btnStop.ForeColor = t.TextSecondary;
            btnStop.BorderColor = t.BorderDefault;
        }

        // ═══════════════════════════════════════
        //  ILocalizable
        // ═══════════════════════════════════════

        public void ApplyLanguage(ILanguagePack lang)
        {
            string name = PrayerHelper.GetName(_prayer, lang);
            lblPrayerName.Text = lang.IsRtl
                ? $"حان وقت صلاة {name}"
                : $"Time for {PrayerHelper.GetEnglishName(_prayer, lang)} Prayer";
            lblPrayerNameEn.Text = PrayerHelper.GetEnglishName(_prayer, lang);

            btnDismiss.Text = "✕ " + (lang.IsRtl ? "إغلاق" : "Dismiss");
        }
    }
}
