namespace UI.Controls.Settings
{
    partial class ucAlertRow
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

            pnlRow = new Guna.UI2.WinForms.Guna2Panel();
            lblEmoji = new Label();
            lblPrayerName = new Label();
            cboMinutes = new Guna.UI2.WinForms.Guna2ComboBox();
            togEnabled = new Guna.UI2.WinForms.Guna2ToggleSwitch();

            pnlRow.SuspendLayout();
            SuspendLayout();

            // 
            // pnlRow
            // 
            pnlRow.Controls.Add(lblEmoji);
            pnlRow.Controls.Add(lblPrayerName);
            pnlRow.Controls.Add(cboMinutes);
            pnlRow.Controls.Add(togEnabled);
            pnlRow.CustomizableEdges = customizableEdges1;
            pnlRow.BorderRadius = 8;
            pnlRow.Dock = DockStyle.Top;
            pnlRow.FillColor = Color.FromArgb(22, 27, 34);
            pnlRow.Location = new Point(0, 0);
            pnlRow.Name = "pnlRow";
            pnlRow.Padding = new Padding(8, 0, 8, 0);
            pnlRow.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pnlRow.Size = new Size(300, 50);
            pnlRow.TabIndex = 0;

            // 
            // lblEmoji — 🌅
            // 
            lblEmoji.BackColor = Color.Transparent;
            lblEmoji.Font = new Font("Segoe UI Emoji", 16F);
            lblEmoji.Location = new Point(8, 8);
            lblEmoji.Name = "lblEmoji";
            lblEmoji.Size = new Size(34, 34);
            lblEmoji.TabIndex = 0;
            lblEmoji.Text = "🌅";
            lblEmoji.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // lblPrayerName — الفجر
            // 
            lblPrayerName.BackColor = Color.Transparent;
            lblPrayerName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPrayerName.ForeColor = Color.FromArgb(230, 237, 243);
            lblPrayerName.Location = new Point(46, 15);
            lblPrayerName.Name = "lblPrayerName";
            lblPrayerName.Size = new Size(70, 20);
            lblPrayerName.TabIndex = 1;
            lblPrayerName.Text = "الفجر";

            // 
            // cboMinutes — [10 دقائق ▾]
            // 
            cboMinutes.BackColor = Color.Transparent;
            cboMinutes.BorderRadius = 6;
            cboMinutes.CustomizableEdges = customizableEdges3;
            cboMinutes.DrawMode = DrawMode.OwnerDrawFixed;
            cboMinutes.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMinutes.FillColor = Color.FromArgb(13, 17, 23);
            cboMinutes.FocusedColor = Color.FromArgb(27, 138, 107);
            cboMinutes.Font = new Font("Segoe UI", 8.5F);
            cboMinutes.ForeColor = Color.FromArgb(230, 237, 243);
            cboMinutes.ItemHeight = 22;
            cboMinutes.Location = new Point(125, 12);
            cboMinutes.Name = "cboMinutes";
            cboMinutes.ShadowDecoration.CustomizableEdges = customizableEdges4;
            cboMinutes.Size = new Size(100, 28);
            cboMinutes.TabIndex = 2;

            // 
            // togEnabled — ON/OFF
            // 
            togEnabled.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            togEnabled.CheckedState.BorderColor = Color.FromArgb(27, 138, 107);
            togEnabled.CheckedState.FillColor = Color.FromArgb(27, 138, 107);
            togEnabled.CheckedState.InnerColor = Color.White;
            togEnabled.UncheckedState.BorderColor = Color.FromArgb(72, 79, 88);
            togEnabled.UncheckedState.FillColor = Color.FromArgb(40, 44, 52);
            togEnabled.UncheckedState.InnerColor = Color.FromArgb(139, 148, 158);
            togEnabled.Checked = true;
            togEnabled.Location = new Point(248, 14);
            togEnabled.Name = "togEnabled";
            togEnabled.Size = new Size(40, 22);
            togEnabled.TabIndex = 3;

            // 
            // ucAlertRow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(pnlRow);
            Margin = new Padding(0, 0, 0, 4);
            Name = "ucAlertRow";
            Size = new Size(300, 50);

            pnlRow.ResumeLayout(false);
            ResumeLayout(false);
        }

        // ── Designer Fields ──
        private Guna.UI2.WinForms.Guna2Panel pnlRow;
        private Label lblEmoji;
        private Label lblPrayerName;
        private Guna.UI2.WinForms.Guna2ComboBox cboMinutes;
        private Guna.UI2.WinForms.Guna2ToggleSwitch togEnabled;
    }
}
