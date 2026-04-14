# 🏗️ Salati — UI Structure (Complete Reference)

> آخر تحديث: 13 أبريل 2026 (V1.0)
> هذا الملف هو المرجع الكامل لهيكل طبقة الـ UI.
> **مختلف عن Aura** — المشروع يستخدم Hybrid Style (Dashboard + Widget + Tray)، بدون Sidebar.

---

## 📐 النمط المعماري

```
Program.cs → frmSplash → frmMain (Title Bar + Dashboard + Settings Slide Panel)
                           │
                           ├── Dashboard (الشاشة الثابتة — Countdown + 5 Prayer Cards)
                           ├── Settings Panel (يسلايد من اليمين — 4 tabs)
                           ├── Widget Mode (📌 تصغير لـ compact strip)
                           └── System Tray (🔇 تصغير للخلفية)

frmAlert (نافذة مستقلة — Popup TopMost عند التنبيه)
```

### الفرق عن Aura/DVLD:

| Aura / DVLD | Salati |
|-------------|--------|
| Sidebar ثابت + Content Panel يتبدل | **لا يوجد Sidebar** — Dashboard ثابت |
| شاشات متعددة (UserControls) + NavigationManager | **شاشة واحدة + Settings Slide Panel** |
| لو Navigation معقد بين 20+ شاشة | **مفيش navigation** — كل حاجة في مكانها |
| Settings = شاشات منفصلة | **Settings = Panel بيسلايد من اليمين** |

### نمط الشاشات:
| النمط | الوظيفة | مثال |
|-------|---------|------|
| Dashboard (ثابت) | الشاشة الأساسية — دايماً ظاهرة | Main window |
| Slide Panel (overlay) | إعدادات بـ 4 tabs — فوق الـ Dashboard | Settings |
| Popup Form | نافذة مستقلة TopMost | frmAlert |
| Compact Form | نافذة صغيرة always-on-top | Widget mode |

---

## 📂 الهيكل الكامل

