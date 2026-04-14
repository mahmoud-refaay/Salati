using UI.Core;
using UI.Core.Theme;
using UI.Core.Language;
using UI.Core.Animation;

namespace UI.Controls.Card
{
    /// <summary>
    /// ⭐ كارت الصلاة القادمة (Hero Card) — أكبر عنصر في الداشبورد.
    /// فيه Timer كل ثانية يحدّث الـ countdown والـ progress bar.
    /// 
    /// ┌──────────────────────────────────────────┐
    /// │              🌤️                          │
    /// │          صلاة العصر                       │
    /// │          ASR PRAYER                       │
    /// │       03:45 PM    01:23:45               │
    /// │       ████████████░░░░  72%              │
    /// │        متبقي | REMAINING                 │
    /// └──────────────────────────────────────────┘
    /// </summary>
    public partial class ucNextPrayer : UserControl, IThemeable, ILocalizable
    {
        // ═══════════════════════════════════════
        //  Events
        // ═══════════════════════════════════════

        /// <summary>يُطلق عند انتهاء العد التنازلي</summary>
        public event EventHandler? CountdownFinished;

        // ═══════════════════════════════════════
        //  Fields
        // ═══════════════════════════════════════

        private ePrayer _prayer = ePrayer.Asr;
        private TimeOnly _prayerTime = new(15, 45);
        private DateTime _countdownTarget;
        private readonly System.Windows.Forms.Timer _countdownTimer;

        // الفرق الكلي (لحساب الـ progress)
        private double _totalSeconds;
        private bool _isFinished;

        // ═══════════════════════════════════════
        //  Constructor
        // ═══════════════════════════════════════

        public ucNextPrayer()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            _countdownTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            _countdownTimer.Tick += OnCountdownTick;

            // TODO: BLL — البيانات تجي من frmMain عبر:
            // Prayer = clsPrayerTimeManager.GetNextPrayer()
            // PrayerTime = clsPrayerTimeManager.GetPrayerTime(prayer)
            // StartCountdown(target) من معاييد الصلاة اليومية
            // CountdownFinished += → frmMain.OnPrayerTimeReached()
        }

        // ═══════════════════════════════════════
        //  Properties
        // ═══════════════════════════════════════

        /// <summary>الصلاة القادمة</summary>
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

        /// <summary>وقت الصلاة القادمة</summary>
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

        // ═══════════════════════════════════════
        //  Countdown Logic
        // ═══════════════════════════════════════

        /// <summary>يبدأ العد التنازلي لوقت الصلاة</summary>
        public void StartCountdown(DateTime target)
        {
            _countdownTarget = target;
            _totalSeconds = (_countdownTarget - DateTime.Now).TotalSeconds;
            _isFinished = false;

            if (_totalSeconds <= 0)
            {
                OnCountdownComplete();
                return;
            }

            _countdownTimer.Start();
            OnCountdownTick(null, EventArgs.Empty); // تحديث فوري
        }

        /// <summary>يوقف العد التنازلي</summary>
        public void StopCountdown()
        {
            _countdownTimer.Stop();
        }

        private void OnCountdownTick(object? sender, EventArgs e)
        {
            var remaining = _countdownTarget - DateTime.Now;

            if (remaining.TotalSeconds <= 0)
            {
                OnCountdownComplete();
                return;
            }

            // تحديث النص
            lblCountdown.Text = remaining.ToString(@"hh\:mm\:ss");

            // تحديث الـ progress (100% = خلص، 0% = لسه بدري)
            double elapsed = _totalSeconds - remaining.TotalSeconds;
            int progress = (int)Math.Clamp((elapsed / _totalSeconds) * 100, 0, 100);
            progressBar.Value = progress;
        }

        private void OnCountdownComplete()
        {
            if (_isFinished) return;
            _isFinished = true;

            _countdownTimer.Stop();
            lblCountdown.Text = "00:00:00";
            progressBar.Value = 100;

            // نبضة عند الانتهاء
            clsAnimationManager.Pulse(pnlHero, 400, 1.03f);

            CountdownFinished?.Invoke(this, EventArgs.Empty);
        }

        // ═══════════════════════════════════════
        //  IThemeable
        // ═══════════════════════════════════════

        public void ApplyTheme(ThemeColors t)
        {
            // Hero gradient background
            pnlHero.FillColor = t.GradientPanelHero1;
            pnlHero.FillColor2 = t.GradientPanelHero2;

            // Shadow
            pnlHero.ShadowDecoration.Color = t.ShadowColorCard;
            pnlHero.ShadowDecoration.Depth = t.ShadowDepthCard + 2;

            // Labels
            lblEmoji.ForeColor = t.Accent1;
            lblPrayerName.ForeColor = t.TextPrimary;
            lblPrayerNameEn.ForeColor = t.TextMuted;
            lblTime.ForeColor = t.Accent2;
            lblCountdown.ForeColor = t.Accent2;
            lblRemaining.ForeColor = t.TextMuted;

            // Progress bar
            progressBar.FillColor = ThemeColorUtils.WithAlpha(t.Accent1, 40);
            progressBar.ProgressColor = t.Accent1;
            progressBar.ProgressColor2 = t.Accent2;
        }

        // ═══════════════════════════════════════
        //  ILocalizable
        // ═══════════════════════════════════════

        public void ApplyLanguage(ILanguagePack lang)
        {
            lblPrayerName.Text = PrayerHelper.GetName(_prayer, lang);
            lblPrayerNameEn.Text = PrayerHelper.GetEnglishName(_prayer, lang);
            lblRemaining.Text = lang.DashboardRemaining;
        }
    }
}
