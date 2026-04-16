using BLL.Services;
using BLL.Extensions;

namespace UI.Core
{
    /// <summary>
    /// خدمة الأذكار — تدير Timer في الخلفية وتطلع إشعارات.
    /// مربوطة بـ BLL → DAL → DB (Dapper).
    /// 
    /// ══ الاستخدام ══
    /// clsAdhkarService.Start(notifyIcon, 30);  // كل 30 دقيقة
    /// clsAdhkarService.Stop();
    /// </summary>
    public static class clsAdhkarService
    {
        // ═══════════════════════════════════════
        //  Fields
        // ═══════════════════════════════════════

        private static System.Windows.Forms.Timer? _timer;
        private static NotifyIcon? _notifyIcon;
        private static int _intervalMinutes = 30;
        private static readonly AdhkarService _service = new();

        // ═══════════════════════════════════════
        //  Public API
        // ═══════════════════════════════════════

        /// <summary>يشغّل الخدمة — يتنادى مرة في frmMain constructor</summary>
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

        /// <summary>يغيّر الفترة بالدقائق</summary>
        public static void SetInterval(int minutes)
        {
            _intervalMinutes = Math.Max(1, minutes);
            if (_timer != null)
                _timer.Interval = _intervalMinutes * 60 * 1000;
        }

        /// <summary>يعرض إشعار فوراً (للاختبار)</summary>
        public static async void ShowNow() => await ShowRandomAdhkarAsync();

        // ═══════════════════════════════════════
        //  Internal — Async! 🆕
        // ═══════════════════════════════════════

        private static async Task ShowRandomAdhkarAsync()
        {
            if (_notifyIcon == null) return;

            // 🆕 BLL → DAL → DB (بدل Mock array!)
            var result = await _service.GetRandomAdhkarAsync();

            if (!result.IsSuccess || result.Data == null) return;

            var item = result.Data;
            string emoji = item.Category.GetEmoji(); // 🆕 Extension Method!

            string title = $"{emoji} صلاتي";
            string body = item.Source != null
                ? $"{item.TextAr}\n— {item.Source}"
                : item.TextAr;

            _notifyIcon.ShowBalloonTip(5000, title, body, ToolTipIcon.None);
        }
    }
}