```
Salati/UI/
│
├── Program.cs                              ← نقطة البداية + Global Error Handlers
│
│ ═══════════════════════════════════════════
│  CORE — البنية التحتية
│ ═══════════════════════════════════════════
│
├── 📁 Core/
│   ├── 📁 Theme/
│   │   ├── ThemeColors.cs                  ← الألوان (BgPrimary, Accent1, Accent2...)
│   │   ├── ThemeModels.cs                  ← ThemeDefinition model
│   │   ├── BuiltInThemes.cs                ← MidnightSerenity + GoldenDawn
│   │   ├── ThemeColorUtils.cs              ← Lighten(), Darken(), Alpha()
│   │   ├── IThemeable.cs                   ← واجهة: ApplyTheme(ThemeColors)
│   │   └── clsThemeManager.cs              ← تطبيق الثيم + Event: ThemeChanged
│   │
│   ├── 📁 Language/
│   │   ├── ILanguagePack.cs                ← واجهة (partial interface)
│   │   ├── ILocalizable.cs                 ← واجهة: ApplyLanguage(ILanguagePack)
│   │   ├── clsLanguageManager.cs           ← تبديل اللغة + RTL/LTR + Event
│   │   ├── 📁 Partials/
│   │   │   ├── ILanguagePack.Common.cs     ← Save, Cancel, Dismiss, Mute...
│   │   │   ├── ILanguagePack.Layout.cs     ← Title bar texts
│   │   │   ├── ILanguagePack.Dashboard.cs  ← Remaining, Next Prayer...
│   │   │   ├── ILanguagePack.Prayers.cs    ← Fajr, Dhuhr, Asr, Maghrib, Isha
│   │   │   ├── ILanguagePack.Settings.cs   ← Prayer, Alert, Appearance, General
│   │   │   └── ILanguagePack.Alert.cs      ← Alert popup texts
│   │   └── 📁 Languages/
│   │       ├── 📁 Arabic/
│   │       │   ├── ArabicPack.cs
│   │       │   └── 📁 Partials/            ← partial classes
│   │       └── 📁 English/
│   │           ├── EnglishPack.cs
│   │           └── 📁 Partials/
│   │
│   ├── 📁 Engine/
│   │   └── clsUIEngine.cs                  ← يطبق Theme + Language على شجرة controls
│   │
│   ├── 📁 Settings/
│   │   └── clsSettingsStore.cs             ← Wrapper لـ Registry (يقرأ/يكتب من DAL)
│   │
│   └── 📁 Validation/
│       └── clsValidator.cs                 ← Input validation helpers
│
│ ═══════════════════════════════════════════
│  CONTROLS — الكنترولز المعاد استخدامها (14 كنترول)
│ ═══════════════════════════════════════════
│
├── 📁 Controls/
│   │
│   ├── 📁 Layout/                          ── عناصر هيكل النافذة ──
│   │   ├── ucTitleBar.cs                   ← شريط العنوان المخصص (📌⚙️🌙 AR — ✕)
│   │   │   │
│   │   │   │  ┌── التصميم ─────────────────────────────────┐
│   │   │   │  │ 🕌 Salati          📌  ⚙️  🌙  AR  ─  ✕  │
│   │   │   │  └───────────────────────────────────────────┘
│   │   │   │
│   │   │   ├── الكنترولز: Guna2Panel + Labels + PictureBox(logo) + Buttons
│   │   │   ├── IThemeable: BgSecondary + TextPrimary + Accent1 (logo)
│   │   │   ├── ILocalizable: AppTitle
│   │   │   └── Events:
│   │   │       ├── btnPin.Click → Toggle Widget Mode
│   │   │       ├── btnSettings.Click → Slide Settings Panel
│   │   │       ├── btnTheme.Click → Toggle Dark/Light
│   │   │       ├── btnLang.Click → Toggle AR/EN
│   │   │       ├── btnMinimize.Click → Minimize to Taskbar
│   │   │       └── btnClose.Click → Minimize to Tray (أو Exit)
│   │   │
│   │   └── ucInfoBar.cs                    ← شريط المعلومات السفلي
│   │       │
│   │       │  ┌── التصميم ─────────────────────────────────┐
│   │       │  │ 📅 Saturday, April 12  📍 Cairo   ▼ Tray   │
│   │       │  └───────────────────────────────────────────┘
│   │       │
│   │       ├── الكنترولز: Labels (date, city) + LinkLabel (minimize to tray)
│   │       ├── IThemeable: TextMuted
│   │       └── ILocalizable: DateFormat, CityName
│   │
│   ├── 📁 Card/                            ── بطاقات العرض ──
│   │   │
│   │   ├── ucNextPrayer.cs                 ← كارت الصلاة القادمة (Hero)
│   │   │   │
│   │   │   │  ┌── التصميم ──────────────────────────────┐
│   │   │   │  │              🌤️                          │
│   │   │   │  │          صلاة العصر                       │
│   │   │   │  │          ASR PRAYER                       │
│   │   │   │  │          03:45 PM                        │
│   │   │   │  │          01:23:45                        │
│   │   │   │  │       ████████████░░░░  72%              │
│   │   │   │  │        متبقي | REMAINING                 │
│   │   │   │  └──────────────────────────────────────────┘
│   │   │   │
│   │   │   ├── الكنترولز: Guna2Panel (border=Accent1) + Labels + Guna2ProgressBar
│   │   │   ├── IThemeable: BgSurface, BorderColor=Accent1, TextPrimary, TextAccent(gold)
│   │   │   ├── ILocalizable: PrayerName, "Remaining", PrayerLabel
│   │   │   ├── Properties:
│   │   │   │   ├── Prayer (ePrayer) → يحدد الأيقونة والاسم
│   │   │   │   ├── PrayerTime (TimeOnly) → يعرض الوقت
│   │   │   │   └── CountdownTarget (DateTime) → يحسب المتبقي
│   │   │   ├── Timer: كل ثانية يحدّث lblCountdown + progressBar
│   │   │   └── Event: CountdownFinished → يبلّغ frmMain
│   │   │
│   │   ├── ucPrayerCard.cs                 ← كارت صلاة واحدة (صغير)
│   │   │   │
│   │   │   │  ┌── التصميم ──────┐
│   │   │   │  │      🌅         │
│   │   │   │  │    الفجر        │
│   │   │   │  │   04:35 AM      │
│   │   │   │  │      ✅         │
│   │   │   │  └────────────────┘
│   │   │   │
│   │   │   ├── الكنترولز: Guna2Panel + PictureBox(emoji) + Labels + status icon
│   │   │   ├── IThemeable:
│   │   │   │   ├── Passed: BgSurface(dim), TextMuted, opacity 60%
│   │   │   │   ├── Next: BgSurface, Border=Accent1(glow), TextPrimary
│   │   │   │   └── Upcoming: BgSurface, TextSecondary
│   │   │   ├── ILocalizable: PrayerName
│   │   │   ├── Properties:
│   │   │   │   ├── Prayer (ePrayer)
│   │   │   │   ├── PrayerTime (TimeOnly)
│   │   │   │   └── Status (ePrayerStatus) → يحدد الشكل والأيقونة
│   │   │   └── Size: ~110 x 95 px
│   │   │
│   │   └── ucThemeCard.cs                  ← كارت اختيار ثيم
│   │       ├── الكنترولز: Guna2Panel + preview panel + Label + Checkmark
│   │       ├── IThemeable: selected=Accent1 border, unselected=BorderDefault
│   │       ├── Properties: ThemeName, IsSelected, PreviewColors
│   │       └── Event: ThemeSelected
│   │
│   ├── 📁 Settings/                        ── كنترولز الإعدادات ──
│   │   │
│   │   ├── ucSettingsPanel.cs              ← ⭐ الـ Panel الرئيسي (Slide من اليمين)
│   │   │   │
│   │   │   │  ┌── التصميم ──────────────────────────┐
│   │   │   │  │  Tab │  ⚙️ الإعدادات          ✕     │
│   │   │   │  │  Icons│                              │
│   │   │   │  │  ───  │  Content Area                │
│   │   │   │  │  🕐  │  (يتغير حسب التاب)           │
│   │   │   │  │  🔔  │                              │
│   │   │   │  │  🎨  │                              │
│   │   │   │  │  ⚙️  │                              │
│   │   │   │  │      │              [💾 Save]       │
│   │   │   │  └──────────────────────────────────────┘
│   │   │   │
│   │   │   ├── الكنترولز: Guna2Panel(main) + Guna2Panel(tabs) + pnlContent
│   │   │   ├── Width: 350px (fixed)
│   │   │   ├── IThemeable: BgSurface, Border=Accent1
│   │   │   ├── ILocalizable: Title "Settings"
│   │   │   ├── الـ 4 Tabs:
│   │   │   │   ├── tabPrayer → يعرض ucSettingsPrayer في pnlContent
│   │   │   │   ├── tabAlerts → يعرض ucSettingsAlerts في pnlContent
│   │   │   │   ├── tabAppearance → يعرض ucSettingsAppearance في pnlContent
│   │   │   │   └── tabGeneral → يعرض ucSettingsGeneral في pnlContent
│   │   │   ├── Animation: Timer slides panel from X=FormWidth to X=FormWidth-350
│   │   │   └── Events:
│   │   │       ├── Show() → slide in + dim overlay
│   │   │       └── Hide() → slide out + remove overlay
│   │   │
│   │   ├── ucSettingsPrayer.cs             ← تاب مواعيد الصلاة
│   │   │   ├── الكنترولز:
│   │   │   │   ├── RadioButtons (Auto API / Manual)
│   │   │   │   ├── Guna2ComboBox ×3 (Country, City, Method) — لـ Auto
│   │   │   │   ├── Guna2DateTimePicker ×5 (Fajr..Isha times) — لـ Manual
│   │   │   │   └── Guna2GradientButton (Refresh + Save)
│   │   │   ├── IThemeable: InputBg, InputText, Accent1(buttons)
│   │   │   ├── ILocalizable: SettingsPrayerSource, Country, City, Method...
│   │   │   └── Logic: RadioButton يبدّل بين API panel و Manual panel
│   │   │
│   │   ├── ucSettingsAlerts.cs             ← تاب التنبيهات
│   │   │   ├── الكنترولز:
│   │   │   │   ├── ucAlertRow ×5 (واحد لكل صلاة)
│   │   │   │   ├── Guna2TrackBar (Volume slider)
│   │   │   │   ├── Guna2ToggleSwitch (Alert at Adhan time)
│   │   │   │   ├── Guna2ComboBox (Alert Type)
│   │   │   │   └── Guna2GradientButton (Save)
│   │   │   ├── IThemeable: كل العناصر
│   │   │   └── ILocalizable: AlertMinutes, SoundName, Volume...
│   │   │
│   │   ├── ucSettingsAppearance.cs         ← تاب المظهر
│   │   │   ├── الكنترولز:
│   │   │   │   ├── ucThemeCard ×2 (Midnight Serenity + Golden Dawn)
│   │   │   │   ├── ucLanguageCard ×2 (Arabic + English)
│   │   │   │   └── Guna2GradientButton (Save)
│   │   │   └── ILocalizable: ThemeLabel, LanguageLabel
│   │   │
│   │   ├── ucSettingsGeneral.cs            ← تاب عام
│   │   │   ├── الكنترولز:
│   │   │   │   ├── Guna2ToggleSwitch ×4 (StartWithWindows, MinimizeOnStart, ShowInTray, CloseToTray)
│   │   │   │   ├── Guna2Button (Reset to Defaults — outline)
│   │   │   │   ├── Guna2Button (Open Settings Folder — outline)
│   │   │   │   ├── Labels (version, credits)
│   │   │   │   └── Guna2GradientButton (Save)
│   │   │   └── ILocalizable: StartWithWindows, MinimizeOnStart...
│   │   │
│   │   └── ucAlertRow.cs                   ← صف تنبيه لصلاة واحدة
│   │       │
│   │       │  ┌── التصميم ──────────────────────────────────────┐
│   │       │  │ 🌅 الفجر    [10m ▾]   [أذان الحرم ▾]    [🔵ON] │
│   │       │  └─────────────────────────────────────────────────┘
│   │       │
│   │       ├── الكنترولز: PictureBox + Label + ComboBox(min) + ComboBox(sound) + ToggleSwitch
│   │       ├── IThemeable: BgCard alt rows, TextPrimary
│   │       ├── Properties: Prayer, IsEnabled, MinutesBefore, SoundFile
│   │       └── Size: fills parent width × 55px height
│   │
│   ├── 📁 Feedback/                        ── تنبيهات ──
│   │   ├── ucToast.cs                      ← إشعار مؤقت (success/error/warning)
│   │   │   ├── Static methods: ShowSuccess(), ShowError(), ShowWarning()
│   │   │   ├── Position: أسفل يمين النافذة
│   │   │   └── Auto-hide: 3 ثواني
│   │   │
│   │   └── ucMessageBox.cs                 ← رسالة تأكيد/تحذير (بديل MessageBox)
│   │       ├── Static methods: ShowConfirm(), ShowAlert()
│   │       ├── IThemeable: BgSurface, Accent1/Accent3 buttons
│   │       └── Returns: DialogResult
│   │
│   └── 📁 Other/
│       └── ucLanguageCard.cs               ← كارت اختيار لغة
│           ├── الكنترولز: Guna2Panel + Label(name) + Label(sub) + Checkmark
│           ├── Properties: LanguageName, IsSelected
│           └── Event: LanguageSelected
│
│ ═══════════════════════════════════════════
│  FORMS — النوافذ (3 نوافذ)
│ ═══════════════════════════════════════════
│
├── 📁 Forms/
│   │
│   ├── frmSplash.cs                        ← شاشة التحميل
│   │   │
│   │   │  ┌── التصميم (480×340 px, Borderless) ─────────┐
│   │   │  │              🕌 (mosque icon)                 │
│   │   │  │              Salati                          │
│   │   │  │              صلاتي                           │
│   │   │  │        وَأَقِيمُوا الصَّلَاةَ                │
│   │   │  │          ██████░░░░  loading                 │
│   │   │  │                               v1.0          │
│   │   │  └──────────────────────────────────────────────┘
│   │   │
│   │   ├── StartPosition: CenterScreen
│   │   ├── FormBorderStyle: None
│   │   ├── Timer: 2-3 sec → Load settings → Show frmMain
│   │   └── Logging: clsEventLogger.LogInfo("Salati started")
│   │
│   ├── frmMain.cs                          ← ⭐ النافذة الرئيسية
│   │   │
│   │   │  ╔══════════════════════════════════════════════╗
│   │   │  ║  ⚡ 3 أوضاع للنافذة:                        ║
│   │   │  ║                                              ║
│   │   │  ║  1. Full Mode (700×500) — Dashboard كامل    ║
│   │   │  ║  2. Widget Mode (300×85) — Compact strip     ║
│   │   │  ║  3. Tray Mode — مخفي + NotifyIcon فقط       ║
│   │   │  ╚══════════════════════════════════════════════╝
│   │   │
│   │   │  ┌── Full Mode ────────────────────────────────┐
│   │   │  │ ucTitleBar (Dock.Top)                        │
│   │   │  │ ┌──────────────────────────────────────────┐ │
│   │   │  │ │ ucNextPrayer (Hero card)                  │ │
│   │   │  │ │ ┌─────┐ ┌─────┐ ┌─────┐ ┌─────┐ ┌─────┐│ │
│   │   │  │ │ │Card │ │Card │ │Card │ │Card │ │Card ││ │
│   │   │  │ │ │Fajr │ │Dhuhr│ │ Asr │ │Magh.│ │Isha ││ │
│   │   │  │ │ └─────┘ └─────┘ └─────┘ └─────┘ └─────┘│ │
│   │   │  │ └──────────────────────────────────────────┘ │
│   │   │  │ ucInfoBar (Dock.Bottom)                      │
│   │   │  └──────────────────────────────────────────────┘
│   │   │
│   │   │  ┌── Widget Mode ──────────────────────────────┐
│   │   │  │ 🌤️ العصر 03:45   01:23:45  ████░░  📌  ✕  │
│   │   │  └──────────────────────────────────────────────┘
│   │   │
│   │   ├── الكنترولز الثابتة:
│   │   │   ├── ucTitleBar (Dock.Top)
│   │   │   ├── ucNextPrayer (في النص)
│   │   │   ├── ucPrayerCard ×5 (FlowLayout أو manual positioning)
│   │   │   ├── ucInfoBar (Dock.Bottom)
│   │   │   ├── ucSettingsPanel (مخفي — يظهر بالسلايد)
│   │   │   ├── pnlOverlay (شفاف — يظهر خلف Settings)
│   │   │   ├── NotifyIcon + ContextMenuStrip (System Tray)
│   │   │   └── Timer (_alertCheckTimer — كل 30 ثانية)
│   │   │
│   │   ├── Methods:
│   │   │   ├── LoadPrayerTimes() → يجلب المواعيد (API أو Manual)
│   │   │   ├── UpdateDashboard() → يحدّث الكروت + ucNextPrayer
│   │   │   ├── SwitchToWidgetMode() → يصغر النافذة لـ 300×85
│   │   │   ├── SwitchToFullMode() → يرجع النافذة لـ 700×500
│   │   │   ├── MinimizeToTray() → يخفي النافذة + يظهر NotifyIcon
│   │   │   ├── ShowFromTray() → يظهر النافذة من الـ Tray
│   │   │   ├── ShowSettingsPanel() → slide animation + overlay
│   │   │   ├── HideSettingsPanel() → slide out + remove overlay
│   │   │   └── CheckPrayerAlerts() → يتشيك كل 30 ثانية
│   │   │
│   │   ├── System Tray:
│   │   │   ├── NotifyIcon.Icon = tray_normal.ico
│   │   │   ├── NotifyIcon.Text = "Salati — العصر 03:45 PM"
│   │   │   ├── ContextMenuStrip:
│   │   │   │   ├── "🕌 Salati" (header)
│   │   │   │   ├── "⏳ العصر: 03:45 PM" (info)
│   │   │   │   ├── "📂 Open" → ShowFromTray()
│   │   │   │   ├── "📌 Widget" → SwitchToWidgetMode()
│   │   │   │   ├── "🔕 Mute" → toggle
│   │   │   │   ├── "⚙️ Settings" → ShowSettingsPanel()
│   │   │   │   └── "❌ Exit" → Application.Exit()
│   │   │   └── DoubleClick → ShowFromTray()
│   │   │
│   │   └── IThemeable + ILocalizable: كل العناصر
│   │
│   └── frmAlert.cs                         ← نافذة التنبيه (Popup)
│       │
│       │  ┌── التصميم (420×300 px, Borderless, TopMost) ──┐
│       │  │          تبقى 10 دقائق على                     │
│       │  │          صلاة العصر 🌤️                          │
│       │  │          03:45 PM                              │
│       │  │   "حي على الصلاة حي على الفلاح"                │
│       │  │   [🔇 Mute]         [✅ Dismiss]                │
│       │  │        يختفي تلقائياً بعد 60 ثانية              │
│       │  └────────────────────────────────────────────────┘
│       │
│       ├── FormBorderStyle: None, TopMost: true
│       ├── StartPosition: Manual (أعلى يمين الشاشة)
│       ├── الكنترولز: Guna2Panel(border glow) + Labels + Buttons
│       ├── Properties:
│       │   ├── Prayer (ePrayer)
│       │   ├── PrayerTime (TimeOnly)
│       │   ├── MinutesBefore (int) → 0 = وقت الأذان
│       │   └── AlertType (eAlertType)
│       ├── Behavior:
│       │   ├── Show() → Slide from top + play sound
│       │   ├── btnMute.Click → clsSoundPlayer.Stop()
│       │   ├── btnDismiss.Click → Close + log
│       │   ├── Auto-dismiss: Timer 60 sec → Close
│       │   └── Log: SP_InsertAlertLog
│       └── IThemeable + ILocalizable
│
│ ═══════════════════════════════════════════
│  ASSETS — الموارد
│ ═══════════════════════════════════════════
│
└── 📁 Assets/
    ├── 📁 Icons/                           ← أيقونتين ICO فقط (الباقي emoji)
    │   ├── app_icon.ico                    ← أيقونة التطبيق
    │   ├── tray_normal.ico                 ← أيقونة Tray عادي
    │   └── tray_alert.ico                  ← أيقونة Tray (تنبيه)
    │   ⚠️ ملاحظة: بدون prayer_icons/ — نستخدم Emoji (🌅☀️🌤️🌇🌙)
    └── 📁 Sounds/
        ├── adhan_makkah.mp3
        ├── adhan_madinah.mp3
        ├── adhan_afasy.mp3
        ├── beep_simple.wav
        └── beep_double.wav

```

