using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using DAL.Config;
using DAL.Logging;

namespace DAL.Repositories;

/// <summary>
/// الـ Repository الأساسي — يوفّر methods مشتركة بـ Dapper.
/// كل Repository بيورث منه ويستخدم methods جاهزة.
/// 
/// ═══ الفرق عن Aura (clsDataAccessHelper) ═══
///   ❌ Aura: DataTable + sync + static
///   ✅ Salati: DTOs + async + instance + Dapper
/// 
/// ═══ الاستخدام ═══
///   class AdhkarRepository : BaseRepository
///   {
///       public Task&lt;IEnumerable&lt;AdhkarDTO&gt;&gt; GetAllAsync()
///           =&gt; QueryAsync&lt;AdhkarDTO&gt;("SP_GetAdhkarByCategory", new { Category = 1 });
///   }
/// </summary>
public abstract class BaseRepository
{
    // ═══════════════════════════════════════
    //  Connection Factory
    // ═══════════════════════════════════════

    /// <summary>ينشئ اتصال جديد (Dapper بيفتح ويقفل لوحده)</summary>
    protected static SqlConnection CreateConnection()
        => new(DbConfig.ConnectionString);

    // ═══════════════════════════════════════
    //  Query — جلب بيانات (SELECT)
    // ═══════════════════════════════════════

    /// <summary>
    /// ينفّذ SP ويرجع قائمة من الكائنات.
    /// Dapper بيعمل mapping تلقائي: اسم Column = اسم Property.
    /// 
    /// مثال: var list = await QueryAsync&lt;AdhkarDTO&gt;("SP_GetAll");
    /// </summary>
    protected static async Task<IEnumerable<T>> QueryAsync<T>(
        string storedProcedure, object? parameters = null)
    {
        try
        {
            using var conn = CreateConnection();
            return await conn.QueryAsync<T>(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        catch (SqlException ex)
        {
            clsLogger.Error($"[DAL] QueryAsync<{typeof(T).Name}> failed — SP: {storedProcedure}", ex, parameters);
            return [];
        }
    }

    /// <summary>
    /// ينفّذ SP ويرجع كائن واحد أو null.
    /// 
    /// مثال: var adhkar = await QuerySingleAsync&lt;AdhkarDTO&gt;("SP_GetRandom");
    /// </summary>
    protected static async Task<T?> QuerySingleAsync<T>(
        string storedProcedure, object? parameters = null) where T : class
    {
        try
        {
            using var conn = CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<T>(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        catch (SqlException ex)
        {
            clsLogger.Error($"[DAL] QuerySingleAsync<{typeof(T).Name}> failed — SP: {storedProcedure}", ex, parameters);
            return null;
        }
    }

    // ═══════════════════════════════════════
    //  Execute — تعديل بيانات (INSERT/UPDATE/DELETE)
    // ═══════════════════════════════════════

    /// <summary>
    /// ينفّذ SP بدون رجوع بيانات (INSERT, UPDATE, DELETE).
    /// يرجع عدد الصفوف المتأثرة.
    /// 
    /// مثال: int rows = await ExecuteAsync("SP_UnmarkPrayer", new { Prayer = 1 });
    /// </summary>
    protected static async Task<int> ExecuteAsync(
        string storedProcedure, object? parameters = null)
    {
        try
        {
            using var conn = CreateConnection();
            return await conn.ExecuteAsync(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        catch (SqlException ex)
        {
            clsLogger.Error($"[DAL] ExecuteAsync failed — SP: {storedProcedure}", ex, parameters);
            return -1;
        }
    }

    /// <summary>
    /// ينفّذ SP ويرجع قيمة واحدة (مثل COUNT، أو OUTPUT parameter).
    /// 
    /// مثال: int count = await ExecuteScalarAsync&lt;int&gt;("SP_GetCount");
    /// </summary>
    protected static async Task<T?> ExecuteScalarAsync<T>(
        string storedProcedure, object? parameters = null)
    {
        try
        {
            using var conn = CreateConnection();
            return await conn.ExecuteScalarAsync<T>(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        catch (SqlException ex)
        {
            clsLogger.Error($"[DAL] ExecuteScalarAsync failed — SP: {storedProcedure}", ex, parameters);
            return default;
        }
    }

    // ═══════════════════════════════════════
    //  Execute with Output Parameters
    // ═══════════════════════════════════════

    /// <summary>
    /// ينفّذ SP مع Output Parameters — لـ SPs زي SP_MarkPrayer, SP_GetStreakCount.
    /// ترجع الـ DynamicParameters علشان تقرأ الـ output values.
    /// 
    /// مثال:
    ///   var p = new DynamicParameters();
    ///   p.Add("@Prayer", 1);
    ///   p.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
    ///   await ExecuteWithOutputAsync("SP_MarkPrayer", p);
    ///   int result = p.Get&lt;int&gt;("@Result");
    /// </summary>
    protected static async Task ExecuteWithOutputAsync(
        string storedProcedure, DynamicParameters parameters)
    {
        try
        {
            using var conn = CreateConnection();
            await conn.ExecuteAsync(
                storedProcedure,
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        catch (SqlException ex)
        {
            clsLogger.Error($"[DAL] ExecuteWithOutputAsync failed — SP: {storedProcedure}", ex);
        }
    }
}
