# 📝 Salati — Features Specification (مواصفات الميزات)

> هذا الملف يفصّل كل ميزة بالتحديد: ماذا تفعل، كيف تعمل تقنياً، وما المطلوب من كل طبقة.
> **محدّث للنمط الجديد:** Registry + SQL Server + Hybrid UI.

---

## 📊 ملخص الميزات

| # | الميزة | الأولوية | الصعوبة | حالة |
|---|--------|----------|---------|------|
| 1 | مواعيد صلاة من API | 🔴 1 | 🟡 متوسط | 🔲 لم تبدأ |
| 2 | مواعيد صلاة يدوية | 🔴 1 | 🟢 سهل | 🔲 لم تبدأ |
| 3 | التنبيه قبل الصلاة | 🔴 1 | 🟡 متوسط | 🔲 لم تبدأ |
| 4 | صوت الأذان | 🔴 2 | 🟢 سهل | 🔲 لم تبدأ |
| 5 | System Tray | 🔴 2 | 🟢 سهل | 🔲 لم تبدأ |
| 6 | إعدادات (Windows Registry) | 🔴 1 | 🟡 متوسط | 🔲 لم تبدأ |
| 7 | دعم اللغات (AR/EN) | 🟡 3 | 🟢 سهل | 🔲 لم تبدأ |
| 8 | ثيمات (Dark/Light) | 🟡 3 | 🟢 سهل | 🔲 لم تبدأ |
| 9 | تشغيل مع الويندوز | 🟡 3 | 🟢 سهل | 🔲 لم تبدأ |
| 10 | Splash Screen | 🟡 4 | 🟢 سهل | 🔲 لم تبدأ |
| 11 | Widget Mode | 🟡 3 | 🟡 متوسط | 🔲 لم تبدأ |
| 12 | Error Logging | 🔴 1 | 🟡 متوسط | 🔲 لم تبدأ |

---

## Feature 1: مواعيد صلاة من API

### DAL:
| الملف | التفاصيل |
|-------|----------|
| `clsPrayerTimeData.cs` | `GetFromAPI(city, country, method)` → `List<PrayerTime>` |
| `clsDailyPrayerTimeData.cs` | `SP_InsertDailyPrayerTime` / `SP_GetTodayPrayerTimes` (Cache) |

### BLL:
| الملف | التفاصيل |
|-------|----------|
| `clsPrayerTimeManager.cs` | `GetTodayPrayers()` — API with DB cache fallback |

### UI:
| الملف | التفاصيل |
|-------|----------|
| `ucSettingsPrayer.cs` | ComboBoxes (دولة، مدينة، طريقة حساب) + زر تحديث |
| `ucNextPrayer.cs` | عرض الصلاة القادمة + countdown |
| `ucPrayerCard.cs` ×5 | عرض كل صلاة |

---

## Feature 2: مواعيد صلاة يدوية

### DAL: يتحفظ في Registry (عبر clsRegistryManager)
### BLL: `clsPrayerTimeManager.GetManualTimes()` يقرأ من Settings
### UI: `ucSettingsPrayer.cs` — 5 حقول TimePicker عند اختيار Manual

---

## Feature 3: التنبيه قبل الصلاة (Core Feature ⭐)

### DAL:
| الملف | التفاصيل |
|-------|----------|
| `clsAlertLogData.cs` | `SP_InsertAlertLog` — تسجيل كل تنبيه |
| `clsAlertConfigData.cs` | `SP_GetAlertConfigs` / `SP_UpsertAlertConfig` |

### BLL:
```csharp
public class clsAlertScheduler
{
    public event EventHandler<AlertEventArgs>? PreAlertTriggered;
    public event EventHandler<AlertEventArgs>? AdhanAlertTriggered;
    
    public void Start(List<PrayerTime> times, List<AlertConfig> configs);
    public void UpdateSchedule(List<PrayerTime> times, List<AlertConfig> configs);
    public void Stop();
    // كل 30 ثانية يتشيك
}
```

### UI:
| الملف | التفاصيل |
|-------|----------|
| `frmAlert.cs` | Popup TopMost — emoji + اسم + وقت + كتم + إغلاق |
| `ucSettingsAlerts.cs` | ucAlertRow × 5 (per-prayer settings) |

---

## Feature 4: صوت الأذان

### BLL:
```csharp
public static class clsSoundPlayer
{
    public static void PlayAdhan(string soundFile, int volume);
    public static void PlayBeep();
    public static void Stop();
    public static bool IsPlaying { get; }
    public static string[] GetAvailableSounds();
}
```

### UI: `frmAlert.cs` يستدعي PlayAdhan عند الظهور، زر 🔇 يستدعي Stop

---

## Feature 5: System Tray

### UI:
```csharp
// في frmMain
private NotifyIcon _trayIcon;
private ContextMenuStrip _trayMenu;

// القائمة:
// 🕌 Salati (header)
// ⏳ العصر: 03:45 PM (info)
// ──────
// 📂 Open | فتح
// 📌 Widget | وضع مصغر
// 🔇 Mute | كتم (toggle)
// ⚙️ Settings | إعدادات
// ──────
// ❌ Exit | خروج
```

---

## Feature 6: إعدادات (Windows Registry) 🆕

