namespace UI.Controls.Feedback
{
    partial class ucMessageBox
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();

            pnlOverlay = new Panel();
            pnlDialog = new Guna.UI2.WinForms.Guna2Panel();
            pnlAccentTop = new Guna.UI2.WinForms.Guna2GradientPanel();
            lblIcon = new Label();
            lblTitle = new Label();
            lblMessage = new Label();
            pnlButtons = new Guna.UI2.WinForms.Guna2Panel();
            btnPrimary = new Guna.UI2.WinForms.Guna2GradientButton();
            btnSecondary = new Guna.UI2.WinForms.Guna2GradientButton();

            pnlOverlay.SuspendLayout();
            pnlDialog.SuspendLayout();
            pnlButtons.SuspendLayout();
            SuspendLayout();

            // 
            // pnlOverlay — خلفية شفافة داكنة
            // 
            pnlOverlay.BackColor = Color.FromArgb(160, 0, 0, 0);
            pnlOverlay.Controls.Add(pnlDialog);
            pnlOverlay.Dock = DockStyle.Fill;
            pnlOverlay.Location = new Point(0, 0);
            pnlOverlay.Name = "pnlOverlay";
            pnlOverlay.Size = new Size(700, 500);
            pnlOverlay.TabIndex = 0;

            // 
            // pnlDialog — الكارت الأساسي
            // 
            pnlDialog.Anchor = AnchorStyles.None;
            pnlDialog.BorderColor = Color.FromArgb(50, 27, 138, 107);
            pnlDialog.BorderRadius = 18;
            pnlDialog.BorderThickness = 1;
            pnlDialog.Controls.Add(pnlButtons);
            pnlDialog.Controls.Add(lblMessage);
            pnlDialog.Controls.Add(lblTitle);
            pnlDialog.Controls.Add(lblIcon);
            pnlDialog.Controls.Add(pnlAccentTop);
            pnlDialog.CustomizableEdges = customizableEdges1;
            pnlDialog.FillColor = Color.FromArgb(22, 27, 34);
            pnlDialog.Location = new Point(140, 140);
            pnlDialog.Name = "pnlDialog";
            pnlDialog.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pnlDialog.ShadowDecoration.Enabled = true;
            pnlDialog.ShadowDecoration.Color = Color.FromArgb(60, 27, 138, 107);
            pnlDialog.ShadowDecoration.Depth = 8;
            pnlDialog.Size = new Size(420, 220);
            pnlDialog.TabIndex = 0;

            // 
            // pnlAccentTop — شريط أخضر-ذهبي فوق الكارت
            // 
            pnlAccentTop.CustomizableEdges = customizableEdges3;
            pnlAccentTop.Dock = DockStyle.Top;
            pnlAccentTop.FillColor = Color.FromArgb(27, 138, 107);
            pnlAccentTop.FillColor2 = Color.FromArgb(200, 169, 110);
            pnlAccentTop.Location = new Point(0, 0);
            pnlAccentTop.Name = "pnlAccentTop";
            pnlAccentTop.ShadowDecoration.CustomizableEdges = customizableEdges4;
            pnlAccentTop.Size = new Size(420, 3);
            pnlAccentTop.TabIndex = 4;

