# 📱 Salati — Screens (الشاشات)

> هذا الملف يوثق كل شاشة في التطبيق: الوصف، المحتوى، والنمط.
> **نمط Hybrid** — لا Sidebar، لا NavigationManager، لا شاشات بتتبدل.
> Dashboard ثابت + Settings slide panel + Widget mode + Tray.

---

## 📊 ملخص الشاشات

| # | الشاشة | النوع | الوصف |
|---|--------|-------|-------|
| 1 | `frmSplash` | Form | شاشة التحميل |
| 2 | `frmMain` | Form | النافذة الرئيسية (Dashboard + Settings + Widget + Tray) |
| 3 | `frmAlert` | Form | نافذة التنبيه (Popup TopMost) |

**الإجمالي**: 3 Forms فقط — لا شاشات UserControl منفصلة تتبدل.

---

## 🔁 أوضاع frmMain (3 أوضاع)

| الوضع | الحجم | TopMost | الوصف |
|-------|-------|---------|-------|
| **Full Mode** | 700×500 | No | Dashboard كامل مع كل العناصر |
| **Widget Mode** | 300×85 | Yes | Strip مصغر — الصلاة القادمة + countdown |
| **Tray Mode** | مخفي | — | مفيش نافذة — NotifyIcon بس في Taskbar |

---

## 🖥️ تفاصيل كل شاشة

---

### 1. frmSplash — شاشة التحميل

**النوع**: Form (يظهر عند بدء التطبيق)
**المدة**: 2-3 ثواني

```
┌────────────────────────────────────────┐
│                                        │
│              🕌                        │
│           Salati                       │
│           صلاتي                        │
│                                        │
│    "وَأَقِيمُوا الصَّلَاةَ"           │
│                                        │
│         ████████░░░  80%              │
│                                        │
│                          v1.0         │
└────────────────────────────────────────┘

الحجم: 480 x 340 px
Borderless, CenterScreen
```

**العناصر:**
- 🕌 أيقونة المسجد (الصورة الوحيدة)
- اسم التطبيق "Salati" + "صلاتي"
- آية أو عبارة إسلامية
- Guna2ProgressBar مع loading animation
- رقم الإصدار
- Timer → Load settings → Show frmMain

---

### 2. frmMain — النافذة الرئيسية ⭐

**النوع**: Form (النافذة الوحيدة — بـ 3 أوضاع)

#### Full Mode (700×500):

```
┌── ucTitleBar ──────────────────────────────────────┐
│ 🕌 Salati                  📌  ⚙️  🌙  AR  ─  ✕   │
├────────────────────────────────────────────────────┤
│                      🌤️                            │
│                  صلاة العصر                        │
│                  ASR PRAYER                        │
│                  03:45 PM                          │
│                  01:23:45                          │
│               ████████████░░░░  72%                │
│                متبقي | REMAINING                   │
│                                                    │
│  ┌──────┐ ┌──────┐ ┌──────┐ ┌──────┐ ┌──────┐    │
│  │ 🌅   │ │ ☀️   │ │ 🌤️   │ │ 🌇   │ │ 🌙   │    │
│  │الفجر │ │الظهر │ │العصر │ │المغرب│ │العشاء│    │
│  │04:35 │ │12:15 │ │03:45 │ │06:30 │ │08:00 │    │
│  │  ✅  │ │  ✅  │ │  ⏳  │ │  ⌛  │ │  ⌛  │    │
│  └──────┘ └──────┘ └──────┘ └──────┘ └──────┘    │
├── ucInfoBar ───────────────────────────────────────┤
│ 📅 Saturday, April 12    📍 Cairo    ▼ Minimize    │
└────────────────────────────────────────────────────┘
```

#### Widget Mode (300×85):

```
┌──────────────────────────────────────┐
│ 🌙 SALATI            📌  ✕          │
│ 🌤️ العصر  03:45 PM   01:23:44       │
│              ████████████░░░░       │
└──────────────────────────────────────┘
```

#### Settings Panel (Slide من اليمين — فوق Dashboard):

```
                    ┌─────────────────────────────┐
                    │ Tab │  ⚙️ الإعدادات        ✕ │
                    │ Icons│                       │
                    │ ───  │                       │
                    │ 🕐  │   [Tab Content]       │
                    │ 🔔  │                       │
                    │ 🎨  │                       │
                    │ ⚙️  │                       │
                    │      │          [💾 Save]   │
                    └─────────────────────────────┘
Width: 350px, slides from right edge
```

