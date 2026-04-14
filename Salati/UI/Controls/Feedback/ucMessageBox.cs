using UI.Core.Theme;
using UI.Core.Language;

namespace UI.Controls.Feedback
{
    /// <summary>
    /// بديل للـ MessageBox بشكل modal overlay.
    /// بيظهر كـ overlay فوق الفورم مع dark background + dialog card.
    /// 
    /// ── الاستخدام ──
    /// bool confirmed = await ucMessageBox.ShowConfirm(this, "حذف", "هل أنت متأكد؟");
    /// await ucMessageBox.ShowError(this, "خطأ", "فشل في الحفظ");
    /// await ucMessageBox.ShowSuccess(this, "نجاح", "تم الحفظ");
    /// </summary>
    public partial class ucMessageBox : UserControl
    {
        public enum enMsgType { Confirm, Success, Error, Warning, Info }

        private TaskCompletionSource<bool>? _resultSource;

        public ucMessageBox()
        {
            InitializeComponent();
            btnPrimary.Click += (s, e) => Close(true);
            btnSecondary.Click += (s, e) => Close(false);
        }

        // ════════════════════════════════════════════════
        //  Static Methods
        // ════════════════════════════════════════════════

        public static Task<bool> ShowConfirm(Form parent, string title, string message)
            => ShowDialog(parent, title, message, enMsgType.Confirm);

        public static Task<bool> ShowWarning(Form parent, string title, string message)
            => ShowDialog(parent, title, message, enMsgType.Warning);

        public static Task<bool> ShowSuccess(Form parent, string title, string message)
            => ShowDialog(parent, title, message, enMsgType.Success);

        public static Task<bool> ShowError(Form parent, string title, string message)
            => ShowDialog(parent, title, message, enMsgType.Error);

        public static Task<bool> ShowInfo(Form parent, string title, string message)
            => ShowDialog(parent, title, message, enMsgType.Info);

        // ════════════════════════════════════════════════
        //  Core Dialog Logic
        // ════════════════════════════════════════════════

        private static Task<bool> ShowDialog(Form parent, string title, string message, enMsgType type)
        {
            var msgBox = new ucMessageBox();
            msgBox.Configure(title, message, type);
            msgBox._resultSource = new TaskCompletionSource<bool>();

            msgBox.Dock = DockStyle.Fill;
            parent.Controls.Add(msgBox);
            msgBox.BringToFront();
            msgBox.Visible = true;
            msgBox.CenterDialog();

            return msgBox._resultSource.Task;
        }

        // ════════════════════════════════════════════════
        //  Configure — ضبط الشكل حسب النوع
        // ════════════════════════════════════════════════

        private void Configure(string title, string message, enMsgType type)
        {
            var t = clsThemeManager.Colors;
            var lang = clsLanguageManager.Current;

            lblTitle.Text = title;
            lblMessage.Text = message;

            // ── ألوان ──
            pnlDialog.FillColor = t.BgSurface;
            pnlButtons.FillColor = ThemeColorUtils.Darken(t.BgSurface, 5);
            lblTitle.ForeColor = t.TextPrimary;
            lblMessage.ForeColor = t.TextSecondary;

            switch (type)
            {
                case enMsgType.Confirm:
                    lblIcon.Text = "◉";
                    lblIcon.ForeColor = t.Accent1;
                    pnlAccentTop.FillColor = t.Accent1;
                    pnlAccentTop.FillColor2 = t.Accent2;
                    btnPrimary.Text = lang.AlertConfirm;
                    btnPrimary.FillColor = t.GradientBtn1;
                    btnPrimary.FillColor2 = t.GradientBtn2;
                    btnSecondary.Text = lang.AlertCancel;
                    btnSecondary.Visible = true;
                    break;

                case enMsgType.Success:
                    lblIcon.Text = "✓";
                    lblIcon.ForeColor = t.Success;
                    pnlAccentTop.FillColor = t.Success;
                    pnlAccentTop.FillColor2 = ThemeColorUtils.Lighten(t.Success, 20);
                    btnPrimary.Text = lang.AlertOk;
                    btnPrimary.FillColor = t.Success;
                    btnPrimary.FillColor2 = ThemeColorUtils.Lighten(t.Success, 15);
                    btnSecondary.Visible = false;
                    break;

                case enMsgType.Error:
                    lblIcon.Text = "✕";
                    lblIcon.ForeColor = t.Danger;
                    pnlAccentTop.FillColor = t.Danger;
                    pnlAccentTop.FillColor2 = ThemeColorUtils.Lighten(t.Danger, 20);
                    btnPrimary.Text = lang.AlertOk;
                    btnPrimary.FillColor = t.Danger;
                    btnPrimary.FillColor2 = ThemeColorUtils.Lighten(t.Danger, 15);
                    btnSecondary.Visible = false;
                    break;

                case enMsgType.Warning:
                    lblIcon.Text = "⚠";
                    lblIcon.ForeColor = t.Warning;
                    pnlAccentTop.FillColor = t.Warning;
                    pnlAccentTop.FillColor2 = ThemeColorUtils.Lighten(t.Warning, 20);
                    btnPrimary.Text = lang.AlertYes;
                    btnSecondary.Text = lang.AlertNo;
                    btnSecondary.Visible = true;
                    break;

                case enMsgType.Info:
                    lblIcon.Text = "ℹ";
                    lblIcon.ForeColor = t.Info;
                    pnlAccentTop.FillColor = t.Info;
                    pnlAccentTop.FillColor2 = ThemeColorUtils.Lighten(t.Info, 20);
                    btnPrimary.Text = lang.AlertOk;
                    btnSecondary.Visible = false;
                    break;
            }

            // ── Center buttons ──
            int dialogWidth = 420;
            int totalButtons = btnSecondary.Visible ? 2 : 1;

            if (totalButtons == 2)
            {
                int totalWidth = 140 + 10 + 140;
                int startX = (dialogWidth - totalWidth) / 2;
                btnSecondary.Location = new Point(startX, 10);
                btnPrimary.Location = new Point(startX + 150, 10);
            }
            else
            {
                btnPrimary.Location = new Point((dialogWidth - 140) / 2, 10);
            }

            pnlDialog.BorderColor = ThemeColorUtils.WithAlpha(lblIcon.ForeColor, 50);
        }

        // ════════════════════════════════════════════════
        //  Helpers
        // ════════════════════════════════════════════════

        private void CenterDialog()
        {
            pnlDialog.Location = new Point(
                (pnlOverlay.Width - pnlDialog.Width) / 2,
                (pnlOverlay.Height - pnlDialog.Height) / 2);
        }

        private void Close(bool result)
        {
            this.Visible = false;
            this.Parent?.Controls.Remove(this);
            _resultSource?.TrySetResult(result);
            this.Dispose();
        }
    }
}
