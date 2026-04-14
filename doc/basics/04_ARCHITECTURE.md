# 🏗️ Salati — Architecture (الهيكل التقني)

> هذا الملف يوثق الهيكل التقني الكامل بنفس نمط مشاريع DVLD و Aura.
> **3-Tier Architecture**: DAL → BLL → UI
> **نمط Hybrid** — لا Sidebar، لا NavigationManager.

---

## 📁 هيكل المشروع الكامل

```
Salati/
├── Salati.slnx                      ← Solution file (.slnx format)
│
├── DAL/                              ← Data Access Layer
│   ├── DAL.csproj
│   ├── Helper/
│   │   ├── clsDataAccessHelper.cs   ← ADO.NET wrapper (SP execution)
│   │   └── clsHttpHelper.cs         ← HttpClient wrapper لـ API calls
│   ├── PrayerTimes/
│   │   ├── clsPrayerTimeData.cs     ← التواصل مع Aladhan API
│   │   └── clsDailyPrayerTimeData.cs ← CRUD → DailyPrayerTimes table
│   ├── Settings/
│   │   ├── clsRegistryManager.cs    ← Windows Registry (كل الإعدادات)
│   │   ├── clsLocationData.cs       ← CRUD → Locations table
│   │   ├── clsAlertConfigData.cs    ← CRUD → AlertConfigs table
│   │   ├── clsSoundData.cs          ← CRUD → Sounds table
│   │   └── clsAppSettingsData.cs    ← CRUD → AppSettings table
│   ├── Logging/
│   │   ├── clsEventLogger.cs        ← Windows Event Viewer logging
│   │   ├── clsErrorLogData.cs       ← CRUD → ErrorLog table
│   │   └── clsAlertLogData.cs       ← CRUD → AlertLog table
│   └── Tracking/
│       └── clsPrayerTrackingData.cs  ← CRUD → PrayerTracking table (v1.5)
│
├── BLL/                              ← Business Logic Layer
│   ├── BLL.csproj
│   ├── PrayerTimes/
│   │   └── clsPrayerTimeManager.cs  ← إدارة مواعيد الصلاة (API + Manual + Cache)
│   ├── Alerts/
│   │   ├── clsAlertScheduler.cs     ← جدولة التنبيهات بالـ Timer
│   │   └── clsSoundPlayer.cs        ← تشغيل أصوات الأذان (NAudio)
│   └── Settings/
│       └── clsSettingsManager.cs    ← منطق الإعدادات + validation + defaults
│
├── UI/                               ← Presentation Layer (WinForms)
│   ├── UI.csproj
│   ├── Program.cs                   ← Entry point + global error handlers
│   │
│   ├── Forms/                        ← النوافذ الأساسية (3 نوافذ فقط)
│   │   ├── frmSplash.cs
│   │   ├── frmMain.cs               ← النافذة الرئيسية (Dashboard + Widget + Tray)
│   │   └── frmAlert.cs              ← نافذة التنبيه (Popup TopMost)
│   │
│   ├── Controls/                     ← Custom Controls (14 كنترول)
│   │   ├── Layout/
│   │   │   ├── ucTitleBar.cs        ← شريط العنوان (📌⚙️🌙 AR ─ ✕)
│   │   │   └── ucInfoBar.cs         ← شريط المعلومات (📅 📍 ▼)
│   │   ├── Card/
│   │   │   ├── ucNextPrayer.cs      ← كارت الصلاة القادمة (Hero)
│   │   │   ├── ucPrayerCard.cs      ← كارت صلاة واحدة
│   │   │   ├── ucThemeCard.cs       ← كارت اختيار ثيم
│   │   │   └── ucLanguageCard.cs    ← كارت اختيار لغة
│   │   ├── Settings/
│   │   │   ├── ucSettingsPanel.cs   ← الـ Slide Panel (4 tabs)
│   │   │   ├── ucSettingsPrayer.cs  ← Tab مواعيد الصلاة
│   │   │   ├── ucSettingsAlerts.cs  ← Tab التنبيهات
│   │   │   ├── ucSettingsAppearance.cs ← Tab المظهر
│   │   │   ├── ucSettingsGeneral.cs ← Tab عام
│   │   │   └── ucAlertRow.cs        ← صف تنبيه صلاة واحدة
│   │   └── Feedback/
│   │       ├── ucToast.cs           ← Toast notifications
│   │       └── ucMessageBox.cs      ← Confirm/Alert dialogs
│   │
│   ├── Core/                         ← البنية التحتية
│   │   ├── Engine/
│   │   │   └── clsUIEngine.cs       ← يطبق Theme + Language على كل control
│   │   ├── Theme/
│   │   │   ├── clsThemeManager.cs   ← إدارة الثيمات + Event: ThemeChanged
│   │   │   ├── ThemeColors.cs       ← كل خصائص الألوان
│   │   │   ├── BuiltInThemes.cs     ← MidnightSerenity + GoldenDawn
│   │   │   ├── ThemeColorUtils.cs   ← Darken, Lighten, Alpha
│   │   │   ├── ThemeModels.cs       ← ThemeDefinition model
│   │   │   └── IThemeable.cs        ← interface void ApplyTheme(ThemeColors t)
│   │   ├── Language/
│   │   │   ├── ILanguagePack.cs     ← العقد الأساسي (partial interface)
│   │   │   ├── ILocalizable.cs      ← interface void ApplyLanguage(ILanguagePack)
│   │   │   ├── clsLanguageManager.cs ← إدارة اللغات + RTL/LTR + Event
│   │   │   ├── Partials/
│   │   │   │   ├── ILanguagePack.Common.cs
│   │   │   │   ├── ILanguagePack.Layout.cs
│   │   │   │   ├── ILanguagePack.Dashboard.cs
│   │   │   │   ├── ILanguagePack.Prayers.cs
│   │   │   │   ├── ILanguagePack.Settings.cs
│   │   │   │   └── ILanguagePack.Alert.cs
│   │   │   └── Languages/
│   │   │       ├── Arabic/ (ArabicPack.cs + Partials/)
│   │   │       └── English/ (EnglishPack.cs + Partials/)
│   │   ├── Settings/
│   │   │   └── clsSettingsStore.cs  ← Registry wrapper (يقرأ/يكتب من clsRegistryManager)
│   │   └── Validation/
│   │       └── clsValidator.cs      ← Input validation helpers
│   │
│   └── Assets/                       ← الأصول
│       ├── Sounds/                   ← أصوات الأذان (.mp3/.wav)
│       │   ├── adhan_makkah.mp3
│       │   ├── adhan_madinah.mp3
│       │   ├── beep_simple.wav
│       │   └── beep_double.wav
│       └── Icons/                    ← أيقونتين فقط (ICO)
│           ├── app_icon.ico
│           ├── tray_normal.ico
│           └── tray_alert.ico
│
└── doc/                              ← التوثيق
    ├── basics/ (8 ملفات)
    ├── DB/ (DATABASE.md)
    └── UI_UX/ (UIStructure.md + 9 mockups)
```

