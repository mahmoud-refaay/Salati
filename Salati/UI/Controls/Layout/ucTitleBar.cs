using UI.Core.Theme;
using UI.Core.Language;

namespace UI.Controls.Layout
{
    /// <summary>
    /// شريط العنوان المخصص — بديل الـ Title Bar الافتراضي.
    /// يحتوي على: 🕌 Logo + App Name + أزرار (📌 ⚙️ 🌙 AR ─ ✕)
    /// يدعم السحب (Drag) لتحريك النافذة.
    /// 
    /// ── الاستخدام (في frmMain) ──
    /// ucTitleBar1.SettingsClicked += (s, e) => ShowSettingsPanel();
    /// ucTitleBar1.CloseClicked += (s, e) => MinimizeToTray();
    /// </summary>
    public partial class ucTitleBar : UserControl, IThemeable, ILocalizable
    {
        // ═══════════════════════════════════════
        //  Events — frmMain يسمع عليهم
        // ═══════════════════════════════════════

        /// <summary>📌 تبديل Widget Mode</summary>
        public event EventHandler? PinClicked;

        /// <summary>⚙️ فتح/إغلاق Settings Panel</summary>
        public event EventHandler? SettingsClicked;

        /// <summary>🌙 تبديل Dark/Light</summary>
        public event EventHandler? ThemeToggled;

        /// <summary>AR/EN تبديل اللغة</summary>
        public event EventHandler? LanguageToggled;

        /// <summary>─ تصغير للـ Taskbar</summary>
        public event EventHandler? MinimizeClicked;

        /// <summary>✕ إغلاق (أو Tray)</summary>
        public event EventHandler? CloseClicked;

        // ═══════════════════════════════════════
        //  Fields
        // ═══════════════════════════════════════

        private bool _isWidgetMode;
        private bool _isDragging;
        private Point _dragStart;

        // ═══════════════════════════════════════
        //  Constructor
        // ═══════════════════════════════════════

        public ucTitleBar()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            WireEvents();
        }

        // ═══════════════════════════════════════
        //  Properties
        // ═══════════════════════════════════════

        /// <summary>هل النافذة في Widget mode؟</summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool IsWidgetMode
        {
            get => _isWidgetMode;
            set
            {
                _isWidgetMode = value;
                btnPin.Text = _isWidgetMode ? "🔙" : "📌";

                // في Widget: نخفي الأزرار اللي مش محتاجينها (المساحة صغيرة)
                // نخلي بس 🔙 + ✕
                btnSettings.Visible = !_isWidgetMode;
                btnTheme.Visible = !_isWidgetMode;
                btnLang.Visible = !_isWidgetMode;
                btnMinimize.Visible = !_isWidgetMode;

                // إخفاء اسم التطبيق في Widget (المساحة صغيرة)
                lblAppTitle.Visible = !_isWidgetMode;
            }
        }

        // ═══════════════════════════════════════
        //  Event Wiring
        // ═══════════════════════════════════════

        private void WireEvents()
        {
            // ── أزرار ──
            btnPin.Click += (s, e) => PinClicked?.Invoke(this, EventArgs.Empty);
            btnSettings.Click += (s, e) => SettingsClicked?.Invoke(this, EventArgs.Empty);
            btnTheme.Click += (s, e) =>
            {
                clsThemeManager.ToggleTheme();
                ThemeToggled?.Invoke(this, EventArgs.Empty);
            };
            btnLang.Click += (s, e) =>
            {
                clsLanguageManager.ToggleLanguage();
                LanguageToggled?.Invoke(this, EventArgs.Empty);
            };
            btnMinimize.Click += (s, e) => MinimizeClicked?.Invoke(this, EventArgs.Empty);
            btnClose.Click += (s, e) => CloseClicked?.Invoke(this, EventArgs.Empty);

            // ── سحب النافذة ──
            EnableDrag(pnlBackground);
            EnableDrag(lblLogo);
            EnableDrag(lblAppTitle);
        }

        // ═══════════════════════════════════════
        //  Drag to Move Window
        // ═══════════════════════════════════════

        private void EnableDrag(Control control)
        {
            control.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left) { _isDragging = true; _dragStart = e.Location; }
            };
            control.MouseMove += (s, e) =>
            {
                if (_isDragging && this.FindForm() is Form form)
                {
                    form.Location = new Point(
                        form.Location.X + e.X - _dragStart.X,
                        form.Location.Y + e.Y - _dragStart.Y);
                }
            };
            control.MouseUp += (s, e) => _isDragging = false;
        }

        // ═══════════════════════════════════════
        //  IThemeable — تطبيق الثيم
        // ═══════════════════════════════════════

        public void ApplyTheme(ThemeColors t)
        {
            // Background gradient
            pnlBackground.FillColor = t.GradientPanelTitle1;
            pnlBackground.FillColor2 = t.GradientPanelTitle2;

            // Logo & Title
            lblLogo.ForeColor = t.Accent1;
            lblAppTitle.ForeColor = t.TextPrimary;

            // أزرار عادية — شفافة مع hover أخضر خفيف
            Color hoverBg = ThemeColorUtils.WithAlpha(t.Accent1, 30);
            Color pressedBg = ThemeColorUtils.WithAlpha(t.Accent1, 50);

            foreach (var btn in new[] { btnPin, btnSettings, btnTheme, btnLang, btnMinimize })
            {
                btn.FillColor = Color.Transparent;
                btn.ForeColor = t.TextSecondary;
                btn.HoverState.FillColor = hoverBg;
                btn.HoverState.ForeColor = t.TextPrimary;
                btn.PressedColor = pressedBg;
            }

            // زر الإغلاق — أحمر عند hover
            btnClose.FillColor = Color.Transparent;
            btnClose.ForeColor = t.TextSecondary;
            btnClose.HoverState.FillColor = t.Danger;
            btnClose.HoverState.ForeColor = Color.White;
            btnClose.PressedColor = ThemeColorUtils.Darken(t.Danger, 15);

            // تحديث أيقونة الثيم
            btnTheme.Text = clsThemeManager.IsDark ? "☀️" : "🌙";
        }

        // ═══════════════════════════════════════
        //  ILocalizable — تطبيق اللغة
        // ═══════════════════════════════════════

        public void ApplyLanguage(ILanguagePack lang)
        {
            lblAppTitle.Text = lang.LayoutAppTitle;
            btnLang.Text = clsLanguageManager.Code == "ar" ? "EN" : "AR";
        }
    }
}
