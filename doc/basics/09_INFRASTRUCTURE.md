# ⚙️ Salati — Professional Infrastructure Systems

> هذا الملف يوثق الأنظمة الداخلية المتقدمة اللي بتخلي المشروع **احترافي من جواه**.
> بسيط من بره — شغل عميق من جواه.

---

## 📋 الأنظمة المغطاة

| # | النظام | الوصف |
|---|--------|-------|
| 1 | **Windows Registry Settings** | حفظ إعدادات المستخدم في Registry مع fallback للافتراضي |
| 2 | **Windows Event Viewer** | تسجيل الأخطاء في Event Log |
| 3 | **Database Error Logging** | حفظ كل خطأ في الداتابيز + عرضه في Development mode |
| 4 | **Advanced T-SQL** | استعلامات محسّنة وسريعة |
| 5 | **Global Error Handling** | نظام التقاط الأخطاء الشامل |

---

## ═══════════════════════════════════════════════
## 1. Windows Registry Settings System
## ═══════════════════════════════════════════════

### الفكرة

> كل إعدادات المستخدم تتحفظ في **Windows Registry** بدل JSON.
> لو الـ Registry key اتلف أو مش موجود → يرجع للقيمة الافتراضية تلقائياً.

### مكان التخزين

```
HKEY_CURRENT_USER\SOFTWARE\Salati\
├── Settings\
│   ├── PrayerSource          = "API"           (REG_SZ)
│   ├── DefaultLocationID     = 1               (REG_DWORD)
│   ├── GlobalVolume          = 80              (REG_DWORD)
│   └── LastFetchDate         = "2026-04-12"    (REG_SZ)
│
├── Appearance\
│   ├── ActiveThemeName       = "Midnight Serenity"  (REG_SZ)
│   └── LanguageCode          = "ar"                  (REG_SZ)
│
├── Startup\
│   ├── StartWithWindows      = 1               (REG_DWORD / BIT)
│   ├── MinimizeOnStart       = 0               (REG_DWORD)
│   ├── ShowInTray            = 1               (REG_DWORD)
│   └── MinimizeToTray        = 1               (REG_DWORD)
│
└── System\
    ├── FirstRunCompleted     = 1               (REG_DWORD)
    ├── AppVersion            = "1.0.0"         (REG_SZ)
    └── InstallDate           = "2026-04-12"    (REG_SZ)

Windows Startup:
HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run\
    └── Salati = "C:\...\Salati.exe"
```

### Architecture

```
┌─ UI Layer ──────────────────────────────────────┐
│  clsSettingsStore (static) ← الواجهة العامة     │
│    ├── .Theme → get/set                          │
│    ├── .Language → get/set                       │
│    ├── .Volume → get/set                         │
│    └── .Save() / .Load()                         │
└────────────┬────────────────────────────────────┘
             │ يستدعي
             ▼
┌─ DAL Layer ─────────────────────────────────────┐
│  clsRegistryManager (static) ← القراءة/الكتابة  │
│    ├── GetString(subKey, name, default)           │
│    ├── GetInt(subKey, name, default)              │
│    ├── GetBool(subKey, name, default)             │
│    ├── SetString(subKey, name, value)             │
│    ├── SetInt(subKey, name, value)                │
│    ├── SetBool(subKey, name, value)               │
│    ├── DeleteValue(subKey, name)                  │
│    ├── KeyExists(subKey, name)                    │
│    └── ResetToDefaults()                          │
└─────────────────────────────────────────────────┘
```

### DAL: clsRegistryManager.cs