**العناصر الثابتة:**
| العنصر | الكنترول | الموقع |
|--------|----------|--------|
| **شريط العنوان** | `ucTitleBar` | Dock.Top |
| **الصلاة القادمة** | `ucNextPrayer` | وسط النافذة |
| **5 كروت صلاة** | `ucPrayerCard` × 5 | أسفل الـ Hero |
| **شريط المعلومات** | `ucInfoBar` | Dock.Bottom |
| **بانل الإعدادات** | `ucSettingsPanel` | مخفي — يظهر بالسلايد |
| **Overlay** | `pnlOverlay` | شفاف — يظهر خلف Settings |
| **System Tray** | `NotifyIcon` + `ContextMenuStrip` | Taskbar |
| **Timer** | `_alertCheckTimer` | كل 30 ثانية check |

**أزرار ucTitleBar:**
| الزر | الوظيفة |
|------|---------|
| 📌 | Toggle Widget Mode |
| ⚙️ | Show/Hide Settings Panel |
| 🌙 | Toggle Dark/Light Theme |
| AR/EN | Toggle Language |
| ─ | Minimize to Taskbar |
| ✕ | Minimize to Tray (أو Exit حسب الإعدادات) |

**System Tray Menu:**
```
🕌 Salati
⏳ العصر: 03:45 PM
   متبقي: 01:23:45
──────────────────
📂 Salati | Open
📌 وضع مصغر | Widget
🔇 كتم مؤقت | Mute     •
⚙️ الإعدادات | Settings
──────────────────
❌ خروج | Exit
```

---

### 3. frmAlert — نافذة التنبيه (Popup)

**النوع**: Form (Popup يظهر فوق كل البرامج)

```
┌────────────────────────────────────────┐
│                  🌤️                     │
│         تبقى 10 دقائق على              │
│           صلاة العصر                   │
│            03:45 PM                    │
│                                        │
│   "حي على الصلاة حي على الفلاح"       │
│                                        │
│  [🔇 كتم | Mute]    [✅ تم | Dismiss]  │
│        يختفي تلقائياً بعد 60 ثانية      │
└────────────────────────────────────────┘

الحجم: 420 x 300 px
TopMost = true, Borderless
StartPosition: Manual (أعلى يمين الشاشة)
```

**السلوك:**
- يظهر فوق كل البرامج (TopMost)
- يشغّل صوت أذان (لو مفعّل)
- يختفي تلقائياً بعد 60 ثانية أو بالضغط على "تم"
- لو التنبيه "قبل الصلاة" → العنوان: "تبقى X دقيقة على صلاة Y"
- لو التنبيه "وقت الأذان" → العنوان: "حان وقت صلاة Y"
- يسجّل في AlertLog (SP_InsertAlertLog)

---

## 🧩 Settings Panel — الـ 4 Tabs

> الإعدادات مش شاشات منفصلة — كلها **tab controls داخل ucSettingsPanel**

### Tab 1: مواعيد الصلاة 🕐

```
مصدر التوقيت | Time Source:
  (●) تلقائي | Auto  ←  Via Location API
  ( ) يدوي | Manual   ←  User defined

── عند API ──
الدولة: [Egypt (مصر)        ▾]
المدينة: [Cairo (القاهرة)    ▾]
طريقة الحساب: [Egyptian General ▾]
[🔄 تحديث | Refresh]

── عند Manual ──
🌅 الفجر:   [04:35]
☀️ الظهر:   [12:15]
🌤️ العصر:   [03:45]
🌇 المغرب:  [06:30]
🌙 العشاء:  [08:00]

[💾 حفظ | Save]
```

### Tab 2: التنبيهات 🔔

```
🌅 الفجر    [10m ▾]   [أذان الحرم ▾]    [🔵ON]
☀️ الظهر    [5m  ▾]   [أذان الحرم ▾]    [🔵ON]
🌤️ العصر    [5m  ▾]   [أذان الحرم ▾]    [🔵ON]
🌇 المغرب   [10m ▾]   [أذان الحرم ▾]    [🔵ON]
🌙 العشاء   [5m  ▾]   [أذان الحرم ▾]    [🔵ON]

إعدادات الصوت | SOUND SETTINGS:
  مستوى الصوت: [████████░░] 80%
  تنبيه وقت الأذان: [🔵ON]
  نوع التنبيه: [صوت أذان | Adhan Sound ▾]

[💾 حفظ | Save]
```

