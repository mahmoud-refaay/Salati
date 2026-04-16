using BLL.Extensions;
using BLL.Services;
using DAL.Enums;
using DAL.Logging;

namespace UI.Core
{
    /// <summary>
    /// جدولة التنبيهات — يشتغل في الخلفية كل 30 ثانية.
    /// 
    /// ═══ كيف يشتغل ═══
    ///   Timer كل 30 ثانية:
    ///     → AlertService.CheckForAlertAsync()
    ///     → لو في تنبيه → يطلعه (صوت + نافذة أو إشعار)
    ///     → يسجّل في AlertLog
    /// 
    /// ═══ الاستخدام ═══
    ///   clsAlertScheduler.Start(notifyIcon);
    ///   clsAlertScheduler.Stop();
    /// 
    /// ═══ RAM Usage ═══
    ///   Timer فقط — ~0.5MB — لا يستهلك موارد
    /// </summary>
    public static class clsAlertScheduler
    {
        private static System.Windows.Forms.Timer? _timer;
        private static NotifyIcon? _notifyIcon;
        private static readonly AlertService _service = new();

        // لمنع تكرار التنبيه لنفس الصلاة
        private static string _lastAlertKey = "";

        /// <summary>يبدأ الفحص كل 30 ثانية</summary>
        public static void Start(NotifyIcon notifyIcon)
        {
            _notifyIcon = notifyIcon;

            _timer?.Stop();
            _timer?.Dispose();
            _timer = new System.Windows.Forms.Timer
            {
                Interval = 30_000 // 30 ثانية
            };
            _timer.Tick += async (s, e) => await CheckAndAlertAsync();
            _timer.Start();

            clsLogger.Info("[AlertScheduler] Started — checking every 30 seconds");
        }

        /// <summary>يوقّف الجدولة</summary>
        public static void Stop()
        {
            _timer?.Stop();
            _timer?.Dispose();
            _timer = null;
            clsLogger.Info("[AlertScheduler] Stopped");
        }

        private static async Task CheckAndAlertAsync()
        {
            try
            {
                var alert = await _service.CheckForAlertAsync();
                if (alert == null) return;

                var (prayer, alertType, minutesBefore, prayerTime) = alert.Value;

                // مفتاح فريد لمنع التكرار
                string key = $"{DateTime.Today:yyyyMMdd}_{prayer}_{minutesBefore}";
                if (key == _lastAlertKey) return;
                _lastAlertKey = key;

                // ── طلّع التنبيه! ──
                string emoji = prayer.GetEmoji();
                string prayerName = prayer.ToArabicName();

                string title, body;
                if (minutesBefore > 0)
                {
                    title = $"{emoji} تنبيه — {prayerName}";
                    body = $"باقي {minutesBefore} دقيقة على صلاة {prayerName}";
                }
                else
                {
                    title = $"{emoji} حان وقت صلاة {prayerName}";
                    body = $"الله أكبر — حيّ على الصلاة 🕌";
                }

                // 1️⃣ إشعار Windows
                _notifyIcon?.ShowBalloonTip(10000, title, body, ToolTipIcon.Info);

                // 2️⃣ فتح frmAlert (لو AlertType = AdhanSound)
                if (alertType == 1) // AdhanSound
                {
                    // فتح نافذة التنبيه على الـ UI thread
                    if (Application.OpenForms.Count > 0)
                    {
                        var mainForm = Application.OpenForms[0];
                        mainForm?.Invoke(() =>
                        {
                        var frmAlert = new Forms.frmAlert();
                            // Cast DAL enum → UI enum (same byte values)
                            var uiPrayer = (UI.Core.ePrayer)(byte)prayer;
                            frmAlert.SetPrayer(uiPrayer, TimeOnly.FromTimeSpan(prayerTime));
                            frmAlert.Show();
                        });
                    }
                }

                // 3️⃣ سجّل في الـ Log
                await _service.LogAlertAsync(prayer, prayerTime, alertType, minutesBefore);

                clsLogger.Info($"[Alert] {prayerName} — {(minutesBefore > 0 ? $"قبل {minutesBefore} دقيقة" : "وقت الأذان")}");
            }
            catch (Exception ex)
            {
                clsLogger.Error("[AlertScheduler] Error during check", ex);
            }
        }
    }
}
