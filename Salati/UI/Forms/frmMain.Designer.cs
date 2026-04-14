namespace UI.Forms
{
    partial class frmMain
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
            components = new System.ComponentModel.Container();

            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();

            pnlMainBg = new Guna.UI2.WinForms.Guna2GradientPanel();
            titleBar = new Controls.Layout.ucTitleBar();
            infoBar = new Controls.Layout.ucInfoBar();
            pnlDashboard = new Panel();
            nextPrayer = new Controls.Card.ucNextPrayer();
            pnlPrayerCards = new FlowLayoutPanel();
            settingsPanel = new Controls.Settings.ucSettingsPanel();
            pnlOverlay = new Panel();
            lblWidgetInfo = new Label();
            notifyIcon = new NotifyIcon(components);
            trayMenu = new ContextMenuStrip(components);
            trayHeaderItem = new ToolStripMenuItem();
            traySep1 = new ToolStripSeparator();
            trayOpenItem = new ToolStripMenuItem();
            trayWidgetItem = new ToolStripMenuItem();
            traySettingsItem = new ToolStripMenuItem();
            traySep2 = new ToolStripSeparator();
            trayExitItem = new ToolStripMenuItem();

            pnlMainBg.SuspendLayout();
            pnlDashboard.SuspendLayout();
            SuspendLayout();

            // 
            // pnlMainBg — خلفية gradient
            // 
            pnlMainBg.Controls.Add(pnlDashboard);
            pnlMainBg.Controls.Add(lblWidgetInfo);
            pnlMainBg.Controls.Add(pnlOverlay);
            pnlMainBg.Controls.Add(settingsPanel);
            pnlMainBg.Controls.Add(infoBar);
            pnlMainBg.Controls.Add(titleBar);
            pnlMainBg.CustomizableEdges = customizableEdges1;
            pnlMainBg.Dock = DockStyle.Fill;
            pnlMainBg.FillColor = Color.FromArgb(13, 17, 23);
            pnlMainBg.FillColor2 = Color.FromArgb(18, 22, 30);
            pnlMainBg.Location = new Point(0, 0);
            pnlMainBg.Name = "pnlMainBg";
            pnlMainBg.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pnlMainBg.Size = new Size(700, 500);
            pnlMainBg.TabIndex = 0;

            // 
            // titleBar — Dock.Top
            // 
            titleBar.Dock = DockStyle.Top;
            titleBar.Location = new Point(0, 0);
            titleBar.Name = "titleBar";
            titleBar.Size = new Size(700, 42);
            titleBar.TabIndex = 0;

            // 
            // infoBar — Dock.Bottom
            // 
            infoBar.Dock = DockStyle.Bottom;
            infoBar.Location = new Point(0, 468);
            infoBar.Name = "infoBar";
            infoBar.Size = new Size(700, 32);
            infoBar.TabIndex = 1;

            // 
            // pnlDashboard — المحتوى
            // 
            pnlDashboard.BackColor = Color.Transparent;
            pnlDashboard.Controls.Add(nextPrayer);
            pnlDashboard.Controls.Add(pnlPrayerCards);
            pnlDashboard.Dock = DockStyle.Fill;
            pnlDashboard.Location = new Point(0, 42);
            pnlDashboard.Name = "pnlDashboard";
            pnlDashboard.Padding = new Padding(16, 10, 16, 6);
            pnlDashboard.Size = new Size(700, 426);
            pnlDashboard.TabIndex = 2;

            // 
            // nextPrayer — Hero Card (أعلى الداشبورد)
            // 
            nextPrayer.Dock = DockStyle.Top;
            nextPrayer.Location = new Point(16, 10);
            nextPrayer.Name = "nextPrayer";
            nextPrayer.Size = new Size(668, 200);
            nextPrayer.TabIndex = 0;

            // 
            // pnlPrayerCards — FlowLayout لـ 5 كروت
            // 
            pnlPrayerCards.BackColor = Color.Transparent;
            pnlPrayerCards.Dock = DockStyle.Fill;
            pnlPrayerCards.FlowDirection = FlowDirection.LeftToRight;
            pnlPrayerCards.Location = new Point(16, 210);
            pnlPrayerCards.Name = "pnlPrayerCards";
            pnlPrayerCards.Padding = new Padding(10, 8, 10, 0);
            pnlPrayerCards.Size = new Size(668, 216);
            pnlPrayerCards.TabIndex = 1;
            pnlPrayerCards.WrapContents = false;

            // 
            // settingsPanel — مخفي — يسلايد من اليمين
            // 
            settingsPanel.Location = new Point(700, 42);
            settingsPanel.Name = "settingsPanel";
            settingsPanel.Size = new Size(420, 458);
            settingsPanel.TabIndex = 3;
            settingsPanel.Visible = false;

            // 
            // pnlOverlay — شفاف خلف Settings
            // 
            pnlOverlay.BackColor = Color.FromArgb(120, 0, 0, 0);
            pnlOverlay.Dock = DockStyle.Fill;
            pnlOverlay.Location = new Point(0, 42);
            pnlOverlay.Name = "pnlOverlay";
            pnlOverlay.Size = new Size(700, 426);
            pnlOverlay.TabIndex = 4;
            pnlOverlay.Visible = false;

            //
            // lblWidgetInfo — يظهر في Widget Mode فقط
            //
            lblWidgetInfo.BackColor = Color.Transparent;
            lblWidgetInfo.Dock = DockStyle.Fill;
            lblWidgetInfo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblWidgetInfo.ForeColor = Color.FromArgb(200, 169, 110);
            lblWidgetInfo.Name = "lblWidgetInfo";
            lblWidgetInfo.TextAlign = ContentAlignment.MiddleCenter;
            lblWidgetInfo.Text = "";
            lblWidgetInfo.Visible = false;

            // 
            // notifyIcon — System Tray
            // 
            notifyIcon.Text = "Salati — صلاتي";
            notifyIcon.Visible = false;
            notifyIcon.ContextMenuStrip = trayMenu;

            // 
            // trayMenu
            // 
            trayMenu.Items.AddRange(new ToolStripItem[] { trayHeaderItem, traySep1, trayOpenItem, trayWidgetItem, traySettingsItem, traySep2, trayExitItem });
            trayMenu.Name = "trayMenu";
            trayMenu.Size = new Size(200, 180);
            // 
            // trayHeaderItem
            // 
            trayHeaderItem.Enabled = false;
            trayHeaderItem.Name = "trayHeaderItem";
            trayHeaderItem.Text = "\ud83d\udd4c Salati";
            // 
            // trayOpenItem
            // 
            trayOpenItem.Name = "trayOpenItem";
            trayOpenItem.Text = "\ud83d\udcc2 Open";
            // 
            // trayWidgetItem
            // 
            trayWidgetItem.Name = "trayWidgetItem";
            trayWidgetItem.Text = "\ud83d\udccc Widget";
            // 
            // traySettingsItem
            // 
            traySettingsItem.Name = "traySettingsItem";
            traySettingsItem.Text = "\u2699\ufe0f Settings";
            // 
            // trayExitItem
            // 
            trayExitItem.Name = "trayExitItem";
            trayExitItem.Text = "\u274c Exit";

            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(13, 17, 23);
            ClientSize = new Size(700, 500);
            Controls.Add(pnlMainBg);
            FormBorderStyle = FormBorderStyle.None;
            MinimizeBox = true;
            Name = "frmMain";
            ShowInTaskbar = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Salati";

            pnlMainBg.ResumeLayout(false);
            pnlDashboard.ResumeLayout(false);
            ResumeLayout(false);
        }

        // ── Designer Fields ──
        private Guna.UI2.WinForms.Guna2GradientPanel pnlMainBg;
        private Controls.Layout.ucTitleBar titleBar;
        private Controls.Layout.ucInfoBar infoBar;
        private Panel pnlDashboard;
        private Controls.Card.ucNextPrayer nextPrayer;
        private FlowLayoutPanel pnlPrayerCards;
        private Controls.Settings.ucSettingsPanel settingsPanel;
        private Panel pnlOverlay;
        private Label lblWidgetInfo;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip trayMenu;
        private ToolStripMenuItem trayHeaderItem;
        private ToolStripSeparator traySep1;
        private ToolStripMenuItem trayOpenItem;
        private ToolStripMenuItem trayWidgetItem;
        private ToolStripMenuItem traySettingsItem;
        private ToolStripSeparator traySep2;
        private ToolStripMenuItem trayExitItem;
    }
}