---

## 🔗 Solution Structure (.slnx)

```xml
<Solution>
  <Project Path="DAL/DAL.csproj" />
  <Project Path="BLL/BLL.csproj" />
  <Project Path="UI/UI.csproj" />
</Solution>
```

### Dependencies:
```
UI → BLL → DAL
UI لا يرجع لـ DAL مباشرةً — كل حاجة عبر BLL
```

---

## 📦 NuGet Packages

| Package | المشروع | الاستخدام |
|---------|---------|-----------|
| **NAudio** | BLL | تشغيل أصوات الأذان |
| **System.Data.SqlClient** | DAL | ADO.NET + SQL Server |
| **Microsoft.Win32.Registry** | DAL | Windows Registry |
| **Guna.UI2.WinForms** | UI | Custom UI controls |

---

## 🧩 Models & Enums

### Enums

```csharp
public enum ePrayer { Fajr=1, Dhuhr=2, Asr=3, Maghrib=4, Isha=5 }
public enum ePrayerSource { API=1, Manual=2 }
public enum eAlertType { AdhanSound=1, SimpleBeep=2, WindowsNotification=3 }
public enum ePrayerStatus { Passed=1, Next=2, Upcoming=3, Disabled=4 }
```

### Models (في BLL)

```csharp
public class PrayerTime
{
    public ePrayer Prayer { get; set; }
    public TimeOnly Time { get; set; }
    public bool IsEnabled { get; set; }
}

public class AlertConfig
{
    public ePrayer Prayer { get; set; }
    public bool IsEnabled { get; set; }
    public int MinutesBefore { get; set; }
    public string SoundFile { get; set; }
    public eAlertType AlertType { get; set; }
}
```

