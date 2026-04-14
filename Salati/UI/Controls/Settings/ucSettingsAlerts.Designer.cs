namespace UI.Controls.Settings
{
    partial class ucSettingsAlerts
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

            lblSectionTitle = new Label();
            pnlAlertRows = new Panel();
            alertRowFajr = new ucAlertRow();
            alertRowDhuhr = new ucAlertRow();
            alertRowAsr = new ucAlertRow();
            alertRowMaghrib = new ucAlertRow();
            alertRowIsha = new ucAlertRow();
            lblVolume = new Label();
            trackVolume = new Guna.UI2.WinForms.Guna2TrackBar();
            lblVolumeValue = new Label();
            lblAlertType = new Label();
            cboAlertType = new Guna.UI2.WinForms.Guna2ComboBox();

            pnlAlertRows.SuspendLayout();
            SuspendLayout();

            // 
            // lblSectionTitle
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
            lblSectionTitle.Text = "\ud83d\udd14 \u0627\u0644\u062a\u0646\u0628\u064a\u0647\u0627\u062a";
            lblSectionTitle.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // pnlAlertRows
            // 
            pnlAlertRows.AutoScroll = true;
            pnlAlertRows.BackColor = Color.Transparent;
            pnlAlertRows.Controls.Add(alertRowFajr);
            pnlAlertRows.Controls.Add(alertRowDhuhr);
            pnlAlertRows.Controls.Add(alertRowAsr);
            pnlAlertRows.Controls.Add(alertRowMaghrib);
            pnlAlertRows.Controls.Add(alertRowIsha);
            pnlAlertRows.Location = new Point(0, 36);
            pnlAlertRows.Name = "pnlAlertRows";
            pnlAlertRows.Size = new Size(310, 270);
            pnlAlertRows.TabIndex = 1;

            // 
            // alertRowFajr
            // 
            alertRowFajr.Dock = DockStyle.Top;
            alertRowFajr.Location = new Point(0, 0);
            alertRowFajr.Name = "alertRowFajr";
            alertRowFajr.Size = new Size(310, 50);
            alertRowFajr.TabIndex = 0;

            // 
            // alertRowDhuhr
            // 
            alertRowDhuhr.Dock = DockStyle.Top;
            alertRowDhuhr.Location = new Point(0, 50);
            alertRowDhuhr.Name = "alertRowDhuhr";
            alertRowDhuhr.Size = new Size(310, 50);
            alertRowDhuhr.TabIndex = 1;

            // 
            // alertRowAsr
            // 
            alertRowAsr.Dock = DockStyle.Top;
            alertRowAsr.Location = new Point(0, 100);
            alertRowAsr.Name = "alertRowAsr";
            alertRowAsr.Size = new Size(310, 50);
            alertRowAsr.TabIndex = 2;

            // 
            // alertRowMaghrib
            // 
            alertRowMaghrib.Dock = DockStyle.Top;
            alertRowMaghrib.Location = new Point(0, 150);
            alertRowMaghrib.Name = "alertRowMaghrib";
            alertRowMaghrib.Size = new Size(310, 50);
            alertRowMaghrib.TabIndex = 3;

            // 
            // alertRowIsha
            // 
            alertRowIsha.Dock = DockStyle.Top;
            alertRowIsha.Location = new Point(0, 200);
            alertRowIsha.Name = "alertRowIsha";
            alertRowIsha.Size = new Size(310, 50);
            alertRowIsha.TabIndex = 4;

            // 
            // lblVolume
            // 
            lblVolume.BackColor = Color.Transparent;
            lblVolume.Font = new Font("Segoe UI", 8.5F);
            lblVolume.ForeColor = Color.FromArgb(139, 148, 158);
            lblVolume.Location = new Point(12, 316);
            lblVolume.Name = "lblVolume";
            lblVolume.Size = new Size(120, 16);
            lblVolume.TabIndex = 2;
            lblVolume.Text = "\ud83d\udd0a \u0645\u0633\u062a\u0648\u0649 \u0627\u0644\u0635\u0648\u062a";

            // 
            // trackVolume
            // 
            trackVolume.FillColor = Color.FromArgb(40, 27, 138, 107);
            trackVolume.HoverState.ThumbColor = Color.FromArgb(27, 138, 107);
            trackVolume.Location = new Point(12, 336);
            trackVolume.Maximum = 100;
            trackVolume.Name = "trackVolume";
            trackVolume.Size = new Size(230, 23);
            trackVolume.TabIndex = 3;
            trackVolume.ThumbColor = Color.FromArgb(27, 138, 107);
            trackVolume.Value = 80;

            // 
            // lblVolumeValue
            // 
            lblVolumeValue.BackColor = Color.Transparent;
            lblVolumeValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblVolumeValue.ForeColor = Color.FromArgb(200, 169, 110);
            lblVolumeValue.Location = new Point(248, 336);
            lblVolumeValue.Name = "lblVolumeValue";
            lblVolumeValue.Size = new Size(50, 20);
            lblVolumeValue.TabIndex = 4;
            lblVolumeValue.Text = "80%";
            lblVolumeValue.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // lblAlertType
            // 
            lblAlertType.BackColor = Color.Transparent;
            lblAlertType.Font = new Font("Segoe UI", 8.5F);
            lblAlertType.ForeColor = Color.FromArgb(139, 148, 158);
            lblAlertType.Location = new Point(12, 370);
            lblAlertType.Name = "lblAlertType";
            lblAlertType.Size = new Size(120, 16);
            lblAlertType.TabIndex = 5;
            lblAlertType.Text = "\u0646\u0648\u0639 \u0627\u0644\u062a\u0646\u0628\u064a\u0647";

            // 
            // cboAlertType
            // 
            cboAlertType.BackColor = Color.Transparent;
            cboAlertType.BorderRadius = 8;
            cboAlertType.CustomizableEdges = customizableEdges1;
            cboAlertType.DrawMode = DrawMode.OwnerDrawFixed;
            cboAlertType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboAlertType.FillColor = Color.FromArgb(13, 17, 23);
            cboAlertType.FocusedColor = Color.FromArgb(27, 138, 107);
            cboAlertType.Font = new Font("Segoe UI", 9F);
            cboAlertType.ForeColor = Color.FromArgb(230, 237, 243);
            cboAlertType.ItemHeight = 24;
            cboAlertType.Location = new Point(12, 388);
            cboAlertType.Name = "cboAlertType";
            cboAlertType.ShadowDecoration.CustomizableEdges = customizableEdges2;
            cboAlertType.Size = new Size(286, 30);
            cboAlertType.TabIndex = 6;

            // 
            // ucSettingsAlerts
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.Transparent;
            Controls.Add(cboAlertType);
            Controls.Add(lblAlertType);
            Controls.Add(lblVolumeValue);
            Controls.Add(trackVolume);
            Controls.Add(lblVolume);
            Controls.Add(pnlAlertRows);
            Controls.Add(lblSectionTitle);
            Name = "ucSettingsAlerts";
            Size = new Size(310, 440);

            pnlAlertRows.ResumeLayout(false);
            ResumeLayout(false);
        }

        // -- Designer Fields --
        private Label lblSectionTitle;
        private Panel pnlAlertRows;
        private ucAlertRow alertRowFajr;
        private ucAlertRow alertRowDhuhr;
        private ucAlertRow alertRowAsr;
        private ucAlertRow alertRowMaghrib;
        private ucAlertRow alertRowIsha;
        private Label lblVolume;
        private Guna.UI2.WinForms.Guna2TrackBar trackVolume;
        private Label lblVolumeValue;
        private Label lblAlertType;
        private Guna.UI2.WinForms.Guna2ComboBox cboAlertType;
    }
}
