namespace UI.Controls.Card
{
    partial class ucAdhkarPanel
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
            Guna.UI2.WinForms.Suite.CustomizableEdges ce1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges ce2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges ce3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges ce4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges ce5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges ce6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();

            pnlBackground = new Guna.UI2.WinForms.Guna2Panel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            btnClose = new Guna.UI2.WinForms.Guna2Button();
            pnlRows = new FlowLayoutPanel();
            pnlProgress = new Guna.UI2.WinForms.Guna2Panel();
            progressBar = new Guna.UI2.WinForms.Guna2ProgressBar();
            lblProgress = new Label();

            pnlBackground.SuspendLayout();
            pnlProgress.SuspendLayout();
            SuspendLayout();

            //
            // pnlBackground
            //
            pnlBackground.Controls.Add(pnlRows);
            pnlBackground.Controls.Add(pnlProgress);
            pnlBackground.Controls.Add(lblSubtitle);
            pnlBackground.Controls.Add(lblTitle);
            pnlBackground.Controls.Add(btnClose);
            pnlBackground.CustomizableEdges = ce1;
            pnlBackground.BorderRadius = 16;
            pnlBackground.FillColor = Color.FromArgb(13, 17, 23);
            pnlBackground.Dock = DockStyle.Fill;
            pnlBackground.Location = new Point(0, 0);
            pnlBackground.Name = "pnlBackground";
            pnlBackground.Padding = new Padding(16, 12, 16, 12);
            pnlBackground.ShadowDecoration.CustomizableEdges = ce2;
            pnlBackground.ShadowDecoration.Enabled = true;
            pnlBackground.ShadowDecoration.Color = Color.FromArgb(80, 0, 0, 0);
            pnlBackground.ShadowDecoration.Depth = 10;
            pnlBackground.Size = new Size(420, 458);
            pnlBackground.TabIndex = 0;

            //
            // lblTitle — 🌅 أذكار الصباح
            //
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(230, 237, 243);
            lblTitle.Location = new Point(16, 14);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(300, 28);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🌅 أذكار الصباح";

            //
            // lblSubtitle
            //
            lblSubtitle.BackColor = Color.Transparent;
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = Color.FromArgb(139, 148, 158);
            lblSubtitle.Location = new Point(16, 42);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(300, 18);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "اضغط على كل ذكر لعدّه";

            //
            // btnClose — ✕
            //
            btnClose.CustomizableEdges = ce3;
            btnClose.ShadowDecoration.CustomizableEdges = ce4;
            btnClose.Animated = true;
            btnClose.BorderRadius = 10;
            btnClose.FillColor = Color.Transparent;
            btnClose.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnClose.ForeColor = Color.FromArgb(139, 148, 158);
            btnClose.Location = new Point(378, 10);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(32, 32);
            btnClose.TabIndex = 2;
            btnClose.Text = "✕";

            //
            // pnlRows — FlowLayout للأذكار
            //
            pnlRows.BackColor = Color.Transparent;
            pnlRows.FlowDirection = FlowDirection.TopDown;
            pnlRows.Location = new Point(16, 70);
            pnlRows.Name = "pnlRows";
            pnlRows.Size = new Size(388, 330);
            pnlRows.TabIndex = 3;
            pnlRows.WrapContents = false;
            pnlRows.AutoScroll = true;

            //
            // pnlProgress — شريط التقدم
            //
            pnlProgress.Controls.Add(progressBar);
            pnlProgress.Controls.Add(lblProgress);
            pnlProgress.CustomizableEdges = ce5;
            pnlProgress.BorderRadius = 10;
            pnlProgress.FillColor = Color.FromArgb(22, 27, 34);
            pnlProgress.Location = new Point(16, 410);
            pnlProgress.Name = "pnlProgress";
            pnlProgress.ShadowDecoration.CustomizableEdges = ce6;
            pnlProgress.Size = new Size(388, 40);
            pnlProgress.TabIndex = 4;

            //
            // progressBar
            //
            progressBar.FillColor = Color.FromArgb(30, 27, 138, 107);
            progressBar.ProgressColor = Color.FromArgb(27, 138, 107);
            progressBar.ProgressColor2 = Color.FromArgb(200, 169, 110);
            progressBar.BorderRadius = 6;
            progressBar.Location = new Point(10, 24);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(368, 10);
            progressBar.TabIndex = 0;
            progressBar.Value = 0;

            //
            // lblProgress — 📊 تم إنجاز: 3/10
            //
            lblProgress.BackColor = Color.Transparent;
            lblProgress.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblProgress.ForeColor = Color.FromArgb(200, 169, 110);
            lblProgress.Location = new Point(10, 3);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(360, 18);
            lblProgress.TabIndex = 1;
            lblProgress.Text = "📊 تم إنجاز: 0/0";

            //
            // ucAdhkarPanel
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(pnlBackground);
            Name = "ucAdhkarPanel";
            Size = new Size(420, 458);

            pnlBackground.ResumeLayout(false);
            pnlProgress.ResumeLayout(false);
            ResumeLayout(false);
        }

        // ── Designer Fields ──
        private Guna.UI2.WinForms.Guna2Panel pnlBackground;
        private Label lblTitle;
        private Label lblSubtitle;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private FlowLayoutPanel pnlRows;
        private Guna.UI2.WinForms.Guna2Panel pnlProgress;
        private Guna.UI2.WinForms.Guna2ProgressBar progressBar;
        private Label lblProgress;
    }
}
