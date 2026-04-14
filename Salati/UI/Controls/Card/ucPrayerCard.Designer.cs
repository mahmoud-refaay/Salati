namespace UI.Controls.Card
{
    partial class ucPrayerCard
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

            pnlCard = new Guna.UI2.WinForms.Guna2Panel();
            lblEmoji = new Label();
            lblPrayerName = new Label();
            lblTime = new Label();
            lblStatus = new Label();

            pnlCard.SuspendLayout();
            SuspendLayout();

            // 
            // pnlCard
            // 
            pnlCard.Controls.Add(lblEmoji);
            pnlCard.Controls.Add(lblPrayerName);
            pnlCard.Controls.Add(lblTime);
            pnlCard.Controls.Add(lblStatus);
            pnlCard.CustomizableEdges = customizableEdges1;
            pnlCard.BorderRadius = 12;
            pnlCard.BorderThickness = 1;
            pnlCard.BorderColor = Color.FromArgb(38, 27, 138, 107);
            pnlCard.Dock = DockStyle.Fill;
            pnlCard.FillColor = Color.FromArgb(22, 27, 34);
            pnlCard.Location = new Point(0, 0);
            pnlCard.Name = "pnlCard";
            pnlCard.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pnlCard.ShadowDecoration.Enabled = true;
            pnlCard.ShadowDecoration.Color = Color.FromArgb(60, 27, 138, 107);
            pnlCard.ShadowDecoration.Depth = 4;
            pnlCard.Size = new Size(110, 100);
            pnlCard.TabIndex = 0;

            // 
            // lblEmoji — 🌅
            // 
            lblEmoji.BackColor = Color.Transparent;
            lblEmoji.Font = new Font("Segoe UI Emoji", 22F);
            lblEmoji.ForeColor = Color.FromArgb(27, 138, 107);
            lblEmoji.Location = new Point(0, 6);
            lblEmoji.Name = "lblEmoji";
            lblEmoji.Size = new Size(110, 38);
            lblEmoji.TabIndex = 0;
            lblEmoji.Text = "🌅";
            lblEmoji.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblPrayerName — الفجر
            // 
            lblPrayerName.BackColor = Color.Transparent;
            lblPrayerName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPrayerName.ForeColor = Color.FromArgb(230, 237, 243);
            lblPrayerName.Location = new Point(0, 44);
            lblPrayerName.Name = "lblPrayerName";
            lblPrayerName.Size = new Size(110, 17);
            lblPrayerName.TabIndex = 1;
            lblPrayerName.Text = "الفجر";
            lblPrayerName.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblTime — 04:35 AM
            // 
            lblTime.BackColor = Color.Transparent;
            lblTime.Font = new Font("Segoe UI", 8.5F);
            lblTime.ForeColor = Color.FromArgb(200, 169, 110);
            lblTime.Location = new Point(0, 61);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(110, 17);
            lblTime.TabIndex = 2;
            lblTime.Text = "04:35 AM";
            lblTime.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblStatus — ✅
            // 
            lblStatus.BackColor = Color.Transparent;
            lblStatus.Font = new Font("Segoe UI Emoji", 10F);
            lblStatus.ForeColor = Color.FromArgb(139, 148, 158);
            lblStatus.Location = new Point(0, 79);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(110, 18);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "✅";
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // ucPrayerCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(pnlCard);
            Name = "ucPrayerCard";
            Size = new Size(110, 100);

            pnlCard.ResumeLayout(false);
            ResumeLayout(false);
        }

        // ── Designer Fields ──
        private Guna.UI2.WinForms.Guna2Panel pnlCard;
        private Label lblEmoji;
        private Label lblPrayerName;
        private Label lblTime;
        private Label lblStatus;
    }
}
