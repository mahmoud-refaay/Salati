namespace UI.Controls.Settings
{
    partial class ucSettingsAppearance
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
            lblThemeTitle = new Label();
            pnlThemes = new FlowLayoutPanel();
            _cardMidnight = new Card.ucThemeCard();
            _cardGolden = new Card.ucThemeCard();
            lblLangTitle = new Label();
            pnlLanguages = new FlowLayoutPanel();
            _cardArabic = new Other.ucLanguageCard();
            _cardEnglish = new Other.ucLanguageCard();

            pnlThemes.SuspendLayout();
            pnlLanguages.SuspendLayout();
            SuspendLayout();

            // 
            // lblThemeTitle
            // 
            lblThemeTitle.BackColor = Color.Transparent;
            lblThemeTitle.Dock = DockStyle.Top;
            lblThemeTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblThemeTitle.ForeColor = Color.FromArgb(230, 237, 243);
            lblThemeTitle.Location = new Point(0, 0);
            lblThemeTitle.Name = "lblThemeTitle";
            lblThemeTitle.Padding = new Padding(4, 0, 0, 0);
            lblThemeTitle.Size = new Size(310, 32);
            lblThemeTitle.TabIndex = 0;
            lblThemeTitle.Text = "\ud83c\udfa8 \u0627\u0644\u062b\u064a\u0645";
            lblThemeTitle.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // pnlThemes
            // 
            pnlThemes.BackColor = Color.Transparent;
            pnlThemes.Controls.Add(_cardMidnight);
            pnlThemes.Controls.Add(_cardGolden);
            pnlThemes.FlowDirection = FlowDirection.LeftToRight;
            pnlThemes.Location = new Point(4, 34);
            pnlThemes.Name = "pnlThemes";
            pnlThemes.Padding = new Padding(0);
            pnlThemes.Size = new Size(302, 95);
            pnlThemes.TabIndex = 1;
            pnlThemes.WrapContents = true;

            // 
            // _cardMidnight
            // 
            _cardMidnight.Location = new Point(2, 2);
            _cardMidnight.Margin = new Padding(2);
            _cardMidnight.Name = "_cardMidnight";
            _cardMidnight.Size = new Size(142, 80);
            _cardMidnight.TabIndex = 0;

            // 
            // _cardGolden
            // 
            _cardGolden.Location = new Point(148, 2);
            _cardGolden.Margin = new Padding(2);
            _cardGolden.Name = "_cardGolden";
            _cardGolden.Size = new Size(142, 80);
            _cardGolden.TabIndex = 1;

            // 
            // lblLangTitle
            // 
            lblLangTitle.BackColor = Color.Transparent;
            lblLangTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblLangTitle.ForeColor = Color.FromArgb(230, 237, 243);
            lblLangTitle.Location = new Point(0, 140);
            lblLangTitle.Name = "lblLangTitle";
            lblLangTitle.Padding = new Padding(4, 0, 0, 0);
            lblLangTitle.Size = new Size(310, 32);
            lblLangTitle.TabIndex = 2;
            lblLangTitle.Text = "\ud83c\udf10 \u0627\u0644\u0644\u063a\u0629";
            lblLangTitle.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // pnlLanguages
            // 
            pnlLanguages.BackColor = Color.Transparent;
            pnlLanguages.Controls.Add(_cardArabic);
            pnlLanguages.Controls.Add(_cardEnglish);
            pnlLanguages.FlowDirection = FlowDirection.LeftToRight;
            pnlLanguages.Location = new Point(4, 174);
            pnlLanguages.Name = "pnlLanguages";
            pnlLanguages.Size = new Size(302, 75);
            pnlLanguages.TabIndex = 3;
            pnlLanguages.WrapContents = true;

            // 
            // _cardArabic
            // 
            _cardArabic.Location = new Point(2, 2);
            _cardArabic.Margin = new Padding(2);
            _cardArabic.Name = "_cardArabic";
            _cardArabic.Size = new Size(142, 60);
            _cardArabic.TabIndex = 0;

            // 
            // _cardEnglish
            // 
            _cardEnglish.Location = new Point(148, 2);
            _cardEnglish.Margin = new Padding(2);
            _cardEnglish.Name = "_cardEnglish";
            _cardEnglish.Size = new Size(142, 60);
            _cardEnglish.TabIndex = 1;

            // 
            // ucSettingsAppearance
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.Transparent;
            Controls.Add(pnlLanguages);
            Controls.Add(lblLangTitle);
            Controls.Add(pnlThemes);
            Controls.Add(lblThemeTitle);
            Name = "ucSettingsAppearance";
            Size = new Size(310, 300);

            pnlThemes.ResumeLayout(false);
            pnlLanguages.ResumeLayout(false);
            ResumeLayout(false);
        }

        // -- Designer Fields --
        private Label lblThemeTitle;
        private FlowLayoutPanel pnlThemes;
        private Label lblLangTitle;
        private FlowLayoutPanel pnlLanguages;
        private Card.ucThemeCard _cardMidnight;
        private Card.ucThemeCard _cardGolden;
        private Other.ucLanguageCard _cardArabic;
        private Other.ucLanguageCard _cardEnglish;
    }
}
