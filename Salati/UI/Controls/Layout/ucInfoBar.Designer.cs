namespace UI.Controls.Layout
{
    partial class ucInfoBar
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

            pnlBackground = new Guna.UI2.WinForms.Guna2Panel();
            lblDate = new Label();
            lblCity = new Label();
            lnkTray = new LinkLabel();

            pnlBackground.SuspendLayout();
            SuspendLayout();

            // 
            // pnlBackground
            // 
            pnlBackground.Controls.Add(lblDate);
            pnlBackground.Controls.Add(lblCity);
            pnlBackground.Controls.Add(lnkTray);
            pnlBackground.CustomizableEdges = customizableEdges1;
            pnlBackground.Dock = DockStyle.Fill;
            pnlBackground.FillColor = Color.FromArgb(10, 14, 20);
            pnlBackground.Location = new Point(0, 0);
            pnlBackground.Name = "pnlBackground";
            pnlBackground.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pnlBackground.Size = new Size(700, 30);
            pnlBackground.TabIndex = 0;

            // 
            // lblDate — 📅 Saturday, April 12
            // 
            lblDate.AutoSize = true;
            lblDate.BackColor = Color.Transparent;
            lblDate.Font = new Font("Segoe UI", 8.5F);
            lblDate.ForeColor = Color.FromArgb(72, 79, 88);
            lblDate.Location = new Point(12, 7);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(150, 15);
            lblDate.TabIndex = 0;
            lblDate.Text = "📅 Saturday, April 12";

            // 
            // lblCity — 📍 Cairo
            // 
            lblCity.AutoSize = true;
            lblCity.BackColor = Color.Transparent;
            lblCity.Font = new Font("Segoe UI", 8.5F);
            lblCity.ForeColor = Color.FromArgb(72, 79, 88);
            lblCity.Location = new Point(200, 7);
            lblCity.Name = "lblCity";
            lblCity.Size = new Size(80, 15);
            lblCity.TabIndex = 1;
            lblCity.Text = "📍 Cairo";

            // 
            // lnkTray — ▼ Tray
            // 
            lnkTray.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lnkTray.AutoSize = true;
            lnkTray.BackColor = Color.Transparent;
            lnkTray.Font = new Font("Segoe UI", 8F);
            lnkTray.LinkColor = Color.FromArgb(72, 79, 88);
            lnkTray.ActiveLinkColor = Color.FromArgb(27, 138, 107);
            lnkTray.VisitedLinkColor = Color.FromArgb(72, 79, 88);
            lnkTray.Location = new Point(612, 8);
            lnkTray.Name = "lnkTray";
            lnkTray.Size = new Size(75, 13);
            lnkTray.TabIndex = 2;
            lnkTray.Text = "▼ Minimize";

            // 
            // ucInfoBar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(10, 14, 20);
            Controls.Add(pnlBackground);
            Name = "ucInfoBar";
            Size = new Size(700, 30);

            pnlBackground.ResumeLayout(false);
            pnlBackground.PerformLayout();
            ResumeLayout(false);
        }

        // ── Designer Fields ──
        private Guna.UI2.WinForms.Guna2Panel pnlBackground;
        private Label lblDate;
        private Label lblCity;
        private LinkLabel lnkTray;
    }
}
