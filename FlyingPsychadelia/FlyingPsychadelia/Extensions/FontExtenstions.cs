using Microsoft.Xna.Framework.Graphics;

namespace FlyingPsychadelia.Extensions
{
    public static class FontExtenstions
    {
        /// <summary>
        /// Queries how much space this menu entry requires.
        /// </summary>
        public static int GetHeight(this SpriteFont font)
        {
            return font.LineSpacing;
        }

        /// <summary>
        /// Queries how wide the entry is, used for centering on the screen.
        /// </summary>
        public static int GetWidth(this SpriteFont font, string text)
        {
            return (int)font.MeasureString(text).X;
        }
    }
}
