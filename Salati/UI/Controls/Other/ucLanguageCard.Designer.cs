namespace UI.Controls.Other
{
    partial class ucLanguageCard
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
            lblFlag = new Label();
            lblName = new Label();
            lblSub = new Label();
            lblCheckmark = new Label();

            pnlCard.SuspendLayout();
            SuspendLayout();

            // 
            // pnlCard
            // 
            pnlCard.Controls.Add(lblFlag);
            pnlCard.Controls.Add(lblName);
            pnlCard.Controls.Add(lblSub);
            pnlCard.Controls.Add(lblCheckmark);
            pnlCard.CustomizableEdges = customizableEdges1;
            pnlCard.BorderRadius = 12;
            pnlCard.BorderThickness = 2;
            pnlCard.BorderColor = Color.Transparent;
            pnlCard.Cursor = Cursors.Hand;
            pnlCard.Dock = DockStyle.Fill;
            pnlCard.FillColor = Color.FromArgb(22, 27, 34);
            pnlCard.Location = new Point(0, 0);
            pnlCard.MinimumSize = new Size(100, 40);
            pnlCard.Name = "pnlCard";
            pnlCard.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pnlCard.ShadowDecoration.Enabled = true;
            pnlCard.ShadowDecoration.Color = Color.FromArgb(40, 27, 138, 107);
            pnlCard.ShadowDecoration.Depth = 3;
            pnlCard.Size = new Size(180, 60);
            pnlCard.TabIndex = 0;

            // 
            // lblFlag — 🇸🇦 أو 🇺🇸
            // 
            lblFlag.BackColor = Color.Transparent;
            lblFlag.Font = new Font("Segoe UI Emoji", 18F);
            lblFlag.ForeColor = Color.FromArgb(230, 237, 243);
            lblFlag.Location = new Point(12, 10);
            lblFlag.Name = "lblFlag";
            lblFlag.Size = new Size(38, 38);
            lblFlag.TabIndex = 0;
            lblFlag.Text = "🇸🇦";
            lblFlag.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblName — العربية
            // 
            lblName.BackColor = Color.Transparent;
            lblName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblName.ForeColor = Color.FromArgb(230, 237, 243);
            lblName.Location = new Point(54, 10);
            lblName.Name = "lblName";
            lblName.Size = new Size(90, 20);
            lblName.TabIndex = 1;
            lblName.Text = "العربية";

            // 
            // lblSub — Arabic
            // 
            lblSub.BackColor = Color.Transparent;
            lblSub.Font = new Font("Segoe UI", 7.5F);
            lblSub.ForeColor = Color.FromArgb(72, 79, 88);
            lblSub.Location = new Point(54, 32);
            lblSub.Name = "lblSub";
            lblSub.Size = new Size(90, 15);
            lblSub.TabIndex = 2;
            lblSub.Text = "Arabic";

            // 
            // lblCheckmark — ✓
            // 
            lblCheckmark.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCheckmark.BackColor = Color.Transparent;
            lblCheckmark.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblCheckmark.ForeColor = Color.FromArgb(27, 138, 107);
            lblCheckmark.Location = new Point(150, 16);
            lblCheckmark.Name = "lblCheckmark";
            lblCheckmark.Size = new Size(24, 26);
            lblCheckmark.TabIndex = 3;
            lblCheckmark.Text = "✓";
            lblCheckmark.Visible = false;

            // 
            // ucLanguageCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(pnlCard);
            Name = "ucLanguageCard";
            Size = new Size(180, 60);

            pnlCard.ResumeLayout(false);
            ResumeLayout(false);
        }

        // ── Designer Fields ──
        private Guna.UI2.WinForms.Guna2Panel pnlCard;
        private Label lblFlag;
        private Label lblName;
        private Label lblSub;
        private Label lblCheckmark;
    }
}
