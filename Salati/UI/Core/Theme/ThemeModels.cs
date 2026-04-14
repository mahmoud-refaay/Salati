namespace UI.Core.Theme
{
    /// <summary>
    /// تعريف ثيم واحد — الاسم + الألوان + هل هو داكن
    /// </summary>
    public class ThemeDefinition
    {
        public string Name { get; set; } = string.Empty;
        public bool IsDark { get; set; }
        public ThemeColors Colors { get; set; } = new();

        public ThemeDefinition() { }

        public ThemeDefinition(string name, bool isDark, ThemeColors colors)
        {
            Name = name;
            IsDark = isDark;
            Colors = colors;
        }
    }
}
