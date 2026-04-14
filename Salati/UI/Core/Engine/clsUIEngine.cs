using System.Runtime.InteropServices;
using UI.Core.Theme;
using UI.Core.Language;

namespace UI.Core.Engine
{
    /// <summary>
    /// المحرك المركزي - يطبق الثيم واللغة على شجرة controls.
    ///
    /// === تقنيات الأداء ===
    /// 1. WM_SETREDRAW على الـ Form فقط (الأولاد بيتأثروا تلقائي)
    /// 2. SuspendLayout على الـ Form فقط (مش كل control)
    /// 3. Single-pass iterative tree walking (Stack بدل Recursion)
    /// 4. DoubleBuffered فقط على containers (Panel, UserControl, Form)
    /// 5. RightToLeft على الـ Form فقط (Inherit default)
    /// 6. Thread-safe عبر InvokeRequired + BeginInvoke
    /// 7. Proper event cleanup عبر FormClosed
    /// </summary>
    public static class clsUIEngine
    {
        // ===== Win32 API =====

        private const int WM_SETREDRAW = 0x000B;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        /// <summary>يوقف رسم الـ window بالكامل (children كمان)</summary>
        private static void FreezeDrawing(Control control)
        {
            if (control.IsHandleCreated)
                SendMessage(control.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
        }

        /// <summary>يرجع الرسم ويعمل invalidate + update</summary>
        private static void UnfreezeDrawing(Control control)
        {
            if (control.IsHandleCreated)
            {
                SendMessage(control.Handle, WM_SETREDRAW, (IntPtr)1, IntPtr.Zero);
                control.Invalidate(true);
                control.Update();
            }
        }

        // ===== Bound Forms =====

        private static readonly Dictionary<Form, (Action<ThemeColors> theme, Action<ILanguagePack> lang)>
            _boundForms = new();

        // ===== Initialize =====

        public static void Initialize(string languageCode = "ar", string themeName = "Midnight Serenity")
        {
            clsLanguageManager.ApplyLanguage(languageCode);
            clsThemeManager.ApplyTheme(themeName);
        }

        // ===== ApplyAll =====

        /// <summary>
        /// يطبق الثيم واللغة على Form وكل اولادها.
        /// Single-pass, thread-safe, no flicker.
        /// </summary>
        public static void ApplyAll(Form form)
        {
            if (form.InvokeRequired)
            {
                form.BeginInvoke(() => ApplyAll(form));
                return;
            }

            // Suspend: Form فقط (الأولاد بيتأثروا تلقائي)
            FreezeDrawing(form);
            form.SuspendLayout();

            try
            {
                // RTL على الـ Form فقط (Inherit default)
                form.RightToLeft = clsLanguageManager.IsRtl ? RightToLeft.Yes : RightToLeft.No;
                form.RightToLeftLayout = clsLanguageManager.IsRtl;

                // Single-pass iterative على كل الشجرة
                WalkTree(form, ApplyBoth);
            }
            finally
            {
                form.ResumeLayout(true);
                UnfreezeDrawing(form);
            }
        }

        /// <summary>يطبق على control واحد واولاده</summary>
        public static void ApplyAll(Control control)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(() => ApplyAll(control));
                return;
            }

            FreezeDrawing(control);
            control.SuspendLayout();

            try
            {
                WalkTree(control, ApplyBoth);
            }
            finally
            {
                control.ResumeLayout(true);
                UnfreezeDrawing(control);
            }
        }

        // ===== BindEvents =====

        /// <summary>
        /// يربط الثيم واللغة بالـ Form.
        /// thread-safe + no flicker + auto-cleanup.
        /// </summary>
        public static void BindEvents(Form form)
        {
            UnbindEvents(form);

            Action<ThemeColors> onTheme = (_) =>
            {
                if (form.IsDisposed) return;
                if (form.InvokeRequired)
                    form.BeginInvoke(() => SmoothApply(form, ApplyThemeOnly));
                else
                    SmoothApply(form, ApplyThemeOnly);
            };

            Action<ILanguagePack> onLang = (_) =>
            {
                if (form.IsDisposed) return;
                if (form.InvokeRequired)
                    form.BeginInvoke(() => SmoothApplyLang(form));
                else
                    SmoothApplyLang(form);
            };

            clsThemeManager.ThemeChanged += onTheme;
            clsLanguageManager.LanguageChanged += onLang;
            _boundForms[form] = (onTheme, onLang);

            // Auto-cleanup: يمنع memory leak
            form.FormClosed += OnBoundFormClosed;
        }

        public static void UnbindEvents(Form form)
        {
            if (_boundForms.TryGetValue(form, out var h))
            {
                clsThemeManager.ThemeChanged -= h.theme;
                clsLanguageManager.LanguageChanged -= h.lang;
                _boundForms.Remove(form);
            }
        }

