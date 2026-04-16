namespace UI.Controls.Card
{
    partial class ucAdhkarRow
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
            lblAdhkarText = new Label();
            lblSource = new Label();
            lblCounter = new Label();
            btnTap = new Guna.UI2.WinForms.Guna2Button();

            pnlRow.SuspendLayout();
            SuspendLayout();

            //
            // pnlRow
            //
            pnlRow.Controls.Add(btnTap);
            pnlRow.Controls.Add(lblCounter);
            pnlRow.Controls.Add(lblSource);
            pnlRow.Controls.Add(lblAdhkarText);
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
            pnlRow.Size = new Size(380, 66);
            pnlRow.TabIndex = 0;

            //
            // lblEmoji — 📿
            //
            lblEmoji.BackColor = Color.Transparent;
            lblEmoji.Font = new Font("Segoe UI Emoji", 16F);
            lblEmoji.Location = new Point(8, 14);
            lblEmoji.Name = "lblEmoji";
            lblEmoji.Size = new Size(36, 36);
            lblEmoji.TabIndex = 0;
            lblEmoji.Text = "📿";
            lblEmoji.TextAlign = ContentAlignment.MiddleCenter;

            //
            // lblAdhkarText — نص الذكر
            //
            lblAdhkarText.BackColor = Color.Transparent;
            lblAdhkarText.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblAdhkarText.ForeColor = Color.FromArgb(230, 237, 243);
            lblAdhkarText.Location = new Point(48, 4);
            lblAdhkarText.Name = "lblAdhkarText";
            lblAdhkarText.Size = new Size(220, 36);
            lblAdhkarText.TabIndex = 1;
            lblAdhkarText.Text = "سبحان الله وبحمده";
            lblAdhkarText.TextAlign = ContentAlignment.MiddleLeft;

            //
            // lblSource — المصدر
            //
            lblSource.BackColor = Color.Transparent;
            lblSource.Font = new Font("Segoe UI", 7.5F);
            lblSource.ForeColor = Color.FromArgb(103, 113, 123);
            lblSource.Location = new Point(48, 42);
            lblSource.Name = "lblSource";
            lblSource.Size = new Size(180, 16);
            lblSource.TabIndex = 2;
            lblSource.Text = "— متفق عليه";
            lblSource.TextAlign = ContentAlignment.MiddleLeft;

            //
            // lblCounter — 0/33
            //
            lblCounter.BackColor = Color.Transparent;
            lblCounter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCounter.ForeColor = Color.FromArgb(200, 169, 110);
            lblCounter.Location = new Point(276, 4);
            lblCounter.Name = "lblCounter";
            lblCounter.Size = new Size(50, 20);
            lblCounter.TabIndex = 3;
            lblCounter.Text = "0/33";
            lblCounter.TextAlign = ContentAlignment.MiddleCenter;

            //
            // btnTap — ➕
            //
            btnTap.CustomizableEdges = ce3;
            btnTap.ShadowDecoration.CustomizableEdges = ce4;
            btnTap.Animated = true;
            btnTap.BorderRadius = 8;
            btnTap.FillColor = Color.FromArgb(27, 138, 107);
            btnTap.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnTap.ForeColor = Color.White;
            btnTap.Location = new Point(276, 28);
            btnTap.Name = "btnTap";
            btnTap.Size = new Size(90, 30);
            btnTap.TabIndex = 4;
            btnTap.Text = "➕ اضغط";

            //
            // ucAdhkarRow
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(pnlRow);
            Margin = new Padding(0, 0, 0, 4);
            Name = "ucAdhkarRow";
            Size = new Size(380, 72);

            pnlRow.ResumeLayout(false);
            ResumeLayout(false);
        }

        // ── Designer Fields ──
        private Guna.UI2.WinForms.Guna2Panel pnlRow;
        private Label lblEmoji;
        private Label lblAdhkarText;
        private Label lblSource;
        private Label lblCounter;
        private Guna.UI2.WinForms.Guna2Button btnTap;
    }
}
