using UI.Core.Theme;
using UI.Core.Language;
using UI.Core.Animation;

namespace UI.Controls.Settings
{
    /// <summary>
    /// ⭐ الـ Panel الرئيسي للإعدادات — يسلايد من اليمين فوق الداشبورد.
    /// فيه 4 tabs (Prayer, Alerts, Appearance, General) + Save button.
    /// 
    /// ── الاستخدام في frmMain ──
    /// ucSettingsPanel1.SlideIn();   // يظهر
    /// ucSettingsPanel1.SlideOut();  // يختفي
    /// ucSettingsPanel1.SaveClicked += (s, e) => SaveAllSettings();
    /// </summary>
    public partial class ucSettingsPanel : UserControl, IThemeable, ILocalizable
    {
        // ═══════════════════════════════════════
        //  Events
        // ═══════════════════════════════════════

        /// <summary>يُطلق عند الضغط على ✕</summary>
        public event EventHandler? CloseRequested;

        /// <summary>يُطلق عند الضغط على 💾 حفظ</summary>
        public event EventHandler? SaveClicked;

        // ═══════════════════════════════════════
        //  Fields
        // ═══════════════════════════════════════

        private readonly ucSettingsPrayer _tabPrayer;
        private readonly ucSettingsAlerts _tabAlerts;
        private readonly ucSettingsAppearance _tabAppearance;
        private readonly ucSettingsGeneral _tabGeneral;

        private Guna.UI2.WinForms.Guna2Button _activeTabBtn = null!;
        private UserControl _activeTab = null!;

        private System.Windows.Forms.Timer? _slideTimer;
        private int _slideTarget;
        private const int SLIDE_SPEED = 25;

        // ═══════════════════════════════════════
        //  Constructor
        // ═══════════════════════════════════════

        public ucSettingsPanel()
        {
            InitializeComponent();

            // إنشاء التابات
            _tabPrayer = new ucSettingsPrayer { Dock = DockStyle.Fill };
            _tabAlerts = new ucSettingsAlerts { Dock = DockStyle.Fill };
            _tabAppearance = new ucSettingsAppearance { Dock = DockStyle.Fill };
            _tabGeneral = new ucSettingsGeneral { Dock = DockStyle.Fill };

            // ⚡ تحميل كل التابات مرة واحدة (بدل Clear/Add كل مرة)
            // الـ Visibility Toggle أسرع بكتير من إزالة/إضافة controls
            _tabAlerts.Visible = false;
            _tabAppearance.Visible = false;
            _tabGeneral.Visible = false;
            pnlContent.Controls.AddRange(new Control[] { _tabPrayer, _tabAlerts, _tabAppearance, _tabGeneral });

            WireEvents();
            SwitchTab(btnTabPrayer, _tabPrayer);
        }

        // ═══════════════════════════════════════
        //  Public Tabs Access
        // ═══════════════════════════════════════

        public ucSettingsPrayer TabPrayer => _tabPrayer;
        public ucSettingsAlerts TabAlerts => _tabAlerts;
        public ucSettingsAppearance TabAppearance => _tabAppearance;
        public ucSettingsGeneral TabGeneral => _tabGeneral;

        // ═══════════════════════════════════════
        //  Slide Animation
        // ═══════════════════════════════════════

        /// <summary>يسلايد من اليمين للظهور</summary>
        public void SlideIn()
        {
            if (this.Parent == null) return;

            this.Visible = true;
            this.BringToFront();
            this.Location = new Point(this.Parent.ClientSize.Width, this.Top);
            _slideTarget = this.Parent.ClientSize.Width - this.Width;

            _slideTimer?.Stop();
            _slideTimer = new System.Windows.Forms.Timer { Interval = 12 };
            _slideTimer.Tick += (s, e) =>
            {
                if (this.Left > _slideTarget)
                {
                    this.Left -= SLIDE_SPEED;
                    if (this.Left <= _slideTarget)
                    {
                        this.Left = _slideTarget;
                        _slideTimer!.Stop();
                        _slideTimer.Dispose();
                        _slideTimer = null;
                    }
                }
            };
            _slideTimer.Start();
        }

        /// <summary>يسلايد لليمين للإخفاء</summary>
        public void SlideOut()
        {
            if (this.Parent == null) return;

            _slideTarget = this.Parent.ClientSize.Width;

            _slideTimer?.Stop();
            _slideTimer = new System.Windows.Forms.Timer { Interval = 12 };
            _slideTimer.Tick += (s, e) =>
            {
                if (this.Left < _slideTarget)
                {
                    this.Left += SLIDE_SPEED;
                    if (this.Left >= _slideTarget)
                    {
                        this.Left = _slideTarget;
                        this.Visible = false;
                        _slideTimer!.Stop();
                        _slideTimer.Dispose();
                        _slideTimer = null;
                    }
                }
            };
            _slideTimer.Start();
        }

        /// <summary>هل الـ panel ظاهر</summary>
        public bool IsOpen => this.Visible && this.Left < (this.Parent?.ClientSize.Width ?? 0);

