namespace UI.Core.Theme
{
    /// <summary>
    /// الثيمات المدمجة — Midnight Serenity (داكن) + Desert Sand (صحراوي دافئ).
    /// كل ثيم فيه ألوان مطابقة للـ Design System.
    /// </summary>
    public static class BuiltInThemes
    {
        // ═══════════════════════════════════════
        //  Midnight Serenity — الثيم الداكن (افتراضي)
        //  أسود مزرق + أخضر إسلامي + ذهبي
        // ═══════════════════════════════════════
        public static ThemeDefinition MidnightSerenity => new(
            name: "Midnight Serenity",
            isDark: true,
            colors: new ThemeColors
            {
                // خلفيات — تباين محسّن
                BgPrimary       = Color.FromArgb(13, 17, 23),       // #0D1117
                BgSecondary     = Color.FromArgb(10, 14, 20),       // #0A0E14
                BgSurface       = Color.FromArgb(22, 27, 34),       // #161B22
                BgCard          = Color.FromArgb(30, 36, 44),       // تباين أحسن

                // ألوان مميزة
                Accent1         = Color.FromArgb(27, 138, 107),     // #1B8A6B — أخضر إسلامي
                Accent2         = Color.FromArgb(200, 169, 110),    // #C8A96E — ذهبي
                Accent3         = Color.FromArgb(224, 108, 117),    // #E06C75 — أحمر

                // نصوص
                TextPrimary     = Color.FromArgb(230, 237, 243),    // #E6EDF3
                TextSecondary   = Color.FromArgb(139, 148, 158),    // #8B949E
                TextMuted       = Color.FromArgb(72, 79, 88),       // #484F58
                TextAccent      = Color.FromArgb(27, 138, 107),     // #1B8A6B

                // حدود
                BorderDefault   = Color.FromArgb(38, 27, 138, 107),
                BorderHover     = Color.FromArgb(200, 169, 110),
                BorderFocused   = Color.FromArgb(27, 138, 107),

                // حالات
                Success         = Color.FromArgb(46, 160, 67),
                Warning         = Color.FromArgb(210, 153, 34),
                Danger          = Color.FromArgb(224, 108, 117),
                Info            = Color.FromArgb(88, 166, 255),

                // أزرار
                GradientBtn1        = Color.FromArgb(27, 138, 107),
                GradientBtn2        = Color.FromArgb(200, 169, 110),
                GradientBtnHover1   = Color.FromArgb(22, 110, 85),
                GradientBtnHover2   = Color.FromArgb(180, 149, 90),
                GradientBtnPressed1 = Color.FromArgb(17, 90, 70),
                GradientBtnPressed2 = Color.FromArgb(160, 130, 72),

                // Gradient Panels
                GradientPanelMain1  = Color.FromArgb(13, 17, 23),
                GradientPanelMain2  = Color.FromArgb(10, 14, 20),
                GradientPanelHero1  = Color.FromArgb(22, 27, 34),
                GradientPanelHero2  = Color.FromArgb(12, 50, 38),       // أخضر أقوى
                GradientPanelTitle1 = Color.FromArgb(10, 14, 20),
                GradientPanelTitle2 = Color.FromArgb(13, 17, 23),

                // Shadow — ظلال أعمق
                ShadowColorCard     = Color.FromArgb(70, 27, 138, 107),
                ShadowDepthCard     = 5,
                ShadowColorButton   = Color.FromArgb(80, 27, 138, 107),
                ShadowDepthButton   = 3,
                ShadowColorAlert    = Color.FromArgb(100, 200, 169, 110),
                ShadowDepthAlert    = 8,

                // إدخال
                InputBg         = Color.FromArgb(13, 17, 23),
                InputText       = Color.FromArgb(230, 237, 243),
                InputPlaceholder = Color.FromArgb(100, 72, 79, 88),
            }
        );

