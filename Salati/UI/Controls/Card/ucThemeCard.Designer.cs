namespace UI.Controls.Card
{
    partial class ucThemeCard
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

            pnlCard = new Guna.UI2.WinForms.Guna2Panel();
            pnlPreview = new Guna.UI2.WinForms.Guna2GradientPanel();
            lblThemeName = new Label();
            lblCheckmark = new Label();

            pnlCard.SuspendLayout();
            SuspendLayout();

            // 
            // pnlCard
            // 
            pnlCard.Controls.Add(pnlPreview);
            pnlCard.Controls.Add(lblThemeName);
            pnlCard.Controls.Add(lblCheckmark);
            pnlCard.CustomizableEdges = customizableEdges1;
            pnlCard.BorderRadius = 10;
            pnlCard.BorderThickness = 2;
            pnlCard.BorderColor = Color.Transparent;
            pnlCard.Cursor = Cursors.Hand;
            pnlCard.Dock = DockStyle.Fill;
            pnlCard.FillColor = Color.FromArgb(22, 27, 34);
            pnlCard.Location = new Point(0, 0);
            pnlCard.MinimumSize = new Size(100, 60);
            pnlCard.Name = "pnlCard";
            pnlCard.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pnlCard.Size = new Size(140, 80);
            pnlCard.TabIndex = 0;

            // 
            // pnlPreview — لون الثيم
            // 
            pnlPreview.CustomizableEdges = customizableEdges3;
            pnlPreview.BorderRadius = 6;
            pnlPreview.FillColor = Color.FromArgb(13, 17, 23);
            pnlPreview.FillColor2 = Color.FromArgb(27, 138, 107);
            pnlPreview.Location = new Point(12, 10);
            pnlPreview.MinimumSize = new Size(20, 10);
            pnlPreview.Name = "pnlPreview";
            pnlPreview.ShadowDecoration.CustomizableEdges = customizableEdges4;
            pnlPreview.Size = new Size(116, 30);
            pnlPreview.TabIndex = 0;

            // 
            // lblThemeName
            // 
            lblThemeName.BackColor = Color.Transparent;
            lblThemeName.Font = new Font("Segoe UI", 8.5F);
            lblThemeName.ForeColor = Color.FromArgb(230, 237, 243);
            lblThemeName.Location = new Point(12, 48);
            lblThemeName.Name = "lblThemeName";
            lblThemeName.Size = new Size(100, 20);
            lblThemeName.TabIndex = 1;
            lblThemeName.Text = "Midnight Serenity";

            // 
            // lblCheckmark — ✓
            // 
            lblCheckmark.BackColor = Color.Transparent;
            lblCheckmark.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblCheckmark.ForeColor = Color.FromArgb(27, 138, 107);
            lblCheckmark.Location = new Point(112, 48);
            lblCheckmark.Name = "lblCheckmark";
            lblCheckmark.Size = new Size(22, 22);
            lblCheckmark.TabIndex = 2;
            lblCheckmark.Text = "✓";
            lblCheckmark.Visible = false;

            // 
            // ucThemeCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(pnlCard);
            Name = "ucThemeCard";
            Size = new Size(140, 80);

            pnlCard.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Guna.UI2.WinForms.Guna2Panel pnlCard;
        private Guna.UI2.WinForms.Guna2GradientPanel pnlPreview;
        private Label lblThemeName;
        private Label lblCheckmark;
    }
}