---

## 📊 ملخص الأرقام

| العنصر | العدد |
|--------|:-----:|
| Forms (نوافذ) | **3** |
| Controls (كنترولز) | **14** |
| Settings Tab Controls | **4** (يحملوا في ucSettingsPanel) |
| Language Partials | **6** ملفات |
| Language strings (تقديري) | **~60** |
| DB Tables | **8** |
| Stored Procedures | **~30** |

---

## 🔗 خريطة: DB → BLL → UI

```
Locations ────→ clsPrayerTimeManager ──→ ucSettingsPrayer (Country, City dropdowns)
                                         ucNextPrayer (next prayer display)
                                         ucPrayerCard ×5 (all times)

DailyPrayerTimes → clsPrayerTimeManager → frmMain.LoadPrayerTimes()
                                           ucNextPrayer (countdown)

Sounds ───────→ clsSoundPlayer ────────→ ucSettingsAlerts (sound dropdown)
                                         frmAlert (play adhan)

AlertConfigs ──→ clsAlertScheduler ────→ ucSettingsAlerts (per-prayer config)
                                         ucAlertRow ×5 (toggle + minutes)

AlertLog ─────→ clsAlertScheduler ─────→ frmAlert (log on show/dismiss)

PrayerTracking → (v1.5) ──────────────→ مستقبلاً

AppSettings ──→ clsSettingsManager ────→ ucSettingsGeneral
                                         ucSettingsAppearance

ErrorLog ─────→ clsEventLogger ────────→ Program.cs (global handlers)

Registry ─────→ clsRegistryManager ────→ clsSettingsStore
               (HKCU\SOFTWARE\Salati)     (Theme, Language, Startup, Volume)
```

