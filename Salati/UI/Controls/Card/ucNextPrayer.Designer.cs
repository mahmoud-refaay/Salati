namespace UI.Controls.Card
{
    partial class ucNextPrayer
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            if (disposing)
                _countdownTimer?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pnlHero = new Guna.UI2.WinForms.Guna2GradientPanel();
            lblEmoji = new Label();
            lblPrayerName = new Label();
            lblPrayerNameEn = new Label();
            lblTime = new Label();
            lblCountdown = new Label();
            progressBar = new Guna.UI2.WinForms.Guna2ProgressBar();
            lblRemaining = new Label();
            pnlHero.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHero
            // 
            pnlHero.BackColor = Color.Transparent;
            pnlHero.BorderRadius = 16;
            pnlHero.Controls.Add(lblEmoji);
            pnlHero.Controls.Add(lblPrayerName);
            pnlHero.Controls.Add(lblPrayerNameEn);
            pnlHero.Controls.Add(lblTime);
            pnlHero.Controls.Add(lblCountdown);
            pnlHero.Controls.Add(progressBar);
            pnlHero.Controls.Add(lblRemaining);
            pnlHero.CustomizableEdges = customizableEdges3;
            pnlHero.Dock = DockStyle.Fill;
            pnlHero.FillColor = Color.FromArgb(22, 27, 34);
            pnlHero.FillColor2 = Color.FromArgb(15, 45, 35);
            pnlHero.Location = new Point(0, 0);
            pnlHero.Name = "pnlHero";
            pnlHero.ShadowDecoration.Color = Color.FromArgb(60, 27, 138, 107);
            pnlHero.ShadowDecoration.CustomizableEdges = customizableEdges4;
            pnlHero.ShadowDecoration.Depth = 6;
            pnlHero.ShadowDecoration.Enabled = true;
            pnlHero.Size = new Size(650, 160);
            pnlHero.TabIndex = 0;
            // 
            // lblEmoji
            // 
            lblEmoji.BackColor = Color.Transparent;
            lblEmoji.Font = new Font("Segoe UI Emoji", 36F);
            lblEmoji.ForeColor = Color.FromArgb(27, 138, 107);
            lblEmoji.Location = new Point(0, 7);
            lblEmoji.Name = "lblEmoji";
            lblEmoji.Size = new Size(650, 55);
            lblEmoji.TabIndex = 0;
            lblEmoji.Text = "🌤️";
            lblEmoji.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblPrayerName
            // 
            lblPrayerName.BackColor = Color.Transparent;
            lblPrayerName.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblPrayerName.ForeColor = Color.FromArgb(230, 237, 243);
            lblPrayerName.Location = new Point(0, 59);
            lblPrayerName.Name = "lblPrayerName";
            lblPrayerName.Size = new Size(650, 25);
            lblPrayerName.TabIndex = 1;
            lblPrayerName.Text = "صلاة العصر";
            lblPrayerName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblPrayerNameEn
            // 
            lblPrayerNameEn.BackColor = Color.Transparent;
            lblPrayerNameEn.Font = new Font("Segoe UI", 7.5F);
            lblPrayerNameEn.ForeColor = Color.FromArgb(72, 79, 88);
            lblPrayerNameEn.Location = new Point(0, 84);
            lblPrayerNameEn.Name = "lblPrayerNameEn";
            lblPrayerNameEn.Size = new Size(650, 14);
            lblPrayerNameEn.TabIndex = 2;
            lblPrayerNameEn.Text = "ASR PRAYER";
            lblPrayerNameEn.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTime
            // 
            lblTime.BackColor = Color.Transparent;
            lblTime.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTime.ForeColor = Color.FromArgb(200, 169, 110);
            lblTime.Location = new Point(200, 100);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(110, 20);
            lblTime.TabIndex = 3;
            lblTime.Text = "03:45 PM";
            lblTime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblCountdown
            // 
            lblCountdown.BackColor = Color.Transparent;
            lblCountdown.Font = new Font("Consolas", 11F, FontStyle.Bold);
            lblCountdown.ForeColor = Color.FromArgb(200, 169, 110);
            lblCountdown.Location = new Point(330, 100);
            lblCountdown.Name = "lblCountdown";
            lblCountdown.Size = new Size(110, 20);
            lblCountdown.TabIndex = 4;
            lblCountdown.Text = "01:23:45";
            lblCountdown.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // progressBar
            // 
            progressBar.BackColor = Color.Transparent;
            progressBar.BorderRadius = 5;
            progressBar.CustomizableEdges = customizableEdges1;
            progressBar.FillColor = Color.FromArgb(40, 27, 138, 107);
            progressBar.Location = new Point(60, 128);
            progressBar.Name = "progressBar";
            progressBar.ProgressColor = Color.FromArgb(27, 138, 107);
            progressBar.ProgressColor2 = Color.FromArgb(200, 169, 110);
            progressBar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            progressBar.Size = new Size(450, 10);
            progressBar.TabIndex = 5;
            progressBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            progressBar.Value = 72;
            // 
            // lblRemaining
            // 
            lblRemaining.BackColor = Color.Transparent;
            lblRemaining.Font = new Font("Segoe UI", 7.5F);
            lblRemaining.ForeColor = Color.FromArgb(72, 79, 88);
            lblRemaining.Location = new Point(0, 142);
            lblRemaining.Name = "lblRemaining";
            lblRemaining.Size = new Size(650, 14);
            lblRemaining.TabIndex = 6;
            lblRemaining.Text = "متبقي | REMAINING";
            lblRemaining.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ucNextPrayer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(pnlHero);
            Name = "ucNextPrayer";
            Size = new Size(650, 160);
            pnlHero.ResumeLayout(false);
            ResumeLayout(false);
        }

        // ── Designer Fields ──
        private Guna.UI2.WinForms.Guna2GradientPanel pnlHero;
        private Label lblEmoji;
        private Label lblPrayerName;
        private Label lblPrayerNameEn;
        private Label lblTime;
        private Label lblCountdown;
        private Guna.UI2.WinForms.Guna2ProgressBar progressBar;
        private Label lblRemaining;
    }
}
