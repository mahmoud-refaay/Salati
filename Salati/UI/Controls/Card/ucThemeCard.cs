using UI.Core.Theme;
using UI.Core.Language;

namespace UI.Controls.Card
{
    /// <summary>
    /// كارت اختيار ثيم — يعرض preview ألوان + اسم + علامة تحديد.
    /// يُستخدم في Settings → Appearance.
    /// </summary>
    public partial class ucThemeCard : UserControl, IThemeable, ILocalizable
    {
        // ═══════════════════════════════════════
        //  Events
        // ═══════════════════════════════════════

        /// <summary>يُطلق عند اختيار الثيم</summary>
        public event EventHandler? ThemeSelected;

        // ═══════════════════════════════════════
        //  Fields
        // ═══════════════════════════════════════

        private ThemeDefinition? _themeDef;
        private bool _isSelected;

        // ═══════════════════════════════════════
        //  Constructor
        // ═══════════════════════════════════════

        public ucThemeCard()
        {
            InitializeComponent();
            pnlCard.Click += (s, e) => ThemeSelected?.Invoke(this, EventArgs.Empty);
            pnlPreview.Click += (s, e) => ThemeSelected?.Invoke(this, EventArgs.Empty);
            lblThemeName.Click += (s, e) => ThemeSelected?.Invoke(this, EventArgs.Empty);
        }

        // ═══════════════════════════════════════
        //  Properties
        // ═══════════════════════════════════════

        /// <summary>الثيم المعروض في الكارت</summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public ThemeDefinition? ThemeDef
        {
            get => _themeDef;
            set
            {
                _themeDef = value;
                if (_themeDef != null)
                {
                    lblThemeName.Text = _themeDef.Name;
                    pnlPreview.FillColor = _themeDef.Colors.BgPrimary;
                    pnlPreview.FillColor2 = _themeDef.Colors.Accent1;
                }
            }
        }

        /// <summary>هل الكارت محدد (selected)</summary>
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
        //  IThemeable
        // ═══════════════════════════════════════

        public void ApplyTheme(ThemeColors t)
        {
            pnlCard.FillColor = t.BgSurface;
            lblThemeName.ForeColor = t.TextPrimary;
            lblCheckmark.ForeColor = t.Accent1;
            pnlCard.BorderColor = _isSelected ? t.Accent1 : Color.Transparent;
        }

        // ═══════════════════════════════════════
        //  ILocalizable
        // ═══════════════════════════════════════

        public void ApplyLanguage(ILanguagePack lang)
        {
            // اسم الثيم ثابت بالإنجليزي — مش محتاج ترجمة
        }
    }
}