### DAL:
| الملف | التفاصيل |
|-------|----------|
| `clsRegistryManager.cs` | Read/Write Registry `HKCU\SOFTWARE\Salati\` |

### المسار:
```
HKEY_CURRENT_USER\SOFTWARE\Salati\
├── Theme          = "Midnight Serenity"
├── Language       = "ar"
├── Volume         = 80
├── PrayerSource   = "API"
├── Country        = "Egypt"
├── City           = "Cairo"
├── Method         = 5
├── StartWithWindows = 1
├── MinimizeOnStart  = 0
├── ShowInTray       = 1
├── CloseToTray      = 1
└── Alerts\
    ├── Fajr\   (Enabled=1, Minutes=10, Sound="adhan_makkah.mp3")
    ├── Dhuhr\  (...)
    ├── Asr\    (...)
    ├── Maghrib\(...)
    └── Isha\   (...)
```

### Fallback:
لو الـ Registry key مش موجود أو فيه error → يرجع للقيم الافتراضية تلقائي.

---

## Feature 9: تشغيل مع الويندوز

### DAL:
```csharp
// في clsRegistryManager.cs
// Path: HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Run
public static void EnableStartup();
public static void DisableStartup();
public static bool IsStartupEnabled();
```

---

## Feature 11: Widget Mode 🆕

### UI:
```csharp
// في frmMain
public void SwitchToWidgetMode()
{
    this.Size = new Size(300, 85);
    this.TopMost = true;
    // إخفاء كل العناصر ما عدا الصلاة القادمة
}

public void SwitchToFullMode()
{
    this.Size = new Size(700, 500);
    this.TopMost = false;
    // إظهار كل العناصر
}
```

---

## Feature 12: Error Logging 🆕

### DAL:
| الملف | التفاصيل |
|-------|----------|
| `clsEventLogger.cs` | Windows Event Viewer (Info/Warning/Error) |
| `clsErrorLogData.cs` | `SP_InsertErrorLog` → ErrorLog table |

### UI:
```csharp
// Program.cs — Global Error Handlers
Application.ThreadException += GlobalHandler;
AppDomain.CurrentDomain.UnhandledException += GlobalHandler;
TaskScheduler.UnobservedTaskException += GlobalHandler;
```

---

## 📋 ترتيب التنفيذ المقترح

```
المرحلة 1 — الأساس:
  ├── Feature 12: Error Logging ← الأول عشان الكل يستفيد
  ├── Feature 6: إعدادات Registry ← الكل يعتمد عليها
  ├── Feature 1: مواعيد صلاة API
  ├── Feature 2: مواعيد صلاة يدوية
  └── UI: frmMain + ucTitleBar + ucNextPrayer + ucPrayerCard + ucInfoBar

المرحلة 2 — التنبيهات:
  ├── Feature 3: التنبيه قبل الصلاة ⭐
  ├── Feature 4: صوت الأذان
  └── UI: frmAlert + ucSettingsPanel + ucSettingsAlerts

المرحلة 3 — البنية التحتية:
  ├── Feature 5: System Tray
  ├── Feature 9: تشغيل مع الويندوز
  ├── Feature 7: دعم اللغات
  ├── Feature 8: ثيمات
  ├── Feature 11: Widget Mode
  └── UI: ucSettingsAppearance + ucSettingsGeneral

المرحلة 4 — التلميع:
  ├── Feature 10: Splash Screen
  ├── Testing + Bug Fixes
  └── v1.0 Release 🎉
```

---

## 📦 ملخص التغييرات حسب الطبقة

### DAL (10 ملفات)
| # | الملف | الميزة |
|---|-------|--------|
| 1 | `clsDataAccessHelper.cs` | ADO.NET wrapper |
| 2 | `clsHttpHelper.cs` | HttpClient wrapper |
| 3 | `clsPrayerTimeData.cs` | Aladhan API |
| 4 | `clsDailyPrayerTimeData.cs` | DB cache |
| 5 | `clsRegistryManager.cs` | Windows Registry |
| 6 | `clsLocationData.cs` | Locations table |
| 7 | `clsAlertConfigData.cs` | AlertConfigs table |
| 8 | `clsSoundData.cs` | Sounds table |
| 9 | `clsEventLogger.cs` | Event Viewer |
| 10 | `clsErrorLogData.cs` | ErrorLog table |

### BLL (4 ملفات)
| # | الملف | الميزة |
|---|-------|--------|
| 1 | `clsPrayerTimeManager.cs` | Prayer time management |
| 2 | `clsAlertScheduler.cs` | Timer scheduling |
| 3 | `clsSoundPlayer.cs` | NAudio playback |
| 4 | `clsSettingsManager.cs` | Settings validation |

### UI — Controls (14 كنترول)
| # | الملف | الفئة |
|---|-------|-------|
| 1 | `ucTitleBar.cs` | Layout |
| 2 | `ucInfoBar.cs` | Layout |
| 3 | `ucNextPrayer.cs` | Card |
| 4 | `ucPrayerCard.cs` | Card |
| 5 | `ucThemeCard.cs` | Card |
| 6 | `ucLanguageCard.cs` | Card |
| 7 | `ucSettingsPanel.cs` | Settings |
| 8 | `ucSettingsPrayer.cs` | Settings |
| 9 | `ucSettingsAlerts.cs` | Settings |
| 10 | `ucSettingsAppearance.cs` | Settings |
| 11 | `ucSettingsGeneral.cs` | Settings |
| 12 | `ucAlertRow.cs` | Settings |
| 13 | `ucToast.cs` | Feedback |
| 14 | `ucMessageBox.cs` | Feedback |
