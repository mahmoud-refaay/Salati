
using UI.Core;
using UI.Core.Theme;
using UI.Core.Language;
using UI.Core.Engine;
using UI.Core.Animation;
using UI.Controls.Card;
using UI.Controls.Feedback;

namespace UI.Forms
{
    /// <summary>
    /// ⭐ النافذة الرئيسية — الداشبورد.
    /// 
    /// ╔══════════════════════════════════════════════╗
    /// ║  ⚡ 3 أوضاع:                                ║
    /// ║  1. Full Mode (700×500) — Dashboard كامل    ║
    /// ║  2. Widget Mode (300×85) — Compact strip     ║
    /// ║  3. Tray Mode — مخفي + NotifyIcon فقط       ║
    /// ╚══════════════════════════════════════════════╝
    /// </summary>
    public partial class frmMain : Form, IThemeable, ILocalizable
    {
        // ═══════════════════════════════════════
        //  Fields
        // ═══════════════════════════════════════

        private readonly ucPrayerCard[] _prayerCards = new ucPrayerCard[5];
        private bool _isWidgetMode;
        private System.Windows.Forms.Timer? _widgetTimer;

        // ═══════════════════════════════════════
        //  Constructor
        // ═══════════════════════════════════════

        public frmMain()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            // تفعيل DoubleBuffered على كل الـ controls (يمنع الفليكر)
            clsUIEngine.EnableDoubleBufferingTree(this);

            CreatePrayerCards();
            WireEvents();
            SetupTrayMenu();
            _ = LoadPrayerTimesAsync();

            // Pre-load: إنشاء handles الشاشات في الذاكرة
            // حجمها صغير + بيخلي أول فتح يكون فوري
            settingsPanel.CreateControl();
            trackingPanel.CreateControl();
            adhkarPanel.CreateControl();
            InitializePrayerTracking();

            // 📿 إشعارات الأذكار في الخلفية (الفترة من إعدادات المستخدم)
            _ = clsAdhkarService.StartAsync(notifyIcon);

            // ⏰ جدولة تنبيهات الصلاة (كل 30 ثانية)
            clsAlertScheduler.Start(notifyIcon);

            // تطبيق Theme + Language (smooth - بدون رعشة)
            clsUIEngine.ApplyAll(this);
            clsUIEngine.BindEvents(this);

            // ══════════════════════════════════════════════════════════
            // 🔊 TEST: بعد 5 ثواني يشغل الأذان أوتوماتيك
            // ══════════════════════════════════════════════════════════
            // var testTimer = new System.Windows.Forms.Timer { Interval = 5000 };
            // testTimer.Tick += (s, e) =>
            // {
            //     testTimer.Stop();
            //     testTimer.Dispose();
            //     try
            //     {
            //         string soundsPath = Path.Combine(Application.StartupPath, "Assets", "Sounds");
            //         var file = Directory.GetFiles(soundsPath, "*.m4a").FirstOrDefault();
            //         if (file == null) { MessageBox.Show("مفيش ملف أذان m4a!", "❌"); return; }
            //         var audio = new NAudio.Wave.MediaFoundationReader(file);
            //         var player = new NAudio.Wave.WaveOutEvent { Volume = 0.7f };
            //         player.Init(audio);
            //         player.Play();
            //         this.Text = $"🔊 بيشتغل: {Path.GetFileName(file)}";
            //     }
            //     catch (Exception ex) { MessageBox.Show($"خطأ: {ex.Message}", "❌"); }
            // };
            // testTimer.Start();
            // ══════════════════════════════════════════════════════════
        }

        // ═══════════════════════════════════════
        //  Prayer Cards Setup
        // ═══════════════════════════════════════