```csharp
using Microsoft.Win32;

namespace DAL.Settings
{
    /// <summary>
    /// إدارة إعدادات التطبيق في Windows Registry.
    /// كل method بتعمل fallback تلقائي لو القيمة مش موجودة أو تالفة.
    /// 
    /// Registry Path: HKCU\SOFTWARE\Salati\{subKey}
    /// </summary>
    public static class clsRegistryManager
    {
        private const string BASE_KEY = @"SOFTWARE\Salati";

        // ═══════════════════════════════════════════
        //  Read Operations (مع Default Fallback)
        // ═══════════════════════════════════════════

        /// <summary>قراءة نص — لو مش موجود يرجع defaultValue</summary>
        public static string GetString(string subKey, string name, string defaultValue)
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey($@"{BASE_KEY}\{subKey}", false);
                var value = key?.GetValue(name);
                return value?.ToString() ?? defaultValue;
            }
            catch
            {
                // Registry تالف أو مفيش صلاحية
                clsEventLogger.LogWarning($"Registry read failed: {subKey}\\{name}, using default: {defaultValue}");
                return defaultValue;
            }
        }

        /// <summary>قراءة عدد صحيح</summary>
        public static int GetInt(string subKey, string name, int defaultValue)
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey($@"{BASE_KEY}\{subKey}", false);
                var value = key?.GetValue(name);
                if (value is int intVal) return intVal;
                if (int.TryParse(value?.ToString(), out int parsed)) return parsed;
                return defaultValue;
            }
            catch
            {
                clsEventLogger.LogWarning($"Registry read failed: {subKey}\\{name}, using default: {defaultValue}");
                return defaultValue;
            }
        }

        /// <summary>قراءة قيمة منطقية</summary>
        public static bool GetBool(string subKey, string name, bool defaultValue)
        {
            return GetInt(subKey, name, defaultValue ? 1 : 0) == 1;
        }

        // ═══════════════════════════════════════════
        //  Write Operations
        // ═══════════════════════════════════════════

        /// <summary>كتابة نص</summary>
        public static bool SetString(string subKey, string name, string value)
        {
            try
            {
                using var key = Registry.CurrentUser.CreateSubKey($@"{BASE_KEY}\{subKey}");
                key?.SetValue(name, value, RegistryValueKind.String);
                return true;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogError($"Registry write failed: {subKey}\\{name}", ex);
                return false;
            }
        }

        /// <summary>كتابة عدد صحيح</summary>
        public static bool SetInt(string subKey, string name, int value)
        {
            try
            {
                using var key = Registry.CurrentUser.CreateSubKey($@"{BASE_KEY}\{subKey}");
                key?.SetValue(name, value, RegistryValueKind.DWord);
                return true;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogError($"Registry write failed: {subKey}\\{name}", ex);
                return false;
            }
        }

        /// <summary>كتابة قيمة منطقية</summary>
        public static bool SetBool(string subKey, string name, bool value)
        {
            return SetInt(subKey, name, value ? 1 : 0);
        }

        // ═══════════════════════════════════════════
        //  Utility
        // ═══════════════════════════════════════════

        /// <summary>هل المفتاح موجود؟</summary>
        public static bool KeyExists(string subKey, string name)
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey($@"{BASE_KEY}\{subKey}", false);
                return key?.GetValue(name) != null;
            }
            catch { return false; }
        }

        /// <summary>حذف قيمة</summary>
        public static bool DeleteValue(string subKey, string name)
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey($@"{BASE_KEY}\{subKey}", true);
                key?.DeleteValue(name, false);
                return true;
            }
            catch { return false; }
        }

        /// <summary>مسح كل الإعدادات (إعادة ضبط)</summary>
        public static bool ResetToDefaults()
        {
            try
            {
                Registry.CurrentUser.DeleteSubKeyTree($@"{BASE_KEY}", false);
                clsEventLogger.LogInfo("Settings reset to defaults via Registry cleanup");
                return true;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogError("Failed to reset Registry settings", ex);
                return false;
            }
        }

        // ═══════════════════════════════════════════
        //  Windows Startup
        // ═══════════════════════════════════════════

        private const string RUN_KEY = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        private const string APP_NAME = "Salati";

        public static void EnableStartup()
        {
            using var key = Registry.CurrentUser.OpenSubKey(RUN_KEY, true);
            key?.SetValue(APP_NAME, Application.ExecutablePath);
        }

        public static void DisableStartup()
        {
            using var key = Registry.CurrentUser.OpenSubKey(RUN_KEY, true);
            key?.DeleteValue(APP_NAME, false);
        }

        public static bool IsStartupEnabled()
        {
            using var key = Registry.CurrentUser.OpenSubKey(RUN_KEY, false);
            return key?.GetValue(APP_NAME) != null;
        }
    }
}
```

### UI: clsSettingsStore.cs (Wrapper)

