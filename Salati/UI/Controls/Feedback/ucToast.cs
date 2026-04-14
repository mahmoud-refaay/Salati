using UI.Core.Theme;
using UI.Core.Language;

namespace UI.Controls.Feedback
{
    /// <summary>
    /// Toast notification — إشعار سريع يظهر من الأسفل ويختفي تلقائياً.
    /// 
    /// ── الاستخدام ──
    /// ucToast.ShowSuccess(parentForm, "تم الحفظ بنجاح");
    /// ucToast.ShowError(parentForm, "حدث خطأ");
    /// ucToast.ShowWarning(parentForm, "تحذير");
    /// ucToast.ShowInfo(parentForm, "معلومة");
    /// </summary>
    public partial class ucToast : UserControl
    {
        public enum enToastType { Success, Error, Warning, Info }

        private System.Windows.Forms.Timer? _autoCloseTimer;
        private System.Windows.Forms.Timer? _slideTimer;
        private int _targetY;
        private const int SLIDE_SPEED = 8;
        private const int DEFAULT_DURATION_MS = 3500;

        public ucToast()
        {
            InitializeComponent();
            btnClose.Click += (s, e) => SlideOut();
        }

        // ════════════════════════════════════════════════
        //  Static Factory Methods
        // ════════════════════════════════════════════════

        public static void ShowSuccess(Form parent, string message, string title = "")
        {
            if (string.IsNullOrEmpty(title)) title = clsLanguageManager.Current.CommonSuccess;
            Show(parent, title, message, enToastType.Success);
        }

        public static void ShowError(Form parent, string message, string title = "")
        {
            if (string.IsNullOrEmpty(title)) title = clsLanguageManager.Current.CommonError;
            Show(parent, title, message, enToastType.Error);
        }

        public static void ShowWarning(Form parent, string message, string title = "")
        {
            if (string.IsNullOrEmpty(title)) title = clsLanguageManager.Current.CommonWarning;
            Show(parent, title, message, enToastType.Warning);
        }

        public static void ShowInfo(Form parent, string message, string title = "")
        {
            if (string.IsNullOrEmpty(title)) title = clsLanguageManager.Current.CommonInfo;
            Show(parent, title, message, enToastType.Info);
        }

        // ════════════════════════════════════════════════
        //  Core Show Logic
        // ════════════════════════════════════════════════

        private static void Show(Form parent, string title, string message, enToastType type,
            int durationMs = DEFAULT_DURATION_MS)
        {
            var toast = new ucToast();
            toast.Configure(title, message, type);

            // البداية خارج النافذة — أسفل
            toast.Location = new Point(
                parent.ClientSize.Width - toast.Width - 20,
                parent.ClientSize.Height);
            toast._targetY = parent.ClientSize.Height - toast.Height - 20;

            parent.Controls.Add(toast);
            toast.BringToFront();
            toast.SlideIn(durationMs);
        }

        // ════════════════════════════════════════════════
        //  Configure — ضبط الشكل حسب النوع
        // ════════════════════════════════════════════════

        private void Configure(string title, string message, enToastType type)
        {
            var t = clsThemeManager.Colors;

            lblTitle.Text = title;
            lblMessage.Text = message;

            // ── الخلفية والحدود ──
            pnlToastBg.FillColor = t.BgSurface;

            switch (type)
            {
                case enToastType.Success:
                    lblIcon.Text = "✓";
                    lblIcon.ForeColor = t.Success;
                    pnlAccentStrip.FillColor = t.Success;
                    pnlToastBg.BorderColor = ThemeColorUtils.WithAlpha(t.Success, 50);
                    break;

                case enToastType.Error:
                    lblIcon.Text = "✕";
                    lblIcon.ForeColor = t.Danger;
                    pnlAccentStrip.FillColor = t.Danger;
                    pnlToastBg.BorderColor = ThemeColorUtils.WithAlpha(t.Danger, 50);
                    break;

                case enToastType.Warning:
                    lblIcon.Text = "⚠";
                    lblIcon.ForeColor = t.Warning;
                    pnlAccentStrip.FillColor = t.Warning;
                    pnlToastBg.BorderColor = ThemeColorUtils.WithAlpha(t.Warning, 50);
                    break;

                case enToastType.Info:
                    lblIcon.Text = "ℹ";
                    lblIcon.ForeColor = t.Info;
                    pnlAccentStrip.FillColor = t.Info;
                    pnlToastBg.BorderColor = ThemeColorUtils.WithAlpha(t.Info, 50);
                    break;
            }

            // ── ألوان النص ──
            lblTitle.ForeColor = t.TextPrimary;
            lblMessage.ForeColor = t.TextSecondary;

            // ── Auto-Resize ──
            AutoResizeToast();
        }

        /// <summary>يكبّر الـ Toast لو الرسالة طويلة</summary>
        private void AutoResizeToast()
        {
            const int MIN_WIDTH = 370;
            const int MAX_WIDTH = 520;
            const int TEXT_LEFT = 60;
            const int RIGHT_PAD = 50;
            const int MIN_HEIGHT = 68;

            int availableTextWidth = MAX_WIDTH - TEXT_LEFT - RIGHT_PAD;
            lblMessage.MaximumSize = new Size(availableTextWidth, 0);
            lblMessage.AutoSize = true;

            Size textSize = TextRenderer.MeasureText(
                lblMessage.Text, lblMessage.Font,
                new Size(availableTextWidth, int.MaxValue),
                TextFormatFlags.WordBreak);

            int neededWidth = TEXT_LEFT + textSize.Width + RIGHT_PAD;
            int finalWidth = Math.Max(MIN_WIDTH, Math.Min(neededWidth, MAX_WIDTH));
            int finalHeight = Math.Max(MIN_HEIGHT, lblMessage.Top + textSize.Height + 16);

            this.Size = new Size(finalWidth, finalHeight);
            pnlToastBg.Size = this.Size;
            btnClose.Location = new Point(finalWidth - btnClose.Width - 8, 8);
            pnlAccentStrip.Height = finalHeight - 18;
        }

        // ════════════════════════════════════════════════
        //  Slide Animation
        // ════════════════════════════════════════════════

        private void SlideIn(int autoCloseDuration)
        {
            _slideTimer = new System.Windows.Forms.Timer { Interval = 12 };
            _slideTimer.Tick += (s, e) =>
            {
                if (this.Top > _targetY)
                {
                    this.Top -= SLIDE_SPEED;
                    if (this.Top <= _targetY)
                    {
                        this.Top = _targetY;
                        _slideTimer!.Stop();
                        _slideTimer.Dispose();
                        StartAutoClose(autoCloseDuration);
                    }
                }
            };
            _slideTimer.Start();
        }

        private void SlideOut()
        {
            _autoCloseTimer?.Stop();
            _autoCloseTimer?.Dispose();

            _slideTimer = new System.Windows.Forms.Timer { Interval = 12 };
            _slideTimer.Tick += (s, e) =>
            {
                if (this.Parent != null && this.Top < this.Parent.ClientSize.Height)
                {
                    this.Top += SLIDE_SPEED;
                }
                else
                {
                    _slideTimer!.Stop();
                    _slideTimer.Dispose();
                    this.Parent?.Controls.Remove(this);
                    this.Dispose();
                }
            };
            _slideTimer.Start();
        }

        private void StartAutoClose(int durationMs)
        {
            _autoCloseTimer = new System.Windows.Forms.Timer { Interval = durationMs };
            _autoCloseTimer.Tick += (s, e) =>
            {
                _autoCloseTimer.Stop();
                SlideOut();
            };
            _autoCloseTimer.Start();
        }
    }
}
