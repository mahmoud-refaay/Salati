namespace UI.Core.Language
{
    // نصوص تتبع الصلاة — Prayer Tracking
    public partial interface ILanguagePack
    {
        // عناوين
        string TrackingTitle { get; }
        string TrackingSubtitle { get; }

        // حالات التتبع
        string TrackingOnTime { get; }
        string TrackingLate { get; }
        string TrackingMissed { get; }
        string TrackingNotYet { get; }
        string TrackingLocked { get; }

        // إحصائيات
        string TrackingCommitment { get; }
        string TrackingStreak { get; }
        string TrackingStreakDays { get; }
        string TrackingThisMonth { get; }

        // أزرار
        string TrackingMarkPrayed { get; }
        string TrackingUnmark { get; }

        // رسائل
        string TrackingMarkedSuccess { get; }
        string TrackingUnmarkedSuccess { get; }
        string TrackingNoPrayerTimes { get; }
        string TrackingTooEarly { get; }
        string TrackingGenericError { get; }
    }
}
