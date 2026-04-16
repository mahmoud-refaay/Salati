namespace UI.Controls.Layout
{
    partial class ucTitleBar
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
            pnlBackground = new Guna.UI2.WinForms.Guna2GradientPanel();
            lblLogo = new Label();
            lblAppTitle = new Label();
            btnPin = new Guna.UI2.WinForms.Guna2Button();
            btnTracking = new Guna.UI2.WinForms.Guna2Button();
            btnSettings = new Guna.UI2.WinForms.Guna2Button();
            btnAdhkar = new Guna.UI2.WinForms.Guna2Button();
            btnTheme = new Guna.UI2.WinForms.Guna2Button();
            btnLang = new Guna.UI2.WinForms.Guna2Button();
            btnMinimize = new Guna.UI2.WinForms.Guna2Button();
            btnClose = new Guna.UI2.WinForms.Guna2Button();
            pnlBackground.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBackground
            // 
            pnlBackground.Controls.Add(lblLogo);
            pnlBackground.Controls.Add(lblAppTitle);
            pnlBackground.Controls.Add(btnPin);
            pnlBackground.Controls.Add(btnTracking);
            pnlBackground.Controls.Add(btnAdhkar);
            pnlBackground.Controls.Add(btnSettings);
            pnlBackground.Controls.Add(btnTheme);
            pnlBackground.Controls.Add(btnLang);
            pnlBackground.Controls.Add(btnMinimize);
            pnlBackground.Controls.Add(btnClose);
            pnlBackground.CustomizableEdges = customizableEdges13;
            pnlBackground.Dock = DockStyle.Fill;
            pnlBackground.FillColor = Color.FromArgb(10, 14, 20);
            pnlBackground.FillColor2 = Color.FromArgb(13, 17, 23);
            pnlBackground.Location = new Point(0, 0);
            pnlBackground.Name = "pnlBackground";
            pnlBackground.ShadowDecoration.CustomizableEdges = customizableEdges14;
            pnlBackground.Size = new Size(700, 45);
            pnlBackground.TabIndex = 0;
            // 
            // lblLogo
            // 
            lblLogo.AutoSize = true;
            lblLogo.BackColor = Color.Transparent;
            lblLogo.Cursor = Cursors.SizeAll;
            lblLogo.Font = new Font("Segoe UI Emoji", 16F);
            lblLogo.ForeColor = Color.FromArgb(27, 138, 107);
            lblLogo.Location = new Point(12, 7);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(43, 30);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "🕌";
            // 
            // lblAppTitle
            // 
            lblAppTitle.AutoSize = true;
            lblAppTitle.BackColor = Color.Transparent;
            lblAppTitle.Cursor = Cursors.SizeAll;
            lblAppTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblAppTitle.ForeColor = Color.FromArgb(230, 237, 243);
            lblAppTitle.Location = new Point(50, 11);
            lblAppTitle.Name = "lblAppTitle";
            lblAppTitle.Size = new Size(62, 25);
            lblAppTitle.TabIndex = 1;
            lblAppTitle.Text = "صلاتي";
            // 
            // btnPin
            // 
            btnPin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnPin.Animated = true;
            btnPin.BackColor = Color.Transparent;
            btnPin.BorderRadius = 6;
            btnPin.CustomizableEdges = customizableEdges1;
            btnPin.FillColor = Color.Transparent;
            btnPin.Font = new Font("Segoe UI Emoji", 11F);
            btnPin.ForeColor = Color.FromArgb(139, 148, 158);
            btnPin.HoverState.FillColor = Color.FromArgb(30, 27, 138, 107);
            btnPin.HoverState.ForeColor = Color.FromArgb(230, 237, 243);
            btnPin.Location = new Point(474, 7);
            btnPin.Name = "btnPin";
            btnPin.PressedColor = Color.FromArgb(50, 27, 138, 107);
            btnPin.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnPin.Size = new Size(32, 32);
            btnPin.TabIndex = 2;
            btnPin.Text = "📌";
            btnPin.UseTransparentBackground = true;
            // 
            // btnAdhkar — 📿 فتح لوحة الأذكار
            // 
            btnAdhkar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAdhkar.Animated = true;
            btnAdhkar.BackColor = Color.Transparent;
            btnAdhkar.BorderRadius = 6;
            btnAdhkar.CustomizableEdges = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            btnAdhkar.FillColor = Color.Transparent;
            btnAdhkar.Font = new Font("Segoe UI Emoji", 11F);
            btnAdhkar.ForeColor = Color.FromArgb(139, 148, 158);
            btnAdhkar.HoverState.FillColor = Color.FromArgb(30, 27, 138, 107);
            btnAdhkar.HoverState.ForeColor = Color.FromArgb(230, 237, 243);
            btnAdhkar.Location = new Point(402, 7);
            btnAdhkar.Name = "btnAdhkar";
            btnAdhkar.PressedColor = Color.FromArgb(50, 27, 138, 107);
            btnAdhkar.ShadowDecoration.CustomizableEdges = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            btnAdhkar.Size = new Size(32, 32);
            btnAdhkar.TabIndex = 9;
            btnAdhkar.Text = "📿";
            btnAdhkar.UseTransparentBackground = true;
            // 
            // btnTracking — 📊 فتح لوحة التتبع
            // 
            btnTracking.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTracking.Animated = true;
            btnTracking.BackColor = Color.Transparent;
            btnTracking.BorderRadius = 6;
            btnTracking.CustomizableEdges = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            btnTracking.FillColor = Color.Transparent;
            btnTracking.Font = new Font("Segoe UI Emoji", 11F);
            btnTracking.ForeColor = Color.FromArgb(139, 148, 158);
            btnTracking.HoverState.FillColor = Color.FromArgb(30, 27, 138, 107);
            btnTracking.HoverState.ForeColor = Color.FromArgb(230, 237, 243);
            btnTracking.Location = new Point(438, 7);
            btnTracking.Name = "btnTracking";
            btnTracking.PressedColor = Color.FromArgb(50, 27, 138, 107);
            btnTracking.ShadowDecoration.CustomizableEdges = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            btnTracking.Size = new Size(32, 32);
            btnTracking.TabIndex = 8;
            btnTracking.Text = "📊";
            btnTracking.UseTransparentBackground = true;
            // 
            // btnSettings
            // 
            btnSettings.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSettings.Animated = true;
            btnSettings.BackColor = Color.Transparent;
            btnSettings.BorderRadius = 6;
            btnSettings.CustomizableEdges = customizableEdges3;
            btnSettings.FillColor = Color.Transparent;
            btnSettings.Font = new Font("Segoe UI Emoji", 11F);
            btnSettings.ForeColor = Color.FromArgb(139, 148, 158);
            btnSettings.HoverState.FillColor = Color.FromArgb(30, 27, 138, 107);
            btnSettings.HoverState.ForeColor = Color.FromArgb(230, 237, 243);
            btnSettings.Location = new Point(518, 7);
            btnSettings.Name = "btnSettings";
            btnSettings.PressedColor = Color.FromArgb(50, 27, 138, 107);
            btnSettings.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnSettings.Size = new Size(32, 32);
            btnSettings.TabIndex = 3;
            btnSettings.Text = "⚙️";
            btnSettings.UseTransparentBackground = true;
            // 
            // btnTheme
            // 
            btnTheme.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTheme.Animated = true;
            btnTheme.BackColor = Color.Transparent;
            btnTheme.BorderRadius = 6;
            btnTheme.CustomizableEdges = customizableEdges5;
            btnTheme.FillColor = Color.Transparent;
            btnTheme.Font = new Font("Segoe UI Emoji", 11F);
            btnTheme.ForeColor = Color.FromArgb(139, 148, 158);
            btnTheme.HoverState.FillColor = Color.FromArgb(30, 27, 138, 107);
            btnTheme.HoverState.ForeColor = Color.FromArgb(230, 237, 243);
            btnTheme.Location = new Point(554, 7);
            btnTheme.Name = "btnTheme";
            btnTheme.PressedColor = Color.FromArgb(50, 27, 138, 107);
            btnTheme.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnTheme.Size = new Size(32, 32);
            btnTheme.TabIndex = 4;
            btnTheme.Text = "☀️";
            btnTheme.UseTransparentBackground = true;
            // 
            // btnLang
            // 
            btnLang.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLang.Animated = true;
            btnLang.BackColor = Color.Transparent;
            btnLang.BorderRadius = 6;
            btnLang.CustomizableEdges = customizableEdges7;
            btnLang.FillColor = Color.Transparent;
            btnLang.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnLang.ForeColor = Color.FromArgb(139, 148, 158);
            btnLang.HoverState.FillColor = Color.FromArgb(30, 27, 138, 107);
            btnLang.HoverState.ForeColor = Color.FromArgb(230, 237, 243);
            btnLang.Location = new Point(591, 7);
            btnLang.Name = "btnLang";
            btnLang.PressedColor = Color.FromArgb(50, 27, 138, 107);
            btnLang.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnLang.Size = new Size(32, 32);
            btnLang.TabIndex = 5;
            btnLang.Text = "EN";
            btnLang.UseTransparentBackground = true;
            // 
            // btnMinimize
            // 
            btnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMinimize.Animated = true;
            btnMinimize.BackColor = Color.Transparent;
            btnMinimize.BorderRadius = 6;
            btnMinimize.CustomizableEdges = customizableEdges9;
            btnMinimize.FillColor = Color.Transparent;
            btnMinimize.Font = new Font("Segoe UI", 11F);
            btnMinimize.ForeColor = Color.FromArgb(139, 148, 158);
            btnMinimize.HoverState.FillColor = Color.FromArgb(30, 27, 138, 107);
            btnMinimize.HoverState.ForeColor = Color.FromArgb(230, 237, 243);
            btnMinimize.Location = new Point(624, 7);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.PressedColor = Color.FromArgb(50, 27, 138, 107);
            btnMinimize.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnMinimize.Size = new Size(32, 32);
            btnMinimize.TabIndex = 6;
            btnMinimize.Text = "─";
            btnMinimize.UseTransparentBackground = true;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Animated = true;
            btnClose.BackColor = Color.Transparent;
            btnClose.BorderRadius = 6;
            btnClose.CustomizableEdges = customizableEdges11;
            btnClose.FillColor = Color.Transparent;
            btnClose.Font = new Font("Segoe UI", 11F);
            btnClose.ForeColor = Color.FromArgb(139, 148, 158);
            btnClose.HoverState.FillColor = Color.FromArgb(224, 108, 117);
            btnClose.HoverState.ForeColor = Color.White;
            btnClose.Location = new Point(660, 7);
            btnClose.Name = "btnClose";
            btnClose.PressedColor = Color.FromArgb(190, 80, 90);
            btnClose.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnClose.Size = new Size(32, 32);
            btnClose.TabIndex = 7;
            btnClose.Text = "✕";
            btnClose.UseTransparentBackground = true;
            // 
            // ucTitleBar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(10, 14, 20);
            Controls.Add(pnlBackground);
            Name = "ucTitleBar";
            Size = new Size(700, 45);
            pnlBackground.ResumeLayout(false);
            pnlBackground.PerformLayout();
            ResumeLayout(false);
        }

        // ── Designer Fields ──
        private Guna.UI2.WinForms.Guna2GradientPanel pnlBackground;
        private Label lblLogo;
        private Label lblAppTitle;
        private Guna.UI2.WinForms.Guna2Button btnPin;
        private Guna.UI2.WinForms.Guna2Button btnTracking;
        private Guna.UI2.WinForms.Guna2Button btnSettings;
        private Guna.UI2.WinForms.Guna2Button btnTheme;
        private Guna.UI2.WinForms.Guna2Button btnLang;
        private Guna.UI2.WinForms.Guna2Button btnMinimize;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private Guna.UI2.WinForms.Guna2Button btnAdhkar;
    }
}
