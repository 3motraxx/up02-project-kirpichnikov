using System.Drawing;
using System.Windows.Forms;

namespace ProductCard
{
    public static class Styles
    {
        public static readonly Color COLOR_MAIN_BG =
            Color.White;

        public static readonly Color COLOR_SECONDARY_BG =
            Color.FromArgb(210, 246, 231);

        public static readonly Color COLOR_ACCENT =
            Color.FromArgb(112, 178, 175);

        public static readonly Color COLOR_HIGHLIGHT =
            Color.FromArgb(255, 128, 128);

        public const string FONT_FAMILY = "Calibri";

        public const float FONT_SIZE_SMALL = 10;
        public const float FONT_SIZE_NORMAL = 12;
        public const float FONT_SIZE_HEADER = 14;
        public const float FONT_SIZE_TITLE = 18;

        public static Font Font(
            float size = FONT_SIZE_NORMAL,
            bool bold = false)
        {
            return new Font(
                FONT_FAMILY,
                size,
                bold
                    ? FontStyle.Bold
                    : FontStyle.Regular
            );
        }
    }
}