namespace UI.Core.Theme
{
    /// <summary>
    /// أدوات مساعدة للألوان — Lighten, Darken, Alpha.
    /// تُستخدم في BuiltInThemes وفي أي مكان محتاج يعدّل لون.
    /// </summary>
    public static class ThemeColorUtils
    {
        /// <summary>يفتّح اللون بنسبة (0-100)</summary>
        public static Color Lighten(Color color, int percent)
        {
            float factor = percent / 100f;
            int r = (int)(color.R + (255 - color.R) * factor);
            int g = (int)(color.G + (255 - color.G) * factor);
            int b = (int)(color.B + (255 - color.B) * factor);
            return Color.FromArgb(color.A, Clamp(r), Clamp(g), Clamp(b));
        }

        /// <summary>يغمّق اللون بنسبة (0-100)</summary>
        public static Color Darken(Color color, int percent)
        {
            float factor = 1 - (percent / 100f);
            int r = (int)(color.R * factor);
            int g = (int)(color.G * factor);
            int b = (int)(color.B * factor);
            return Color.FromArgb(color.A, Clamp(r), Clamp(g), Clamp(b));
        }

        /// <summary>يغيّر الشفافية (0=شفاف, 255=صلب)</summary>
        public static Color WithAlpha(Color color, int alpha)
        {
            return Color.FromArgb(Clamp(alpha), color.R, color.G, color.B);
        }

        /// <summary>يخلط لونين مع بعض بنسبة (0.0-1.0 للون الأول)</summary>
        public static Color Mix(Color color1, Color color2, float ratio)
        {
            float inv = 1f - ratio;
            int r = (int)(color1.R * ratio + color2.R * inv);
            int g = (int)(color1.G * ratio + color2.G * inv);
            int b = (int)(color1.B * ratio + color2.B * inv);
            return Color.FromArgb(255, Clamp(r), Clamp(g), Clamp(b));
        }

        private static int Clamp(int value) => Math.Max(0, Math.Min(255, value));
    }
}