        private void CreatePrayerCards()
        {
            var prayers = new[] { ePrayer.Fajr, ePrayer.Dhuhr, ePrayer.Asr, ePrayer.Maghrib, ePrayer.Isha };

            for (int i = 0; i < 5; i++)
            {
                _prayerCards[i] = new ucPrayerCard
                {
                    Prayer = prayers[i],
                    Status = ePrayerStatus.Upcoming,
                    Size = new Size(120, 115),
                    Margin = new Padding(6, 4, 6, 4),
                };
                pnlPrayerCards.Controls.Add(_prayerCards[i]);

                // 🎬 Hover scale — الكارت يكبر شوية لما الماوس يدخل
                clsAnimationManager.AttachHoverScale(_prayerCards[i], 1.03f);
            }
        }

        // ═══════════════════════════════════════
        //  🎬 Dashboard Entrance Animations
        // ═══════════════════════════════════════

        /// <summary>أنيمشن دخول الداشبورد — يتنادى مرة واحدة عند الفتح</summary>
        private bool _entrancePlayed;
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (_entrancePlayed) return;
            _entrancePlayed = true;

            // Hero Card — slide from top
            clsAnimationManager.SlideIn(nextPrayer, eSlideDirection.FromTop, 350, eEasing.EaseOut);

            // Prayer Cards — staggered slide from bottom (80ms delay per card)
            for (int i = 0; i < _prayerCards.Length; i++)
            {
                var card = _prayerCards[i];
                card.Visible = false; // نخفيه لحد ما ييجي دوره

                int delay = 150 + (i * 80); // stagger: 150, 230, 310, 390, 470
                var delayTimer = new System.Windows.Forms.Timer { Interval = delay };
                var cardRef = card;
                delayTimer.Tick += (s, ev) =>
                {
                    delayTimer.Stop();
                    delayTimer.Dispose();
                    cardRef.Visible = true;
                    clsAnimationManager.SlideIn(cardRef, eSlideDirection.FromBottom, 300, eEasing.EaseOut);
                };
                delayTimer.Start();
            }
        }

        // ═══════════════════════════════════════
        //  Prayer Times — من API/DB (Dapper + Aladhan)
        // ═══════════════════════════════════════

        private async Task LoadPrayerTimesAsync()
        {
            TimeOnly[] prayerTimes;

            try
            {
                var service = new BLL.Services.PrayerTimesService();
                var result = await service.GetTodayTimesAsync();

                if (result.IsSuccess && result.Data != null)
                {
                    var times = result.Data;
                    prayerTimes = new TimeOnly[]
                    {
                        TimeOnly.FromTimeSpan(times.FajrTime),
                        TimeOnly.FromTimeSpan(times.DhuhrTime),
                        TimeOnly.FromTimeSpan(times.AsrTime),
                        TimeOnly.FromTimeSpan(times.MaghribTime),
                        TimeOnly.FromTimeSpan(times.IshaTime),
                    };
                }
                else
                {
                    // Fallback: لو فشل API أو DB فاضية → أوقات تقريبية للقاهرة
                    DAL.Logging.clsLogger.Warn($"[UI] API/DB failed — using fallback times: {result.Error}");
                    prayerTimes = GetFallbackTimes();
                }
            }
            catch (Exception ex)
            {
                DAL.Logging.clsLogger.Error("[UI] LoadPrayerTimes exception — using fallback", ex);
                prayerTimes = GetFallbackTimes();
            }

            ApplyPrayerTimes(prayerTimes);
        }

        /// <summary>أوقات تقريبية للقاهرة — تُستخدم لو API/DB فشل</summary>
        private static TimeOnly[] GetFallbackTimes() => new TimeOnly[]
        {
            new(4, 15),   // Fajr
            new(11, 55),  // Dhuhr
            new(15, 25),  // Asr
            new(18, 35),  // Maghrib
            new(19, 55),  // Isha
        };

        /// <summary>يضبط الأوقات على الكروت + يحدد الصلاة التالية</summary>
        private void ApplyPrayerTimes(TimeOnly[] prayerTimes)
        {
            var now = TimeOnly.FromDateTime(DateTime.Now);

            for (int i = 0; i < 5; i++)
            {
                _prayerCards[i].PrayerTime = prayerTimes[i];

                if (prayerTimes[i] < now)
                    _prayerCards[i].Status = ePrayerStatus.Passed;
                else if (i == 0 || prayerTimes[i - 1] < now)
                {
                    _prayerCards[i].Status = ePrayerStatus.Next;

                    // Hero card
                    nextPrayer.Prayer = _prayerCards[i].Prayer;
                    nextPrayer.PrayerTime = prayerTimes[i];

                    var target = DateTime.Today.Add(prayerTimes[i].ToTimeSpan());
                    if (target < DateTime.Now)
                        target = target.AddDays(1);
                    nextPrayer.StartCountdown(target);
                }
                else
                {
                    _prayerCards[i].Status = ePrayerStatus.Upcoming;
                }
            }
        }

        // ═══════════════════════════════════════
        //  Event Wiring
        // ═══════════════════════════════════════

        private void WireEvents()
        {
            // ── Title Bar ──
            titleBar.SettingsClicked += (s, e) => ShowSettings();
            titleBar.TrackingClicked += (s, e) => ShowTracking();
            titleBar.AdhkarClicked += (s, e) => ShowMorningAdhkar();
            titleBar.ThemeToggled += (s, e) => { /* clsUIEngine handles it */ };
            titleBar.LanguageToggled += (s, e) => { /* clsUIEngine handles it */ };
            titleBar.MinimizeClicked += (s, e) => this.WindowState = FormWindowState.Minimized;
            titleBar.CloseClicked += (s, e) => this.WindowState = FormWindowState.Minimized;
            titleBar.PinClicked += (s, e) => ToggleWidgetMode();

            // ── Settings Panel ──
            settingsPanel.CloseRequested += (s, e) => HideSettings();
            settingsPanel.SaveClicked += (s, e) =>
            {
                // TODO: BLL — clsSettingsStore.SaveAll()
                ucToast.ShowSuccess(this, "تم حفظ الإعدادات بنجاح");
                HideSettings();
            };

            // ── Tracking Panel ──
            trackingPanel.CloseRequested += (s, e) => HideTracking();
            trackingPanel.PrayerToggled += (s, prayer) => { };

            // ── Adhkar Panel ──
            adhkarPanel.CloseRequested += (s, e) => HideAdhkar();

            // ── Next Prayer ──
            nextPrayer.CountdownFinished += (s, e) =>
            {
                // TODO: BLL — فتح frmAlert + تشغيل الأذان
                // var alert = new frmAlert();
                // alert.Show();
            };

            // ── Overlay click → إغلاق ──
            pnlOverlay.Click += (s, e) => HideSettings();
            pnlOverlay2.Click += (s, e) => HideTracking();
            pnlOverlay3.Click += (s, e) => HideAdhkar();

            // ── Tray ──
            notifyIcon.DoubleClick += (s, e) => ShowFromTray();
        }

        // ═══════════════════════════════════════
        //  Settings Panel (Slide)
        // ═══════════════════════════════════════

        private void ShowSettings()
        {
            CloseTrackingImmediately();
            pnlOverlay.Visible = true;
            pnlOverlay.BringToFront();
            settingsPanel.BringToFront();
            settingsPanel.SlideIn();
        }

        private void HideSettings()
        {
            settingsPanel.SlideOut();

            // إخفاء الـ overlay بعد السلايد
            var hideTimer = new System.Windows.Forms.Timer { Interval = 400 };
            hideTimer.Tick += (s, e) =>
            {
                pnlOverlay.Visible = false;
                hideTimer.Stop();
                hideTimer.Dispose();
            };
            hideTimer.Start();
        }

        // ═══════════════════════════════════════
        //  Tracking Panel (Slide)
        // ═══════════════════════════════════════

        private async void ShowTracking()
        {
            CloseSettingsImmediately();
            pnlOverlay2.Visible = true;
            pnlOverlay2.BringToFront();
            trackingPanel.BringToFront();
            trackingPanel.SlideIn();
            await trackingPanel.LoadFromDatabaseAsync();
        }

        private void HideTracking()
        {
            trackingPanel.SlideOut();

            var hideTimer = new System.Windows.Forms.Timer { Interval = 400 };
            hideTimer.Tick += (s, e) =>
            {
                pnlOverlay2.Visible = false;
                hideTimer.Stop();
                hideTimer.Dispose();
            };
            hideTimer.Start();
        }

        // ═══════════════════════════════════════
        //  Adhkar Panel (Slide)
        // ═══════════════════════════════════════

        public async void ShowMorningAdhkar()
        {
            CloseSettingsImmediately();
            CloseTrackingImmediately();
            pnlOverlay3.Visible = true;
            pnlOverlay3.BringToFront();
            adhkarPanel.BringToFront();
            adhkarPanel.SlideIn();
            await adhkarPanel.LoadMorningAdhkarAsync();
        }

        public async void ShowEveningAdhkar()
        {
            CloseSettingsImmediately();
            CloseTrackingImmediately();
            pnlOverlay3.Visible = true;
            pnlOverlay3.BringToFront();
            adhkarPanel.BringToFront();
            adhkarPanel.SlideIn();
            await adhkarPanel.LoadEveningAdhkarAsync();
        }

        private void HideAdhkar()
        {
            adhkarPanel.SlideOut();

            var hideTimer = new System.Windows.Forms.Timer { Interval = 400 };
            hideTimer.Tick += (s, e) =>
            {
                pnlOverlay3.Visible = false;
                hideTimer.Stop();
                hideTimer.Dispose();
            };
            hideTimer.Start();
        }

        // ═══════════════════════════════════════
        //  Widget Mode (300×85)
        // ═══════════════════════════════════════

        private void ToggleWidgetMode()
        {
            if (_isWidgetMode)
                SwitchToFullMode();
            else
                SwitchToWidgetMode();
        }

        private void SwitchToWidgetMode()
        {
            _isWidgetMode = true;
            titleBar.IsWidgetMode = true; // 📌 → 🔙
            CloseSettingsImmediately();
            CloseTrackingImmediately();
            CloseAdhkarImmediately();
            this.Size = new Size(300, 85);
            this.TopMost = true;
            this.Location = new Point(
                Screen.PrimaryScreen!.WorkingArea.Right - 310,
                Screen.PrimaryScreen.WorkingArea.Top + 10);

            pnlDashboard.Visible = false;
            infoBar.Visible = false;

            // عرض معلومات الصلاة القادمة
            lblWidgetInfo.Visible = true;
            UpdateWidgetInfo();
            _widgetTimer?.Stop();
            _widgetTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            _widgetTimer.Tick += (s, e) => UpdateWidgetInfo();
            _widgetTimer.Start();
        }

        private void SwitchToFullMode()
        {
            _isWidgetMode = false;
            titleBar.IsWidgetMode = false; // 🔙 → 📌
            this.Size = new Size(700, 500);
            this.TopMost = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.CenterToScreen();

            pnlDashboard.Visible = true;
            infoBar.Visible = true;
            lblWidgetInfo.Visible = false;
            _widgetTimer?.Stop();
            _widgetTimer?.Dispose();
            _widgetTimer = null;
        }

        /// <summary>يحدّث الـ Widget بمعلومات الصلاة القادمة</summary>
        private void UpdateWidgetInfo()
        {
            var emoji = PrayerHelper.GetEmoji(nextPrayer.Prayer);
            var name = PrayerHelper.GetName(nextPrayer.Prayer, Core.Language.clsLanguageManager.Current);
            var remaining = nextPrayer.PrayerTime.ToTimeSpan() - TimeOnly.FromDateTime(DateTime.Now).ToTimeSpan();

            if (remaining.TotalSeconds <= 0)
                remaining = remaining.Add(TimeSpan.FromDays(1));

            lblWidgetInfo.Text = $"{emoji} {name} — {remaining:hh\\:mm\\:ss}";
        }

        // ═══════════════════════════════════════
        //  System Tray
        // ═══════════════════════════════════════

        private void SetupTrayMenu()
        {
            // Items and separators are pre-created in Designer.cs
            // Here we only wire events
            trayOpenItem.Click += (s, e) => ShowFromTray();
            trayWidgetItem.Click += (s, e) => { ShowFromTray(); SwitchToWidgetMode(); };
            traySettingsItem.Click += (s, e) => { ShowFromTray(); ShowSettings(); };
            trayExitItem.Click += (s, e) =>
            {
                notifyIcon.Visible = false;
                Application.Exit();
            };
        }

        private void MinimizeToTray()
        {
            this.Hide();
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(1500, "Salati", "البرنامج شغال في الخلفية 🕌\nالتنبيهات والأذكار مستمرة", ToolTipIcon.Info);
        }

        private void ShowFromTray()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;

            if (_isWidgetMode)
                SwitchToFullMode();
        }

        private void CloseSettingsImmediately()
        {
            pnlOverlay.Visible = false;
            settingsPanel.Visible = false;
            settingsPanel.Left = this.ClientSize.Width;
        }

        private void CloseTrackingImmediately()
        {
            pnlOverlay2.Visible = false;
            trackingPanel.Visible = false;
            trackingPanel.Left = this.ClientSize.Width;
        }

        private void CloseAdhkarImmediately()
        {
            pnlOverlay3.Visible = false;
            adhkarPanel.Visible = false;
            adhkarPanel.Left = this.ClientSize.Width;
        }

        // ═══════════════════════════════════════
        //  IThemeable
        // ═══════════════════════════════════════

        public void ApplyTheme(ThemeColors t)
        {
            pnlMainBg.FillColor = t.BgPrimary;
            pnlMainBg.FillColor2 = ThemeColorUtils.Lighten(t.BgPrimary, 5);
            pnlOverlay.BackColor = Color.FromArgb(120, 0, 0, 0);
            pnlOverlay2.BackColor = Color.FromArgb(120, 0, 0, 0);
            pnlOverlay3.BackColor = Color.FromArgb(120, 0, 0, 0);
        }

        // ═══════════════════════════════════════
        //  ILocalizable
        // ═══════════════════════════════════════

        public void ApplyLanguage(ILanguagePack lang)
        {
            this.Text = lang.LayoutAppTitle;
        }

        // ===== WndProc — Minimize/Restore Protection =====

        private const int WM_SYSCOMMAND = 0x0112;
        private const int WM_SETREDRAW = 0x000B;
        private const int SC_RESTORE = 0xF120;
        private const int SC_MINIMIZE = 0xF020;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr w, IntPtr l);

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SYSCOMMAND)
            {
                int command = m.WParam.ToInt32() & 0xFFF0;

                if (command == SC_MINIMIZE)
                {
                    // Freeze drawing BEFORE minimize (يمنع Guna2 crash عند Height=0)
                    SendMessage(this.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
                }
                else if (command == SC_RESTORE)
                {
                    // Restore + unfreeze
                    this.WindowState = FormWindowState.Normal;
                    this.Show();
                    this.Activate();

                    // Re-enable drawing AFTER restore
                    SendMessage(this.Handle, WM_SETREDRAW, (IntPtr)1, IntPtr.Zero);
                    this.Invalidate(true);
                    this.Update();
                    return; // handled
                }
            }
            base.WndProc(ref m);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.Style |= 0x20000; // WS_MINIMIZEBOX
                return cp;
            }
        }

        // حماية إضافية: لو الـ Form اتصغرت بأي طريقة
        protected override void OnResize(EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                return; // مش بنعمل layout لما الشاشة minimized

            base.OnResize(e);
        }

        // ═══════════════════════════════════════
        //  Prayer Tracking Integration
        // ═══════════════════════════════════════

        private async void InitializePrayerTracking()
        {
            // ملء الأيام الفائتة بـ Missed
            var service = new BLL.Services.PrayerTrackingService();
            await service.FillMissedDaysAsync();
        }

    }
}
