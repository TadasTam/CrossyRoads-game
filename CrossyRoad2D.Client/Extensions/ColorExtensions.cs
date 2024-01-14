using System.Windows.Media;

namespace CrossyRoad2D.Client.Extensions
{
    public static class ColorExtensions
    {
        public static Color ToWindowsMediaColor(this Common.Models.Color color)
        {
            return Color.FromScRgb(color.Alpha, color.Red, color.Green, color.Blue);
        }

        public static SolidColorBrush ToWindowsMediaSolidColorBrush(this Common.Models.Color color)
        {
            return new SolidColorBrush(ToWindowsMediaColor(color));
        }
    }
}
