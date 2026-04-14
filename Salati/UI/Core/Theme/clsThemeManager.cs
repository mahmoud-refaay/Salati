namespace UI.Core.Theme
{
    /// <summary>
    /// مدير الثيمات — مسؤول عن:
    /// 1. تخزين الثيم الحالي
    /// 2. تبديل الثيمات
    /// 3. إبلاغ كل الـ controls عبر Event
    /// 4. تطبيق الثيم على شجرة controls
    /// </summary>
    public static class clsThemeManager
    {
        // ═══════════════════════════════════════
        //  الحالة الحالية
        // ═══════════════════════════════════════

        private static ThemeDefinition _current = BuiltInThemes.Default;

        /// <summary>الثيم الحالي</summary>
        public static ThemeDefinition Current => _current;

        /// <summary>ألوان الثيم الحالي (shortcut)</summary>
        public static ThemeColors Colors => _current.Colors;

        /// <summary>هل الثيم الحالي داكن؟</summary>
        public static bool IsDark => _current.IsDark;

        // ═══════════════════════════════════════
        //  Event — بيتنفّر لما الثيم يتغير
        // ═══════════════════════════════════════

        /// <summary>يُطلق عند تغيير الثيم</summary>
        public static event Action<ThemeColors>? ThemeChanged;

        // ═══════════════════════════════════════
        //  العمليات
        // ═══════════════════════════════════════

        /// <summary>يطبّق ثيم جديد بالاسم</summary>
        public static void ApplyTheme(string themeName)
        {
            var theme = BuiltInThemes.GetByName(themeName);
            if (theme == null) return;

            _current = theme;
            ThemeChanged?.Invoke(_current.Colors);
        }

        /// <summary>يطبّق ثيم جديد مباشرة</summary>
        public static void ApplyTheme(ThemeDefinition theme)
        {
            _current = theme;
            ThemeChanged?.Invoke(_current.Colors);
        }

        /// <summary>يبدّل بين الثيمتين</summary>
        public static void ToggleTheme()
        {
            if (_current.Name == BuiltInThemes.MidnightSerenity.Name)
                ApplyTheme(BuiltInThemes.DesertSand);
            else
                ApplyTheme(BuiltInThemes.MidnightSerenity);
        }

        /// <summary>
        /// يطبّق الثيم الحالي على control وكل أولاده.
        /// يمشي على الشجرة recursively ويستدعي ApplyTheme على كل IThemeable.
        /// </summary>
        public static void ApplyToControlTree(Control root)
        {
            ApplyToControl(root);

            foreach (Control child in root.Controls)
            {
                ApplyToControlTree(child);
            }
        }

        private static void ApplyToControl(Control control)
        {
            if (control is IThemeable themeable)
            {
                themeable.ApplyTheme(_current.Colors);
            }
        }
    }
}
