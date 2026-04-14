namespace UI.Core.Language
{
    /// <summary>
    /// أي كنترول أو Form عايز يستجيب للغة لازم يطبق الواجهة دي.
    /// ممنوع استخدام نصوص hardcoded — كل حاجة من ILanguagePack.
    /// </summary>
    public interface ILocalizable
    {
        void ApplyLanguage(ILanguagePack lang);
    }
}