### Tab 3: المظهر 🎨

```
الثيم | Theme:
  ┌───────────┐  ┌───────────┐
  │ 🌙 Dark   │  │ ☀️ Light  │
  │ Midnight  │  │ Golden    │
  │ Serenity  │  │ Dawn      │
  │    ✓      │  │           │
  └───────────┘  └───────────┘

اللغة | Language:
  ┌───────────┐  ┌───────────┐
  │ العربية   │  │ English   │
  │ Arabic ✓  │  │ الإنجليزية│
  └───────────┘  └───────────┘

[💾 حفظ | Save]
```

### Tab 4: عام ⚙️

```
بدء التشغيل | STARTUP:
  تشغيل مع بداية الويندوز    [🔵ON]
  تصغير عند البدء            [⚪OFF]
  إظهار في System Tray       [🔵ON]
  تصغير لـ Tray عند الإغلاق  [🔵ON]

البيانات | DATA:
  [🔄 إعادة ضبط الإعدادات | Reset to Defaults]
  [📂 فتح مجلد الإعدادات | Open Settings Folder]

عن التطبيق | ABOUT:
  🕌 Salati v1.0.0
  صنع بـ ❤️ بواسطة فريق Salati
  🔗 GitHub

[💾 حفظ | Save]
```

---

## 🧩 Custom Controls (المكونات المشتركة) — 14 كنترول

| # | الكنترول | الفئة | الوصف |
|---|----------|-------|-------|
| 1 | `ucTitleBar` | Layout | شريط العنوان المخصص (📌⚙️🌙 AR ─ ✕) |
| 2 | `ucInfoBar` | Layout | شريط المعلومات السفلي (📅 📍 ▼) |
| 3 | `ucNextPrayer` | Card | كارت الصلاة القادمة (Hero — Countdown + Progress) |
| 4 | `ucPrayerCard` | Card | كارت صلاة واحدة (emoji + اسم + وقت + حالة) |
| 5 | `ucThemeCard` | Card | كارت اختيار ثيم |
| 6 | `ucLanguageCard` | Card | كارت اختيار لغة |
| 7 | `ucSettingsPanel` | Settings | الـ Slide Panel الرئيسي (4 tabs) |
| 8 | `ucSettingsPrayer` | Settings | Tab مواعيد الصلاة |
| 9 | `ucSettingsAlerts` | Settings | Tab التنبيهات |
| 10 | `ucSettingsAppearance` | Settings | Tab المظهر |
| 11 | `ucSettingsGeneral` | Settings | Tab عام |
| 12 | `ucAlertRow` | Settings | صف تنبيه لصلاة واحدة |
| 13 | `ucToast` | Feedback | Toast notification (success/error/warning) |
| 14 | `ucMessageBox` | Feedback | Confirm/Alert dialog |

---

## 🔗 خريطة التنقل (بسيطة جداً)

```
frmSplash
  └──► frmMain (Full Mode — Dashboard)
         ├── 📌 Pin → Widget Mode
         ├── ⚙️ Settings → ucSettingsPanel (slide)
         │     ├── 🕐 Tab: Prayer Times
         │     ├── 🔔 Tab: Alerts
         │     ├── 🎨 Tab: Appearance
         │     └── ⚙️ Tab: General
         ├── ─ Minimize → Taskbar
         └── ✕ Close → Tray Mode
               └── DoubleClick Tray → Full Mode

frmAlert (يظهر مستقل — TopMost — عند التنبيه)
```

> **ملاحظة:** مفيش NavigationManager ولا ScreenRegistry ولا IScreen.
> كل شاشة ثابتة في مكانها. الانتقال بسيط (show/hide/slide).

---

## 📊 حالات ucPrayerCard

| الحالة | اللون | الـ Emoji | المعنى |
|--------|-------|----------|--------|
| **Passed** | رمادي + opacity 60% | ✅ | فاتت |
| **Next** | أخضر (border glow) | ⏳ | القادمة |
| **Upcoming** | عادي | ⌛ | لاحقاً |
| **Disabled** | شفاف | 🔕 | معطّلة |
