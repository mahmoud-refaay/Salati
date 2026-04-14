# 🧠 Salati — AI Context Reference

# ═══════════════════════════════════════════════════════════════════
# آخر تحديث: 13 أبريل 2026
# الهدف: ملف مرجعي يُقرأ في بداية أي شات جديد — يوفّر التوكنز
# الاستخدام: "اقرأ الملف D:\My_Code\learn code\Projects\Salati\doc\basics\07_AI_CONTEXT.md وبعدين [الطلب]"
# ═══════════════════════════════════════════════════════════════════

## 📋 معلومات المشروع الأساسية

| العنصر | القيمة |
|--------|--------|
| **الاسم** | Salati — Prayer Time Reminder |
| **النوع** | WinForms Desktop App |
| **الإطار** | .NET 9.0-windows |
| **معمارية** | 3-Tier (DAL → BLL → UI) |
| **UI Framework** | Guna.UI2 (NuGet) |
| **قاعدة البيانات** | SQL Server — 8 جداول — ~30 SP |
| **الإعدادات** | Windows Registry (`HKCU\SOFTWARE\Salati\`) + fallback تلقائي |
| **Logging** | Windows Event Viewer + ErrorLog table |
| **نمط الـ UI** | **Hybrid** — لا Sidebar، لا NavigationManager |
| **الأيقونات** | Emoji نصية فقط (🌅☀️🌤️🌇🌙) — بدون صور PNG |
| **المسار الجذري** | `D:\My_Code\learn code\Projects\Salati\` |
| **Solution** | `Salati.slnx` |
| **Build** | `dotnet build "UI/UI.csproj"` |

---

## 🏗️ هيكل المشروع (3 طبقات)

```
Salati/
├── DAL/                              ← Data Access Layer
│   ├── Helper/ (clsDataAccessHelper, clsHttpHelper)
│   ├── PrayerTimes/ (clsPrayerTimeData, clsDailyPrayerTimeData)
│   ├── Settings/ (clsRegistryManager, clsLocationData, clsAlertConfigData, clsSoundData, clsAppSettingsData)
│   └── Logging/ (clsEventLogger, clsErrorLogData, clsAlertLogData)
│
├── BLL/                              ← Business Logic Layer
│   ├── PrayerTimes/ (clsPrayerTimeManager)
│   ├── Alerts/ (clsAlertScheduler, clsSoundPlayer)
│   └── Settings/ (clsSettingsManager)
│
├── UI/                               ← Presentation Layer
│   ├── Program.cs                   ← Entry point + global error handlers
│   ├── Forms/ (frmSplash, frmMain, frmAlert)
│   ├── Controls/
│   │   ├── Layout/ (ucTitleBar, ucInfoBar)
│   │   ├── Card/ (ucNextPrayer, ucPrayerCard, ucThemeCard, ucLanguageCard)
│   │   ├── Settings/ (ucSettingsPanel, ucSettingsPrayer, ucSettingsAlerts,
│   │   │              ucSettingsAppearance, ucSettingsGeneral, ucAlertRow)
│   │   └── Feedback/ (ucToast, ucMessageBox)
│   ├── Core/
│   │   ├── Engine/ (clsUIEngine)
│   │   ├── Theme/ (clsThemeManager, ThemeColors, BuiltInThemes, IThemeable)
│   │   ├── Language/ (clsLanguageManager, ILanguagePack, ILocalizable, Partials/, Languages/)
│   │   ├── Settings/ (clsSettingsStore — Registry wrapper)
│   │   └── Validation/ (clsValidator)
│   └── Assets/ (Sounds/, Icons/)
│
└── doc/ (basics/, DB/, UI_UX/)
```

---

## 🎨 نظام الثيمات

| الثيم | النوع |
|-------|-------|
| **Midnight Serenity** | 🌙 Dark (default) — أخضر إسلامي + ذهبي |
| **Golden Dawn** | ☀️ Light — بيج دافئ + أخضر غامق |

**كل control يطبّق IThemeable**: `ApplyTheme(ThemeColors t)`
**كل control يطبّق ILocalizable**: `ApplyLanguage(ILanguagePack lang)`

### ألوان Theme الأساسية:
```
Accent1 = #1B8A6B (أخضر إسلامي)
Accent2 = #C8A96E (ذهبي)
Accent3 = #E06C75 (أحمر)
BgPrimary = #0D1117 (أسود مزرق)
TextPrimary = #E6EDF3 (أبيض)
```

---

## 📐 نمط الـ UI (Hybrid)

```
frmMain → 3 أوضاع:
├── Full Mode (700×500) — Dashboard كامل
├── Widget Mode (300×85) — Compact always-on-top
└── Tray Mode (مخفي) — NotifyIcon في Taskbar

الانتقال بين الأوضاع:
- ucTitleBar.📌 → Widget Mode
- ucTitleBar.✕ → Tray Mode (أو Exit)
- TrayIcon.DoubleClick → Full Mode
- ucTitleBar.⚙️ → ucSettingsPanel (slide from right)
```

**مفيش:**
- ❌ Sidebar
- ❌ NavigationManager
- ❌ ScreenRegistry
- ❌ IScreen
- ❌ UserControl screens تتبدل
- ❌ صور PNG للأيقونات (Emoji بس)

---

## ⚙️ أنماط معمارية مهمة

### 1. Theme Application
```csharp
public void ApplyTheme(ThemeColors t) {
    this.BackColor = t.BgPrimary;
    label.ForeColor = t.TextPrimary;
    panel.FillColor = t.BgSurface;
}
```

### 2. Settings Store (Registry)
```csharp
clsSettingsStore.Load();           // مرة في Program.cs
clsSettingsStore.PrayerSource;     // قراءة
clsSettingsStore.Volume = 80;      // كتابة
clsSettingsStore.Save();           // حفظ في Registry
// لو error → fallback لقيم افتراضية
```

### 3. Alert Scheduler
```csharp
_scheduler.AlertTriggered += OnAlert;
_scheduler.Start(prayerTimes, alertConfigs);
// كل 30 ثانية check
```

### 4. Toast Notifications
```csharp
ucToast.ShowSuccess(this.FindForm()!, "تم الحفظ");
ucToast.ShowError(parentForm, "حدث خطأ");
```

### 5. Error Handling
```csharp
// Program.cs — global handlers
clsEventLogger.LogError(exception);     // Event Viewer
clsErrorLogData.Insert(exception);      // DB ErrorLog table
```

---

## 📦 Enums

```csharp
public enum ePrayer { Fajr=1, Dhuhr=2, Asr=3, Maghrib=4, Isha=5 }
public enum ePrayerSource { API=1, Manual=2 }
public enum eAlertType { AdhanSound=1, SimpleBeep=2, WindowsNotification=3 }
public enum ePrayerStatus { Passed=1, Next=2, Upcoming=3, Disabled=4 }
```

---

## 📌 ملاحظات مهمة للـ AI

1. **لا يوجد Sidebar/NavigationManager** — Dashboard ثابت في frmMain
2. **Settings = slide panel من اليمين** — مش شاشات منفصلة
3. **Guna2GradientButton** يستخدم `FillColor + FillColor2` مش `BackColor`
4. **الإعدادات في Registry** — `HKCU\SOFTWARE\Salati\` عبر `clsSettingsStore`
5. **صفر ألوان hardcoded** — كل حاجة من ThemeColors
6. **صفر نصوص hardcoded** — كل حاجة من ILanguagePack
7. **Emoji فقط للأيقونات** (🌅☀️🌤️🌇🌙) — بدون PNG/SVG
8. **SQL Server** = قاعدة البيانات — 8 جداول + ~30 SP
9. **NAudio** لتشغيل الأصوات — مش `System.Media.SoundPlayer`
10. **Event Viewer + ErrorLog** — نظام logging مزدوج
11. **3 أوضاع لـ frmMain** — Full / Widget / Tray
12. **للتوثيق الكامل** اقرأ أيضاً: `UIStructure.md` + `09_INFRASTRUCTURE.md` + `DATABASE.md`