---

## 🎨 Language Pack Structure (~60 strings)

| ملف Partial | النصوص | العدد |
|-------------|--------|:-----:|
| `Common` | Save, Cancel, Dismiss, Mute, Refresh, Reset, Close, Error, Success | **9** |
| `Layout` | AppTitle, MinimizeToTray, Date format | **3** |
| `Dashboard` | Remaining, NextPrayer, Passed, Upcoming, NoInternet | **5** |
| `Prayers` | Fajr, Dhuhr, Asr, Maghrib, Isha (+ English names) | **10** |
| `Settings` | PrayerSource, Auto, Manual, Country, City, Method, Theme, Language, StartWithWindows, MinimizeOnStart, ShowInTray, CloseToTray, Volume, AlertType, MinutesBefore, About, Version, ResetDefaults, OpenFolder + headers | **25** |
| `Alert` | TimeFor, MinutesUntil, AutoDismiss, HayaAlaSalah | **4** |
| **الإجمالي** | | **~56** |

---

## ⚠️ ملاحظات مهمة

1. **مفيش Sidebar** — الـ navigation كله عبر:
   - `ucTitleBar` (أزرار: settings, theme, language, pin)
   - `ucSettingsPanel` (slide + 4 tabs)
   - النافذة نفسها (Full / Widget / Tray)

2. **مفيش NavigationManager** — مفيش شاشات بتتبدل زي Aura.
   الـ Dashboard **ثابت دايماً**. الإعدادات **overlay فوقيه**.

3. **الإعدادات في Registry** — مش JSON زي Aura.
   `clsSettingsStore` → `clsRegistryManager` مع fallback تلقائي.

4. **3 أوضاع للنافذة**:
   - Full (700×500) — المستخدم بيستخدم التطبيق
   - Widget (300×85) — compact always-on-top
   - Tray (مخفي) — في الخلفية

5. **frmAlert مستقلة** — تفتح لوحدها TopMost.
   مش جزء من frmMain.

6. **ucSettingsPanel** هو المكوّن الأعقد —
   فيه slide animation + 4 tabs + كل كنترولز الإعدادات.

7. **ucNextPrayer** فيه Timer خاص —
   كل ثانية يحدّث الـ countdown والـ progress bar.