```csharp
namespace UI.Core.Settings
{
    /// <summary>
    /// واجهة الإعدادات للـ UI — تخفي تفاصيل الـ Registry.
    /// كل property فيها قيمة افتراضية — لو الـ Registry تالف هترجع تلقائي.
    /// </summary>
    public static class clsSettingsStore
    {
        // ═══ Default Values (ثوابت — مش بتتغير) ═══
        private static class Defaults
        {
            public const string PrayerSource = "API";
            public const int DefaultLocationID = 1;
            public const string Theme = "Midnight Serenity";
            public const string Language = "ar";
            public const bool StartWithWindows = true;
            public const bool MinimizeOnStart = false;
            public const bool ShowInTray = true;
            public const bool MinimizeToTray = true;
            public const int Volume = 80;
        }

        // ═══ Prayer Settings ═══
        public static string PrayerSource
        {
            get => clsRegistryManager.GetString("Settings", "PrayerSource", Defaults.PrayerSource);
            set => clsRegistryManager.SetString("Settings", "PrayerSource", value);
        }

        public static int DefaultLocationID
        {
            get => clsRegistryManager.GetInt("Settings", "DefaultLocationID", Defaults.DefaultLocationID);
            set => clsRegistryManager.SetInt("Settings", "DefaultLocationID", value);
        }

        // ═══ Appearance ═══
        public static string ActiveThemeName
        {
            get => clsRegistryManager.GetString("Appearance", "ActiveThemeName", Defaults.Theme);
            set => clsRegistryManager.SetString("Appearance", "ActiveThemeName", value);
        }

        public static string LanguageCode
        {
            get => clsRegistryManager.GetString("Appearance", "LanguageCode", Defaults.Language);
            set => clsRegistryManager.SetString("Appearance", "LanguageCode", value);
        }

        // ═══ Startup ═══
        public static bool StartWithWindows
        {
            get => clsRegistryManager.GetBool("Startup", "StartWithWindows", Defaults.StartWithWindows);
            set
            {
                clsRegistryManager.SetBool("Startup", "StartWithWindows", value);
                if (value) clsRegistryManager.EnableStartup();
                else clsRegistryManager.DisableStartup();
            }
        }

        public static bool MinimizeOnStart
        {
            get => clsRegistryManager.GetBool("Startup", "MinimizeOnStart", Defaults.MinimizeOnStart);
            set => clsRegistryManager.SetBool("Startup", "MinimizeOnStart", value);
        }

        public static bool ShowInTray
        {
            get => clsRegistryManager.GetBool("Startup", "ShowInTray", Defaults.ShowInTray);
            set => clsRegistryManager.SetBool("Startup", "ShowInTray", value);
        }

        public static bool MinimizeToTray
        {
            get => clsRegistryManager.GetBool("Startup", "MinimizeToTray", Defaults.MinimizeToTray);
            set => clsRegistryManager.SetBool("Startup", "MinimizeToTray", value);
        }

        // ═══ Sound ═══
        public static int GlobalVolume
        {
            get => clsRegistryManager.GetInt("Settings", "GlobalVolume", Defaults.Volume);
            set => clsRegistryManager.SetInt("Settings", "GlobalVolume", Math.Clamp(value, 0, 100));
        }

        // ═══ Reset ═══
        public static bool ResetToDefaults() => clsRegistryManager.ResetToDefaults();
    }
}
```

### سلسلة الـ Fallback:

```
المستخدم يطلب إعداد
    │
    ▼
clsSettingsStore.ActiveThemeName (UI)
    │
    ▼
clsRegistryManager.GetString("Appearance", "ActiveThemeName", "Midnight Serenity") (DAL)
    │
    ├── Registry موجود ومسحوب بنجاح?
    │   ├── ✅ نعم → يرجع القيمة المحفوظة
    │   └── ❌ لا (مش موجود / تالف / صلاحية) →
    │       ├── يسجل Warning في Event Viewer
    │       └── يرجع القيمة الافتراضية "Midnight Serenity"
    │
    ▼
القيمة وصلت للـ UI سليمة في كل الأحوال ✅
```

---

## ═══════════════════════════════════════════════
## 2. Windows Event Viewer Logging
## ═══════════════════════════════════════════════

### الفكرة

> كل خطأ أو حدث مهم يتسجل في **Windows Event Viewer** تحت اسم "Salati".
> ده بيفيد في الـ debugging وتتبع المشاكل حتى لو التطبيق اتقفل.

### Event Log Source

```
Source:  "Salati"
Log:    "Application"
```

### مستويات الأحداث

| Level | Method | الاستخدام |
|-------|--------|-----------|
| **Information** | `LogInfo()` | بدء التطبيق، إغلاق، تغيير إعدادات |
| **Warning** | `LogWarning()` | فشل في API، Registry تالف، fallback |
| **Error** | `LogError()` | exceptions، فشل Database |

### DAL: clsEventLogger.cs

