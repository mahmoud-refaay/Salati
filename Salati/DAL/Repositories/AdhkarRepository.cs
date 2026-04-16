using DAL.DTOs;
using DAL.Enums;

namespace DAL.Repositories;

/// <summary>
/// Repository الأذكار والأحاديث — يتعامل مع جدول Adhkar.
/// 
/// SPs المستخدمة:
///   - SP_GetRandomAdhkar      → ذكر عشوائي (للإشعارات)
///   - SP_GetAdhkarByCategory  → كل الأذكار من تصنيف معين
///   - SP_GetMorningEveningAdhkar → أذكار الصباح/المساء (مرتبة)
/// </summary>
public class AdhkarRepository : BaseRepository
{
    /// <summary>جيب ذكر/حديث عشوائي (للإشعارات في الخلفية)</summary>
    public async Task<AdhkarDTO?> GetRandomAsync(eAdhkarCategory? category = null)
    {
        return await QuerySingleAsync<AdhkarDTO>(
            "SP_GetRandomAdhkar",
            new { Category = (byte?)category }
        );
    }

    /// <summary>جيب كل الأذكار من تصنيف معين</summary>
    public async Task<IEnumerable<AdhkarDTO>> GetByCategoryAsync(eAdhkarCategory category)
    {
        return await QueryAsync<AdhkarDTO>(
            "SP_GetAdhkarByCategory",
            new { Category = (byte)category }
        );
    }

    /// <summary>جيب أذكار الصباح — مرتبة بالـ SortOrder</summary>
    public async Task<IEnumerable<AdhkarDTO>> GetMorningAdhkarAsync()
    {
        return await QueryAsync<AdhkarDTO>(
            "SP_GetMorningEveningAdhkar",
            new { Type = (byte)eAdhkarCategory.MorningAdhkar }
        );
    }

    /// <summary>جيب أذكار المساء — مرتبة بالـ SortOrder</summary>
    public async Task<IEnumerable<AdhkarDTO>> GetEveningAdhkarAsync()
    {
        return await QueryAsync<AdhkarDTO>(
            "SP_GetMorningEveningAdhkar",
            new { Type = (byte)eAdhkarCategory.EveningAdhkar }
        );
    }
}
