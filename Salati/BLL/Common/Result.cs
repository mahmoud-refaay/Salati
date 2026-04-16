namespace BLL.Common;

/// <summary>
/// Result Pattern — بدل ما ترجع bool أو throw exception:
///   ✅ Result&lt;T&gt;.Success(data) → نجح + بيانات
///   ❌ Result&lt;T&gt;.Failure("رسالة") → فشل + سبب واضح
/// 
/// ═══ ليه Result Pattern أحسن؟ ═══
///   - مش بتستخدم exceptions للـ flow control
///   - الـ UI يعرف يعرض رسالة مناسبة
///   - نفس الـ pattern في ASP.NET Core APIs
/// 
/// ═══ الاستخدام ═══
///   var result = await service.MarkPrayerAsync(prayer, status);
///   if (result.IsSuccess)
///       ucToast.ShowSuccess(this, "تم ✅");
///   else
///       ucToast.ShowError(this, result.Error!);
/// </summary>
public record Result<T>
{
    public bool IsSuccess { get; init; }
    public T? Data { get; init; }
    public string? Error { get; init; }

    // ── Factory Methods ──

    /// <summary>نتيجة ناجحة مع بيانات</summary>
    public static Result<T> Success(T data)
        => new() { IsSuccess = true, Data = data };

    /// <summary>نتيجة فاشلة مع سبب</summary>
    public static Result<T> Failure(string error)
        => new() { IsSuccess = false, Error = error };
}

/// <summary>
/// Result بدون بيانات — لعمليات مش بترجع شيء (مثل Delete, Mark).
/// 
/// var result = await service.DeleteAsync(id);
/// if (result.IsSuccess) ...
/// </summary>
public record Result
{
    public bool IsSuccess { get; init; }
    public string? Error { get; init; }

    public static Result Success()
        => new() { IsSuccess = true };

    public static Result Failure(string error)
        => new() { IsSuccess = false, Error = error };
}
