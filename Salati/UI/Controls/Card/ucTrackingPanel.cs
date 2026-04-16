using BLL.Services;
using UI.Core;
using UI.Core.Theme;
using UI.Core.Language;
using UI.Core.Animation;

namespace UI.Controls.Card
{
    /// <summary>
    /// لوحة تتبع الصلاة — 5 صفوف + إحصائيات + Streak.
    /// مربوطة بـ BLL → DAL → DB (Dapper).
    /// </summary>
    public partial class ucTrackingPanel : UserControl, IThemeable, ILocalizable
    {
        // ═══════════════════════════════════════
        //  Fields
        // ═══════════════════════════════════════

        private readonly ucPrayerTrackRow[] _rows = new ucPrayerTrackRow[5];
        private readonly PrayerTrackingService _service = new();

        // ═══════════════════════════════════════
        //  Events
        // ═══════════════════════════════════════

        /// <summary>المستخدم عايز يقفل اللوحة</summary>
        public event EventHandler? CloseRequested;

        /// <summary>يُطلق لما المستخدم يعلّم/يلغي صلاة</summary>
        public event EventHandler<ePrayer>? PrayerToggled;

        // ═══════════════════════════════════════
        //  Constructor
        // ═══════════════════════════════════════

        public ucTrackingPanel()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            CreateRows();
            btnClose.Click += (s, e) => CloseRequested?.Invoke(this, EventArgs.Empty);
            clsAnimationManager.AttachHoverScale(btnClose, 1.1f);
        }

        // ═══════════════════════════════════════
        //  Create Rows
        // ═══════════════════════════════════════

        private void CreateRows()
        {
            var prayers = new[] { ePrayer.Fajr, ePrayer.Dhuhr, ePrayer.Asr, ePrayer.Maghrib, ePrayer.Isha };

            for (int i = 0; i < 5; i++)
            {
                _rows[i] = new ucPrayerTrackRow
                {
                    Prayer = prayers[i],
                    Size = new Size(380, 58),
                    Margin = new Padding(0, 0, 0, 4),
                };

                var prayer = prayers[i];
                _rows[i].ToggleClicked += (s, e) => PrayerToggled?.Invoke(this, prayer);
                pnlRows.Controls.Add(_rows[i]);
            }
        }

        // ═══════════════════════════════════════
        //  Public API
        // ═══════════════════════════════════════

        /// <summary>يحدّث حالة صلاة معينة</summary>
        public void SetPrayerStatus(ePrayer prayer, eTrackingStatus status, TimeOnly time, bool isLocked)
        {
            int idx = (int)prayer - 1;
            if (idx < 0 || idx >= 5) return;

            _rows[idx].PrayerTime = time;
            _rows[idx].IsLocked = isLocked;
            _rows[idx].TrackingStatus = status;
        }

        /// <summary>يحدّث الإحصائيات</summary>
        public void SetStats(int commitmentPercent, int streakDays)
        {
            progressBar.Value = Math.Clamp(commitmentPercent, 0, 100);
            lblCommitment.Text = $"📊 {clsLanguageManager.Current.TrackingCommitment}: {commitmentPercent}%";
            lblStreak.Text = string.Format(clsLanguageManager.Current.TrackingStreakDays, streakDays);
        }

        /// <summary>🆕 يحمّل بيانات من الداتابيز — async</summary>
        public async Task LoadFromDatabaseAsync()
        {
            try
            {
                // ── 1. حالة صلوات اليوم ──
                var trackingResult = await _service.GetTodayTrackingAsync();

                if (trackingResult.IsSuccess && trackingResult.Data != null && trackingResult.Data.Count > 0)
                {
                    var now = TimeOnly.FromDateTime(DateTime.Now);
                    foreach (var dto in trackingResult.Data)
                    {
                        int idx = (int)dto.Prayer - 1;
                        if (idx < 0 || idx >= 5) continue;

                        var time = TimeOnly.FromTimeSpan(dto.PrayerTime);
                        bool isLocked = time > now;

                        // Map DAL enum → UI enum
                        var uiStatus = (eTrackingStatus)(byte)dto.Status;
                        SetPrayerStatus((ePrayer)(byte)dto.Prayer, uiStatus, time, isLocked);
                    }

                    // ── 2. إحصائيات ──
                    int streak = await _service.GetStreakCountAsync();
                    decimal percentage = await _service.GetOverallPercentageAsync();
                    SetStats((int)percentage, streak);
                }
                else
                {
                    // DB فاضية → عرض mock data
                    LoadMockData();
                }
            }
            catch
            {
                // أي خطأ → fallback to mock
                LoadMockData();
            }
        }

        /// <summary>يحمّل بيانات mock للاختبار (fallback)</summary>
        public void LoadMockData()
        {
            var mockTimes = new TimeOnly[]
            {
                new(4, 35), new(12, 15), new(15, 45), new(18, 30), new(20, 0)
            };

            var now = TimeOnly.FromDateTime(DateTime.Now);

            for (int i = 0; i < 5; i++)
            {
                bool passed = mockTimes[i] < now;
                eTrackingStatus status = passed ? eTrackingStatus.NotMarked : eTrackingStatus.NotMarked;
                SetPrayerStatus((ePrayer)(i + 1), status, mockTimes[i], !passed);
            }

            // Mock stats
            SetStats(60, 3);
        }

        // ═══════════════════════════════════════
        //  Slide Animation (نفس نمط Settings)
        // ═══════════════════════════════════════

        public void SlideIn()
        {
            if (this.Parent == null)
                return;

            this.Visible = true;
            this.Left = this.Parent.ClientSize.Width; // خارج الشاشة
            clsAnimationManager.SlideIn(this, eSlideDirection.FromRight, 350, eEasing.EaseOut);
        }

        public void SlideOut()
        {
            clsAnimationManager.SlideOut(this, eSlideDirection.FromRight, 300, eEasing.EaseIn);
        }

        // ═══════════════════════════════════════
        //  IThemeable
        // ═══════════════════════════════════════

        public void ApplyTheme(ThemeColors t)
        {
            pnlBackground.FillColor = t.BgPrimary;
            pnlBackground.ShadowDecoration.Color = Color.FromArgb(80, 0, 0, 0);

            lblTitle.ForeColor = t.TextPrimary;
            lblSubtitle.ForeColor = t.TextSecondary;
            btnClose.ForeColor = t.TextMuted;
            btnClose.HoverState.ForeColor = t.Danger;

            pnlRows.BackColor = Color.Transparent;

            pnlStats.FillColor = t.BgSurface;
            progressBar.FillColor = ThemeColorUtils.WithAlpha(t.Accent1, 30);
            progressBar.ProgressColor = t.Accent1;
            progressBar.ProgressColor2 = t.Accent2;
            lblCommitment.ForeColor = t.Accent2;
            lblStreak.ForeColor = t.Accent1;
        }

        // ═══════════════════════════════════════
        //  ILocalizable
        // ═══════════════════════════════════════

        public void ApplyLanguage(ILanguagePack lang)
        {
            lblTitle.Text = $"🕌 {lang.TrackingTitle}";
            lblSubtitle.Text = lang.TrackingSubtitle;
        }
    }
}
