using System.Windows.Media;

namespace ScriptExecutorLib.Resources.Style
{
    //Color palette from https://coolors.co/palettes/trending

    public class AppColor
    {
        public static SolidColorBrush ColorFill = new SolidColorBrush(Color.FromArgb(0xFF, 0x13, 0x14, 0x74));
        public static SolidColorBrush ColorFillSecond = new SolidColorBrush(Color.FromArgb(0xFF, 0x13, 0x31, 0x5c));
        public static SolidColorBrush ColorFillThird = new SolidColorBrush(Color.FromArgb(0xFF, 0x0B, 0x25, 0x45));

        public static SolidColorBrush ColorInvertFill = new SolidColorBrush(Color.FromArgb(0xFF, 0x8D, 0xA9, 0xC4));
        public static SolidColorBrush ColorInvertFillSecond = new SolidColorBrush(Color.FromArgb(0xFF, 0xEE, 0xF4, 0xED));
        public static SolidColorBrush ColorInvertFillThird = new SolidColorBrush(Color.FromArgb(0xFF, 0xCE, 0xE5, 0xF2));

        public static SolidColorBrush ColorSignalError = Brushes.Red;
        public static SolidColorBrush ColorSignalInfo = Brushes.Blue;
        public static SolidColorBrush ColorSignalOK = Brushes.LightGreen;
        public static SolidColorBrush ColorSignalWarning = Brushes.DarkOrange;

        public static SolidColorBrush ColorShadow = new SolidColorBrush(Color.FromArgb(0xFF, 0xA5, 0xA5, 0xA5));
        public static SolidColorBrush ColorShadowTransparent = new SolidColorBrush(Color.FromArgb(0x35, 0xA5, 0xA5, 0xA5));

        public static SolidColorBrush ColorText = new SolidColorBrush(Color.FromArgb(0xFF, 0x0B, 0x25, 0x45));
        public static SolidColorBrush ColorTextSecond = new SolidColorBrush(Color.FromArgb(0xFF, 0x13, 0x31, 0x5c));

        public static SolidColorBrush ColorInvertText = new SolidColorBrush(Color.FromArgb(0xFF, 0xEE, 0xF4, 0xED));
        public static SolidColorBrush ColorInvertTextSecond = new SolidColorBrush(Color.FromArgb(0xFF, 0xCE, 0xE5, 0xF2));
    }
}