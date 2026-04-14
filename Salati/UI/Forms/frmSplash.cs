using UI.Core.Engine;
using UI.Core.Theme;

namespace UI.Forms
{
    /// <summary>
    /// شاشة التحميل - Borderless, CenterScreen.
    /// تعرض Logo + ProgressBar + آية قرآنية.
    /// 
    /// Flow: Program.cs -> frmSplash -> (async load) -> frmMain
    /// 
    /// === أنيمشن Sequence ===
    /// 1. Form FadeIn
    /// 2. 🕌 Slide من أعلى + FadeIn
    /// 3. "Salati" FadeIn
    /// 4. "صلاتي" FadeIn
    /// 5. الآية FadeIn
    /// 6. Progress Bar + Loading
    /// 7. عند 100%: 🕌 Pulse
    /// 8. Form FadeOut
    /// </summary>
    public partial class frmSplash : Form
    {
        // ===== Constructor =====

        public frmSplash()
        {
            InitializeComponent();

            var t = clsThemeManager.Colors;
            pnlBackground.FillColor = t.BgPrimary;
            pnlBackground.FillColor2 = t.BgSecondary;
            lblMosque.ForeColor = t.Accent1;
            lblAppName.ForeColor = t.TextPrimary;
            lblAppNameAr.ForeColor = t.Accent2;
            lblQuran.ForeColor = t.TextMuted;
            progressBar.ProgressColor = t.Accent1;
            progressBar.ProgressColor2 = t.Accent2;
            progressBar.FillColor = ThemeColorUtils.WithAlpha(t.Accent1, 30);
            lblLoading.ForeColor = t.TextMuted;
            lblVersion.ForeColor = ThemeColorUtils.Darken(t.TextMuted, 10);

            // كل حاجة مخفية في البداية — هتظهر بالأنيمشن
            this.Opacity = 0;
            lblMosque.Visible = false;
            lblAppName.Visible = false;
            lblAppNameAr.Visible = false;
            lblQuran.Visible = false;
            progressBar.Visible = false;
            lblLoading.Visible = false;
            lblVersion.Visible = false;
        }

        // ===== OnShown (Async Entry Point) =====

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);

