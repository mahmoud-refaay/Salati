using UI.Core;
using UI.Core.Theme;
using UI.Core.Language;

namespace UI.Controls.Settings
{
    /// <summary>
    /// صف تنبيه لصلاة واحدة — يعرض الاسم + كم دقيقة قبل + ON/OFF.
    /// يُستخدم 5 مرات داخل ucSettingsAlerts.
    /// 
    /// ┌─────────────────────────────────────────────────┐
    /// │ 🌅 الفجر     [10 min ▾]                  [🔵ON] │
    /// └─────────────────────────────────────────────────┘
    /// </summary>
    public partial class ucAlertRow : UserControl, IThemeable, ILocalizable
    {
        // ═══════════════════════════════════════
        //  Events
        // ═══════════════════════════════════════

        /// <summary>يُطلق عند تغيير أي إعداد في الصف</summary>
        public event EventHandler? SettingChanged;

        // ═══════════════════════════════════════
        //  Fields
        // ═══════════════════════════════════════

        private ePrayer _prayer = ePrayer.Fajr;
        private bool _isAltRow; // لتلوين الصفوف بالتبديل

        // ═══════════════════════════════════════
        //  Constructor
        // ═══════════════════════════════════════

        public ucAlertRow()
        {
            InitializeComponent();
            LoadMinutesOptions();

            togEnabled.CheckedChanged += (s, e) => SettingChanged?.Invoke(this, EventArgs.Empty);
            cboMinutes.SelectedIndexChanged += (s, e) => SettingChanged?.Invoke(this, EventArgs.Empty);
        }

        // ═══════════════════════════════════════
        //  Properties
        // ═══════════════════════════════════════

        /// <summary>أي صلاة</summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public ePrayer Prayer
        {
            get => _prayer;
            set
            {
                _prayer = value;
                lblEmoji.Text = PrayerHelper.GetEmoji(_prayer);
            }
        }

        /// <summary>هل التنبيه مفعّل</summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool IsEnabled
        {
            get => togEnabled.Checked;
            set => togEnabled.Checked = value;
        }

        /// <summary>كم دقيقة قبل (0 = وقت الأذان)</summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public int MinutesBefore
        {
            get
            {
                if (cboMinutes.SelectedItem is MinuteOption opt)
                    return opt.Value;
                return 10;
            }
            set
            {
                for (int i = 0; i < cboMinutes.Items.Count; i++)
                {
                    if (cboMinutes.Items[i] is MinuteOption opt && opt.Value == value)
                    {
                        cboMinutes.SelectedIndex = i;
                        return;
                    }
                }
                cboMinutes.SelectedIndex = 0;
            }
        }

        /// <summary>هل صف بلون بديل (alt row coloring)</summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool IsAltRow
        {
            get => _isAltRow;
            set => _isAltRow = value;
        }

        // ═══════════════════════════════════════
        //  Private Helpers
        // ═══════════════════════════════════════

        private void LoadMinutesOptions()
        {
            cboMinutes.Items.Clear();
            cboMinutes.Items.Add(new MinuteOption(0, "وقت الأذان"));
            cboMinutes.Items.Add(new MinuteOption(5, "5 min"));
            cboMinutes.Items.Add(new MinuteOption(10, "10 min"));
            cboMinutes.Items.Add(new MinuteOption(15, "15 min"));
            cboMinutes.Items.Add(new MinuteOption(20, "20 min"));
            cboMinutes.Items.Add(new MinuteOption(30, "30 min"));
            cboMinutes.SelectedIndex = 2; // default: 10 min
        }

        /// <summary>Helper class for ComboBox items</summary>
        private class MinuteOption
        {
            public int Value { get; }
            public string Label { get; }

            public MinuteOption(int value, string label)
            {
                Value = value;
                Label = label;
            }

            public override string ToString() => Label;
        }

        // ═══════════════════════════════════════
        //  IThemeable
        // ═══════════════════════════════════════

        public void ApplyTheme(ThemeColors t)
        {
            pnlRow.FillColor = _isAltRow
                ? ThemeColorUtils.Lighten(t.BgSurface, 3)
                : t.BgSurface;

            lblPrayerName.ForeColor = t.TextPrimary;

            cboMinutes.FillColor = t.InputBg;
            cboMinutes.ForeColor = t.InputText;
            cboMinutes.FocusedColor = t.Accent1;
            cboMinutes.BorderColor = t.BorderDefault;

            togEnabled.CheckedState.FillColor = t.Accent1;
            togEnabled.CheckedState.BorderColor = t.Accent1;
            togEnabled.UncheckedState.FillColor = ThemeColorUtils.Darken(t.BgSurface, 5);
            togEnabled.UncheckedState.BorderColor = t.TextMuted;
        }

        // ═══════════════════════════════════════
        //  ILocalizable
        // ═══════════════════════════════════════

        public void ApplyLanguage(ILanguagePack lang)
        {
            lblPrayerName.Text = PrayerHelper.GetName(_prayer, lang);

            // تحديث نص "وقت الأذان" بالدسايند
            if (cboMinutes.Items.Count > 0 && cboMinutes.Items[0] is MinuteOption opt)
            {
                cboMinutes.Items[0] = new MinuteOption(0,
                    lang.IsRtl ? "وقت الأذان" : "At Adhan");
            }
        }
    }
}
