using UI.Core.Language.Languages;

namespace UI.Core.Language
{
    /// <summary>
    /// مدير اللغات — مسؤول عن:
    /// 1. تخزين اللغة الحالية
    /// 2. تبديل اللغات (AR ↔ EN)
    /// 3. إبلاغ كل الـ controls عبر Event
    /// 4. ضبط اتجاه النص (RTL / LTR)
    /// 5. تطبيق اللغة على شجرة controls
    /// </summary>
    public static class clsLanguageManager
    {
        // ═══════════════════════════════════════
        //  اللغات المتاحة
        // ═══════════════════════════════════════

        private static readonly ArabicPack _arabic = new();
        private static readonly EnglishPack _english = new();

        // ═══════════════════════════════════════
        //  الحالة الحالية
        // ═══════════════════════════════════════

        private static ILanguagePack _current = _arabic; // الافتراضي: عربي

        /// <summary>حزمة اللغة الحالية</summary>
        public static ILanguagePack Current => _current;

        /// <summary>هل RTL الحالي؟</summary>
        public static bool IsRtl => _current.IsRtl;

        /// <summary>كود اللغة الحالي</summary>
        public static string Code => _current.LanguageCode;

        // ═══════════════════════════════════════
        //  Event — بيتنفّر لما اللغة تتغير
        // ═══════════════════════════════════════

        /// <summary>يُطلق عند تغيير اللغة</summary>
        public static event Action<ILanguagePack>? LanguageChanged;

        // ═══════════════════════════════════════
        //  العمليات
        // ═══════════════════════════════════════

        /// <summary>يطبّق لغة بالكود</summary>
        public static void ApplyLanguage(string languageCode)
        {
            _current = languageCode.ToLower() switch
            {
                "ar" => _arabic,
                "en" => _english,
                _ => _arabic
            };

            LanguageChanged?.Invoke(_current);
        }

        /// <summary>يبدّل بين عربي وإنجليزي</summary>
        public static void ToggleLanguage()
        {
            if (_current.LanguageCode == "ar")
                ApplyLanguage("en");
            else
                ApplyLanguage("ar");
        }

        /// <summary>
        /// يطبّق اللغة الحالية على control وكل أولاده.
        /// يمشي على الشجرة recursively + يضبط RTL.
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
            // RightToLeft يتحط على Form فقط (Inherit default)
            // النصوص تتطبق على ILocalizable فقط
            if (control is ILocalizable localizable)
            {
                localizable.ApplyLanguage(_current);
            }
        }

        /// <summary>يضبط RTL/LTR على Form كاملة</summary>
        public static void ApplyDirectionToForm(Form form)
        {
            form.RightToLeft = _current.IsRtl ? RightToLeft.Yes : RightToLeft.No;
            form.RightToLeftLayout = _current.IsRtl;
        }
    }
}
