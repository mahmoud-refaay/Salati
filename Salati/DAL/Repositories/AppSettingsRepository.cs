using DAL.Repositories;

namespace DAL.Repositories;

/// <summary>
/// Repository الإعدادات — يقرأ/يكتب من جدول AppSettings.
/// كل setting عبارة عن Key/Value pair.
/// </summary>
public class AppSettingsRepository : BaseRepository
{
    /// <summary>قراءة قيمة إعداد</summary>
    public async Task<string?> GetValueAsync(string key)
    {
        return await ExecuteScalarAsync<string>(
            "SP_GetSettingValue", new { SettingKey = key });
    }

    /// <summary>كتابة قيمة إعداد</summary>
    public async Task SetValueAsync(string key, string value)
    {
        await ExecuteAsync("SP_UpdateSettingValue",
            new { SettingKey = key, SettingValue = value });
    }

    /// <summary>قراءة كل إعدادات فئة معينة</summary>
    public async Task<IEnumerable<(string Key, string? Value)>> GetByCategoryAsync(string category)
    {
        return await QueryAsync<(string Key, string? Value)>(
            "SP_GetSettingsByCategory", new { Category = category });
    }
}
