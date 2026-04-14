namespace UI.Core.Theme
{
    /// <summary>
    /// يحتوي على كل ألوان الثيم — أي عنصر UI بيقرأ ألوانه من هنا.
    /// ممنوع نهائياً استخدام Color.FromArgb() مباشرة في أي كنترول.
    /// </summary>
    public class ThemeColors
    {
        // ═══════════════════════════════════════
        //  ألوان الخلفيات
        // ═══════════════════════════════════════

        /// <summary>خلفية النافذة الرئيسية</summary>
        public Color BgPrimary { get; set; }

        /// <summary>خلفية الـ Title Bar</summary>
        public Color BgSecondary { get; set; }

        /// <summary>خلفية الـ Cards والـ Panels</summary>
        public Color BgSurface { get; set; }

        /// <summary>خلفية Cards الفاتحة</summary>
        public Color BgCard { get; set; }

        // ═══════════════════════════════════════
        //  الألوان المميزة (Accent)
        // ═══════════════════════════════════════

        /// <summary>أخضر إسلامي — اللون الأساسي</summary>
        public Color Accent1 { get; set; }

        /// <summary>ذهبي — لمسة فخامة</summary>
        public Color Accent2 { get; set; }

        /// <summary>أحمر — للأخطاء والتحذيرات</summary>
        public Color Accent3 { get; set; }

        // ═══════════════════════════════════════
        //  ألوان النصوص
        // ═══════════════════════════════════════

        /// <summary>النص الأساسي (عناوين)</summary>
        public Color TextPrimary { get; set; }

        /// <summary>النص الثانوي (labels فرعية)</summary>
        public Color TextSecondary { get; set; }

        /// <summary>النص الخافت (placeholders, descriptions)</summary>
        public Color TextMuted { get; set; }

        /// <summary>النص المميز (links, active items)</summary>
        public Color TextAccent { get; set; }

        // ═══════════════════════════════════════
        //  ألوان الحدود
        // ═══════════════════════════════════════

        /// <summary>حدود عادية (شفافة أخضر خفيف)</summary>
        public Color BorderDefault { get; set; }

        /// <summary>حدود عند Hover</summary>
        public Color BorderHover { get; set; }

        /// <summary>حدود عند Focus</summary>
        public Color BorderFocused { get; set; }

        // ═══════════════════════════════════════
        //  ألوان الحالات
        // ═══════════════════════════════════════

        /// <summary>نجاح (أخضر)</summary>
        public Color Success { get; set; }

        /// <summary>تحذير (أصفر)</summary>
        public Color Warning { get; set; }

        /// <summary>خطأ (أحمر)</summary>
        public Color Danger { get; set; }

        /// <summary>معلومات (أزرق)</summary>
        public Color Info { get; set; }

        // ═══════════════════════════════════════
        //  ألوان الأزرار (Guna2GradientButton)
        // ═══════════════════════════════════════

        /// <summary>لون الزر الأول — FillColor (أخضر)</summary>
        public Color GradientBtn1 { get; set; }

        /// <summary>لون الزر الثاني — FillColor2 (ذهبي)</summary>
        public Color GradientBtn2 { get; set; }

        /// <summary>لون الزر عند Hover</summary>
        public Color GradientBtnHover1 { get; set; }

        /// <summary>لون الزر عند Hover (اللون الثاني)</summary>
        public Color GradientBtnHover2 { get; set; }

        /// <summary>لون الزر عند الضغط</summary>
        public Color GradientBtnPressed1 { get; set; }

        /// <summary>لون الزر عند الضغط (اللون الثاني)</summary>
        public Color GradientBtnPressed2 { get; set; }

        // ═══════════════════════════════════════
        //  ألوان Gradient Panels (Guna2GradientPanel — 2 ألوان)
        // ═══════════════════════════════════════

        /// <summary>Gradient Panel الرئيسي (خلفية النافذة) — FillColor</summary>
        public Color GradientPanelMain1 { get; set; }
        /// <summary>FillColor2</summary>
        public Color GradientPanelMain2 { get; set; }

        /// <summary>Gradient Panel للـ Hero Card (الصلاة القادمة) — FillColor</summary>
        public Color GradientPanelHero1 { get; set; }
        /// <summary>FillColor2</summary>
        public Color GradientPanelHero2 { get; set; }

        /// <summary>Gradient Panel للـ Title Bar — FillColor</summary>
        public Color GradientPanelTitle1 { get; set; }
        /// <summary>FillColor2</summary>
        public Color GradientPanelTitle2 { get; set; }

        // ═══════════════════════════════════════
        //  الظل (Guna2 ShadowDecoration)
        // ═══════════════════════════════════════

        /// <summary>لون الظل للـ Cards</summary>
        public Color ShadowColorCard { get; set; }

        /// <summary>عمق الظل للـ Cards (1-10)</summary>
        public int ShadowDepthCard { get; set; }

        /// <summary>لون الظل للـ Buttons</summary>
        public Color ShadowColorButton { get; set; }

        /// <summary>عمق الظل للـ Buttons (1-10)</summary>
        public int ShadowDepthButton { get; set; }

        /// <summary>لون الظل للـ Alert Popup</summary>
        public Color ShadowColorAlert { get; set; }

        /// <summary>عمق الظل للـ Alert (أكبر — بارز)</summary>
        public int ShadowDepthAlert { get; set; }

        // ═══════════════════════════════════════
        //  ألوان الإدخال (Input)
        // ═══════════════════════════════════════

        /// <summary>خلفية حقول الإدخال</summary>
        public Color InputBg { get; set; }

        /// <summary>نص حقول الإدخال</summary>
        public Color InputText { get; set; }

        /// <summary>Placeholder text</summary>
        public Color InputPlaceholder { get; set; }
    }
}
