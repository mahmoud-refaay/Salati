namespace UI.Core.Language
{
    // نصوص الأذكار — Adhkar & Reminders
    public partial interface ILanguagePack
    {
        // عناوين
        string AdhkarMorningTitle { get; }
        string AdhkarEveningTitle { get; }

        // أزرار ونصوص
        string AdhkarTap { get; }
        string AdhkarDone { get; }
        string AdhkarProgress { get; }
        string AdhkarCompleted { get; }
    }
}
