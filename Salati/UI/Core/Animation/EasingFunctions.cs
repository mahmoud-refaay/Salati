namespace UI.Core.Animation
{
    /// <summary>
    /// حسابات الـ Easing — تحول progress خطي (0→1) لحركة ناعمة.
    /// </summary>
    public static class EasingFunctions
    {
        /// <summary>يحسب القيمة حسب نوع الـ Easing</summary>
        public static float Calculate(eEasing easing, float t)
        {
            // t = progress (0.0 → 1.0)
            return easing switch
            {
                eEasing.Linear => t,
                eEasing.EaseIn => t * t,
                eEasing.EaseOut => t * (2 - t),
                eEasing.EaseInOut => t < 0.5f ? 2 * t * t : -1 + (4 - 2 * t) * t,
                eEasing.EaseOutBack => EaseOutBack(t),
                _ => t
            };
        }

        private static float EaseOutBack(float t)
        {
            const float c1 = 1.70158f;
            const float c3 = c1 + 1;
            return 1 + c3 * MathF.Pow(t - 1, 3) + c1 * MathF.Pow(t - 1, 2);
        }
    }
}
