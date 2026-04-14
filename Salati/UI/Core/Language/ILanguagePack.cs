namespace UI.Core.Language
{
    /// <summary>
    /// العقد الأساسي لحزمة اللغة — partial interface.
    /// كل قسم من التطبيق ليه partial خاص بيه.
    /// </summary>
    public partial interface ILanguagePack
    {
        /// <summary>كود اللغة (ar / en)</summary>
        string LanguageCode { get; }

        /// <summary>اسم اللغة بالعربي (العربية / الإنجليزية)</summary>
        string LanguageNameAr { get; }

        /// <summary>اسم اللغة بالإنجليزي (Arabic / English)</summary>
        string LanguageNameEn { get; }

        /// <summary>هل RTL؟</summary>
        bool IsRtl { get; }
    }
}