```csharp
using System.Diagnostics;

namespace DAL.Helper
{
    /// <summary>
    /// يسجل الأحداث في Windows Event Viewer + يقدر يسجل في DB كمان.
    /// 
    /// Source: "Salati" → Application Log
    /// 
    /// الاستخدام:
    ///   clsEventLogger.LogInfo("App started");
    ///   clsEventLogger.LogError("DB connection failed", ex);
    /// </summary>
    public static class clsEventLogger
    {
        private const string SOURCE_NAME = "Salati";
        private const string LOG_NAME = "Application";

        // ═══ تسجيل الـ Source (مرة واحدة) ═══
        static clsEventLogger()
        {
            try
            {
                if (!EventLog.SourceExists(SOURCE_NAME))
                    EventLog.CreateEventSource(SOURCE_NAME, LOG_NAME);
            }
            catch
            {
                // محتاجين Admin permissions لإنشاء Source أول مرة
                // ممكن نعمل installer يعملها أو نتجاهل
            }
        }

        // ═══════════════════════════════════════════
        //  Public Logging Methods
        // ═══════════════════════════════════════════

        /// <summary>تسجيل معلومة</summary>
        public static void LogInfo(string message)
        {
            WriteToEventLog(message, EventLogEntryType.Information);
        }

        /// <summary>تسجيل تحذير</summary>
        public static void LogWarning(string message)
        {
            WriteToEventLog(message, EventLogEntryType.Warning);
        }

        /// <summary>تسجيل خطأ (مع Exception)</summary>
        public static void LogError(string message, Exception? ex = null)
        {
            string fullMessage = ex != null
                ? $"{message}\n\n" +
                  $"Exception Type: {ex.GetType().FullName}\n" +
                  $"Message: {ex.Message}\n" +
                  $"Source: {ex.Source}\n" +
                  $"StackTrace:\n{ex.StackTrace}\n" +
                  $"Inner Exception: {ex.InnerException?.Message ?? "None"}"
                : message;

            WriteToEventLog(fullMessage, EventLogEntryType.Error);

            // ═══ تسجيل في الداتابيز كمان ═══
            try
            {
                clsErrorLogData.Insert(
                    message: message,
                    exceptionType: ex?.GetType().FullName,
                    exceptionMessage: ex?.Message,
                    stackTrace: ex?.StackTrace,
                    source: ex?.Source ?? "Unknown",
                    severity: "Error"
                );
            }
            catch
            {
                // لو الداتابيز نفسها فيها مشكلة — Event Viewer هيكفي
            }
        }

        /// <summary>تسجيل خطأ حرج (Critical)</summary>
        public static void LogCritical(string message, Exception? ex = null)
        {
            string fullMessage = $"[CRITICAL] {message}";
            if (ex != null)
                fullMessage += $"\n\nException: {ex.GetType().FullName}\n{ex.Message}\n{ex.StackTrace}";

            WriteToEventLog(fullMessage, EventLogEntryType.Error);

            try
            {
                clsErrorLogData.Insert(
                    message: message,
                    exceptionType: ex?.GetType().FullName,
                    exceptionMessage: ex?.Message,
                    stackTrace: ex?.StackTrace,
                    source: ex?.Source ?? "Unknown",
                    severity: "Critical"
                );
            }
            catch { }
        }

        // ═══════════════════════════════════════════
        //  Private Helper
        // ═══════════════════════════════════════════

        private static void WriteToEventLog(string message, EventLogEntryType type)
        {
            try
            {
                EventLog.WriteEntry(SOURCE_NAME, message, type);
            }
            catch
            {
                // لو Event Viewer مش شغال — مفيش حاجة نعملها
                // على الأقل البيانات هتتسجل في DB
            }
        }
    }
}
```

### أمثلة الأحداث اللي هتتسجل

| الحدث | Level | المثال |
|-------|-------|--------|
| بدء التطبيق | ℹ️ Info | `"Salati started — v1.0.0"` |
| إغلاق التطبيق | ℹ️ Info | `"Salati shutdown gracefully"` |
| جلب مواعيد من API | ℹ️ Info | `"Prayer times fetched for Cairo, Egypt"` |
| فشل API | ⚠️ Warning | `"API fetch failed: No internet — using cached times"` |
| Registry تالف | ⚠️ Warning | `"Registry read failed: Appearance\Theme — using default"` |
| فشل تشغيل صوت | ⚠️ Warning | `"Sound playback failed: adhan_1.mp3 — file not found"` |
| خطأ Database | ❌ Error | `"SP_GetTodayPrayerTimes failed: Connection refused"` |
| خطأ غير متوقع | ❌ Error | `"Unhandled exception in scrDashboard.LoadDataAsync"` |
| كراش التطبيق | 🔴 Critical | `"Application crash: StackOverflowException"` |

