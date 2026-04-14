namespace UI.Forms
{
    partial class frmAlert
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

            pnlBg = new Guna.UI2.WinForms.Guna2GradientPanel();
            lblEmoji = new Label();
            lblPrayerName = new Label();
            lblPrayerNameEn = new Label();
            lblTime = new Label();
            lblQuran = new Label();
            btnDismiss = new Guna.UI2.WinForms.Guna2GradientButton();
            btnStop = new Guna.UI2.WinForms.Guna2Button();

            pnlBg.SuspendLayout();
            SuspendLayout();

            // 
            // pnlBg — خلفية gradient
            // 
            pnlBg.Controls.Add(lblEmoji);
            pnlBg.Controls.Add(lblPrayerName);
            pnlBg.Controls.Add(lblPrayerNameEn);
            pnlBg.Controls.Add(lblTime);
            pnlBg.Controls.Add(lblQuran);
            pnlBg.Controls.Add(btnDismiss);
            pnlBg.Controls.Add(btnStop);
            pnlBg.CustomizableEdges = customizableEdges1;
            pnlBg.Dock = DockStyle.Fill;
            pnlBg.FillColor = Color.FromArgb(13, 17, 23);
            pnlBg.FillColor2 = Color.FromArgb(22, 27, 34);
            pnlBg.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            pnlBg.Location = new Point(0, 0);
            pnlBg.Name = "pnlBg";
            pnlBg.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pnlBg.Size = new Size(420, 360);
            pnlBg.TabIndex = 0;

            // 
            // lblEmoji — 🕌
            // 
            lblEmoji.BackColor = Color.Transparent;
            lblEmoji.Font = new Font("Segoe UI Emoji", 56F);
            lblEmoji.ForeColor = Color.FromArgb(27, 138, 107);
            lblEmoji.Location = new Point(150, 15);
            lblEmoji.Name = "lblEmoji";
            lblEmoji.Size = new Size(120, 100);
            lblEmoji.TabIndex = 0;
            lblEmoji.Text = "🌅";
            lblEmoji.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblPrayerName — حان وقت صلاة الفجر
            // 
            lblPrayerName.BackColor = Color.Transparent;
            lblPrayerName.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblPrayerName.ForeColor = Color.FromArgb(230, 237, 243);
            lblPrayerName.Location = new Point(30, 115);
            lblPrayerName.Name = "lblPrayerName";
            lblPrayerName.Size = new Size(360, 36);
            lblPrayerName.TabIndex = 1;
            lblPrayerName.Text = "حان وقت صلاة الفجر";
            lblPrayerName.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblPrayerNameEn — Fajr Prayer
            // 
            lblPrayerNameEn.BackColor = Color.Transparent;
            lblPrayerNameEn.Font = new Font("Segoe UI", 10F);
            lblPrayerNameEn.ForeColor = Color.FromArgb(139, 148, 158);
            lblPrayerNameEn.Location = new Point(100, 152);
            lblPrayerNameEn.Name = "lblPrayerNameEn";
            lblPrayerNameEn.Size = new Size(220, 22);
            lblPrayerNameEn.TabIndex = 2;
            lblPrayerNameEn.Text = "Fajr Prayer";
            lblPrayerNameEn.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblTime — 04:35 AM
            // 
            lblTime.BackColor = Color.Transparent;
            lblTime.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTime.ForeColor = Color.FromArgb(200, 169, 110);
            lblTime.Location = new Point(110, 185);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(200, 44);
            lblTime.TabIndex = 3;
            lblTime.Text = "04:35 AM";
            lblTime.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblQuran — آية
            // 
            lblQuran.BackColor = Color.Transparent;
            lblQuran.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblQuran.ForeColor = Color.FromArgb(72, 79, 88);
            lblQuran.Location = new Point(40, 240);
            lblQuran.Name = "lblQuran";
            lblQuran.Size = new Size(340, 20);
            lblQuran.TabIndex = 4;
            lblQuran.Text = "إِنَّ الصَّلَاةَ كَانَتْ عَلَى الْمُؤْمِنِينَ كِتَابًا مَوْقُوتًا";
            lblQuran.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // btnDismiss — ✕ إغلاق
            // 
            btnDismiss.Animated = true;
            btnDismiss.BorderRadius = 12;
            btnDismiss.Cursor = Cursors.Hand;
            btnDismiss.FillColor = Color.FromArgb(27, 138, 107);
            btnDismiss.FillColor2 = Color.FromArgb(200, 169, 110);
            btnDismiss.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnDismiss.ForeColor = Color.White;
            btnDismiss.HoverState.FillColor = Color.FromArgb(200, 169, 110);
            btnDismiss.HoverState.FillColor2 = Color.FromArgb(27, 138, 107);
            btnDismiss.Location = new Point(70, 280);
            btnDismiss.Name = "btnDismiss";
            btnDismiss.PressedColor = Color.FromArgb(17, 90, 70);
            btnDismiss.Size = new Size(170, 44);
            btnDismiss.TabIndex = 5;
            btnDismiss.Text = "✕ إغلاق";

            // 
            // btnStop — 🔇 إيقاف الصوت
            // 
            btnStop.BackColor = Color.Transparent;
            btnStop.BorderColor = Color.FromArgb(72, 79, 88);
            btnStop.BorderRadius = 12;
            btnStop.BorderThickness = 1;
            btnStop.CustomizableEdges = customizableEdges3;
            btnStop.FillColor = Color.Transparent;
            btnStop.Font = new Font("Segoe UI", 10F);
            btnStop.ForeColor = Color.FromArgb(139, 148, 158);
            btnStop.HoverState.FillColor = Color.FromArgb(30, 224, 108, 117);
            btnStop.HoverState.ForeColor = Color.FromArgb(224, 108, 117);
            btnStop.Location = new Point(260, 280);
            btnStop.Name = "btnStop";
            btnStop.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnStop.Size = new Size(90, 44);
            btnStop.TabIndex = 6;
            btnStop.Text = "🔇";

            // 
            // frmAlert
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(13, 17, 23);
            ClientSize = new Size(420, 360);
            Controls.Add(pnlBg);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmAlert";
            ShowInTaskbar = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Salati — Alert";
            TopMost = true;

            pnlBg.ResumeLayout(false);
            ResumeLayout(false);
        }

        // ── Designer Fields ──
        private Guna.UI2.WinForms.Guna2GradientPanel pnlBg;
        private Label lblEmoji;
        private Label lblPrayerName;
        private Label lblPrayerNameEn;
        private Label lblTime;
        private Label lblQuran;
        private Guna.UI2.WinForms.Guna2GradientButton btnDismiss;
        private Guna.UI2.WinForms.Guna2Button btnStop;
    }
}