        // ═══════════════════════════════════════
        //  Desert Sand — ثيم صحراوي دافئ
        //  بني غامق + ذهبي + برتقالي — مريح للعين
        // ═══════════════════════════════════════
        public static ThemeDefinition DesertSand => new(
            name: "Desert Sand",
            isDark: true,
            colors: new ThemeColors
            {
                // خلفيات — بني صحراوي غامق
                BgPrimary       = Color.FromArgb(35, 30, 25),       // بني غامق دافئ
                BgSecondary     = Color.FromArgb(28, 23, 18),       // أغمق
                BgSurface       = Color.FromArgb(45, 38, 32),       // surface بني
                BgCard          = Color.FromArgb(50, 43, 36),       // كارت بني

                // ألوان مميزة — ذهبي + برتقالي دافئ
                Accent1         = Color.FromArgb(212, 168, 84),     // ذهبي دافئ
                Accent2         = Color.FromArgb(193, 123, 58),     // برتقالي صحراوي
                Accent3         = Color.FromArgb(180, 65, 55),      // أحمر طيني

                // نصوص — كريمي على بني
                TextPrimary     = Color.FromArgb(232, 221, 208),    // بيج فاتح
                TextSecondary   = Color.FromArgb(170, 155, 138),    // بني فاتح
                TextMuted       = Color.FromArgb(110, 98, 85),      // بني متوسط
                TextAccent      = Color.FromArgb(212, 168, 84),     // ذهبي

                // حدود
                BorderDefault   = Color.FromArgb(35, 212, 168, 84),
                BorderHover     = Color.FromArgb(212, 168, 84),
                BorderFocused   = Color.FromArgb(193, 123, 58),

                // حالات
                Success         = Color.FromArgb(120, 170, 68),
                Warning         = Color.FromArgb(212, 168, 84),
                Danger          = Color.FromArgb(180, 65, 55),
                Info            = Color.FromArgb(100, 160, 200),

                // أزرار — ذهبي → برتقالي
                GradientBtn1        = Color.FromArgb(212, 168, 84),
                GradientBtn2        = Color.FromArgb(193, 123, 58),
                GradientBtnHover1   = Color.FromArgb(190, 148, 68),
                GradientBtnHover2   = Color.FromArgb(170, 105, 45),
                GradientBtnPressed1 = Color.FromArgb(165, 128, 55),
                GradientBtnPressed2 = Color.FromArgb(150, 90, 35),

                // Gradient Panels
                GradientPanelMain1  = Color.FromArgb(35, 30, 25),
                GradientPanelMain2  = Color.FromArgb(28, 23, 18),
                GradientPanelHero1  = Color.FromArgb(45, 38, 32),
                GradientPanelHero2  = Color.FromArgb(55, 42, 25),       // لمسة ذهبية
                GradientPanelTitle1 = Color.FromArgb(28, 23, 18),
                GradientPanelTitle2 = Color.FromArgb(35, 30, 25),

                // Shadow — ظلال ذهبية دافئة
                ShadowColorCard     = Color.FromArgb(60, 212, 168, 84),
                ShadowDepthCard     = 5,
                ShadowColorButton   = Color.FromArgb(70, 212, 168, 84),
                ShadowDepthButton   = 3,
                ShadowColorAlert    = Color.FromArgb(90, 193, 123, 58),
                ShadowDepthAlert    = 8,

                // إدخال
                InputBg         = Color.FromArgb(30, 25, 20),
                InputText       = Color.FromArgb(232, 221, 208),
                InputPlaceholder = Color.FromArgb(90, 110, 98, 85),
            }
        );

        /// <summary>كل الثيمات المدمجة</summary>
        public static List<ThemeDefinition> All => [MidnightSerenity, DesertSand];

        /// <summary>الثيم الافتراضي</summary>
        public static ThemeDefinition Default => MidnightSerenity;

        /// <summary>يجيب ثيم بالاسم</summary>
        public static ThemeDefinition? GetByName(string name)
        {
            return All.FirstOrDefault(t =>
                t.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
