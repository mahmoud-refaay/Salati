using UI.Core;
using UI.Core.Theme;
using UI.Core.Language;
using UI.Core.Animation;

namespace UI.Controls.Card
{
    /// <summary>
    /// صف تتبع صلاة واحدة — يعرض الإيموجي + الاسم + الوقت + حالة + زر Toggle.
    /// 
    /// ┌───────────────────────────────────────────────────────────┐
    /// │  🌅  الفجر    04:35 AM    ✅ صليت في الوقت    [☑️ Toggle] │
    /// └───────────────────────────────────────────────────────────┘
    /// </summary>
    public partial class ucPrayerTrackRow : UserControl, IThemeable, ILocalizable
    {
        // ═══════════════════════════════════════
        //  Fields
        // ═══════════════════════════════════════

        private ePrayer _prayer = ePrayer.Fajr;
        private eTrackingStatus _trackingStatus = eTrackingStatus.NotMarked;
        private TimeOnly _prayerTime = new(4, 35);
        private bool _isLocked; // الصلاة لسه ما أذّنتش

        // ═══════════════════════════════════════
        //  Events
        // ═══════════════════════════════════════

        /// <summary>يُطلق لما المستخدم يعلّم/يلغي التعليم</summary>
        public event EventHandler? ToggleClicked;

        // ═══════════════════════════════════════
        //  Constructor
        // ═══════════════════════════════════════

        public ucPrayerTrackRow()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            btnToggle.Click += (s, e) => ToggleClicked?.Invoke(this, EventArgs.Empty);
            clsAnimationManager.AttachHoverScale(btnToggle, 1.05f);
        }

        // ═══════════════════════════════════════
        //  Properties
        // ═══════════════════════════════════════

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

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public eTrackingStatus TrackingStatus
        {
            get => _trackingStatus;
            set
            {
                _trackingStatus = value;
                lblStatusIcon.Text = PrayerHelper.GetTrackingEmoji(_trackingStatus);
                UpdateStatusDisplay();
                ApplyTheme(clsThemeManager.Colors);
            }
        }

        /// <summary>الصلاة لسه ما أذّنتش — الزر مقفل</summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool IsLocked
        {
            get => _isLocked;
            set
            {
                _isLocked = value;
                btnToggle.Enabled = !_isLocked;
                UpdateStatusDisplay();
                ApplyTheme(clsThemeManager.Colors);
            }
        }

        // ═══════════════════════════════════════
        //  Internal
        // ═══════════════════════════════════════

        private void UpdateStatusDisplay()
        {
            var lang = clsLanguageManager.Current;

            if (_isLocked)
            {
                lblStatusIcon.Text = "🔒";
                lblStatusText.Text = lang.TrackingLocked;
                btnToggle.Text = "🔒";
            }
            else
            {
                lblStatusIcon.Text = PrayerHelper.GetTrackingEmoji(_trackingStatus);
                lblStatusText.Text = _trackingStatus switch
                {
                    eTrackingStatus.OnTime => lang.TrackingOnTime,
                    eTrackingStatus.Late => lang.TrackingLate,
                    eTrackingStatus.Missed => lang.TrackingMissed,
                    eTrackingStatus.NotMarked => lang.TrackingNotYet,
                    _ => ""
                };
                btnToggle.Text = _trackingStatus == eTrackingStatus.NotMarked
                    ? lang.TrackingMarkPrayed
                    : lang.TrackingUnmark;
            }
        }

        // ═══════════════════════════════════════
        //  IThemeable
        // ═══════════════════════════════════════

        public void ApplyTheme(ThemeColors t)
        {
            if (_isLocked)
            {
                // مقفل — لون خافت
                pnlRow.FillColor = ThemeColorUtils.WithAlpha(t.BgSurface, 100);
                pnlRow.BorderColor = Color.Transparent;
                pnlRow.ShadowDecoration.Enabled = false;
                lblEmoji.ForeColor = t.TextMuted;
                lblPrayerName.ForeColor = t.TextMuted;
                lblTime.ForeColor = t.TextMuted;
                lblStatusIcon.ForeColor = t.TextMuted;
                lblStatusText.ForeColor = t.TextMuted;
                btnToggle.FillColor = t.TextMuted;
                btnToggle.ForeColor = t.BgPrimary;
            }
            else
            {
                pnlRow.FillColor = t.BgSurface;
                pnlRow.BorderColor = t.BorderDefault;
                pnlRow.ShadowDecoration.Enabled = true;
                pnlRow.ShadowDecoration.Color = t.ShadowColorCard;
                pnlRow.ShadowDecoration.Depth = 3;
                lblEmoji.ForeColor = t.Accent1;
                lblPrayerName.ForeColor = t.TextPrimary;
                lblTime.ForeColor = t.TextSecondary;

                switch (_trackingStatus)
                {
                    case eTrackingStatus.OnTime:
                        lblStatusIcon.ForeColor = t.Success;
                        lblStatusText.ForeColor = t.Success;
                        btnToggle.FillColor = t.TextMuted;
                                break;
                    case eTrackingStatus.Late:
                        lblStatusIcon.ForeColor = t.Warning;
                        lblStatusText.ForeColor = t.Warning;
                        btnToggle.FillColor = t.TextMuted;
                                break;
                    case eTrackingStatus.Missed:
                        lblStatusIcon.ForeColor = t.Danger;
                        lblStatusText.ForeColor = t.Danger;
                        btnToggle.FillColor = t.Accent1;
                        break;
                    default: // NotMarked
                        lblStatusIcon.ForeColor = t.TextMuted;
                        lblStatusText.ForeColor = t.TextSecondary;
                        btnToggle.FillColor = t.Accent1;
                        break;
                }
                btnToggle.ForeColor = Color.White;
            }
        }

        // ═══════════════════════════════════════
        //  ILocalizable
        // ═══════════════════════════════════════

        public void ApplyLanguage(ILanguagePack lang)
        {
            lblPrayerName.Text = PrayerHelper.GetName(_prayer, lang);
            UpdateStatusDisplay();
        }
    }
}
