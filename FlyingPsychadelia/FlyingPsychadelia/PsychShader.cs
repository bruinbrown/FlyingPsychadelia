using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FlyingPsychadelia
{
    public class PsychShader
    {
        private readonly GraphicsDevice _graphics;
        private readonly Texture2D _gradient;
        static private Random _random;

        public PsychShader(GraphicsDevice graphics)
        {
            _graphics = graphics;
            _random = new Random();
            _gradient = CreateBg();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //todo: enable when flying only

            Color randColor = new Color();
            randColor.R = (byte)_random.Next(100);
            randColor.G = (byte)_random.Next(100);
            randColor.B = (byte)_random.Next(100);
            randColor.A = (byte)_random.Next(100);
            var progressRectangle = new Rectangle(0, 0, _graphics.Viewport.Width, _graphics.Viewport.Height);
            spriteBatch.Draw(_gradient, progressRectangle, randColor);
        }

        private Texture2D CreateBg()
        {
            var width = _graphics.Viewport.Width;
            var height = _graphics.Viewport.Height;

            var backgroundTex = new Texture2D(_graphics, width, height);
            var bgc = new Color[height * width];

            var count = width;
            for (int i = 0; i < bgc.Length; i++)
            {
                bgc[i] = new Color(255,255,255,255);
            }
            backgroundTex.SetData(bgc);
            return backgroundTex;

        }
    }

}