---

## ═══════════════════════════════════════════════
## 3. Database Error Logging
## ═══════════════════════════════════════════════

### جدول ErrorLog (إضافة للداتابيز)

```sql
-- ═══════════════════════════════════════════
-- ErrorLog — سجل الأخطاء
-- ═══════════════════════════════════════════
CREATE TABLE ErrorLog (
    ErrorLogID       INT IDENTITY(1,1) PRIMARY KEY,
    ErrorDateTime    DATETIME          NOT NULL DEFAULT GETDATE(),
    Severity         NVARCHAR(20)      NOT NULL DEFAULT 'Error',
    [Message]        NVARCHAR(500)     NOT NULL,
    ExceptionType    NVARCHAR(200)     NULL,
    ExceptionMessage NVARCHAR(1000)    NULL,
    StackTrace       NVARCHAR(MAX)     NULL,
    [Source]         NVARCHAR(200)     NULL,
    UserAction       NVARCHAR(200)     NULL,
    AdditionalData   NVARCHAR(MAX)     NULL,
    IsResolved       BIT               NOT NULL DEFAULT 0,
    ResolvedAt       DATETIME          NULL,
    ResolvedNotes    NVARCHAR(500)     NULL,

    CONSTRAINT CK_ErrorLog_Severity
        CHECK (Severity IN ('Info', 'Warning', 'Error', 'Critical'))
);
GO

-- Index للبحث السريع بالتاريخ
CREATE NONCLUSTERED INDEX IX_ErrorLog_DateTime
    ON ErrorLog (ErrorDateTime DESC);
GO

-- Index للبحث بالـ Severity
CREATE NONCLUSTERED INDEX IX_ErrorLog_Severity
    ON ErrorLog (Severity)
    WHERE IsResolved = 0;
GO
```

### Stored Procedures

```sql
-- ═══════════════════════════════════════════
-- SP: إدخال خطأ جديد
-- ═══════════════════════════════════════════
CREATE PROCEDURE SP_InsertErrorLog
    @Severity         NVARCHAR(20),
    @Message          NVARCHAR(500),
    @ExceptionType    NVARCHAR(200) = NULL,
    @ExceptionMessage NVARCHAR(1000) = NULL,
    @StackTrace       NVARCHAR(MAX) = NULL,
    @Source           NVARCHAR(200) = NULL,
    @UserAction       NVARCHAR(200) = NULL,
    @AdditionalData   NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO ErrorLog
        (Severity, [Message], ExceptionType, ExceptionMessage,
         StackTrace, [Source], UserAction, AdditionalData)
    VALUES
        (@Severity, @Message, @ExceptionType, @ExceptionMessage,
         @StackTrace, @Source, @UserAction, @AdditionalData);

    SELECT SCOPE_IDENTITY() AS ErrorLogID;
END;
GO

-- ═══════════════════════════════════════════
-- SP: جلب الأخطاء (للعرض في Debugging)
-- ═══════════════════════════════════════════
CREATE PROCEDURE SP_GetRecentErrors
    @Count      INT = 50,
    @Severity   NVARCHAR(20) = NULL,
    @OnlyUnresolved BIT = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP (@Count)
        ErrorLogID, ErrorDateTime, Severity,
        [Message], ExceptionType, ExceptionMessage,
        StackTrace, [Source], UserAction,
        IsResolved, ResolvedAt, ResolvedNotes
    FROM ErrorLog
    WHERE (@Severity IS NULL OR Severity = @Severity)
      AND (@OnlyUnresolved = 0 OR IsResolved = 0)
    ORDER BY ErrorDateTime DESC;
END;
GO

-- ═══════════════════════════════════════════
-- SP: إحصائيات الأخطاء
-- ═══════════════════════════════════════════
CREATE PROCEDURE SP_GetErrorStats
AS
BEGIN
    SET NOCOUNT ON;

    -- إجمالي الأخطاء
    SELECT
        COUNT(*) AS TotalErrors,
        SUM(CASE WHEN Severity = 'Critical' THEN 1 ELSE 0 END) AS CriticalCount,
        SUM(CASE WHEN Severity = 'Error' THEN 1 ELSE 0 END) AS ErrorCount,
        SUM(CASE WHEN Severity = 'Warning' THEN 1 ELSE 0 END) AS WarningCount,
        SUM(CASE WHEN IsResolved = 0 THEN 1 ELSE 0 END) AS UnresolvedCount,
        MIN(ErrorDateTime) AS FirstError,
        MAX(ErrorDateTime) AS LastError
    FROM ErrorLog;

    -- الأخطاء الأكثر تكراراً (Top 10)
    SELECT TOP 10
        [Message],
        ExceptionType,
        COUNT(*) AS Occurrences,
        MAX(ErrorDateTime) AS LastOccurrence
    FROM ErrorLog
    WHERE Severity IN ('Error', 'Critical')
    GROUP BY [Message], ExceptionType
    ORDER BY Occurrences DESC;

    -- الأخطاء حسب اليوم (آخر 7 أيام)
    SELECT
        CAST(ErrorDateTime AS DATE) AS [Date],
        Severity,
        COUNT(*) AS [Count]
    FROM ErrorLog
    WHERE ErrorDateTime >= DATEADD(DAY, -7, GETDATE())
    GROUP BY CAST(ErrorDateTime AS DATE), Severity
    ORDER BY [Date] DESC, Severity;
END;
GO

-- ═══════════════════════════════════════════
-- SP: تسجيل كمحلول
-- ═══════════════════════════════════════════
CREATE PROCEDURE SP_ResolveError
    @ErrorLogID   INT,
    @ResolvedNotes NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE ErrorLog
    SET IsResolved = 1,
        ResolvedAt = GETDATE(),
        ResolvedNotes = @ResolvedNotes
    WHERE ErrorLogID = @ErrorLogID;
END;
GO

-- ═══════════════════════════════════════════
-- SP: تنظيف الأخطاء القديمة
-- ═══════════════════════════════════════════
CREATE PROCEDURE SP_CleanupErrorLog
    @DaysToKeep INT = 90
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM ErrorLog
    WHERE ErrorDateTime < DATEADD(DAY, -@DaysToKeep, GETDATE())
      AND IsResolved = 1;

    -- الأخطاء الغير محلولة متحذفش أبداً
END;
GO
```

