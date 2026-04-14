namespace UI.Controls.Feedback
{
    partial class ucToast
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

            pnlToastBg = new Guna.UI2.WinForms.Guna2Panel();
            pnlAccentStrip = new Guna.UI2.WinForms.Guna2Panel();
            lblIcon = new Label();
            lblTitle = new Label();
            lblMessage = new Label();
            btnClose = new Guna.UI2.WinForms.Guna2Button();

            pnlToastBg.SuspendLayout();
            SuspendLayout();

            // 
            // pnlToastBg
            // 
            pnlToastBg.BackColor = Color.FromArgb(22, 27, 34);
            pnlToastBg.BorderColor = Color.FromArgb(50, 27, 138, 107);
            pnlToastBg.BorderRadius = 14;
            pnlToastBg.BorderThickness = 1;
            pnlToastBg.Controls.Add(pnlAccentStrip);
            pnlToastBg.Controls.Add(lblIcon);
            pnlToastBg.Controls.Add(lblTitle);
            pnlToastBg.Controls.Add(lblMessage);
            pnlToastBg.Controls.Add(btnClose);
            pnlToastBg.CustomizableEdges = customizableEdges1;
            pnlToastBg.Dock = DockStyle.Fill;
            pnlToastBg.FillColor = Color.FromArgb(22, 27, 34);
            pnlToastBg.Location = new Point(0, 0);
            pnlToastBg.Name = "pnlToastBg";
            pnlToastBg.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pnlToastBg.ShadowDecoration.Enabled = true;
            pnlToastBg.ShadowDecoration.Color = Color.FromArgb(60, 27, 138, 107);
            pnlToastBg.ShadowDecoration.Depth = 4;
            pnlToastBg.Size = new Size(370, 68);
            pnlToastBg.TabIndex = 0;

            // 
            // pnlAccentStrip — شريط لوني جانبي
            // 
            pnlAccentStrip.BorderRadius = 2;
            pnlAccentStrip.CustomizableEdges = customizableEdges3;
            pnlAccentStrip.FillColor = Color.FromArgb(27, 138, 107);
            pnlAccentStrip.Location = new Point(8, 10);
            pnlAccentStrip.Name = "pnlAccentStrip";
            pnlAccentStrip.ShadowDecoration.CustomizableEdges = customizableEdges4;
            pnlAccentStrip.Size = new Size(4, 50);
            pnlAccentStrip.TabIndex = 0;

            // 
            // lblIcon — ✓
            // 
            lblIcon.AutoSize = true;
            lblIcon.BackColor = Color.Transparent;
            lblIcon.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblIcon.ForeColor = Color.FromArgb(27, 138, 107);
            lblIcon.Location = new Point(18, 14);
            lblIcon.Name = "lblIcon";
            lblIcon.Size = new Size(33, 32);
            lblIcon.TabIndex = 1;
            lblIcon.Text = "✓";

            // 
            // lblTitle — Success
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(230, 237, 243);
            lblTitle.Location = new Point(57, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(62, 20);
            lblTitle.TabIndex = 2;
            lblTitle.Text = "Success";

            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.BackColor = Color.Transparent;
            lblMessage.Font = new Font("Segoe UI", 9F);
            lblMessage.ForeColor = Color.FromArgb(139, 148, 158);
            lblMessage.Location = new Point(57, 36);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(189, 15);
            lblMessage.TabIndex = 3;
            lblMessage.Text = "Operation completed successfully.";

            // 
            // btnClose — ✕
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Animated = true;
            btnClose.BorderRadius = 8;
            btnClose.CustomizableEdges = customizableEdges5;
            btnClose.FillColor = Color.Transparent;
            btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClose.ForeColor = Color.FromArgb(100, 139, 148, 158);
            btnClose.HoverState.FillColor = Color.FromArgb(30, 224, 108, 117);
            btnClose.HoverState.ForeColor = Color.FromArgb(224, 108, 117);
            btnClose.Location = new Point(334, 8);
            btnClose.Name = "btnClose";
            btnClose.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnClose.Size = new Size(28, 28);
            btnClose.TabIndex = 4;
            btnClose.Text = "✕";

            // 
            // ucToast
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(pnlToastBg);
            Name = "ucToast";
            Size = new Size(370, 68);

            pnlToastBg.ResumeLayout(false);
            pnlToastBg.PerformLayout();
            ResumeLayout(false);
        }

        // ── Designer Fields ──
        private Guna.UI2.WinForms.Guna2Panel pnlToastBg;
        private Guna.UI2.WinForms.Guna2Panel pnlAccentStrip;
        private Label lblIcon;
        private Label lblTitle;
        private Label lblMessage;
        private Guna.UI2.WinForms.Guna2Button btnClose;
    }
}
