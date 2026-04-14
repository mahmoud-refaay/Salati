namespace UI.Core.Language
{
    // نصوص الداشبورد — Countdown, Next Prayer, etc.
    public partial interface ILanguagePack
    {
        string DashboardRemaining { get; }
        string DashboardNextPrayer { get; }
        string DashboardPassed { get; }
        string DashboardUpcoming { get; }
        string DashboardNoInternet { get; }
    }
}