### DAL: clsErrorLogData.cs

```csharp
namespace DAL.Helper
{
    public static class clsErrorLogData
    {
        public static int Insert(string message, string? exceptionType = null,
            string? exceptionMessage = null, string? stackTrace = null,
            string source = "Unknown", string severity = "Error",
            string? userAction = null, string? additionalData = null)
        {
            var parameters = new SqlParameter[]
            {
                new("@Severity", severity),
                new("@Message", message),
                new("@ExceptionType", (object?)exceptionType ?? DBNull.Value),
                new("@ExceptionMessage", (object?)exceptionMessage ?? DBNull.Value),
                new("@StackTrace", (object?)stackTrace ?? DBNull.Value),
                new("@Source", source),
                new("@UserAction", (object?)userAction ?? DBNull.Value),
                new("@AdditionalData", (object?)additionalData ?? DBNull.Value)
            };

            var result = clsDataAccessHelper.ExecuteScalar("SP_InsertErrorLog", parameters);
            return result != null ? Convert.ToInt32(result) : -1;
        }
    }
}
```

---

## ═══════════════════════════════════════════════
## 4. Global Error Handling (Program.cs)
## ═══════════════════════════════════════════════

### الفكرة

> نظام شامل لالتقاط **كل** الأخطاء — حتى اللي مش متوقعة.
> 3 مستويات: UI Thread + Background Threads + AppDomain

```csharp
// Program.cs
static class Program
{
    [STAThread]
    static void Main()
    {
        // ═══ 1. Global Exception Handlers ═══
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

        // UI Thread exceptions
        Application.ThreadException += (sender, e) =>
        {
            clsEventLogger.LogError("UI Thread Exception", e.Exception);
            ShowCrashDialog(e.Exception);
        };

        // Background Thread exceptions
        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            var ex = e.ExceptionObject as Exception;
            clsEventLogger.LogCritical("Unhandled AppDomain Exception", ex);

            if (e.IsTerminating)
                clsEventLogger.LogCritical("Application is terminating due to unhandled exception");
        };

        // Task exceptions
        TaskScheduler.UnobservedTaskException += (sender, e) =>
        {
            clsEventLogger.LogError("Unobserved Task Exception", e.Exception);
            e.SetObserved(); // منع الكراش
        };

        // ═══ 2. App Startup Logging ═══
        clsEventLogger.LogInfo($"Salati started — v{Application.ProductVersion}");

        // ═══ 3. Load Settings from Registry ═══
        var theme = clsSettingsStore.ActiveThemeName;    // يقرأ من Registry
        var lang = clsSettingsStore.LanguageCode;

        // ═══ 4. Initialize and Run ═══
        ApplicationConfiguration.Initialize();

        // Splash → Main
        Application.Run(new frmSplash());

        // ═══ 5. Shutdown Logging ═══
        clsEventLogger.LogInfo("Salati shutdown gracefully");
    }

    private static void ShowCrashDialog(Exception ex)
    {
        var result = MessageBox.Show(
            $"حدث خطأ غير متوقع:\n{ex.Message}\n\nهل تريد المتابعة؟",
            "Salati — خطأ",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Error
        );

        if (result == DialogResult.No)
            Application.Exit();
    }
}
```

