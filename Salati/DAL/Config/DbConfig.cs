namespace DAL.Config;

/// <summary>
/// إعدادات الاتصال بقاعدة البيانات — مكان واحد مركزي.
/// 
/// TODO: في المستقبل — يتحوّل لـ appsettings.json
/// حالياً: hardcoded للتبسيط (نفس نمط Aura مع تحسينات لاحقاً).
/// </summary>
public static class DbConfig
{
    public static readonly string ConnectionString =
        @"Server=.;Database=SalatiDB;Trusted_Connection=True;TrustServerCertificate=True;";
}
