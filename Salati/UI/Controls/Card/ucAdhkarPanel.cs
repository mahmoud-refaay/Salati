using BLL.Services;
using DAL.DTOs;
using UI.Core;
using UI.Core.Theme;
using UI.Core.Language;
using UI.Core.Animation;

namespace UI.Controls.Card
{
    /// <summary>
    /// لوحة أذكار الصباح/المساء — أذكار مع عداد لكل ذكر.
    /// مربوطة بـ BLL → DAL → DB (Dapper).
    /// 
    /// ╔═══════════════════════════════════════════════╗
    /// ║  🌅 أذكار الصباح                        [✕] ║
    /// ╠═══════════════════════════════════════════════╣
    /// ║  [N × ucAdhkarRow — كل واحد فيه عداد]       ║
    /// ╠═══════════════════════════════════════════════╣
    /// ║  📊 تم إنجاز: ████████░░ 7/10               ║
    /// ╚═══════════════════════════════════════════════╝
    /// </summary>
    public partial class ucAdhkarPanel : UserControl, IThemeable, ILocalizable
    {
        // ═══════════════════════════════════════
        //  Fields
        // ═══════════════════════════════════════

        private readonly List<ucAdhkarRow> _rows = [];
        private readonly AdhkarService _service = new();
        private bool _isMorning = true;

        // ═══════════════════════════════════════
        //  Events
        // ═══════════════════════════════════════

        /// <summary>المستخدم عايز يقفل اللوحة</summary>
        public event EventHandler? CloseRequested;

        // ═══════════════════════════════════════
        //  Constructor
        // ═══════════════════════════════════════

        public ucAdhkarPanel()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            btnClose.Click += (s, e) => CloseRequested?.Invoke(this, EventArgs.Empty);
            clsAnimationManager.AttachHoverScale(btnClose, 1.1f);
        }

        // ═══════════════════════════════════════
        //  Public API — Async! 🆕
        // ═══════════════════════════════════════

        /// <summary>يحمّل أذكار الصباح من الداتابيز</summary>
        public async Task LoadMorningAdhkarAsync()
        {
            _isMorning = true;
            var result = await _service.GetMorningAdhkarAsync();

            if (result.IsSuccess)
                BuildRows(result.Data!);
            else
                lblProgress.Text = $"⚠️ {result.Error}";
        }

        /// <summary>يحمّل أذكار المساء من الداتابيز</summary>
        public async Task LoadEveningAdhkarAsync()
        {
            _isMorning = false;
            var result = await _service.GetEveningAdhkarAsync();

            if (result.IsSuccess)
                BuildRows(result.Data!);
            else
                lblProgress.Text = $"⚠️ {result.Error}";
        }

        // ═══════════════════════════════════════
        //  Internal — Build Rows from DTOs
        // ═══════════════════════════════════════

        private void BuildRows(List<AdhkarDTO> data)
        {
            // مسح القديم
            foreach (var row in _rows)
            {
                row.TapClicked -= OnRowTap;
                row.Completed -= OnRowCompleted;
            }
            pnlRows.Controls.Clear();
            _rows.Clear();

            // بناء الجديد من DTOs
            foreach (var item in data)
            {
                var row = new ucAdhkarRow
                {
                    AdhkarText = item.TextAr,
                    Source = item.Source ?? "",
                    RepeatCount = item.RepeatCount,
                    Size = new Size(370, 72),
                    Margin = new Padding(0, 0, 0, 4),
                };

                row.TapClicked += OnRowTap;
                row.Completed += OnRowCompleted;
                _rows.Add(row);
                pnlRows.Controls.Add(row);
            }

            UpdateProgress();

            // تطبيق الثيم على الصفوف الجديدة
            Core.Engine.clsUIEngine.ApplyAll(this);
        }

        private void OnRowTap(object? sender, EventArgs e)
        {
            UpdateProgress();
        }

        private void OnRowCompleted(object? sender, EventArgs e)
        {
            UpdateProgress();

            // لو كل الأذكار اكتملت
            if (_rows.TrueForAll(r => r.IsCompleted))
            {
                lblProgress.Text = "✅ الحمد لله — اكتملت الأذكار!";
                progressBar.Value = 100;
            }
        }

        private void UpdateProgress()
        {
            int completed = _rows.Count(r => r.IsCompleted);
            int total = _rows.Count;

            lblProgress.Text = $"📊 تم إنجاز: {completed}/{total}";

            progressBar.Value = total > 0
                ? (int)((float)completed / total * 100)
                : 0;
        }

        // ═══════════════════════════════════════
        //  Slide Animation (نفس نمط Settings/Tracking)
        // ═══════════════════════════════════════

        public void SlideIn()
        {
            if (this.Parent == null)
                return;

            this.Visible = true;
            this.Left = this.Parent.ClientSize.Width;
            clsAnimationManager.SlideIn(this, eSlideDirection.FromRight, 350, eEasing.EaseOut);
        }

        public void SlideOut()
        {
            clsAnimationManager.SlideOut(this, eSlideDirection.FromRight, 300, eEasing.EaseIn);
        }

        // ═══════════════════════════════════════
        //  IThemeable
        // ═══════════════════════════════════════

        public void ApplyTheme(ThemeColors t)
        {
            pnlBackground.FillColor = t.BgPrimary;
            pnlBackground.ShadowDecoration.Color = Color.FromArgb(80, 0, 0, 0);

            lblTitle.ForeColor = t.TextPrimary;
            lblSubtitle.ForeColor = t.TextSecondary;
            btnClose.ForeColor = t.TextMuted;
            btnClose.HoverState.ForeColor = t.Danger;

            pnlRows.BackColor = Color.Transparent;

            pnlProgress.FillColor = t.BgSurface;
            progressBar.FillColor = ThemeColorUtils.WithAlpha(t.Accent1, 30);
            progressBar.ProgressColor = t.Accent1;
            progressBar.ProgressColor2 = t.Accent2;
            lblProgress.ForeColor = t.Accent2;
        }

        // ═══════════════════════════════════════
        //  ILocalizable
        // ═══════════════════════════════════════

        public void ApplyLanguage(ILanguagePack lang)
        {
            lblTitle.Text = _isMorning
                ? $"🌅 {lang.AdhkarMorningTitle}"
                : $"🌇 {lang.AdhkarEveningTitle}";
            lblSubtitle.Text = lang.AdhkarTap;
        }
    }
}
