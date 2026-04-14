namespace UI.Core.Language
{
    // نصوص الأطار — Title Bar + Info Bar
    public partial interface ILanguagePack
    {
        string LayoutAppTitle { get; }
        string LayoutMinimizeToTray { get; }
        string LayoutSettings { get; }
    }
}
