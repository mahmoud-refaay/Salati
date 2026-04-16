namespace UI.Core.Language.Languages
{
    /// <summary>حزمة اللغة العربية</summary>
    public class ArabicPack : ILanguagePack
    {
        // ═══ Base ═══
        public string LanguageCode => "ar";
        public string LanguageNameAr => "العربية";
        public string LanguageNameEn => "Arabic";
        public bool IsRtl => true;

        // ═══ Common ═══
        public string CommonSave => "حفظ";
        public string CommonCancel => "إلغاء";
        public string CommonClose => "إغلاق";
        public string CommonDismiss => "تم";
        public string CommonMute => "كتم";
        public string CommonRefresh => "تحديث";
        public string CommonReset => "إعادة ضبط";
        public string CommonError => "خطأ";
        public string CommonSuccess => "نجاح";
        public string CommonLoading => "جاري التحميل";
        public string CommonOk => "موافق";
        public string CommonWarning => "تحذير";
        public string CommonInfo => "معلومة";

        // ═══ Layout ═══
        public string LayoutAppTitle => "صلاتي";
        public string LayoutMinimizeToTray => "تصغير للخلفية";
        public string LayoutSettings => "الإعدادات";

        // ═══ Dashboard ═══
        public string DashboardRemaining => "متبقي";
        public string DashboardNextPrayer => "الصلاة القادمة";
        public string DashboardPassed => "فاتت";
        public string DashboardUpcoming => "لاحقاً";
        public string DashboardNoInternet => "لا يوجد اتصال بالإنترنت";

        // ═══ Prayers ═══
        public string PrayerFajr => "الفجر";
        public string PrayerDhuhr => "الظهر";
        public string PrayerAsr => "العصر";
        public string PrayerMaghrib => "المغرب";
        public string PrayerIsha => "العشاء";
        public string PrayerFajrEn => "FAJR";
        public string PrayerDhuhrEn => "DHUHR";
        public string PrayerAsrEn => "ASR";
        public string PrayerMaghribEn => "MAGHRIB";
        public string PrayerIshaEn => "ISHA";

        // ═══ Settings ═══
        public string SettingsTitle => "الإعدادات";
        public string SettingsTabPrayer => "المواعيد";
        public string SettingsTabAlerts => "التنبيهات";
        public string SettingsTabAppearance => "المظهر";
        public string SettingsTabGeneral => "عام";

        public string SettingsPrayerSource => "مصدر التوقيت";
        public string SettingsPrayerAuto => "تلقائي من الإنترنت";
        public string SettingsPrayerManual => "إدخال يدوي";
        public string SettingsPrayerCountry => "الدولة";
        public string SettingsPrayerCity => "المدينة";
        public string SettingsPrayerMethod => "طريقة الحساب";

        public string SettingsAlertMinutes => "قبل بكام دقيقة";
        public string SettingsAlertSound => "صوت الأذان";
        public string SettingsAlertVolume => "مستوى الصوت";
        public string SettingsAlertAtAdhan => "تنبيه وقت الأذان";
        public string SettingsAlertType => "نوع التنبيه";

        public string SettingsTheme => "الثيم";
        public string SettingsLanguage => "اللغة";

        public string SettingsStartWithWindows => "تشغيل مع بداية الويندوز";
        public string SettingsMinimizeOnStart => "تصغير عند البدء";
        public string SettingsShowInTray => "إظهار في System Tray";
        public string SettingsCloseToTray => "تصغير لـ Tray عند الإغلاق";
        public string SettingsResetDefaults => "إعادة ضبط الإعدادات";
        public string SettingsOpenFolder => "فتح مجلد الإعدادات";
        public string SettingsAboutVersion => "الإصدار";
        public string SettingsAboutCredits => "صنع بـ ❤️ بواسطة فريق Salati";

        // ═══ Alert ═══
        public string AlertTimeFor => "حان وقت صلاة {0}";
        public string AlertMinutesUntil => "تبقى {0} دقيقة على صلاة {1}";
        public string AlertAutoDismiss => "يختفي تلقائياً بعد {0} ثانية";
        public string AlertHayaAlaSalah => "حي على الصلاة حي على الفلاح";
        public string AlertConfirm => "تأكيد";
        public string AlertCancel => "إلغاء";
        public string AlertOk => "موافق";
        public string AlertYes => "نعم";
        public string AlertNo => "لا";

        // ═══ Tracking ═══
        public string TrackingTitle => "تتبع الصلاة";
        public string TrackingSubtitle => "حافظ على صلواتك";
        public string TrackingOnTime => "صليت في الوقت";
        public string TrackingLate => "صليت متأخراً";
        public string TrackingMissed => "لم أصلِّ";
        public string TrackingNotYet => "لم أحدد بعد";
        public string TrackingLocked => "لم يحن الوقت";
        public string TrackingCommitment => "الالتزام";
        public string TrackingStreak => "أيام متتالية";
        public string TrackingStreakDays => "{0} يوم 🔥";
        public string TrackingThisMonth => "هذا الشهر";
        public string TrackingMarkPrayed => "صليت ✓";
        public string TrackingUnmark => "إلغاء ✕";
        public string TrackingMarkedSuccess => "تم تعليم {0} بنجاح";
        public string TrackingUnmarkedSuccess => "تم إلغاء تعليم {0}";
        public string TrackingNoPrayerTimes => "لا توجد مواعيد صلاة متاحة لليوم";
        public string TrackingTooEarly => "لم يحن وقت هذه الصلاة بعد";
        public string TrackingGenericError => "تعذر تحديث تتبع الصلاة";

        // ═══ Adhkar ═══
        public string AdhkarMorningTitle => "أذكار الصباح";
        public string AdhkarEveningTitle => "أذكار المساء";
        public string AdhkarTap => "اضغط على كل ذكر لعدّه";
        public string AdhkarDone => "✅ اكتمل";
        public string AdhkarProgress => "تم إنجاز: {0}/{1}";
        public string AdhkarCompleted => "الحمد لله — اكتملت الأذكار!";
    }
}
