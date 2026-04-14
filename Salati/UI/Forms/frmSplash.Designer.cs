namespace UI.Forms
{
    partial class frmSplash
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

            pnlBackground = new Guna.UI2.WinForms.Guna2GradientPanel();
            lblMosque = new Label();
            lblAppName = new Label();
            lblAppNameAr = new Label();
            lblQuran = new Label();
            progressBar = new Guna.UI2.WinForms.Guna2ProgressBar();
            lblLoading = new Label();
            lblVersion = new Label();

            pnlBackground.SuspendLayout();
            SuspendLayout();

            // 
            // pnlBackground — خلفية gradient
            // 
            pnlBackground.Controls.Add(lblMosque);
            pnlBackground.Controls.Add(lblAppName);
            pnlBackground.Controls.Add(lblAppNameAr);
            pnlBackground.Controls.Add(lblQuran);
            pnlBackground.Controls.Add(progressBar);
            pnlBackground.Controls.Add(lblLoading);
            pnlBackground.Controls.Add(lblVersion);
            pnlBackground.CustomizableEdges = customizableEdges1;
            pnlBackground.Dock = DockStyle.Fill;
            pnlBackground.FillColor = Color.FromArgb(13, 17, 23);
            pnlBackground.FillColor2 = Color.FromArgb(22, 27, 34);
            pnlBackground.Location = new Point(0, 0);
            pnlBackground.Name = "pnlBackground";
            pnlBackground.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pnlBackground.Size = new Size(480, 340);
            pnlBackground.TabIndex = 0;

            // 
            // lblMosque — 🕌
            // 
            lblMosque.BackColor = Color.Transparent;
            lblMosque.Font = new Font("Segoe UI Emoji", 48F);
            lblMosque.ForeColor = Color.FromArgb(27, 138, 107);
            lblMosque.Location = new Point(190, 30);
            lblMosque.Name = "lblMosque";
            lblMosque.Size = new Size(100, 90);
            lblMosque.TabIndex = 0;
            lblMosque.Text = "🕌";
            lblMosque.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblAppName — Salati
            // 
            lblAppName.BackColor = Color.Transparent;
            lblAppName.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblAppName.ForeColor = Color.FromArgb(230, 237, 243);
            lblAppName.Location = new Point(130, 120);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(220, 48);
            lblAppName.TabIndex = 1;
            lblAppName.Text = "Salati";
            lblAppName.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblAppNameAr — صلاتي
            // 
            lblAppNameAr.BackColor = Color.Transparent;
            lblAppNameAr.Font = new Font("Segoe UI", 14F);
            lblAppNameAr.ForeColor = Color.FromArgb(200, 169, 110);
            lblAppNameAr.Location = new Point(160, 168);
            lblAppNameAr.Name = "lblAppNameAr";
            lblAppNameAr.Size = new Size(160, 28);
            lblAppNameAr.TabIndex = 2;
            lblAppNameAr.Text = "صلاتي";
            lblAppNameAr.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblQuran — وَأَقِيمُوا الصَّلَاةَ
            // 
            lblQuran.BackColor = Color.Transparent;
            lblQuran.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblQuran.ForeColor = Color.FromArgb(72, 79, 88);
            lblQuran.Location = new Point(100, 208);
            lblQuran.Name = "lblQuran";
            lblQuran.Size = new Size(280, 24);
            lblQuran.TabIndex = 3;
            lblQuran.Text = "وَأَقِيمُوا الصَّلَاةَ وَآتُوا الزَّكَاةَ";
            lblQuran.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // progressBar
            // 
            progressBar.BorderRadius = 4;
            progressBar.CustomizableEdges = customizableEdges3;
            progressBar.FillColor = Color.FromArgb(30, 27, 138, 107);
            progressBar.Location = new Point(80, 260);
            progressBar.Name = "progressBar";
            progressBar.ProgressColor = Color.FromArgb(27, 138, 107);
            progressBar.ProgressColor2 = Color.FromArgb(200, 169, 110);
            progressBar.ShadowDecoration.CustomizableEdges = customizableEdges4;
            progressBar.Size = new Size(320, 8);
            progressBar.TabIndex = 4;

            // 
            // lblLoading — جاري التحميل...
            // 
            lblLoading.BackColor = Color.Transparent;
            lblLoading.Font = new Font("Segoe UI", 8F);
            lblLoading.ForeColor = Color.FromArgb(72, 79, 88);
            lblLoading.Location = new Point(180, 275);
            lblLoading.Name = "lblLoading";
            lblLoading.Size = new Size(120, 16);
            lblLoading.TabIndex = 5;
            lblLoading.Text = "جاري التحميل...";
            lblLoading.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblVersion — v1.0
            // 
            lblVersion.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblVersion.BackColor = Color.Transparent;
            lblVersion.Font = new Font("Segoe UI", 7.5F);
            lblVersion.ForeColor = Color.FromArgb(50, 55, 62);
            lblVersion.Location = new Point(400, 316);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(70, 16);
            lblVersion.TabIndex = 6;
            lblVersion.Text = "v1.0.0";
            lblVersion.TextAlign = ContentAlignment.MiddleRight;

            // 
            // frmSplash
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(13, 17, 23);
            ClientSize = new Size(480, 340);
            Controls.Add(pnlBackground);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmSplash";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Salati";

            pnlBackground.ResumeLayout(false);
            ResumeLayout(false);
        }

        // ── Designer Fields ──
        private Guna.UI2.WinForms.Guna2GradientPanel pnlBackground;
        private Label lblMosque;
        private Label lblAppName;
        private Label lblAppNameAr;
        private Label lblQuran;
        private Guna.UI2.WinForms.Guna2ProgressBar progressBar;
        private Label lblLoading;
        private Label lblVersion;
    }
}
