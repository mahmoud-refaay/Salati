# 📋 Salati — UI Protocol (بروتوكول بناء الواجهات)

> هذا الملف يوثق طريقة العمل القياسية لإنشاء أي كنترول أو Form جديد.
> **يجب اتباع هذه القواعد حرفياً** عشان الثيم واللغة يشتغلوا تلقائي.
> **ملاحظة:** مفيش NavigationManager/ScreenRegistry/IScreen — المشروع Hybrid.

---

## 🧩 الفلسفة الأساسية: UI كمكعبات (Building Blocks)

- **لا للـ Monolithic Forms:** أي نافذة معقدة يجب ألا تُبنى ككتلة واحدة.
- **التقسيم:** قسّم النافذة إلى UserControls صغيرة تقوم بوظيفة واحدة.
- **التركيب:** قم بتجميع هذه المكونات داخل الـ Form.
- **التوحيد:** كل مكون لازم يطبق الواجهات الأساسية (IThemeable, ILocalizable).

---

## 📋 الخطوات الصارمة لإنشاء كنترول جديد

> يُمنع الانتقال من خطوة إلى أخرى قبل التأكد من إتمامها بنجاح وبدون أخطاء.

### الخطوة 1: التصميم والهيكل (Design & Shell)

1. إنشاء ملف الـ `UserControl`:
   ```
   UI/Controls/{Category}/{ControlName}.cs
   UI/Controls/{Category}/{ControlName}.Designer.cs
   ```
   **الفئات:** `Layout/` | `Card/` | `Settings/` | `Feedback/`

2. الكود الأساسي:
   ```csharp
   using UI.Core.Theme;
   using UI.Core.Language;

   namespace UI.Controls.{Category}
   {
       public partial class {ControlName} : UserControl, IThemeable, ILocalizable
       {
           public {ControlName}()
           {
               InitializeComponent();
           }

           public void ApplyTheme(ThemeColors t)
           {
               this.BackColor = t.BgPrimary;
               // ... تطبيق الألوان
           }

           public void ApplyLanguage(ILanguagePack lang)
           {
               // ... تطبيق النصوص
           }
       }
   }
   ```

3. رسم الواجهة في الـ Designer وترتيب العناصر.

4. **تنبيه:** لا تكتب أي Business Logic أو استدعاء BLL/DAL في هذه المرحلة.

### الخطوة 2: نظام الترجمة (Localization)

1. افتح الـ Partial المناسب في `ILanguagePack.{Section}.cs` وأضف:
   ```csharp
   string {SectionElement} { get; }
   ```

2. أضف في `EnglishPack.cs` (الـ partial المناسب):
   ```csharp
   public string {SectionElement} => "English Text";
   ```

3. أضف في `ArabicPack.cs` (الـ partial المناسب):
   ```csharp
   public string {SectionElement} => "النص بالعربي";
   ```

4. اربط النصوص في `ApplyLanguage()`:
   ```csharp
   public void ApplyLanguage(ILanguagePack lang)
   {
       lblTitle.Text = lang.{SectionElement};
   }
   ```

### الخطوة 3: الثيمات والألوان (Theming)

1. في `ApplyTheme()` طبّق كل الألوان من `ThemeColors`:
   ```csharp
   public void ApplyTheme(ThemeColors t)
   {
       this.BackColor = t.BgPrimary;
       lblTitle.ForeColor = t.TextPrimary;
       pnlCard.FillColor = t.BgSurface;
       pnlCard.BorderColor = t.BorderDefault;
   }
   ```

2. **ممنوع نهائياً** استخدام ألوان hardcoded:
   ```csharp
   // ❌ ممنوع
   panel.FillColor = Color.FromArgb(20, 20, 45);

   // ✅ صحيح
   panel.FillColor = t.BgSurface;
   ```

### الخطوة 4: البناء والتحقق (Build & Verify)

1. `dotnet build`
2. **0 Errors** — لا تنتقل لكنترول تاني وفي error
3. حل أي مشكلة فوراً

---

## 🔄 ملخص دورة العمل

```
1. Create UserControl (Design + IThemeable + ILocalizable)
2. Update ILanguagePack Partial + EnglishPack + ArabicPack
3. Implement ApplyTheme() — zero hardcoded colors
4. Implement ApplyLanguage() — zero hardcoded strings
5. dotnet build → MUST BE 0 ERRORS
6. Repeat for next control
```

> **ملاحظة:** مفيش خطوة "Register in ScreenRegistry" — المشروع لا يستخدم Navigation.
> كل كنترول يتحط يدوي في الـ Form المناسب (frmMain أو frmAlert).

---

## 📐 خريطة الألوان — ThemeColors Roles

| Role | الاستخدام | المكان |
|------|-----------|--------|
| `BgPrimary` | خلفية النافذة | `BackColor` |
| `BgSecondary` | خلفية TitleBar | gradient panel |
| `BgSurface` | خلفية cards/panels | `FillColor` |
| `BgCard` | خلفية cards فاتحة أكتر | settings cards |
| `Accent1` | اللون المميز الأساسي (أخضر) | borders, active items |
| `Accent2` | اللون المميز الثاني (ذهبي) | highlights, times |
| `Accent3` | لون الخطأ (أحمر) | danger elements |
| `TextPrimary` | نص رئيسي | عناوين |
| `TextSecondary` | نص ثانوي | labels فرعية |
| `TextMuted` | نص خافت | descriptions, placeholders |
| `TextAccent` | نص مميز | links, active items |
| `BorderDefault` | حدود عادية | panels borders |
| `BorderFocused` | حدود مركزة | input focus |
| `BorderHover` | حدود hover | input hover |
| `Success` | نجاح (أخضر) | toast, status |
| `Warning` | تحذير (أصفر) | toast, alerts |
| `Danger` | خطر (أحمر) | delete, errors |
| `GradientBtn1` | تدرج الزر 1 | `FillColor` |
| `GradientBtn2` | تدرج الزر 2 | `FillColor2` |
| `InputBg` | خلفية input | text boxes |
| `InputText` | نص input | text boxes |
| `InputPlaceholder` | placeholder | text boxes |

---

## 📝 تسمية مفاتيح الـ Language Pack

| الـ Partial | الصيغة | مثال |
|------------|--------|------|
| `Common` | `Common{Action}` | `CommonSave`, `CommonCancel` |
| `Layout` | `Layout{Element}` | `LayoutAppTitle`, `LayoutMinimizeToTray` |
| `Dashboard` | `Dashboard{Element}` | `DashboardRemaining`, `DashboardNextPrayer` |
| `Prayers` | `Prayer{Name}` | `PrayerFajr`, `PrayerDhuhr` |
| `Settings` | `Settings{Tab}{Element}` | `SettingsPrayerSource`, `SettingsAlertVolume` |
| `Alert` | `Alert{Element}` | `AlertTimeFor`, `AlertMinutesBefore` |

---

## ✅ Checklist — قبل ما تسلم أي كنترول

- [ ] الكنترول بيرث من `UserControl`
- [ ] Implements `IThemeable`
- [ ] Implements `ILocalizable`
- [ ] كل الألوان من `ThemeColors` — **صفر hardcoded**
- [ ] كل النصوص من `ILanguagePack` — **صفر hardcoded strings**
- [ ] كل Emoji مستخدمة صح — **صفر صور PNG للأيقونات العادية**
- [ ] كل نص جديد مضاف في `ILanguagePack` partial + `EnglishPack` + `ArabicPack`
- [ ] `dotnet build` → **0 Errors**
