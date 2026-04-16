using DAL.Repositories;

namespace BLL.Services;

/// <summary>
/// Service الإعدادات — يقرأ/يكتب من جدول AppSettings.
/// 
/// ═══ الاستخدام ═══
///   var settings = new AppSettingsService();
///   bool startWithWindows = await settings.GetBoolAsync("StartWithWindows");
///   await settings.SetAsync("ActiveThemeName", "Ocean Breeze");
/// 
/// ═══ القاعدة ═══
///   كل setting عبارة عن string — الـ Service بيحوّلها للنوع المناسب.
/// </summary>
public class AppSettingsService
{
    private readonly AppSettingsRepository _repo = new();

    // ═══════════════════════════════════════
    //  Read — Typed Getters
    // ═══════════════════════════════════════

    /// <summary>قراءة string</summary>
    public async Task<string> GetStringAsync(string key, string defaultValue = "")
    {
        var value = await _repo.GetValueAsync(key);
        return value ?? defaultValue;
    }

    /// <summary>قراءة bool</summary>
    public async Task<bool> GetBoolAsync(string key, bool defaultValue = false)
    {
        var value = await _repo.GetValueAsync(key);
        return value != null && bool.TryParse(value, out var result) ? result : defaultValue;
    }

    /// <summary>قراءة int</summary>
    public async Task<int> GetIntAsync(string key, int defaultValue = 0)
    {
        var value = await _repo.GetValueAsync(key);
        return value != null && int.TryParse(value, out var result) ? result : defaultValue;
    }

    // ═══════════════════════════════════════
    //  Write
    // ═══════════════════════════════════════

    /// <summary>كتابة قيمة</summary>
    public async Task SetAsync(string key, string value)
    {
        await _repo.SetValueAsync(key, value);
    }

    /// <summary>كتابة bool</summary>
    public async Task SetBoolAsync(string key, bool value)
    {
        await _repo.SetValueAsync(key, value.ToString().ToLower());
    }

    // ═══════════════════════════════════════
    //  Convenience — إعدادات شائعة
    // ═══════════════════════════════════════

    public async Task<string> GetThemeNameAsync()
        => await GetStringAsync("ActiveThemeName", "Midnight Serenity");

    public async Task<string> GetLanguageCodeAsync()
        => await GetStringAsync("LanguageCode", "ar");

    public async Task<bool> GetStartWithWindowsAsync()
        => await GetBoolAsync("StartWithWindows", true);

    public async Task<bool> GetMinimizeToTrayAsync()
        => await GetBoolAsync("MinimizeToTray", true);

    public async Task<int> GetDefaultLocationIdAsync()
        => await GetIntAsync("DefaultLocationID", 1);

    public async Task<int> GetGlobalVolumeAsync()
        => await GetIntAsync("GlobalVolume", 80);
}
