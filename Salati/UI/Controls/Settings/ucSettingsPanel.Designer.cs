namespace UI.Controls.Settings
{
    partial class ucSettingsPanel
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pnlMain = new Guna.UI2.WinForms.Guna2Panel();
            pnlContent = new Panel();
            pnlFooter = new Panel();
            btnSave = new Guna.UI2.WinForms.Guna2GradientButton();
            pnlTabs = new Panel();
            btnTabPrayer = new Guna.UI2.WinForms.Guna2Button();
            btnTabAlerts = new Guna.UI2.WinForms.Guna2Button();
            btnTabAppearance = new Guna.UI2.WinForms.Guna2Button();
            btnTabGeneral = new Guna.UI2.WinForms.Guna2Button();
            pnlHeader = new Panel();
            lblTitle = new Label();
            btnClose = new Guna.UI2.WinForms.Guna2Button();
            pnlMain.SuspendLayout();
            pnlFooter.SuspendLayout();
            pnlTabs.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.BorderColor = Color.FromArgb(38, 27, 138, 107);
            pnlMain.BorderThickness = 1;
            pnlMain.Controls.Add(pnlContent);
            pnlMain.Controls.Add(pnlFooter);
            pnlMain.Controls.Add(pnlTabs);
            pnlMain.Controls.Add(pnlHeader);
            pnlMain.CustomizableEdges = customizableEdges13;
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.FillColor = Color.FromArgb(13, 17, 23);
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.ShadowDecoration.Color = Color.FromArgb(80, 0, 0, 0);
            pnlMain.ShadowDecoration.CustomizableEdges = customizableEdges14;
            pnlMain.ShadowDecoration.Depth = 10;
            pnlMain.ShadowDecoration.Enabled = true;
            pnlMain.Size = new Size(505, 498);
            pnlMain.TabIndex = 0;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.Transparent;
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(38, 42);
            pnlContent.MinimumSize = new Size(200, 100);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(4);
            pnlContent.Size = new Size(467, 406);
            pnlContent.TabIndex = 2;
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.FromArgb(10, 14, 20);
            pnlFooter.Controls.Add(btnSave);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(38, 448);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(467, 50);
            pnlFooter.TabIndex = 3;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Animated = true;
            btnSave.BorderRadius = 10;
            btnSave.Cursor = Cursors.Hand;
            btnSave.CustomizableEdges = customizableEdges1;
            btnSave.FillColor = Color.FromArgb(27, 138, 107);
            btnSave.FillColor2 = Color.FromArgb(200, 169, 110);
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.HoverState.FillColor = Color.FromArgb(200, 169, 110);
            btnSave.HoverState.FillColor2 = Color.FromArgb(27, 138, 107);
            btnSave.Location = new Point(317, 8);
            btnSave.Name = "btnSave";
            btnSave.PressedColor = Color.FromArgb(17, 90, 70);
            btnSave.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnSave.Size = new Size(138, 34);
            btnSave.TabIndex = 0;
            btnSave.Text = "💾 حفظ";
            // 
            // pnlTabs
            // 
            pnlTabs.BackColor = Color.FromArgb(10, 14, 20);
            pnlTabs.Controls.Add(btnTabPrayer);
            pnlTabs.Controls.Add(btnTabAlerts);
            pnlTabs.Controls.Add(btnTabAppearance);
            pnlTabs.Controls.Add(btnTabGeneral);
            pnlTabs.Dock = DockStyle.Left;
            pnlTabs.Location = new Point(0, 42);
            pnlTabs.Name = "pnlTabs";
            pnlTabs.Size = new Size(38, 456);
            pnlTabs.TabIndex = 1;
            // 
            // btnTabPrayer
            // 
            btnTabPrayer.BorderRadius = 6;
            btnTabPrayer.CustomizableEdges = customizableEdges3;
            btnTabPrayer.FillColor = Color.FromArgb(30, 27, 138, 107);
            btnTabPrayer.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnTabPrayer.ForeColor = Color.FromArgb(27, 138, 107);
            btnTabPrayer.Location = new Point(2, 6);
            btnTabPrayer.Name = "btnTabPrayer";
            btnTabPrayer.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnTabPrayer.Size = new Size(34, 34);
            btnTabPrayer.TabIndex = 0;
            btnTabPrayer.Text = "⏰";
            // 
            // btnTabAlerts
            // 
            btnTabAlerts.BorderRadius = 6;
            btnTabAlerts.CustomizableEdges = customizableEdges5;
            btnTabAlerts.FillColor = Color.Transparent;
            btnTabAlerts.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnTabAlerts.ForeColor = Color.FromArgb(139, 148, 158);
            btnTabAlerts.Location = new Point(2, 46);
            btnTabAlerts.Name = "btnTabAlerts";
            btnTabAlerts.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnTabAlerts.Size = new Size(34, 34);
            btnTabAlerts.TabIndex = 1;
            btnTabAlerts.Text = "♪";
            // 
            // btnTabAppearance
            // 
            btnTabAppearance.BorderRadius = 6;
            btnTabAppearance.CustomizableEdges = customizableEdges7;
            btnTabAppearance.FillColor = Color.Transparent;
            btnTabAppearance.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnTabAppearance.ForeColor = Color.FromArgb(139, 148, 158);
            btnTabAppearance.Location = new Point(2, 86);
            btnTabAppearance.Name = "btnTabAppearance";
            btnTabAppearance.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnTabAppearance.Size = new Size(34, 34);
            btnTabAppearance.TabIndex = 2;
            btnTabAppearance.Text = "◐";
            // 
            // btnTabGeneral
            // 
            btnTabGeneral.BorderRadius = 6;
            btnTabGeneral.CustomizableEdges = customizableEdges9;
            btnTabGeneral.FillColor = Color.Transparent;
            btnTabGeneral.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnTabGeneral.ForeColor = Color.FromArgb(139, 148, 158);
            btnTabGeneral.Location = new Point(2, 126);
            btnTabGeneral.Name = "btnTabGeneral";
            btnTabGeneral.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnTabGeneral.Size = new Size(34, 34);
            btnTabGeneral.TabIndex = 3;
            btnTabGeneral.Text = "⚙";
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(10, 14, 20);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(btnClose);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(505, 42);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(230, 237, 243);
            lblTitle.Location = new Point(12, 8);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(240, 26);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "⚙️ الإعدادات";
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Animated = true;
            btnClose.BorderRadius = 6;
            btnClose.CustomizableEdges = customizableEdges11;
            btnClose.FillColor = Color.Transparent;
            btnClose.Font = new Font("Segoe UI", 11F);
            btnClose.ForeColor = Color.FromArgb(139, 148, 158);
            btnClose.HoverState.FillColor = Color.FromArgb(224, 108, 117);
            btnClose.HoverState.ForeColor = Color.White;
            btnClose.Location = new Point(465, 6);
            btnClose.Name = "btnClose";
            btnClose.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnClose.Size = new Size(32, 32);
            btnClose.TabIndex = 1;
            btnClose.Text = "✕";
            // 
            // ucSettingsPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(pnlMain);
            Name = "ucSettingsPanel";
            Size = new Size(505, 498);
            pnlMain.ResumeLayout(false);
            pnlFooter.ResumeLayout(false);
            pnlTabs.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        // ── Designer Fields ──
        private Guna.UI2.WinForms.Guna2Panel pnlMain;
        private Panel pnlHeader;
        private Label lblTitle;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private Panel pnlTabs;
        private Guna.UI2.WinForms.Guna2Button btnTabPrayer;
        private Guna.UI2.WinForms.Guna2Button btnTabAlerts;
        private Guna.UI2.WinForms.Guna2Button btnTabAppearance;
        private Guna.UI2.WinForms.Guna2Button btnTabGeneral;
        private Panel pnlContent;
        private Panel pnlFooter;
        private Guna.UI2.WinForms.Guna2GradientButton btnSave;
    }
}