        private static void OnBoundFormClosed(object? sender, FormClosedEventArgs e)
        {
            if (sender is Form form)
            {
                UnbindEvents(form);
                form.FormClosed -= OnBoundFormClosed;
            }
        }

        // ===== Smooth Apply (Theme/Language separately) =====

        /// <summary>Theme فقط - freeze + single pass + unfreeze</summary>
        private static void SmoothApply(Form form, Action<Control> action)
        {
            FreezeDrawing(form);
            form.SuspendLayout();

            try
            {
                WalkTree(form, action);
            }
            finally
            {
                form.ResumeLayout(false);
                UnfreezeDrawing(form);
            }
        }

        /// <summary>
        /// Language — يحتاج RTL كمان.
        /// 
        /// ⚠️ تغيير RightToLeftLayout بيعمل RecreateHandle = الشاشة بتختفي وترجع.
        /// الحل: Opacity fade — نخفي الفورم قبل التغيير ونرجّعه بعده.
        /// WM_SETREDRAW مش كافي لأن الـ Handle بيتدمر ويتعمل من جديد.
        /// 
        /// ⚡ BeginInvoke مش بينفع لأن الـ message بيتبعت على Handle قديم بيتدمر.
        /// الحل: Timer بـ 50ms — بيشتغل على الـ message loop مش على handle محدد.
        /// </summary>
        private static void SmoothApplyLang(Form form)
        {
            // Phase 1: إخفاء الفورم
            double savedOpacity = form.Opacity;
            form.Opacity = 0;

            try
            {
                form.SuspendLayout();

                // Phase 2: تغيير RTL (هيعمل RecreateHandle — بس الفورم مخفي)
                form.RightToLeft = clsLanguageManager.IsRtl ? RightToLeft.Yes : RightToLeft.No;
                form.RightToLeftLayout = clsLanguageManager.IsRtl;

                // Phase 3: تحديث النصوص
                WalkTree(form, ApplyLangOnly);

                form.ResumeLayout(false);
            }
            catch
            {
                try { form.ResumeLayout(false); } catch { }
            }

            // Phase 4: إظهار الفورم — Timer عشان يحصل بعد RecreateHandle بالكامل
            var restoreTimer = new System.Windows.Forms.Timer { Interval = 50 };
            restoreTimer.Tick += (s, e) =>
            {
                restoreTimer.Stop();
                restoreTimer.Dispose();
                form.Opacity = savedOpacity;
            };
            restoreTimer.Start();
        }

        // ===== Actions =====

        private static void ApplyBoth(Control c)
        {
            if (c is IThemeable t) t.ApplyTheme(clsThemeManager.Colors);
            if (c is ILocalizable l) l.ApplyLanguage(clsLanguageManager.Current);
        }

        private static void ApplyThemeOnly(Control c)
        {
            if (c is IThemeable t) t.ApplyTheme(clsThemeManager.Colors);
        }

        private static void ApplyLangOnly(Control c)
        {
            if (c is ILocalizable l) l.ApplyLanguage(clsLanguageManager.Current);
        }

        // ===== Iterative Tree Walking (Stack - لا recursion) =====

        /// <summary>
        /// يمشي على كل controls في الشجرة بدون recursion.
        /// يستخدم Stack بدل function calls = اسرع واامن (لا StackOverflow).
        /// </summary>
        private static void WalkTree(Control root, Action<Control> action)
        {
            var stack = new Stack<Control>(32); // initial capacity
            stack.Push(root);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                action(current);

                // Push children (reverse order عشان يتنفذوا بالترتيب)
                var controls = current.Controls;
                for (int i = controls.Count - 1; i >= 0; i--)
                    stack.Push(controls[i]);
            }
        }

        // ===== DoubleBuffered (Selective) =====

        // Cached PropertyInfo - Reflection مرة واحدة فقط (مش كل control)
        private static readonly System.Reflection.PropertyInfo? _doubleBufferedProp =
            typeof(Control).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic);

        /// <summary>
        /// يفعل DoubleBuffered على containers فقط (Panel, UserControl, Form).
        /// Labels و Buttons مش محتاجين (وبيضيعوا memory).
        /// </summary>
        public static void EnableDoubleBufferingTree(Control root)
        {
            var stack = new Stack<Control>(32);
            stack.Push(root);

            while (stack.Count > 0)
            {
                var c = stack.Pop();

                // فقط containers بتستفيد من DoubleBuffered
                if (c is Form or Panel or UserControl or FlowLayoutPanel or TableLayoutPanel)
                    _doubleBufferedProp?.SetValue(c, true);

                for (int i = c.Controls.Count - 1; i >= 0; i--)
                    stack.Push(c.Controls[i]);
            }
        }
    }
}
