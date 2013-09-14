using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlyingPsychadelia
{
    public class ProgressionShader
    {
        private readonly GraphicsDevice _graphics;
        private readonly Color _baseColour;
        private readonly Texture2D _gradient;

        public ProgressionShader(GraphicsDevice graphics, Color baseColour)
        {
            _graphics = graphics;
            _baseColour = baseColour;
            _gradient = CreateBg();
        }

        public void Draw(SpriteBatch spriteBatch, int progress)
        {
            var progressRectangle = new Rectangle(0, 0, (_graphics.Viewport.Width * progress/100), _graphics.Viewport.Height);
            spriteBatch.Draw(_gradient, progressRectangle, _baseColour);
        }

        private Texture2D CreateBg()
        {
            var width = _graphics.Viewport.Width;
            var height = _graphics.Viewport.Height;

            var backgroundTex = new Texture2D(_graphics, width, height);
            var bgc = new Color[height*width];

            var count = width;
            for (int i = 0; i < bgc.Length; i++)
            {
                var texColour = 255 - (200 * count)/width; // Defines the colour of the gradient.
                count--;
                if ((i+1) % width == 0) count = width;
                bgc[i] = new Color(texColour, texColour, texColour, 0);
            }
            backgroundTex.SetData(bgc);
            return backgroundTex;

        }
    }

}
