namespace UI.Core.Theme
{
    /// <summary>
    /// أي كنترول أو Form عايز يستجيب للثيم لازم يطبق الواجهة دي.
    /// ممنوع استخدام ألوان hardcoded — كل حاجة من ThemeColors.
    /// </summary>
    public interface IThemeable
    {
        void ApplyTheme(ThemeColors theme);
    }
}
