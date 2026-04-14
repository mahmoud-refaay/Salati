using UI.Core.Engine;
using System.Runtime.InteropServices;

namespace UI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // ════════════════════════════════════════
            //  1. تهيئة النظام (Theme + Language)
            // ════════════════════════════════════════
            // TODO: قراءة القيم المحفوظة من Registry
            clsUIEngine.Initialize(
                languageCode: "ar",
                themeName: "Midnight Serenity"
            );

            // ════════════════════════════════════════
            //  2. Global Error Handlers
            // ════════════════════════════════════════
            Application.ThreadException += (s, e) =>
            {
                // TODO: clsEventLogger.LogError(e.Exception);
                // TODO: clsErrorLogData.Insert(e.Exception);
                MessageBox.Show(e.Exception.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var ex = e.ExceptionObject as Exception;
                // TODO: clsEventLogger.LogError(ex);
                // TODO: clsErrorLogData.Insert(ex);
                MessageBox.Show(ex?.Message ?? "Unknown error", "Fatal Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            // ════════════════════════════════════════
            //  3. تشغيل التطبيق
            // ════════════════════════════════════════
            // frmSplash → (loading) → frmMain
            Application.Run(new Forms.frmSplash());

            // ══════════════════════════════════════════════
            //  🔊 TEST: تشغيل الأذان — (التيست الحالي في frmMain)
            // ══════════════════════════════════════════════
            // TestPlayAdhan();
        }

        // ════════════════════════════════════════════════════════════════
        //  🔊 TEST METHOD — تيست تشغيل الأذان (مؤقت — يتعمله كومنت بعد كده)
        //
        //  بيستخدم NAudio (MediaFoundationReader) — بيشغل m4a, mp3, wav
        //  الطريقة الاحترافية في .NET 6+
        // ════════════════════════════════════════════════════════════════
        private static void TestPlayAdhan()
        {
            string soundsPath = Path.Combine(Application.StartupPath, "Assets", "Sounds");

            // لو مفيش ملفات صوت
            if (!Directory.Exists(soundsPath))
            {
                MessageBox.Show($"مجلد الصوتيات مش موجود:\n{soundsPath}", "❌ خطأ");
                return;
            }

            // إيجاد كل ملفات الصوت
            var soundFiles = Directory.GetFiles(soundsPath, "*.*")
                .Where(f => f.EndsWith(".m4a") || f.EndsWith(".mp3") || f.EndsWith(".wav"))
                .ToArray();

            if (soundFiles.Length == 0)
            {
                MessageBox.Show("مفيش ملفات صوت (m4a/mp3/wav) في المجلد!", "❌ خطأ");
                return;
            }

            // ── NAudio Player ──
            NAudio.Wave.WaveOutEvent? outputDevice = null;
            NAudio.Wave.MediaFoundationReader? audioFile = null;

            // ── بناء فورم التيست ──
            var testForm = new Form
            {
                Text = "🔊 Adhan Test Player",
                Size = new Size(420, 250),
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                BackColor = Color.FromArgb(20, 25, 32),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10)
            };

            // ComboBox لاختيار الملف
            var lblFile = new Label { Text = "🎵 اختر ملف الأذان:", Location = new Point(20, 15), AutoSize = true };
            var cboFiles = new ComboBox
            {
                Location = new Point(20, 40),
                Size = new Size(360, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(30, 35, 45),
                ForeColor = Color.White
            };
            foreach (var f in soundFiles)
                cboFiles.Items.Add(Path.GetFileName(f));
            cboFiles.SelectedIndex = 0;

            // Volume slider
            var lblVol = new Label { Text = "🔊 مستوى الصوت:", Location = new Point(20, 80), AutoSize = true };
            var trackVolume = new TrackBar
            {
                Location = new Point(20, 105),
                Size = new Size(360, 30),
                Minimum = 0,
                Maximum = 100,
                Value = 70,
                BackColor = Color.FromArgb(20, 25, 32)
            };

            // Status label
            var lblStatus = new Label
            {
                Text = "⏸️ جاهز",
                Location = new Point(20, 145),
                Size = new Size(360, 25),
                ForeColor = Color.FromArgb(139, 233, 253)
            };

            // Play button
            var btnPlay = new Button
            {
                Text = "▶️ تشغيل",
                Location = new Point(20, 175),
                Size = new Size(170, 38),
                BackColor = Color.FromArgb(27, 138, 107),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnPlay.FlatAppearance.BorderSize = 0;

            // Stop button
            var btnStop = new Button
            {
                Text = "⏹️ إيقاف",
                Location = new Point(210, 175),
                Size = new Size(170, 38),
                BackColor = Color.FromArgb(224, 108, 117),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnStop.FlatAppearance.BorderSize = 0;

            // ── Volume change ──
            trackVolume.ValueChanged += (s, e) =>
            {
                if (outputDevice != null)
                    outputDevice.Volume = trackVolume.Value / 100f;
            };

            // ── Play ──
            btnPlay.Click += (s, e) =>
            {
                try
                {
                    // إيقاف أي صوت سابق
                    outputDevice?.Stop();
                    outputDevice?.Dispose();
                    audioFile?.Dispose();

                    // تشغيل الملف المختار
                    string selectedFile = Path.Combine(soundsPath, cboFiles.SelectedItem!.ToString()!);
                    audioFile = new NAudio.Wave.MediaFoundationReader(selectedFile);
                    outputDevice = new NAudio.Wave.WaveOutEvent();
                    outputDevice.Volume = trackVolume.Value / 100f;
                    outputDevice.Init(audioFile);
                    outputDevice.Play();

                    lblStatus.Text = $"🔊 بيشتغل: {cboFiles.SelectedItem}";
                    lblStatus.ForeColor = Color.FromArgb(80, 250, 123);
                }
                catch (Exception ex)
                {
                    lblStatus.Text = $"❌ خطأ: {ex.Message}";
                    lblStatus.ForeColor = Color.FromArgb(224, 108, 117);
                }
            };

            // ── Stop ──
            btnStop.Click += (s, e) =>
            {
                outputDevice?.Stop();
                lblStatus.Text = "⏹️ متوقف";
                lblStatus.ForeColor = Color.FromArgb(139, 233, 253);
            };

            // ── Cleanup on close ──
            testForm.FormClosed += (s, e) =>
            {
                outputDevice?.Stop();
                outputDevice?.Dispose();
                audioFile?.Dispose();
            };

            // Add controls
            testForm.Controls.AddRange(new Control[] { lblFile, cboFiles, lblVol, trackVolume, lblStatus, btnPlay, btnStop });

            // Show
            testForm.ShowDialog();
        }
    }
}