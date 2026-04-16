using System.Diagnostics;

namespace DAL.Logging;

/// <summary>
/// مسجّل الأخطاء المركزي — يسجّل في:
///   1️⃣ Windows Event Viewer (Application → Source: "Salati")
///   2️⃣ ملف Log محلي (Logs/salati_yyyy-MM-dd.log)
/// 
/// الاستخدام:
///   clsLogger.Error("فشل في جلب الأذكار", ex);
///   clsLogger.Info("البرنامج بدأ بنجاح");
///   clsLogger.Warn("Connection timeout — إعادة المحاولة");
/// </summary>
public static class clsLogger
{
    // ═══════════════════════════════════════
    //  Constants
    // ═══════════════════════════════════════

    private const string EVENT_SOURCE = "Salati";
    private const string EVENT_LOG = "Application";
    private const string LOG_FOLDER = "Logs";

    // ═══════════════════════════════════════
    //  Static Constructor — تسجيل الـ Source مرة واحدة
    // ═══════════════════════════════════════

    static clsLogger()
    {
        try
        {
            // تسجل Source في Event Viewer (محتاج Admin أول مرة بس)
            if (!EventLog.SourceExists(EVENT_SOURCE))
                EventLog.CreateEventSource(EVENT_SOURCE, EVENT_LOG);
        }
        catch
        {
            // لو مش Admin — مش مشكلة، هنكتب في الملف بس
        }
    }

    // ═══════════════════════════════════════
    //  Public API
    // ═══════════════════════════════════════

    /// <summary>خطأ — Exception حقيقي حصل 🔴</summary>
    public static void Error(string message, Exception? ex = null, object? context = null)
    {
        string full = FormatMessage("ERROR", message, ex, context);
        WriteToEventLog(full, EventLogEntryType.Error);
        WriteToFile(full);
    }

    /// <summary>تحذير — شيء غير متوقع لكن البرنامج لسه شغال 🟡</summary>
    public static void Warn(string message, object? context = null)
    {
        string full = FormatMessage("WARN", message, null, context);
        WriteToEventLog(full, EventLogEntryType.Warning);
        WriteToFile(full);
    }

    /// <summary>معلومات — حدث عادي يستحق التسجيل 🔵</summary>
    public static void Info(string message)
    {
        string full = FormatMessage("INFO", message);
        WriteToFile(full);
        // مش بنكتب Info في Event Viewer — هيبقى كتير
    }

    // ═══════════════════════════════════════
    //  Internal — Format
    // ═══════════════════════════════════════

    private static string FormatMessage(string level, string message,
        Exception? ex = null, object? context = null)
    {
        var parts = new List<string>
        {
            $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}"
        };

        if (context != null)
            parts.Add($"  Context: {context}");

        if (ex != null)
        {
            parts.Add($"  Exception: {ex.GetType().Name}: {ex.Message}");
            if (ex.StackTrace != null)
                parts.Add($"  StackTrace: {ex.StackTrace}");
            if (ex.InnerException != null)
                parts.Add($"  Inner: {ex.InnerException.Message}");
        }

        return string.Join(Environment.NewLine, parts);
    }

    // ═══════════════════════════════════════
    //  Internal — Write
    // ═══════════════════════════════════════

    /// <summary>يكتب في Windows Event Viewer</summary>
    private static void WriteToEventLog(string message, EventLogEntryType type)
    {
        try
        {
            if (EventLog.SourceExists(EVENT_SOURCE))
                EventLog.WriteEntry(EVENT_SOURCE, message, type);
        }
        catch
        {
            // Silent fail — مش عايزين Logger يسبب crash!
        }
    }

    /// <summary>يكتب في ملف محلي — ملف جديد كل يوم</summary>
    private static void WriteToFile(string message)
    {
        try
        {
            string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LOG_FOLDER);
            Directory.CreateDirectory(logDir);

            string logFile = Path.Combine(logDir, $"salati_{DateTime.Now:yyyy-MM-dd}.log");
            File.AppendAllText(logFile, message + Environment.NewLine + Environment.NewLine);
        }
        catch
        {
            // Silent fail
        }
    }
}