---

## ═══════════════════════════════════════════════
## 5. Advanced T-SQL Patterns
## ═══════════════════════════════════════════════

### فهرسة (Indexing) احترافية

```sql
-- ═══ أهم الفهارس لأداء سريع ═══

-- DailyPrayerTimes: بحث سريع بالتاريخ
CREATE NONCLUSTERED INDEX IX_PrayerTimes_Date
    ON DailyPrayerTimes ([Date] DESC)
    INCLUDE (FajrTime, DhuhrTime, AsrTime, MaghribTime, IshaTime);
GO

-- AlertLog: بحث بالتاريخ + الصلاة
CREATE NONCLUSTERED INDEX IX_AlertLog_DatePrayer
    ON AlertLog (AlertDateTime DESC, Prayer)
    INCLUDE (AlertType, WasDismissed);
GO

-- PrayerTracking: بحث بالتاريخ + الحالة
CREATE NONCLUSTERED INDEX IX_Tracking_DateStatus
    ON PrayerTracking ([Date] DESC, [Status])
    INCLUDE (Prayer, PrayerTime);
GO

-- AppSettings: بحث بالمفتاح
-- UNIQUE Index على SettingKey موجود بالفعل من القيد
```

### UPSERT Pattern (MERGE)

```sql
-- بدل IF EXISTS → UPDATE / ELSE → INSERT
-- استخدم MERGE لعملية واحدة ذرية (Atomic)

CREATE PROCEDURE SP_UpsertPrayerTimes
    @LocationID INT, @Date DATE,
    @Fajr TIME, @Sunrise TIME, @Dhuhr TIME,
    @Asr TIME, @Maghrib TIME, @Isha TIME,
    @Source TINYINT, @HijriDate NVARCHAR(30)
AS
BEGIN
    SET NOCOUNT ON;

    MERGE DailyPrayerTimes AS target
    USING (SELECT @LocationID, @Date) AS source (LocationID, [Date])
    ON target.LocationID = source.LocationID AND target.[Date] = source.[Date]

    WHEN MATCHED THEN UPDATE SET
        FajrTime = @Fajr, SunriseTime = @Sunrise,
        DhuhrTime = @Dhuhr, AsrTime = @Asr,
        MaghribTime = @Maghrib, IshaTime = @Isha,
        [Source] = @Source, HijriDate = @HijriDate,
        FetchedAt = GETDATE()

    WHEN NOT MATCHED THEN INSERT
        (LocationID, [Date], FajrTime, SunriseTime, DhuhrTime,
         AsrTime, MaghribTime, IshaTime, [Source], HijriDate)
    VALUES
        (@LocationID, @Date, @Fajr, @Sunrise, @Dhuhr,
         @Asr, @Maghrib, @Isha, @Source, @HijriDate);
END;
GO
```

### Defensive Coding في T-SQL

```sql
-- كل SP يبدأ بـ TRY/CATCH
CREATE PROCEDURE SP_UpdateAlertConfigSafe
    @Prayer TINYINT, @IsEnabled BIT,
    @MinutesBefore INT, @SoundID INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- ═══ Validation ═══
        IF @Prayer NOT BETWEEN 1 AND 5
        BEGIN
            RAISERROR('Invalid Prayer value: %d', 16, 1, @Prayer);
            RETURN -1;
        END

        IF @MinutesBefore NOT BETWEEN 0 AND 120
        BEGIN
            RAISERROR('Invalid MinutesBefore: %d', 16, 1, @MinutesBefore);
            RETURN -2;
        END

        IF NOT EXISTS (SELECT 1 FROM Sounds WHERE SoundID = @SoundID)
        BEGIN
            RAISERROR('Sound not found: %d', 16, 1, @SoundID);
            RETURN -3;
        END

        -- ═══ Execute ═══
        UPDATE AlertConfigs
        SET IsEnabled = @IsEnabled,
            MinutesBefore = @MinutesBefore,
            SoundID = @SoundID,
            UpdatedDate = GETDATE()
        WHERE Prayer = @Prayer;

        RETURN @@ROWCOUNT;

    END TRY
    BEGIN CATCH
        -- تسجيل الخطأ
        INSERT INTO ErrorLog (Severity, [Message], ExceptionType, [Source])
        VALUES (
            'Error',
            ERROR_MESSAGE(),
            'SQL: ' + CAST(ERROR_NUMBER() AS NVARCHAR),
            'SP_UpdateAlertConfigSafe'
        );

        RETURN -99;
    END CATCH
END;
GO
```