            try
            {
                // ═════════ Entrance Sequence ═════════

                // 1. Form fade in
                await FadeFormAsync(0, 1, 400);

                // 2. 🕌 يظهر من أعلى — slide down + fade
                await AnimateControlEntrance(lblMosque, -40, 350);

                // 3. "Salati" يظهر بـ fade
                await AnimateControlEntrance(lblAppName, -20, 250);

                // 4. "صلاتي" يظهر بـ fade
                await AnimateControlEntrance(lblAppNameAr, -15, 200);

                // 5. الآية القرآنية
                await AnimateControlEntrance(lblQuran, -10, 200);

                // 6. Progress bar + loading text + version
                progressBar.Value = 0;
                progressBar.Visible = true;
                lblLoading.Visible = true;
                lblVersion.Visible = true;

                // ═════════ Loading ═════════
                await LoadApplicationAsync();

                // 7. 🕌 Pulse عند الانتهاء
                await PulseAsync(lblMosque, 400);

                // 8. Fade out
                await Task.Delay(200);
                await FadeFormAsync(1, 0, 350);

                // فتح الداشبورد
                OpenMainForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Startup Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        // ═════════════════════════════════════════════════════
        //  أنيمشن: Entrance — عنصر ينزل من أعلى مع fade
        // ═════════════════════════════════════════════════════

        /// <summary>
        /// يظهر control مع slide من أعلى + fade in.
        /// offset: الإزاحة الابتدائية (سالب = فوق)
        /// </summary>
        private Task AnimateControlEntrance(Control ctrl, int offsetY, int durationMs)
        {
            var tcs = new TaskCompletionSource();
            var targetY = ctrl.Top;

            ctrl.Top = targetY + offsetY;
            ctrl.Visible = true;

            // نستخدم BackColor alpha simulation عبر ForeColor opacity
            var targetColor = ctrl.ForeColor;
            ctrl.ForeColor = Color.FromArgb(0, targetColor);

            int frames = Math.Max(1, durationMs / 16);
            int frame = 0;

            var timer = new System.Windows.Forms.Timer { Interval = 16 };
            timer.Tick += (s, e) =>
            {
                frame++;
                float t = EaseOutCubic((float)frame / frames);

                // Slide
                ctrl.Top = targetY + (int)(offsetY * (1 - t));

                // Fade (ForeColor alpha)
                int alpha = Math.Clamp((int)(255 * t), 0, 255);
                ctrl.ForeColor = Color.FromArgb(alpha, targetColor);

                if (frame >= frames)
                {
                    ctrl.Top = targetY;
                    ctrl.ForeColor = targetColor;
                    timer.Stop();
                    timer.Dispose();
                    tcs.SetResult();
                }
            };
            timer.Start();
            return tcs.Task;
        }

        // ═════════════════════════════════════════════════════
        //  أنيمشن: Form Fade (smooth timer)
        // ═════════════════════════════════════════════════════

        private Task FadeFormAsync(double from, double to, int durationMs)
        {
            var tcs = new TaskCompletionSource();
            this.Opacity = from;

            int frames = Math.Max(1, durationMs / 16);
            int frame = 0;
            double delta = to - from;

            var timer = new System.Windows.Forms.Timer { Interval = 16 };
            timer.Tick += (s, e) =>
            {
                frame++;
                float t = EaseOutCubic((float)frame / frames);
                this.Opacity = from + delta * t;

                if (frame >= frames)
                {
                    this.Opacity = to;
                    timer.Stop();
                    timer.Dispose();
                    tcs.SetResult();
                }
            };
            timer.Start();
            return tcs.Task;
        }

        // ═════════════════════════════════════════════════════
        //  أنيمشن: Pulse (تكبير ثم رجوع)
        // ═════════════════════════════════════════════════════

        private Task PulseAsync(Control ctrl, int durationMs)
        {
            var tcs = new TaskCompletionSource();
            var origSize = ctrl.Size;
            var origLoc = ctrl.Location;
            float scale = 1.15f;

            int frames = Math.Max(1, durationMs / 16);
            int frame = 0;

            var timer = new System.Windows.Forms.Timer { Interval = 16 };
            timer.Tick += (s, e) =>
            {
                frame++;
                float progress = (float)frame / frames;
                // Ping-pong: 0→1→0
                float t = progress < 0.5f
                    ? EaseOutCubic(progress * 2f)
                    : EaseOutCubic((1f - progress) * 2f);

                float s2 = 1f + (scale - 1f) * t;
                int dw = (int)(origSize.Width * (s2 - 1));
                int dh = (int)(origSize.Height * (s2 - 1));

                ctrl.Size = new Size(origSize.Width + dw, origSize.Height + dh);
                ctrl.Location = new Point(origLoc.X - dw / 2, origLoc.Y - dh / 2);

                if (frame >= frames)
                {
                    ctrl.Size = origSize;
                    ctrl.Location = origLoc;
                    timer.Stop();
                    timer.Dispose();
                    tcs.SetResult();
                }
            };
            timer.Start();
            return tcs.Task;
        }

        // ═════════════════════════════════════════════════════
        //  Easing
        // ═════════════════════════════════════════════════════

        private static float EaseOutCubic(float t) => 1f - MathF.Pow(1f - t, 3f);

        // ═════════════════════════════════════════════════════
        //  Loading Logic
        // ═════════════════════════════════════════════════════

        private async Task LoadApplicationAsync()
        {
            var progress = new Progress<(int percent, string message)>(report =>
            {
                progressBar.Value = report.percent;
                lblLoading.Text = report.message;
            });

            await Task.Run(async () =>
            {
                var p = (IProgress<(int, string)>)progress;

                p.Report((10, "تهيئة النظام..."));
                await Task.Delay(300);

                p.Report((30, "تحميل الإعدادات..."));
                // TODO: BLL - clsSettingsStore.LoadAll()
                await Task.Delay(300);

                p.Report((55, "تحميل مواعيد الصلاة..."));
                // TODO: BLL - clsPrayerTimeManager.LoadTodayTimes()
                await Task.Delay(300);

                p.Report((80, "تجهيز التنبيهات..."));
                // TODO: BLL - clsAlertScheduler.Initialize()
                await Task.Delay(200);

                p.Report((100, "جاهز ✓"));
                await Task.Delay(200);
            });
        }

        // ===== Open Main Form =====

        private void OpenMainForm()
        {
            var main = new frmMain();
            this.Hide();
            main.FormClosed += (s, e) => this.Close();
            main.Show();
        }
    }
}
