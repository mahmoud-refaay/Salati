namespace UI.Core.Language
{
    // نصوص مشتركة — Save, Cancel, Close, etc.
    public partial interface ILanguagePack
    {
        string CommonSave { get; }
        string CommonCancel { get; }
        string CommonClose { get; }
        string CommonDismiss { get; }
        string CommonMute { get; }
        string CommonRefresh { get; }
        string CommonReset { get; }
        string CommonError { get; }
        string CommonSuccess { get; }
        string CommonLoading { get; }
        string CommonOk { get; }
        string CommonWarning { get; }
        string CommonInfo { get; }
    }
}