            // 
            // lblIcon — ◉
            // 
            lblIcon.AutoSize = true;
            lblIcon.BackColor = Color.Transparent;
            lblIcon.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
            lblIcon.ForeColor = Color.FromArgb(27, 138, 107);
            lblIcon.Location = new Point(175, 16);
            lblIcon.Name = "lblIcon";
            lblIcon.Size = new Size(70, 65);
            lblIcon.TabIndex = 3;
            lblIcon.Text = "◉";

            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(230, 237, 243);
            lblTitle.Location = new Point(20, 75);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(380, 28);
            lblTitle.TabIndex = 2;
            lblTitle.Text = "تأكيد";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblMessage
            // 
            lblMessage.BackColor = Color.Transparent;
            lblMessage.Font = new Font("Segoe UI", 10F);
            lblMessage.ForeColor = Color.FromArgb(139, 148, 158);
            lblMessage.Location = new Point(20, 108);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(380, 40);
            lblMessage.TabIndex = 1;
            lblMessage.Text = "هل أنت متأكد؟";
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // pnlButtons — حاوية الأزرار
            // 
            pnlButtons.Controls.Add(btnSecondary);
            pnlButtons.Controls.Add(btnPrimary);
            pnlButtons.CustomizableEdges = customizableEdges5;
            pnlButtons.Dock = DockStyle.Bottom;
            pnlButtons.FillColor = Color.FromArgb(18, 22, 30);
            pnlButtons.Location = new Point(0, 160);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.ShadowDecoration.CustomizableEdges = customizableEdges6;
            pnlButtons.Size = new Size(420, 60);
            pnlButtons.TabIndex = 0;

            // 
            // btnSecondary — إلغاء
            // 
            btnSecondary.Animated = true;
            btnSecondary.BorderRadius = 12;
            btnSecondary.Cursor = Cursors.Hand;
            btnSecondary.CustomizableEdges = customizableEdges7;
            btnSecondary.FillColor = Color.FromArgb(28, 33, 40);
            btnSecondary.FillColor2 = Color.FromArgb(25, 30, 37);
            btnSecondary.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSecondary.ForeColor = Color.FromArgb(139, 148, 158);
            btnSecondary.HoverState.FillColor = Color.FromArgb(38, 43, 50);
            btnSecondary.HoverState.FillColor2 = Color.FromArgb(35, 40, 48);
            btnSecondary.HoverState.ForeColor = Color.FromArgb(230, 237, 243);
            btnSecondary.Location = new Point(19, 10);
            btnSecondary.Name = "btnSecondary";
            btnSecondary.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnSecondary.Size = new Size(140, 40);
            btnSecondary.TabIndex = 0;
            btnSecondary.Text = "إلغاء";

            // 
            // btnPrimary — تأكيد (Gradient أخضر-ذهبي)
            // 
            btnPrimary.Animated = true;
            btnPrimary.BorderRadius = 12;
            btnPrimary.Cursor = Cursors.Hand;
            btnPrimary.CustomizableEdges = customizableEdges9;
            btnPrimary.FillColor = Color.FromArgb(27, 138, 107);
            btnPrimary.FillColor2 = Color.FromArgb(200, 169, 110);
            btnPrimary.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnPrimary.ForeColor = Color.White;
            btnPrimary.HoverState.FillColor = Color.FromArgb(200, 169, 110);
            btnPrimary.HoverState.FillColor2 = Color.FromArgb(27, 138, 107);
            btnPrimary.Location = new Point(238, 10);
            btnPrimary.Name = "btnPrimary";
            btnPrimary.PressedColor = Color.FromArgb(17, 90, 70);
            btnPrimary.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnPrimary.Size = new Size(140, 40);
            btnPrimary.TabIndex = 1;
            btnPrimary.Text = "تأكيد";

            // 
            // ucMessageBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(pnlOverlay);
            Name = "ucMessageBox";
            Size = new Size(700, 500);

            pnlOverlay.ResumeLayout(false);
            pnlDialog.ResumeLayout(false);
            pnlDialog.PerformLayout();
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        // ── Designer Fields ──
        private Panel pnlOverlay;
        private Guna.UI2.WinForms.Guna2Panel pnlDialog;
        private Guna.UI2.WinForms.Guna2GradientPanel pnlAccentTop;
        private Label lblIcon;
        private Label lblTitle;
        private Label lblMessage;
        private Guna.UI2.WinForms.Guna2Panel pnlButtons;
        private Guna.UI2.WinForms.Guna2GradientButton btnPrimary;
        private Guna.UI2.WinForms.Guna2GradientButton btnSecondary;
    }
}
