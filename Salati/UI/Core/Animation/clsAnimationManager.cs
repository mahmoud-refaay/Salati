namespace UI.Core.Animation
{
    /// <summary>
    /// مدير الأنيمشن المركزي — مسؤول عن تشغيل الأنيمشنز على أي كنترول.
    /// لا يتم كتابة أي كود أنيمشن داخل الكنترولز — كل حاجة عبر هذا الكلاس.
    /// 
    /// ═══════════════════════════════════════════════════════
    ///  الاستخدام:
    /// ═══════════════════════════════════════════════════════
    /// 
    ///   // Slide panel from right
    ///   clsAnimationManager.SlideIn(myPanel, eSlideDirection.FromRight, 300);
    /// 
    ///   // Fade in a card
    ///   clsAnimationManager.FadeIn(myCard, 250);
    /// 
    ///   // Pulse effect on button
    ///   clsAnimationManager.Pulse(myButton, 200);
    /// 
    ///   // Smooth color transition
    ///   clsAnimationManager.ColorTransition(myPanel, oldColor, newColor, 400);
    /// 
    ///   // Attach hover animation (auto — مش محتاج تعمل حاجة تاني)
    ///   clsAnimationManager.AttachHoverScale(myButton, 1.05f);
    ///   clsAnimationManager.AttachHoverGlow(myPanel, glowColor);
    /// 
    /// ═══════════════════════════════════════════════════════
    /// </summary>
    public static class clsAnimationManager
    {
        // الـ Timer الداخلي — واحد لكل الأنيمشنز
        private static readonly System.Windows.Forms.Timer _globalTimer;
        private static readonly List<AnimationTask> _activeTasks = [];

        // ── Settings ──
        private static bool _enabled = true;
        private const int TICK_INTERVAL = 16; // ~60 FPS

        static clsAnimationManager()
        {
            _globalTimer = new System.Windows.Forms.Timer { Interval = TICK_INTERVAL };
            _globalTimer.Tick += OnGlobalTick;
        }

        /// <summary>تفعيل/تعطيل كل الأنيمشنز (مفيد لأجهزة ضعيفة)</summary>
        public static bool Enabled
        {
            get => _enabled;
            set => _enabled = value;
        }

        // ════════════════════════════════════════════════════
        //  1. SLIDE — تحريك كنترول من اتجاه معين
        // ════════════════════════════════════════════════════

        /// <summary>
        /// يحرّك كنترول من خارج النافذة لمكانه الصحيح.
        /// </summary>
        public static void SlideIn(Control control, eSlideDirection direction, int durationMs = 300,
                                    eEasing easing = eEasing.EaseOut, Action? onComplete = null)
        {
            if (!_enabled) { onComplete?.Invoke(); return; }

            var targetLocation = control.Location;
            var parent = control.Parent;

            // نقطة البداية (خارج النافذة)
            Point start = direction switch
            {
                eSlideDirection.FromLeft => new Point(-control.Width, targetLocation.Y),
                eSlideDirection.FromRight => new Point(parent?.Width ?? 800, targetLocation.Y),
                eSlideDirection.FromTop => new Point(targetLocation.X, -control.Height),
                eSlideDirection.FromBottom => new Point(targetLocation.X, parent?.Height ?? 600),
                _ => targetLocation
            };

            control.Location = start;
            control.Visible = true;

            StartTask(new AnimationTask
            {
                Target = control,
                Type = eAnimationType.SlideIn,
                Easing = easing,
                DurationMs = durationMs,
                StartPoint = start,
                EndPoint = targetLocation,
                OnComplete = onComplete
            });
        }

        /// <summary>
        /// يحرّك كنترول للخارج ثم يخفيه.
        /// </summary>
        public static void SlideOut(Control control, eSlideDirection direction, int durationMs = 300,
                                     eEasing easing = eEasing.EaseIn, Action? onComplete = null)
        {
            if (!_enabled) { control.Visible = false; onComplete?.Invoke(); return; }

            var startLocation = control.Location;
            var parent = control.Parent;

            Point end = direction switch
            {
                eSlideDirection.FromLeft => new Point(-control.Width, startLocation.Y),
                eSlideDirection.FromRight => new Point(parent?.Width ?? 800, startLocation.Y),
                eSlideDirection.FromTop => new Point(startLocation.X, -control.Height),
                eSlideDirection.FromBottom => new Point(startLocation.X, parent?.Height ?? 600),
                _ => startLocation
            };

            StartTask(new AnimationTask
            {
                Target = control,
                Type = eAnimationType.SlideIn,
                Easing = easing,
                DurationMs = durationMs,
                StartPoint = startLocation,
                EndPoint = end,
                OnComplete = () => { control.Visible = false; control.Location = startLocation; onComplete?.Invoke(); }
            });
        }

        // ════════════════════════════════════════════════════
        //  2. FADE — تدرج الشفافية
        // ════════════════════════════════════════════════════

        /// <summary>يظهر كنترول تدريجياً (للـ Forms بس — عبر Opacity)</summary>
        public static void FadeIn(Form form, int durationMs = 300,
                                   eEasing easing = eEasing.EaseOut, Action? onComplete = null)
        {
            if (!_enabled) { form.Opacity = 1; onComplete?.Invoke(); return; }

            form.Opacity = 0;
            form.Visible = true;

            StartTask(new AnimationTask
            {
                TargetForm = form,
                Type = eAnimationType.FadeIn,
                Easing = easing,
                DurationMs = durationMs,
                StartValue = 0f,
                EndValue = 1f,
                OnComplete = onComplete
            });
        }

        /// <summary>يخفي Form تدريجياً</summary>
        public static void FadeOut(Form form, int durationMs = 300,
                                    eEasing easing = eEasing.EaseIn, Action? onComplete = null)
        {
            if (!_enabled) { form.Opacity = 0; form.Visible = false; onComplete?.Invoke(); return; }

            StartTask(new AnimationTask
            {
                TargetForm = form,
                Type = eAnimationType.FadeOut,
                Easing = easing,
                DurationMs = durationMs,
                StartValue = 1f,
                EndValue = 0f,
                OnComplete = () => { form.Visible = false; onComplete?.Invoke(); }
            });
        }

        // ════════════════════════════════════════════════════
        //  3. PULSE — نبض (تكبير ثم رجوع)
        // ════════════════════════════════════════════════════

        /// <summary>
        /// يعمل نبضة واحدة على كنترول (تكبير 10% ثم رجوع).
        /// مفيد للـ countdown عند وصول الصفر.
        /// </summary>
        public static void Pulse(Control control, int durationMs = 200, float scaleAmount = 1.1f,
                                  Action? onComplete = null)
        {
            if (!_enabled) { onComplete?.Invoke(); return; }

            var originalSize = control.Size;
            var originalLoc = control.Location;

            StartTask(new AnimationTask
            {
                Target = control,
                Type = eAnimationType.Pulse,
                Easing = eEasing.EaseInOut,
                DurationMs = durationMs,
                StartValue = 1f,
                EndValue = scaleAmount,
                OriginalSize = originalSize,
                OriginalLocation = originalLoc,
                OnComplete = onComplete
            });
        }

        // ════════════════════════════════════════════════════
        //  4. SHAKE — اهتزاز (للأخطاء)
        // ════════════════════════════════════════════════════

        /// <summary>
        /// يهز كنترول يمين وشمال (3 مرات).
        /// مفيد لإظهار خطأ في الإدخال.
        /// </summary>
        public static void Shake(Control control, int intensity = 6, int durationMs = 300,
                                  Action? onComplete = null)
        {
            if (!_enabled) { onComplete?.Invoke(); return; }

            StartTask(new AnimationTask
            {
                Target = control,
                Type = eAnimationType.Shake,
                Easing = eEasing.Linear,
                DurationMs = durationMs,
                StartValue = 0f,
                EndValue = intensity,
                OriginalLocation = control.Location,
                OnComplete = onComplete
            });
        }

        // ════════════════════════════════════════════════════
        //  5. COLOR TRANSITION — تغيير لون تدريجي
        // ════════════════════════════════════════════════════

        /// <summary>
        /// يغيّر لون الخلفية تدريجياً.
        /// مفيد عند تغيير الثيم.
        /// </summary>
        public static void ColorTransition(Control control, Color fromColor, Color toColor,
                                            int durationMs = 400, eEasing easing = eEasing.EaseInOut,
                                            Action? onComplete = null)
        {
            if (!_enabled) { control.BackColor = toColor; onComplete?.Invoke(); return; }

            StartTask(new AnimationTask
            {
                Target = control,
                Type = eAnimationType.ColorTransition,
                Easing = easing,
                DurationMs = durationMs,
                FromColor = fromColor,
                ToColor = toColor,
                OnComplete = onComplete
            });
        }

        // ════════════════════════════════════════════════════
        //  6. ATTACH — أنيمشنات تلقائية (ربط مرة واحدة)
        // ════════════════════════════════════════════════════

        /// <summary>
        /// يربط أنيمشن hover scale على كنترول.
        /// لما الماوس يدخل — يكبر. لما يطلع — يرجع.
        /// اربطه مرة واحدة في Constructor.
        /// </summary>
        public static void AttachHoverScale(Control control, float scaleAmount = 1.05f, int durationMs = 150)
        {
            var originalSize = control.Size;
            var originalLoc = control.Location;

            control.MouseEnter += (s, e) =>
            {
                if (!_enabled) return;

                int dw = (int)(originalSize.Width * (scaleAmount - 1));
                int dh = (int)(originalSize.Height * (scaleAmount - 1));

                control.Size = new Size(originalSize.Width + dw, originalSize.Height + dh);
                control.Location = new Point(originalLoc.X - dw / 2, originalLoc.Y - dh / 2);
            };

            control.MouseLeave += (s, e) =>
            {
                control.Size = originalSize;
                control.Location = originalLoc;
            };
        }

        /// <summary>
        /// يربط أنيمشن hover glow (تغيير border color) على كنترول Guna2.
        /// اربطه مرة واحدة في Constructor.
        /// </summary>
        public static void AttachHoverHighlight(Control control, Color normalColor, Color hoverColor)
        {
            control.MouseEnter += (s, e) =>
            {
                if (!_enabled) return;
                control.BackColor = hoverColor;
            };
            control.MouseLeave += (s, e) =>
            {
                control.BackColor = normalColor;
            };
        }

        // ════════════════════════════════════════════════════
        //  الـ Engine الداخلي — Global Timer
        // ════════════════════════════════════════════════════

        private static void StartTask(AnimationTask task)
        {
            // ألغي أي أنيمشن سابق على نفس الكنترول
            _activeTasks.RemoveAll(t => t.Target == task.Target && t.TargetForm == task.TargetForm);

            task.StartTime = DateTime.Now;
            _activeTasks.Add(task);

            if (!_globalTimer.Enabled)
                _globalTimer.Start();
        }

        private static void OnGlobalTick(object? sender, EventArgs e)
        {
            if (_activeTasks.Count == 0)
            {
                _globalTimer.Stop();
                return;
            }

            var completed = new List<AnimationTask>();

            foreach (var task in _activeTasks)
            {
                float elapsed = (float)(DateTime.Now - task.StartTime).TotalMilliseconds;
                float rawProgress = Math.Clamp(elapsed / task.DurationMs, 0f, 1f);
                float progress = EasingFunctions.Calculate(task.Easing, rawProgress);

                try
                {
                    switch (task.Type)
                    {
                        case eAnimationType.SlideIn:
                            ApplySlide(task, progress);
                            break;

                        case eAnimationType.FadeIn:
                        case eAnimationType.FadeOut:
                            ApplyFade(task, progress);
                            break;

                        case eAnimationType.Pulse:
                            ApplyPulse(task, rawProgress);
                            break;

                        case eAnimationType.Shake:
                            ApplyShake(task, rawProgress);
                            break;

                        case eAnimationType.ColorTransition:
                            ApplyColorTransition(task, progress);
                            break;
                    }
                }
                catch
                {
                    // الكنترول ممكن يكون اتحذف
                    completed.Add(task);
                    continue;
                }

                if (rawProgress >= 1f)
                    completed.Add(task);
            }

            foreach (var task in completed)
            {
                _activeTasks.Remove(task);
                task.OnComplete?.Invoke();
            }

            if (_activeTasks.Count == 0)
                _globalTimer.Stop();
        }

        // ════════════════════════════════════════════════════
        //  Apply Methods — تطبيق كل نوع
        // ════════════════════════════════════════════════════

        private static void ApplySlide(AnimationTask task, float progress)
        {
            if (task.Target == null) return;
            int x = (int)(task.StartPoint.X + (task.EndPoint.X - task.StartPoint.X) * progress);
            int y = (int)(task.StartPoint.Y + (task.EndPoint.Y - task.StartPoint.Y) * progress);
            task.Target.Location = new Point(x, y);
        }

        private static void ApplyFade(AnimationTask task, float progress)
        {
            if (task.TargetForm == null) return;
            float opacity = task.StartValue + (task.EndValue - task.StartValue) * progress;
            task.TargetForm.Opacity = Math.Clamp(opacity, 0, 1);
        }

        private static void ApplyPulse(AnimationTask task, float rawProgress)
        {
            if (task.Target == null) return;

            // أول نص — تكبير. تاني نص — رجوع.
            float scale;
            if (rawProgress < 0.5f)
                scale = 1f + (task.EndValue - 1f) * (rawProgress * 2);
            else
                scale = task.EndValue - (task.EndValue - 1f) * ((rawProgress - 0.5f) * 2);

            int w = (int)(task.OriginalSize.Width * scale);
            int h = (int)(task.OriginalSize.Height * scale);
            int dx = (w - task.OriginalSize.Width) / 2;
            int dy = (h - task.OriginalSize.Height) / 2;

            task.Target.Size = new Size(w, h);
            task.Target.Location = new Point(task.OriginalLocation.X - dx, task.OriginalLocation.Y - dy);
        }

        private static void ApplyShake(AnimationTask task, float rawProgress)
        {
            if (task.Target == null) return;

            // اهتزاز sinusoidal يتلاشى
            float decay = 1f - rawProgress;
            float offset = MathF.Sin(rawProgress * MathF.PI * 6) * task.EndValue * decay;

            task.Target.Location = new Point(
                task.OriginalLocation.X + (int)offset,
                task.OriginalLocation.Y
            );
        }

        private static void ApplyColorTransition(AnimationTask task, float progress)
        {
            if (task.Target == null) return;

            int r = (int)(task.FromColor.R + (task.ToColor.R - task.FromColor.R) * progress);
            int g = (int)(task.FromColor.G + (task.ToColor.G - task.FromColor.G) * progress);
            int b = (int)(task.FromColor.B + (task.ToColor.B - task.FromColor.B) * progress);

            task.Target.BackColor = Color.FromArgb(
                Math.Clamp(r, 0, 255),
                Math.Clamp(g, 0, 255),
                Math.Clamp(b, 0, 255)
            );
        }

        // ════════════════════════════════════════════════════
        //  AnimationTask — وحدة أنيمشن واحدة
        // ════════════════════════════════════════════════════

        private class AnimationTask
        {
            public Control? Target { get; set; }
            public Form? TargetForm { get; set; }
            public eAnimationType Type { get; set; }
            public eEasing Easing { get; set; }
            public int DurationMs { get; set; }
            public DateTime StartTime { get; set; }

            // Slide
            public Point StartPoint { get; set; }
            public Point EndPoint { get; set; }

            // Fade/Pulse/Shake
            public float StartValue { get; set; }
            public float EndValue { get; set; }

            // Pulse/Shake (original state)
            public Size OriginalSize { get; set; }
            public Point OriginalLocation { get; set; }

            // Color
            public Color FromColor { get; set; }
            public Color ToColor { get; set; }

            // Callback
            public Action? OnComplete { get; set; }
        }
    }
}