        // ═══════════════════════════════════════
        //  Event Wiring
        // ═══════════════════════════════════════

        private void WireEvents()
        {
            btnClose.Click += (s, e) =>
            {
                SlideOut();
                CloseRequested?.Invoke(this, EventArgs.Empty);
            };

            // TODO: BLL — عند الضغط Save يتم استدعاء:
            // clsSettingsStore.SavePrayerSettings(TabPrayer)
            // clsSettingsStore.SaveAlertSettings(TabAlerts)
            // clsSettingsStore.SaveGeneralSettings(TabGeneral)
            // ثم ucToast.ShowSuccess(parent, "تم الحفظ")
            btnSave.Click += (s, e) => SaveClicked?.Invoke(this, EventArgs.Empty);

            btnTabPrayer.Click += (s, e) => SwitchTab(btnTabPrayer, _tabPrayer);
            btnTabAlerts.Click += (s, e) => SwitchTab(btnTabAlerts, _tabAlerts);
            btnTabAppearance.Click += (s, e) => SwitchTab(btnTabAppearance, _tabAppearance);
            btnTabGeneral.Click += (s, e) => SwitchTab(btnTabGeneral, _tabGeneral);
        }

        // ═══════════════════════════════════════
        //  Tab Switching (Visibility Toggle — بدون flicker)
        // ═══════════════════════════════════════

        /// <summary>
        /// يبدّل بين التابات عبر Visible بدل Controls.Clear/Add.
        /// كل التابات محمّلة مسبقاً في pnlContent — بس بنقلب الظهور.
        /// </summary>
        private void SwitchTab(Guna.UI2.WinForms.Guna2Button tabBtn, UserControl tabContent)
        {
            // Hide all tabs
            _tabPrayer.Visible = false;
            _tabAlerts.Visible = false;
            _tabAppearance.Visible = false;
            _tabGeneral.Visible = false;

            // Show selected
            tabContent.Visible = true;
            tabContent.BringToFront();
            _activeTab = tabContent;
            _activeTabBtn = tabBtn;

            // Update tab button highlight
            RefreshTabHighlight();
        }

        /// <summary>
        /// يحدّث ألوان أزرار التابات فقط — بدون لمس الـ content.
        /// يُستخدم في SwitchTab و ApplyTheme.
        /// </summary>
        private void RefreshTabHighlight()
        {
            var t = clsThemeManager.Colors;

            foreach (var btn in new[] { btnTabPrayer, btnTabAlerts, btnTabAppearance, btnTabGeneral })
            {
                btn.FillColor = Color.Transparent;
                btn.ForeColor = t.TextMuted;
            }

            if (_activeTabBtn != null)
            {
                _activeTabBtn.FillColor = ThemeColorUtils.WithAlpha(t.Accent1, 30);
                _activeTabBtn.ForeColor = t.Accent1;
            }
        }

        // ═══════════════════════════════════════
        //  IThemeable
        // ═══════════════════════════════════════

        public void ApplyTheme(ThemeColors t)
        {
            // Main panel
            pnlMain.FillColor = t.BgPrimary;
            pnlMain.BorderColor = ThemeColorUtils.WithAlpha(t.Accent1, 38);
            pnlMain.ShadowDecoration.Color = Color.FromArgb(80, 0, 0, 0);

            // Header
            pnlHeader.BackColor = t.BgSecondary;
            lblTitle.ForeColor = t.TextPrimary;
            btnClose.ForeColor = t.TextSecondary;
            btnClose.HoverState.FillColor = t.Danger;

            // Tabs strip
            pnlTabs.BackColor = t.BgSecondary;

            // Refreshing active tab highlight (بدون لمس الـ content)
            RefreshTabHighlight();

            // Footer
            pnlFooter.BackColor = t.BgSecondary;
            btnSave.FillColor = t.GradientBtn1;
            btnSave.FillColor2 = t.GradientBtn2;
            btnSave.HoverState.FillColor = t.GradientBtnHover2;
            btnSave.HoverState.FillColor2 = t.GradientBtnHover1;
            btnSave.PressedColor = t.GradientBtnPressed1;

            // Apply theme to all tabs
            _tabPrayer.ApplyTheme(t);
            _tabAlerts.ApplyTheme(t);
            _tabAppearance.ApplyTheme(t);
            _tabGeneral.ApplyTheme(t);
        }

        // ═══════════════════════════════════════
        //  ILocalizable
        // ═══════════════════════════════════════

        public void ApplyLanguage(ILanguagePack lang)
        {
            lblTitle.Text = "⚙️ " + lang.SettingsTitle;
            btnSave.Text = "💾 " + lang.CommonSave;

            _tabPrayer.ApplyLanguage(lang);
            _tabAlerts.ApplyLanguage(lang);
            _tabAppearance.ApplyLanguage(lang);
            _tabGeneral.ApplyLanguage(lang);
        }
    }
}
