namespace UI.Core.Animation
{
    /// <summary>
    /// أنواع الأنيمشن المتاحة في النظام.
    /// كل نوع ليه سلوك مختلف في الرسم والحركة.
    /// </summary>
    public enum eAnimationType
    {
        /// <summary>تحريك الموقع (X/Y)</summary>
        SlideIn,

        /// <summary>تدرج الشفافية (Opacity)</summary>
        FadeIn,

        /// <summary>تدرج الشفافية عكسي</summary>
        FadeOut,

        /// <summary>تكبير/تصغير ناعم</summary>
        Scale,

        /// <summary>نبضة (scale up ثم رجوع)</summary>
        Pulse,

        /// <summary>تغيير لون تدريجي</summary>
        ColorTransition,

        /// <summary>اهتزاز خفيف (shake)</summary>
        Shake,
    }

    /// <summary>
    /// اتجاهات الـ Slide Animation.
    /// </summary>
    public enum eSlideDirection
    {
        FromLeft,
        FromRight,
        FromTop,
        FromBottom,
    }

    /// <summary>
    /// أنواع Easing — التسارع والتباطؤ.
    /// </summary>
    public enum eEasing
    {
        /// <summary>خطي — سرعة ثابتة</summary>
        Linear,

        /// <summary>بطيء في البداية — سريع في النهاية</summary>
        EaseIn,

        /// <summary>سريع في البداية — بطيء في النهاية</summary>
        EaseOut,

        /// <summary>بطيء في البداية والنهاية — سريع في النص</summary>
        EaseInOut,

        /// <summary>يتخطى الهدف ثم يرجع (bounce effect)</summary>
        EaseOutBack,
    }
}
