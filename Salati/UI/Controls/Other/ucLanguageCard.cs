using UI.Core.Theme;
using UI.Core.Language;

namespace UI.Controls.Other
{
    /// <summary>
    /// كارت اختيار لغة — يعرض العلم + الاسم + subtitle + علامة تحديد.
    /// يُستخدم في Settings → Appearance.
    /// 
    /// ┌─────────────────────────┐
    /// │ 🇸🇦  العربية          ✓ │
    /// │      Arabic              │
    /// └─────────────────────────┘
    /// 
    /// ── الاستخدام ──
    /// var card = new ucLanguageCard();
    /// card.SetLanguage("ar", "🇸🇦", "العربية", "Arabic");
    /// card.IsSelected = true;
    /// card.LanguageSelected += (s, e) => SwitchLanguage("ar");
    /// </summary>
    public partial class ucLanguageCard : UserControl, IThemeable, ILocalizable
    {
        // ═══════════════════════════════════════
        //  Events
        // ═══════════════════════════════════════

        /// <summary>يُطلق عند اختيار اللغة</summary>
        public event EventHandler? LanguageSelected;

        // ═══════════════════════════════════════
        //  Fields
        // ═══════════════════════════════════════

        private string _languageCode = "ar";
        private bool _isSelected;

        // ═══════════════════════════════════════
        //  Constructor
        // ═══════════════════════════════════════

        public ucLanguageCard()
        {
            InitializeComponent();

            // كل حاجة في الكارت clickable
            pnlCard.Click += OnCardClick;
            lblFlag.Click += OnCardClick;
            lblName.Click += OnCardClick;
            lblSub.Click += OnCardClick;
            lblCheckmark.Click += OnCardClick;
        }

        private void OnCardClick(object? sender, EventArgs e)
            => LanguageSelected?.Invoke(this, EventArgs.Empty);

        // ═══════════════════════════════════════
        //  Properties
        // ═══════════════════════════════════════

        /// <summary>كود اللغة (ar / en)</summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string LanguageCode
        {
            get => _languageCode;
            set => _languageCode = value;
        }

        /// <summary>هل الكارت محدد</summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                lblCheckmark.Visible = _isSelected;
                pnlCard.BorderColor = _isSelected
                    ? clsThemeManager.Colors.Accent1
                    : Color.Transparent;
            }
        }

        // ═══════════════════════════════════════
        //  Public Methods
        // ═══════════════════════════════════════

        /// <summary>ضبط بيانات اللغة</summary>
        public void SetLanguage(string code, string flag, string name, string sub)
        {
            _languageCode = code;
            lblFlag.Text = flag;
            lblName.Text = name;
            lblSub.Text = sub;
        }

        // ═══════════════════════════════════════
        //  IThemeable
        // ═══════════════════════════════════════

        public void ApplyTheme(ThemeColors t)
        {
            pnlCard.FillColor = t.BgSurface;
            pnlCard.ShadowDecoration.Color = t.ShadowColorCard;
            pnlCard.ShadowDecoration.Depth = Math.Max(1, t.ShadowDepthCard - 1);
            lblName.ForeColor = t.TextPrimary;
            lblSub.ForeColor = t.TextMuted;
            lblCheckmark.ForeColor = t.Accent1;
            pnlCard.BorderColor = _isSelected ? t.Accent1 : Color.Transparent;
        }

        // ═══════════════════════════════════════
        //  ILocalizable
        // ═══════════════════════════════════════

        public void ApplyLanguage(ILanguagePack lang)
        {
            // الأسماء تتحدد عند الإنشاء — مش محتاج ترجمة ديناميكية
        }
    }
}