---

## ⚙️ الأنماط المعمارية

### 1. IThemeable Interface
```csharp
public interface IThemeable
{
    void ApplyTheme(ThemeColors t);
}
```

### 2. ILocalizable Interface
```csharp
public interface ILocalizable
{
    void ApplyLanguage(ILanguagePack lang);
}
```

### 3. Theme Application
```csharp
public void ApplyTheme(ThemeColors t)
{
    this.BackColor = t.BgPrimary;
    lblTitle.ForeColor = t.TextPrimary;
    pnlCard.FillColor = t.BgSurface;
    btnSave.FillColor = t.Accent1;
    btnSave.FillColor2 = t.Accent2;
}
```

### 4. Settings Store Pattern (عبر Registry)
```csharp
// التخزين: HKCU\SOFTWARE\Salati\
clsSettingsStore.Load();           // مرة واحدة في Program.cs
clsSettingsStore.Save();           // بعد أي تغيير

var source = clsSettingsStore.PrayerSource;
clsSettingsStore.Volume = 80;
clsSettingsStore.Save();

// لو الـ Registry error → يرجع لـ Default values تلقائي
```

### 5. Alert Scheduler Pattern
```csharp
_alertScheduler = new clsAlertScheduler();
_alertScheduler.AlertTriggered += OnAlertTriggered;
_alertScheduler.Start(prayerTimes, alertConfigs);

// كل 30 ثانية يتشيك
_alertScheduler.UpdateSchedule(newTimes, newConfigs);
_alertScheduler.Stop();
```

### 6. Toast Notifications
```csharp
ucToast.ShowSuccess(this.FindForm()!, "تم الحفظ");
ucToast.ShowError(parentForm, "حدث خطأ");
ucToast.ShowWarning(parentForm, "تأكد من الاتصال بالإنترنت");
```

### 7. Error Handling (Global)
```csharp
// Program.cs
Application.ThreadException += (s, e) =>
{
    clsEventLogger.LogError(e.Exception);
    clsErrorLogData.Insert(e.Exception);
};

AppDomain.CurrentDomain.UnhandledException += (s, e) =>
{
    clsEventLogger.LogError((Exception)e.ExceptionObject);
    clsErrorLogData.Insert((Exception)e.ExceptionObject);
};
```

---

## 🔌 API Integration (Aladhan API)

### Endpoint:
```
GET https://api.aladhan.com/v1/timingsByCity?city={city}&country={country}&method={method}
```

### Response (مبسّطة):
```json
{
  "data": {
    "timings": {
      "Fajr": "04:35", "Dhuhr": "12:15", "Asr": "15:45",
      "Maghrib": "18:30", "Isha": "20:00"
    }
  }
}
```

---

## 📊 Data Flow

```
┌─────────────────────────────────────────────────┐
│                    DAL Layer                     │
│                                                  │
│  clsPrayerTimeData  ←→  Aladhan API (Internet)  │
│  clsRegistryManager ←→  Windows Registry        │
│  clsEventLogger     ←→  Windows Event Viewer    │
│  cls*Data classes    ←→  SQL Server (8 tables)   │
└─────────────┬───────────────────────────────────┘
              │
              ▼
┌─────────────────────────────────────────────────┐
│                    BLL Layer                     │
│                                                  │
│  clsPrayerTimeManager  ← API/Manual + Cache      │
│  clsAlertScheduler     ← Timer-based scheduling  │
│  clsSoundPlayer        ← NAudio playback         │
│  clsSettingsManager    ← validation + defaults   │
└─────────────┬───────────────────────────────────┘
              │
              ▼
┌─────────────────────────────────────────────────┐
│                    UI Layer                      │
│                                                  │
│  frmMain (Dashboard + Widget + Tray)             │
│  frmAlert (Popup TopMost)                        │
│  14 UserControls                                 │
│  clsThemeManager, clsLanguageManager            │
│  clsSettingsStore (Registry wrapper)            │
└─────────────────────────────────────────────────┘
```

> **ملاحظة مهمة:** مفيش NavigationManager ولا ScreenRegistry ولا IScreen.
> Dashboard ثابت في frmMain. Settings = slide panel. كل شاشة ثابتة في مكانها.
