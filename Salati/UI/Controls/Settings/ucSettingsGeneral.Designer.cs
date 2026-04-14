namespace UI.Controls.Settings
{
    partial class ucSettingsGeneral
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

            lblSectionTitle = new Label();
            lblStartup = new Label();
            togStartWithWindows = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            lblMinOnStart = new Label();
            togMinimizeOnStart = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            lblTray = new Label();
            togShowInTray = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            lblCloseToTray = new Label();
            togCloseToTray = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            pnlSeparator = new Panel();
            btnResetDefaults = new Guna.UI2.WinForms.Guna2Button();
            btnOpenFolder = new Guna.UI2.WinForms.Guna2Button();
            pnlAbout = new Panel();
            lblVersion = new Label();
            lblCredits = new Label();

            pnlAbout.SuspendLayout();
            SuspendLayout();

            // 
            // lblSectionTitle — ⚙️ عام
            // 
            lblSectionTitle.BackColor = Color.Transparent;
            lblSectionTitle.Dock = DockStyle.Top;
            lblSectionTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblSectionTitle.ForeColor = Color.FromArgb(230, 237, 243);
            lblSectionTitle.Location = new Point(0, 0);
            lblSectionTitle.Name = "lblSectionTitle";
            lblSectionTitle.Padding = new Padding(4, 0, 0, 0);
            lblSectionTitle.Size = new Size(310, 32);
            lblSectionTitle.TabIndex = 0;
            lblSectionTitle.Text = "⚙️ عام";
            lblSectionTitle.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // lblStartup — تشغيل مع بداية الويندوز
            // 
            lblStartup.AutoSize = false;
            lblStartup.BackColor = Color.Transparent;
            lblStartup.Font = new Font("Segoe UI", 9F);
            lblStartup.ForeColor = Color.FromArgb(230, 237, 243);
            lblStartup.Location = new Point(12, 40);
            lblStartup.Name = "lblStartup";
            lblStartup.Size = new Size(230, 22);
            lblStartup.TabIndex = 1;
            lblStartup.Text = "تشغيل مع بداية الويندوز";
            lblStartup.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // togStartWithWindows
            // 
            togStartWithWindows.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            togStartWithWindows.CheckedState.BorderColor = Color.FromArgb(27, 138, 107);
            togStartWithWindows.CheckedState.FillColor = Color.FromArgb(27, 138, 107);
            togStartWithWindows.CheckedState.InnerColor = Color.White;
            togStartWithWindows.UncheckedState.BorderColor = Color.FromArgb(72, 79, 88);
            togStartWithWindows.UncheckedState.FillColor = Color.FromArgb(40, 44, 52);
            togStartWithWindows.UncheckedState.InnerColor = Color.FromArgb(139, 148, 158);
            togStartWithWindows.Location = new Point(256, 47);
            togStartWithWindows.Name = "togStartWithWindows";
            togStartWithWindows.Size = new Size(40, 22);
            togStartWithWindows.TabIndex = 2;

            // 
            // lblMinOnStart — تصغير عند البدء
            // 
            lblMinOnStart.AutoSize = false;
            lblMinOnStart.BackColor = Color.Transparent;
            lblMinOnStart.Font = new Font("Segoe UI", 9F);
            lblMinOnStart.ForeColor = Color.FromArgb(230, 237, 243);
            lblMinOnStart.Location = new Point(12, 78);
            lblMinOnStart.Name = "lblMinOnStart";
            lblMinOnStart.Size = new Size(230, 22);
            lblMinOnStart.TabIndex = 3;
            lblMinOnStart.Text = "تصغير عند البدء";
            lblMinOnStart.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // togMinimizeOnStart
            // 
            togMinimizeOnStart.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            togMinimizeOnStart.CheckedState.BorderColor = Color.FromArgb(27, 138, 107);
            togMinimizeOnStart.CheckedState.FillColor = Color.FromArgb(27, 138, 107);
            togMinimizeOnStart.CheckedState.InnerColor = Color.White;
            togMinimizeOnStart.UncheckedState.BorderColor = Color.FromArgb(72, 79, 88);
            togMinimizeOnStart.UncheckedState.FillColor = Color.FromArgb(40, 44, 52);
            togMinimizeOnStart.UncheckedState.InnerColor = Color.FromArgb(139, 148, 158);
            togMinimizeOnStart.Location = new Point(256, 85);
            togMinimizeOnStart.Name = "togMinimizeOnStart";
            togMinimizeOnStart.Size = new Size(40, 22);
            togMinimizeOnStart.TabIndex = 4;

            // 
            // lblTray — إظهار في System Tray
            // 
            lblTray.AutoSize = false;
            lblTray.BackColor = Color.Transparent;
            lblTray.Font = new Font("Segoe UI", 9F);
            lblTray.ForeColor = Color.FromArgb(230, 237, 243);
            lblTray.Location = new Point(12, 116);
            lblTray.Name = "lblTray";
            lblTray.Size = new Size(230, 22);
            lblTray.TabIndex = 5;
            lblTray.Text = "إظهار في System Tray";
            lblTray.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // togShowInTray
            // 
            togShowInTray.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            togShowInTray.Checked = true;
            togShowInTray.CheckedState.BorderColor = Color.FromArgb(27, 138, 107);
            togShowInTray.CheckedState.FillColor = Color.FromArgb(27, 138, 107);
            togShowInTray.CheckedState.InnerColor = Color.White;
            togShowInTray.UncheckedState.BorderColor = Color.FromArgb(72, 79, 88);
            togShowInTray.UncheckedState.FillColor = Color.FromArgb(40, 44, 52);
            togShowInTray.UncheckedState.InnerColor = Color.FromArgb(139, 148, 158);
            togShowInTray.Location = new Point(256, 123);
            togShowInTray.Name = "togShowInTray";
            togShowInTray.Size = new Size(40, 22);
            togShowInTray.TabIndex = 6;

            // 
            // lblCloseToTray — تصغير لـ Tray عند الإغلاق
            // 
            lblCloseToTray.AutoSize = false;
            lblCloseToTray.BackColor = Color.Transparent;
            lblCloseToTray.Font = new Font("Segoe UI", 9F);
            lblCloseToTray.ForeColor = Color.FromArgb(230, 237, 243);
            lblCloseToTray.Location = new Point(12, 154);
            lblCloseToTray.Name = "lblCloseToTray";
            lblCloseToTray.Size = new Size(230, 22);
            lblCloseToTray.TabIndex = 7;
            lblCloseToTray.Text = "تصغير لـ Tray عند الإغلاق";
            lblCloseToTray.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // togCloseToTray
            // 
            togCloseToTray.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            togCloseToTray.Checked = true;
            togCloseToTray.CheckedState.BorderColor = Color.FromArgb(27, 138, 107);
            togCloseToTray.CheckedState.FillColor = Color.FromArgb(27, 138, 107);
            togCloseToTray.CheckedState.InnerColor = Color.White;
            togCloseToTray.UncheckedState.BorderColor = Color.FromArgb(72, 79, 88);
            togCloseToTray.UncheckedState.FillColor = Color.FromArgb(40, 44, 52);
            togCloseToTray.UncheckedState.InnerColor = Color.FromArgb(139, 148, 158);
            togCloseToTray.Location = new Point(256, 161);
            togCloseToTray.Name = "togCloseToTray";
            togCloseToTray.Size = new Size(40, 22);
            togCloseToTray.TabIndex = 8;

            // 
            // pnlSeparator — خط فاصل
            // 
            pnlSeparator.BackColor = Color.FromArgb(30, 139, 148, 158);
            pnlSeparator.Location = new Point(12, 200);
            pnlSeparator.Name = "pnlSeparator";
            pnlSeparator.Size = new Size(286, 1);
            pnlSeparator.TabIndex = 9;

            // 
            // btnResetDefaults — 🔄 إعادة ضبط
            // 
            btnResetDefaults.BackColor = Color.Transparent;
            btnResetDefaults.BorderColor = Color.FromArgb(72, 79, 88);
            btnResetDefaults.BorderRadius = 8;
            btnResetDefaults.BorderThickness = 1;
            btnResetDefaults.CustomizableEdges = customizableEdges1;
            btnResetDefaults.FillColor = Color.Transparent;
            btnResetDefaults.Font = new Font("Segoe UI", 8.5F);
            btnResetDefaults.ForeColor = Color.FromArgb(139, 148, 158);
            btnResetDefaults.Location = new Point(12, 216);
            btnResetDefaults.Name = "btnResetDefaults";
            btnResetDefaults.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnResetDefaults.Size = new Size(140, 34);
            btnResetDefaults.TabIndex = 10;
            btnResetDefaults.Text = "🔄 إعادة ضبط";

            // 
            // btnOpenFolder — 📂 فتح المجلد
            // 
            btnOpenFolder.BackColor = Color.Transparent;
            btnOpenFolder.BorderColor = Color.FromArgb(72, 79, 88);
            btnOpenFolder.BorderRadius = 8;
            btnOpenFolder.BorderThickness = 1;
            btnOpenFolder.CustomizableEdges = customizableEdges3;
            btnOpenFolder.FillColor = Color.Transparent;
            btnOpenFolder.Font = new Font("Segoe UI", 8.5F);
            btnOpenFolder.ForeColor = Color.FromArgb(139, 148, 158);
            btnOpenFolder.Location = new Point(160, 216);
            btnOpenFolder.Name = "btnOpenFolder";
            btnOpenFolder.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnOpenFolder.Size = new Size(140, 34);
            btnOpenFolder.TabIndex = 11;
            btnOpenFolder.Text = "📂 فتح المجلد";

            // 
            // pnlAbout — معلومات الإصدار
            // 
            pnlAbout.BackColor = Color.Transparent;
            pnlAbout.Controls.Add(lblVersion);
            pnlAbout.Controls.Add(lblCredits);
            pnlAbout.Location = new Point(0, 266);
            pnlAbout.Name = "pnlAbout";
            pnlAbout.Size = new Size(310, 60);
            pnlAbout.TabIndex = 12;

            // 
            // lblVersion — v1.0.0
            // 
            lblVersion.BackColor = Color.Transparent;
            lblVersion.Font = new Font("Segoe UI", 8F);
            lblVersion.ForeColor = Color.FromArgb(72, 79, 88);
            lblVersion.Location = new Point(12, 8);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(286, 16);
            lblVersion.TabIndex = 0;
            lblVersion.Text = "الإصدار 1.0.0";
            lblVersion.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblCredits — صنع بـ ❤️
            // 
            lblCredits.BackColor = Color.Transparent;
            lblCredits.Font = new Font("Segoe UI", 8F);
            lblCredits.ForeColor = Color.FromArgb(72, 79, 88);
            lblCredits.Location = new Point(12, 28);
            lblCredits.Name = "lblCredits";
            lblCredits.Size = new Size(286, 16);
            lblCredits.TabIndex = 1;
            lblCredits.Text = "صنع بـ ❤️ بواسطة فريق Salati";
            lblCredits.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // ucSettingsGeneral
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.Transparent;
            Controls.Add(pnlAbout);
            Controls.Add(btnOpenFolder);
            Controls.Add(btnResetDefaults);
            Controls.Add(pnlSeparator);
            Controls.Add(togCloseToTray);
            Controls.Add(lblCloseToTray);
            Controls.Add(togShowInTray);
            Controls.Add(lblTray);
            Controls.Add(togMinimizeOnStart);
            Controls.Add(lblMinOnStart);
            Controls.Add(togStartWithWindows);
            Controls.Add(lblStartup);
            Controls.Add(lblSectionTitle);
            Name = "ucSettingsGeneral";
            Size = new Size(310, 340);

            pnlAbout.ResumeLayout(false);
            ResumeLayout(false);
        }

        // ── Designer Fields ──
        private Label lblSectionTitle;
        private Label lblStartup;
        private Guna.UI2.WinForms.Guna2ToggleSwitch togStartWithWindows;
        private Label lblMinOnStart;
        private Guna.UI2.WinForms.Guna2ToggleSwitch togMinimizeOnStart;
        private Label lblTray;
        private Guna.UI2.WinForms.Guna2ToggleSwitch togShowInTray;
        private Label lblCloseToTray;
        private Guna.UI2.WinForms.Guna2ToggleSwitch togCloseToTray;
        private Panel pnlSeparator;
        private Guna.UI2.WinForms.Guna2Button btnResetDefaults;
        private Guna.UI2.WinForms.Guna2Button btnOpenFolder;
        private Panel pnlAbout;
        private Label lblVersion;
        private Label lblCredits;
    }
}
