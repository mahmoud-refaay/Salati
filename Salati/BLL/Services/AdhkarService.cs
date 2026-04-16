using BLL.Common;
using DAL.DTOs;
using DAL.Enums;
using DAL.Repositories;

namespace BLL.Services;

/// <summary>
/// Service الأذكار — الطبقة اللي الـ UI بيتكلم معاها.
/// 
/// ═══ الدور ═══
///   - بيستلم طلب من UI
///   - يطبّق أي business logic (validation, transformation)
///   - يستدعي الـ Repository (DAL)
///   - يرجع Result&lt;T&gt; (مش exception)
/// 
/// ═══ الاستخدام ═══
///   var service = new AdhkarService();
///   var result = await service.GetMorningAdhkarAsync();
///   if (result.IsSuccess) { /* استخدم result.Data */ }
/// </summary>
public class AdhkarService
{
    private readonly AdhkarRepository _repo = new();

    // ═══════════════════════════════════════
    //  إشعارات — ذكر عشوائي
    // ═══════════════════════════════════════

    /// <summary>جيب ذكر/حديث عشوائي — للإشعارات في الخلفية</summary>
    public async Task<Result<AdhkarDTO>> GetRandomAdhkarAsync(eAdhkarCategory? category = null)
    {
        var adhkar = await _repo.GetRandomAsync(category);

        return adhkar != null
            ? Result<AdhkarDTO>.Success(adhkar)
            : Result<AdhkarDTO>.Failure("لا توجد أذكار متاحة");
    }

    // ═══════════════════════════════════════
    //  تصنيف — قائمة أذكار
    // ═══════════════════════════════════════

    /// <summary>جيب كل الأذكار من تصنيف معين</summary>
    public async Task<Result<List<AdhkarDTO>>> GetByCategoryAsync(eAdhkarCategory category)
    {
        var list = (await _repo.GetByCategoryAsync(category)).ToList();

        return list.Count > 0
            ? Result<List<AdhkarDTO>>.Success(list)
            : Result<List<AdhkarDTO>>.Failure($"لا توجد أذكار في تصنيف: {category}");
    }

    // ═══════════════════════════════════════
    //  أذكار الصباح والمساء
    // ═══════════════════════════════════════

    /// <summary>أذكار الصباح — مرتبة</summary>
    public async Task<Result<List<AdhkarDTO>>> GetMorningAdhkarAsync()
    {
        var list = (await _repo.GetMorningAdhkarAsync()).ToList();

        return list.Count > 0
            ? Result<List<AdhkarDTO>>.Success(list)
            : Result<List<AdhkarDTO>>.Failure("لا توجد أذكار صباح — تأكد من البيانات في قاعدة البيانات");
    }

    /// <summary>أذكار المساء — مرتبة</summary>
    public async Task<Result<List<AdhkarDTO>>> GetEveningAdhkarAsync()
    {
        var list = (await _repo.GetEveningAdhkarAsync()).ToList();

        return list.Count > 0
            ? Result<List<AdhkarDTO>>.Success(list)
            : Result<List<AdhkarDTO>>.Failure("لا توجد أذكار مساء — تأكد من البيانات في قاعدة البيانات");
    }
}
