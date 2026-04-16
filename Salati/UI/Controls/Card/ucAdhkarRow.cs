using UI.Core;
using UI.Core.Theme;
using UI.Core.Language;
using UI.Core.Animation;

namespace UI.Controls.Card
{
    /// <summary>
    /// صف ذكر واحد — يعرض النص + المصدر + عداد + زر ➕.
    /// 
    /// ┌──────────────────────────────────────────────────────┐
    /// │  📿  "سبحان الله وبحمده"           [3/100]  [➕]    │
    /// │      — متفق عليه                                    │
    /// └──────────────────────────────────────────────────────┘
    /// </summary>
    public partial class ucAdhkarRow : UserControl, IThemeable, ILocalizable
    {
        // ═══════════════════════════════════════
        //  Fields
        // ═══════════════════════════════════════

        private int _repeatCount = 1;
        private int _currentCount;
        private bool _isCompleted;

        // ═══════════════════════════════════════
        //  Events
        // ═══════════════════════════════════════

        /// <summary>المستخدم ضغط ➕</summary>
        public event EventHandler? TapClicked;

        /// <summary>الذكر اكتمل (وصل للعدد المطلوب)</summary>
        public event EventHandler? Completed;

        // ═══════════════════════════════════════
        //  Constructor
        // ═══════════════════════════════════════

        public ucAdhkarRow()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            btnTap.Click += OnTapClick;
            clsAnimationManager.AttachHoverScale(btnTap, 1.08f);
        }

        // ═══════════════════════════════════════
        //  Properties
        // ═══════════════════════════════════════

        /// <summary>نص الذكر</summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string AdhkarText
        {
            get => lblAdhkarText.Text;
            set => lblAdhkarText.Text = value;
        }

        /// <summary>المصدر</summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string Source
        {
            get => lblSource.Text;
            set => lblSource.Text = string.IsNullOrEmpty(value) ? "" : $"— {value}";
        }

        /// <summary>عدد التكرار المطلوب</summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public int RepeatCount
        {
            get => _repeatCount;
            set
            {
                _repeatCount = Math.Max(1, value);
                UpdateCounterDisplay();
            }
        }

        /// <summary>العدد الحالي</summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public int CurrentCount
        {
            get => _currentCount;
            set
            {
                _currentCount = Math.Clamp(value, 0, _repeatCount);
                _isCompleted = _currentCount >= _repeatCount;
                UpdateCounterDisplay();
                UpdateCompletionState();
            }
        }

        /// <summary>هل اكتمل؟</summary>
        [System.ComponentModel.Browsable(false)]
        public bool IsCompleted => _isCompleted;

        // ═══════════════════════════════════════
        //  Tap Logic
        // ═══════════════════════════════════════

        private void OnTapClick(object? sender, EventArgs e)
        {
            if (_isCompleted) return;

            _currentCount++;
            _isCompleted = _currentCount >= _repeatCount;
            UpdateCounterDisplay();

            // نبضة صغيرة
            clsAnimationManager.Pulse(lblCounter, 150, 1.15f);

            TapClicked?.Invoke(this, EventArgs.Empty);

            if (_isCompleted)
            {
                UpdateCompletionState();
                Completed?.Invoke(this, EventArgs.Empty);
            }
        }

        // ═══════════════════════════════════════
        //  UI Updates
        // ═══════════════════════════════════════

        private void UpdateCounterDisplay()
        {
            lblCounter.Text = $"{_currentCount}/{_repeatCount}";
        }

        private void UpdateCompletionState()
        {
            if (_isCompleted)
            {
                lblEmoji.Text = "✅";
                btnTap.Text = "✓";
                btnTap.Enabled = false;
                btnTap.FillColor = Color.FromArgb(40, 27, 138, 107);
            }
            else
            {
                lblEmoji.Text = "📿";
                btnTap.Text = "➕";
                btnTap.Enabled = true;
            }
        }

        /// <summary>يعيد الصف لحالة البداية</summary>
        public void Reset()
        {
            _currentCount = 0;
            _isCompleted = false;
            UpdateCounterDisplay();
            UpdateCompletionState();
        }

        // ═══════════════════════════════════════
        //  IThemeable
        // ═══════════════════════════════════════

        public void ApplyTheme(ThemeColors t)
        {
            pnlRow.FillColor = t.BgSurface;
            pnlRow.BorderColor = ThemeColorUtils.WithAlpha(t.Accent1, 38);
            pnlRow.ShadowDecoration.Color = ThemeColorUtils.WithAlpha(t.Accent1, 40);

            lblAdhkarText.ForeColor = t.TextPrimary;
            lblSource.ForeColor = t.TextMuted;
            lblCounter.ForeColor = t.Accent2;

            btnTap.FillColor = t.Accent1;
            btnTap.ForeColor = Color.White;
            btnTap.HoverState.FillColor = ThemeColorUtils.Lighten(t.Accent1, 10);
        }

        // ═══════════════════════════════════════
        //  ILocalizable
        // ═══════════════════════════════════════

        public void ApplyLanguage(ILanguagePack lang)
        {
            if (!_isCompleted)
                btnTap.Text = "➕";
        }
    }
}
