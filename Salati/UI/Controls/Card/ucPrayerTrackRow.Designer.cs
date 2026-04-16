namespace UI.Controls.Card
{
    partial class ucPrayerTrackRow
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

            pnlRow = new Guna.UI2.WinForms.Guna2Panel();
            lblEmoji = new Label();
            lblPrayerName = new Label();
            lblTime = new Label();
            lblStatusIcon = new Label();
            lblStatusText = new Label();
            btnToggle = new Guna.UI2.WinForms.Guna2Button();

            pnlRow.SuspendLayout();
            SuspendLayout();

            //
            // pnlRow
            //
            pnlRow.Controls.Add(btnToggle);
            pnlRow.Controls.Add(lblStatusText);
            pnlRow.Controls.Add(lblStatusIcon);
            pnlRow.Controls.Add(lblTime);
            pnlRow.Controls.Add(lblPrayerName);
            pnlRow.Controls.Add(lblEmoji);
            pnlRow.CustomizableEdges = ce1;
            pnlRow.BorderRadius = 10;
            pnlRow.BorderThickness = 1;
            pnlRow.BorderColor = Color.FromArgb(38, 27, 138, 107);
            pnlRow.Dock = DockStyle.Top;
            pnlRow.FillColor = Color.FromArgb(22, 27, 34);
            pnlRow.Location = new Point(0, 0);
            pnlRow.Name = "pnlRow";
            pnlRow.Padding = new Padding(8, 0, 8, 0);
            pnlRow.ShadowDecoration.CustomizableEdges = ce2;
            pnlRow.ShadowDecoration.Enabled = true;
            pnlRow.ShadowDecoration.Color = Color.FromArgb(40, 27, 138, 107);
            pnlRow.ShadowDecoration.Depth = 3;
            pnlRow.Size = new Size(400, 52);
            pnlRow.TabIndex = 0;

            //
            // lblEmoji — 🌅
            //
            lblEmoji.BackColor = Color.Transparent;
            lblEmoji.Font = new Font("Segoe UI Emoji", 16F);
            lblEmoji.Location = new Point(10, 8);
            lblEmoji.Name = "lblEmoji";
            lblEmoji.Size = new Size(36, 36);
            lblEmoji.TabIndex = 0;
            lblEmoji.Text = "🌅";
            lblEmoji.TextAlign = ContentAlignment.MiddleCenter;

            //
            // lblPrayerName — الفجر
            //
            lblPrayerName.BackColor = Color.Transparent;
            lblPrayerName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPrayerName.ForeColor = Color.FromArgb(230, 237, 243);
            lblPrayerName.Location = new Point(50, 6);
            lblPrayerName.Name = "lblPrayerName";
            lblPrayerName.Size = new Size(75, 20);
            lblPrayerName.TabIndex = 1;
            lblPrayerName.Text = "الفجر";
            lblPrayerName.TextAlign = ContentAlignment.MiddleLeft;

            //
            // lblTime — 04:35 AM
            //
            lblTime.BackColor = Color.Transparent;
            lblTime.Font = new Font("Segoe UI", 8.5F);
            lblTime.ForeColor = Color.FromArgb(139, 148, 158);
            lblTime.Location = new Point(50, 28);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(75, 16);
            lblTime.TabIndex = 2;
            lblTime.Text = "04:35 AM";
            lblTime.TextAlign = ContentAlignment.MiddleLeft;

            //
            // lblStatusIcon — ✅
            //
            lblStatusIcon.BackColor = Color.Transparent;
            lblStatusIcon.Font = new Font("Segoe UI Emoji", 14F);
            lblStatusIcon.Location = new Point(135, 10);
            lblStatusIcon.Name = "lblStatusIcon";
            lblStatusIcon.Size = new Size(30, 30);
            lblStatusIcon.TabIndex = 3;
            lblStatusIcon.Text = "⬜";
            lblStatusIcon.TextAlign = ContentAlignment.MiddleCenter;

            //
            // lblStatusText — صليت في الوقت
            //
            lblStatusText.BackColor = Color.Transparent;
            lblStatusText.Font = new Font("Segoe UI", 8.5F);
            lblStatusText.ForeColor = Color.FromArgb(139, 148, 158);
            lblStatusText.Location = new Point(168, 14);
            lblStatusText.Name = "lblStatusText";
            lblStatusText.Size = new Size(120, 22);
            lblStatusText.TabIndex = 4;
            lblStatusText.Text = "";
            lblStatusText.TextAlign = ContentAlignment.MiddleLeft;

            //
            // btnToggle — صليت ✓
            //
            btnToggle.CustomizableEdges = ce3;
            btnToggle.ShadowDecoration.CustomizableEdges = ce4;
            btnToggle.Animated = true;
            btnToggle.BorderRadius = 8;
            btnToggle.FillColor = Color.FromArgb(27, 138, 107);
            btnToggle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnToggle.ForeColor = Color.White;
            btnToggle.Location = new Point(298, 10);
            btnToggle.Name = "btnToggle";
            btnToggle.Size = new Size(90, 32);
            btnToggle.TabIndex = 5;
            btnToggle.Text = "صليت ✓";

            //
            // ucPrayerTrackRow
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(pnlRow);
            Margin = new Padding(0, 0, 0, 6);
            Name = "ucPrayerTrackRow";
            Size = new Size(400, 58);

            pnlRow.ResumeLayout(false);
            ResumeLayout(false);
        }

        // ── Designer Fields ──
        private Guna.UI2.WinForms.Guna2Panel pnlRow;
        private Label lblEmoji;
        private Label lblPrayerName;
        private Label lblTime;
        private Label lblStatusIcon;
        private Label lblStatusText;
        private Guna.UI2.WinForms.Guna2Button btnToggle;
    }
}
