using BLL.Services;
using BLL.Extensions;

namespace UI.Core
{
    /// <summary>
    /// خدمة الأذكار — تدير Timer في الخلفية وتطلع إشعارات.
    /// مربوطة بـ BLL → DAL → DB (Dapper).
    /// 
    /// ══ الفرق بين الأنواع ══
    ///   📿 Category 1,2,3,4 → إشعارات عشوائية (هذه الخدمة)
    ///   🌅 Category 5       → أذكار الصباح (ucAdhkarPanel)
    ///   🌇 Category 6       → أذكار المساء (ucAdhkarPanel)
    /// 
    /// ══ الاستخدام ══
    ///   await clsAdhkarService.StartAsync(notifyIcon);  // يقرأ الفترة من DB
    ///   clsAdhkarService.Stop();
    /// </summary>
    public static class clsAdhkarService
    {
        // ═══════════════════════════════════════
        //  Fields
        // ═══════════════════════════════════════

        private static System.Windows.Forms.Timer? _timer;
        private static NotifyIcon? _notifyIcon;
        private static int _intervalMinutes = 30;
        private static readonly AdhkarService _adhkarService = new();
        private static readonly AppSettingsService _settingsService = new();

        // ═══════════════════════════════════════
        //  Public API
        // ═══════════════════════════════════════

        /// <summary>يشغّل الخدمة — يقرأ الفترة من الإعدادات</summary>
        public static async Task StartAsync(NotifyIcon notifyIcon)
        {
            _notifyIcon = notifyIcon;

            // قراءة الفترة من DB (إعداد المستخدم)
            _intervalMinutes = await _settingsService.GetIntAsync("AdhkarIntervalMinutes", 30);
            bool isEnabled = await _settingsService.GetBoolAsync("AdhkarNotificationsEnabled", true);

            if (!isEnabled)
            {
                DAL.Logging.clsLogger.Info("[AdhkarService] Notifications disabled by user");
                return;
            }

            _timer?.Stop();
            _timer?.Dispose();
            _timer = new System.Windows.Forms.Timer
            {
                Interval = _intervalMinutes * 60 * 1000
            };
            _timer.Tick += async (s, e) => await ShowRandomAdhkarAsync();
            _timer.Start();

            DAL.Logging.clsLogger.Info($"[AdhkarService] Started — every {_intervalMinutes} minutes");
        }

        /// <summary>يشغّل بفترة محددة (fallback لو DB مش جاهز)</summary>
        public static void Start(NotifyIcon notifyIcon, int intervalMinutes = 30)
        {
            _notifyIcon = notifyIcon;
            _intervalMinutes = intervalMinutes;

            _timer?.Stop();
            _timer?.Dispose();
            _timer = new System.Windows.Forms.Timer
            {
                Interval = _intervalMinutes * 60 * 1000
            };
            _timer.Tick += async (s, e) => await ShowRandomAdhkarAsync();
            _timer.Start();
        }

        /// <summary>يوقّف الخدمة</summary>
        public static void Stop()
        {
            _timer?.Stop();
            _timer?.Dispose();
            _timer = null;
        }

        /// <summary>يغيّر الفترة بالدقائق (من الإعدادات)</summary>
        public static async Task SetIntervalAsync(int minutes)
        {
            _intervalMinutes = Math.Max(1, minutes);
            if (_timer != null)
                _timer.Interval = _intervalMinutes * 60 * 1000;

            // حفظ في DB
            await _settingsService.SetAsync("AdhkarIntervalMinutes", minutes.ToString());
        }

        /// <summary>يعرض إشعار فوراً (للاختبار)</summary>
        public static async void ShowNow() => await ShowRandomAdhkarAsync();

        // ═══════════════════════════════════════
        //  Internal
        // ═══════════════════════════════════════

        private static async Task ShowRandomAdhkarAsync()
        {
            if (_notifyIcon == null) return;

            // يجيب ذكر عشوائي من Category 1,2,3,4 (مش 5,6)
            var result = await _adhkarService.GetRandomAdhkarAsync();

            if (!result.IsSuccess || result.Data == null) return;

            var item = result.Data;
            string emoji = item.Category.GetEmoji();

            string title = $"{emoji} صلاتي";
            string body = item.Source != null
                ? $"{item.TextAr}\n— {item.Source}"
                : item.TextAr;

            _notifyIcon.ShowBalloonTip(5000, title, body, ToolTipIcon.None);
        }
    }
}
