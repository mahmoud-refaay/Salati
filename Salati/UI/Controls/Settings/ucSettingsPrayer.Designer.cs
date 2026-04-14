namespace UI.Controls.Settings
{
    partial class ucSettingsPrayer
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();

            lblSectionTitle = new Label();
            radAuto = new Guna.UI2.WinForms.Guna2RadioButton();
            radManual = new Guna.UI2.WinForms.Guna2RadioButton();
            pnlAutoFields = new Panel();
            lblCountry = new Label();
            cboCountry = new Guna.UI2.WinForms.Guna2ComboBox();
            lblCity = new Label();
            cboCity = new Guna.UI2.WinForms.Guna2ComboBox();
            lblMethod = new Label();
            cboMethod = new Guna.UI2.WinForms.Guna2ComboBox();
            pnlManualFields = new Panel();
            lblManFajr = new Label();
            dtpFajr = new DateTimePicker();
            lblManDhuhr = new Label();
            dtpDhuhr = new DateTimePicker();
            lblManAsr = new Label();
            dtpAsr = new DateTimePicker();
            lblManMaghrib = new Label();
            dtpMaghrib = new DateTimePicker();
            lblManIsha = new Label();
            dtpIsha = new DateTimePicker();

            pnlAutoFields.SuspendLayout();
            SuspendLayout();

            // 
            // lblSectionTitle — 🕐 مواعيد الصلاة
            // 
            lblSectionTitle.BackColor = Color.Transparent;
            lblSectionTitle.Dock = DockStyle.Top;
            lblSectionTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblSectionTitle.ForeColor = Color.FromArgb(230, 237, 243);
            lblSectionTitle.Location = new Point(0, 0);
            lblSectionTitle.Name = "lblSectionTitle";
            lblSectionTitle.Padding = new Padding(4, 0, 0, 0);
            lblSectionTitle.Size = new Size(310, 32);
            lblSectionTitle.TabIndex = 0;
            lblSectionTitle.Text = "🕐 مواعيد الصلاة";
            lblSectionTitle.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // radAuto — تلقائي من الإنترنت
            // 
            radAuto.AutoSize = true;
            radAuto.CheckedState.BorderColor = Color.FromArgb(27, 138, 107);
            radAuto.CheckedState.FillColor = Color.FromArgb(27, 138, 107);
            radAuto.Checked = true;
            radAuto.Font = new Font("Segoe UI", 9F);
            radAuto.ForeColor = Color.FromArgb(230, 237, 243);
            radAuto.Location = new Point(12, 36);
            radAuto.Name = "radAuto";
            radAuto.Size = new Size(160, 21);
            radAuto.TabIndex = 1;
            radAuto.Text = "تلقائي من الإنترنت";

            // 
            // radManual — إدخال يدوي
            // 
            radManual.AutoSize = true;
            radManual.CheckedState.BorderColor = Color.FromArgb(27, 138, 107);
            radManual.CheckedState.FillColor = Color.FromArgb(27, 138, 107);
            radManual.Font = new Font("Segoe UI", 9F);
            radManual.ForeColor = Color.FromArgb(230, 237, 243);
            radManual.Location = new Point(180, 36);
            radManual.Name = "radManual";
            radManual.Size = new Size(110, 21);
            radManual.TabIndex = 2;
            radManual.Text = "إدخال يدوي";

            // 
            // pnlAutoFields
            // 
            pnlAutoFields.BackColor = Color.Transparent;
            pnlAutoFields.Controls.Add(lblCountry);
            pnlAutoFields.Controls.Add(cboCountry);
            pnlAutoFields.Controls.Add(lblCity);
            pnlAutoFields.Controls.Add(cboCity);
            pnlAutoFields.Controls.Add(lblMethod);
            pnlAutoFields.Controls.Add(cboMethod);
            pnlAutoFields.Location = new Point(0, 64);
            pnlAutoFields.Name = "pnlAutoFields";
            pnlAutoFields.Size = new Size(310, 200);
            pnlAutoFields.TabIndex = 3;

            // 
            // lblCountry
            // 
            lblCountry.BackColor = Color.Transparent;
            lblCountry.Font = new Font("Segoe UI", 8.5F);
            lblCountry.ForeColor = Color.FromArgb(139, 148, 158);
            lblCountry.Location = new Point(12, 8);
            lblCountry.Name = "lblCountry";
            lblCountry.Size = new Size(280, 16);
            lblCountry.TabIndex = 0;
            lblCountry.Text = "الدولة";

            // 
            // cboCountry
            // 
            cboCountry.BackColor = Color.Transparent;
            cboCountry.BorderRadius = 8;
            cboCountry.CustomizableEdges = customizableEdges1;
            cboCountry.DrawMode = DrawMode.OwnerDrawFixed;
            cboCountry.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCountry.FillColor = Color.FromArgb(13, 17, 23);
            cboCountry.FocusedColor = Color.FromArgb(27, 138, 107);
            cboCountry.Font = new Font("Segoe UI", 9F);
            cboCountry.ForeColor = Color.FromArgb(230, 237, 243);
            cboCountry.ItemHeight = 24;
            cboCountry.Location = new Point(12, 26);
            cboCountry.Name = "cboCountry";
            cboCountry.ShadowDecoration.CustomizableEdges = customizableEdges2;
            cboCountry.Size = new Size(286, 30);
            cboCountry.TabIndex = 1;

            // 
            // lblCity
            // 
            lblCity.BackColor = Color.Transparent;
            lblCity.Font = new Font("Segoe UI", 8.5F);
            lblCity.ForeColor = Color.FromArgb(139, 148, 158);
            lblCity.Location = new Point(12, 66);
            lblCity.Name = "lblCity";
            lblCity.Size = new Size(280, 16);
            lblCity.TabIndex = 2;
            lblCity.Text = "المدينة";

            // 
            // cboCity
            // 
            cboCity.BackColor = Color.Transparent;
            cboCity.BorderRadius = 8;
            cboCity.CustomizableEdges = customizableEdges3;
            cboCity.DrawMode = DrawMode.OwnerDrawFixed;
            cboCity.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCity.FillColor = Color.FromArgb(13, 17, 23);
            cboCity.FocusedColor = Color.FromArgb(27, 138, 107);
            cboCity.Font = new Font("Segoe UI", 9F);
            cboCity.ForeColor = Color.FromArgb(230, 237, 243);
            cboCity.ItemHeight = 24;
            cboCity.Location = new Point(12, 84);
            cboCity.Name = "cboCity";
            cboCity.ShadowDecoration.CustomizableEdges = customizableEdges4;
            cboCity.Size = new Size(286, 30);
            cboCity.TabIndex = 3;

            // 
            // lblMethod
            // 
            lblMethod.BackColor = Color.Transparent;
            lblMethod.Font = new Font("Segoe UI", 8.5F);
            lblMethod.ForeColor = Color.FromArgb(139, 148, 158);
            lblMethod.Location = new Point(12, 124);
            lblMethod.Name = "lblMethod";
            lblMethod.Size = new Size(280, 16);
            lblMethod.TabIndex = 4;
            lblMethod.Text = "طريقة الحساب";

            // 
            // cboMethod
            // 
            cboMethod.BackColor = Color.Transparent;
            cboMethod.BorderRadius = 8;
            cboMethod.CustomizableEdges = customizableEdges5;
            cboMethod.DrawMode = DrawMode.OwnerDrawFixed;
            cboMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMethod.FillColor = Color.FromArgb(13, 17, 23);
            cboMethod.FocusedColor = Color.FromArgb(27, 138, 107);
            cboMethod.Font = new Font("Segoe UI", 9F);
            cboMethod.ForeColor = Color.FromArgb(230, 237, 243);
            cboMethod.ItemHeight = 24;
            cboMethod.Location = new Point(12, 142);
            cboMethod.Name = "cboMethod";
            cboMethod.ShadowDecoration.CustomizableEdges = customizableEdges6;
            cboMethod.Size = new Size(286, 30);
            cboMethod.TabIndex = 5;

            // 
            // pnlManualFields — 5 DateTimePickers لكل صلاة
            // 
            pnlManualFields.BackColor = Color.Transparent;
            pnlManualFields.Controls.Add(lblManFajr);
            pnlManualFields.Controls.Add(dtpFajr);
            pnlManualFields.Controls.Add(lblManDhuhr);
            pnlManualFields.Controls.Add(dtpDhuhr);
            pnlManualFields.Controls.Add(lblManAsr);
            pnlManualFields.Controls.Add(dtpAsr);
            pnlManualFields.Controls.Add(lblManMaghrib);
            pnlManualFields.Controls.Add(dtpMaghrib);
            pnlManualFields.Controls.Add(lblManIsha);
            pnlManualFields.Controls.Add(dtpIsha);
            pnlManualFields.Location = new Point(0, 64);
            pnlManualFields.Name = "pnlManualFields";
            pnlManualFields.Size = new Size(310, 220);
            pnlManualFields.TabIndex = 4;
            pnlManualFields.Visible = false;

            // 
            // lblManFajr
            // 
            lblManFajr.BackColor = Color.Transparent;
            lblManFajr.Font = new Font("Segoe UI", 9F);
            lblManFajr.ForeColor = Color.FromArgb(230, 237, 243);
            lblManFajr.Location = new Point(12, 10);
            lblManFajr.Name = "lblManFajr";
            lblManFajr.Size = new Size(110, 20);
            lblManFajr.Text = "🌅 الفجر";
            // 
            // dtpFajr
            // 
            dtpFajr.CustomFormat = "hh:mm tt";
            dtpFajr.Font = new Font("Segoe UI", 9F);
            dtpFajr.Format = DateTimePickerFormat.Custom;
            dtpFajr.Location = new Point(130, 6);
            dtpFajr.Name = "dtpFajr";
            dtpFajr.ShowUpDown = true;
            dtpFajr.Size = new Size(160, 26);
            // 
            // lblManDhuhr
            // 
            lblManDhuhr.BackColor = Color.Transparent;
            lblManDhuhr.Font = new Font("Segoe UI", 9F);
            lblManDhuhr.ForeColor = Color.FromArgb(230, 237, 243);
            lblManDhuhr.Location = new Point(12, 46);
            lblManDhuhr.Name = "lblManDhuhr";
            lblManDhuhr.Size = new Size(110, 20);
            lblManDhuhr.Text = "☀️ الظهر";
            // 
            // dtpDhuhr
            // 
            dtpDhuhr.CustomFormat = "hh:mm tt";
            dtpDhuhr.Font = new Font("Segoe UI", 9F);
            dtpDhuhr.Format = DateTimePickerFormat.Custom;
            dtpDhuhr.Location = new Point(130, 42);
            dtpDhuhr.Name = "dtpDhuhr";
            dtpDhuhr.ShowUpDown = true;
            dtpDhuhr.Size = new Size(160, 26);
            // 
            // lblManAsr
            // 
            lblManAsr.BackColor = Color.Transparent;
            lblManAsr.Font = new Font("Segoe UI", 9F);
            lblManAsr.ForeColor = Color.FromArgb(230, 237, 243);
            lblManAsr.Location = new Point(12, 82);
            lblManAsr.Name = "lblManAsr";
            lblManAsr.Size = new Size(110, 20);
            lblManAsr.Text = "🌤️ العصر";
            // 
            // dtpAsr
            // 
            dtpAsr.CustomFormat = "hh:mm tt";
            dtpAsr.Font = new Font("Segoe UI", 9F);
            dtpAsr.Format = DateTimePickerFormat.Custom;
            dtpAsr.Location = new Point(130, 78);
            dtpAsr.Name = "dtpAsr";
            dtpAsr.ShowUpDown = true;
            dtpAsr.Size = new Size(160, 26);
            // 
            // lblManMaghrib
            // 
            lblManMaghrib.BackColor = Color.Transparent;
            lblManMaghrib.Font = new Font("Segoe UI", 9F);
            lblManMaghrib.ForeColor = Color.FromArgb(230, 237, 243);
            lblManMaghrib.Location = new Point(12, 118);
            lblManMaghrib.Name = "lblManMaghrib";
            lblManMaghrib.Size = new Size(110, 20);
            lblManMaghrib.Text = "🌅 المغرب";
            // 
            // dtpMaghrib
            // 
            dtpMaghrib.CustomFormat = "hh:mm tt";
            dtpMaghrib.Font = new Font("Segoe UI", 9F);
            dtpMaghrib.Format = DateTimePickerFormat.Custom;
            dtpMaghrib.Location = new Point(130, 114);
            dtpMaghrib.Name = "dtpMaghrib";
            dtpMaghrib.ShowUpDown = true;
            dtpMaghrib.Size = new Size(160, 26);
            // 
            // lblManIsha
            // 
            lblManIsha.BackColor = Color.Transparent;
            lblManIsha.Font = new Font("Segoe UI", 9F);
            lblManIsha.ForeColor = Color.FromArgb(230, 237, 243);
            lblManIsha.Location = new Point(12, 154);
            lblManIsha.Name = "lblManIsha";
            lblManIsha.Size = new Size(110, 20);
            lblManIsha.Text = "🌙 العشاء";
            // 
            // dtpIsha
            // 
            dtpIsha.CustomFormat = "hh:mm tt";
            dtpIsha.Font = new Font("Segoe UI", 9F);
            dtpIsha.Format = DateTimePickerFormat.Custom;
            dtpIsha.Location = new Point(130, 150);
            dtpIsha.Name = "dtpIsha";
            dtpIsha.ShowUpDown = true;
            dtpIsha.Size = new Size(160, 26);

            // 
            // ucSettingsPrayer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.Transparent;
            Controls.Add(pnlManualFields);
            Controls.Add(pnlAutoFields);
            Controls.Add(radManual);
            Controls.Add(radAuto);
            Controls.Add(lblSectionTitle);
            Name = "ucSettingsPrayer";
            Size = new Size(310, 300);

            pnlAutoFields.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        // ── Designer Fields ──
        private Label lblSectionTitle;
        private Guna.UI2.WinForms.Guna2RadioButton radAuto;
        private Guna.UI2.WinForms.Guna2RadioButton radManual;
        private Panel pnlAutoFields;
        private Label lblCountry;
        private Guna.UI2.WinForms.Guna2ComboBox cboCountry;
        private Label lblCity;
        private Guna.UI2.WinForms.Guna2ComboBox cboCity;
        private Label lblMethod;
        private Guna.UI2.WinForms.Guna2ComboBox cboMethod;
        private Panel pnlManualFields;
        private Label lblManFajr;
        private Label lblManDhuhr;
        private Label lblManAsr;
        private Label lblManMaghrib;
        private Label lblManIsha;
        private DateTimePicker dtpFajr;
        private DateTimePicker dtpDhuhr;
        private DateTimePicker dtpAsr;
        private DateTimePicker dtpMaghrib;
        private DateTimePicker dtpIsha;
    }
}