### Query Optimization Tips

```sql
-- ═══ 1. SET NOCOUNT ON في كل SP (يقلل network traffic) ═══
SET NOCOUNT ON;

-- ═══ 2. استخدم TOP مع ORDER BY (مش SELECT * من جدول كبير) ═══
SELECT TOP 50 * FROM ErrorLog ORDER BY ErrorDateTime DESC;

-- ═══ 3. استخدم EXISTS بدل COUNT لو بتتحقق بس ═══
-- ❌ بطيء
IF (SELECT COUNT(*) FROM DailyPrayerTimes WHERE [Date] = @Date) > 0
-- ✅ سريع
IF EXISTS (SELECT 1 FROM DailyPrayerTimes WHERE [Date] = @Date)

-- ═══ 4. Parameterized queries دايماً (ضد SQL Injection) ═══
-- الـ ADO.NET بيعمل كده تلقائي مع SqlParameter

-- ═══ 5. Index hints لما تحتاج ═══
SELECT * FROM ErrorLog WITH (INDEX(IX_ErrorLog_DateTime))
WHERE ErrorDateTime >= @StartDate;

-- ═══ 6. Batch operations بدل row-by-row ═══
-- حذف بالـ batch (1000 صف كل مرة — مش يقفل الجدول)
WHILE 1 = 1
BEGIN
    DELETE TOP (1000) FROM AlertLog
    WHERE AlertDateTime < DATEADD(DAY, -90, GETDATE());

    IF @@ROWCOUNT < 1000 BREAK;
END;
```

---

## 📊 ملخص الهيكل النهائي

```
┌─── Registry (Instant) ──────────────────────────┐
│  HKCU\SOFTWARE\Salati\                           │
│  الإعدادات السريعة: ثيم، لغة، startup، volume    │
│  Fallback → Default values في الكود              │
└──────────────────────────────────────────────────┘
         │
         │ لو تلف → يرجع تلقائي
         ▼
┌─── SQL Server (Persistent) ─────────────────────┐
│  SalatiDB                                        │
│  البيانات الكبيرة: مواعيد، تنبيهات، سجل، أخطاء  │
│  + ErrorLog لكل الأخطاء                          │
│  + Advanced T-SQL مع TRY/CATCH                   │
└──────────────────────────────────────────────────┘
         │
         │ كل خطأ يتسجل
         ▼
┌─── Event Viewer (System-wide) ──────────────────┐
│  Source: "Salati"                                 │
│  Info / Warning / Error                          │
│  يتشاف من Windows Event Viewer                   │
│  مفيد حتى لو التطبيق والـ DB فيهم مشكلة         │
└──────────────────────────────────────────────────┘
         │
         │ آخر خط دفاع
         ▼
┌─── Global Error Handlers (Program.cs) ──────────┐
│  UI Thread → ThreadException                     │
│  Background → AppDomain.UnhandledException       │
│  Tasks → TaskScheduler.UnobservedTaskException   │
│  بيشتغلوا حتى لو كل حاجة تانية وقعت            │
└──────────────────────────────────────────────────┘
```

### الفرق عن المشاريع القديمة:

| النقطة | DVLD / Aura | Salati |
|--------|-------------|--------|
| **الإعدادات** | JSON (%AppData%) | **Windows Registry** (أسرع + مش ملف يتحذف) |
| **الأخطاء** | Global handlers → ملف log | **Event Viewer + Database** (مراقبة كاملة) |
| **T-SQL** | SPs عادية | **MERGE + TRY/CATCH + Indexes** (احترافي) |
| **Fallback** | لو JSON تلف = reset | **تلقائي لكل إعداد على حدة** (ذكي) |
