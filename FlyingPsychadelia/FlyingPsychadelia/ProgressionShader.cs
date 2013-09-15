using FuncWorks.XNA.XTiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FlyingPsychadelia
{
    public class ProgressionShader
    {
        private readonly GraphicsDevice _graphics;
        private readonly Map _map;
        private readonly Color _baseColour;
        private readonly Texture2D _gradient;
        static private Random _random;

        public ProgressionShader(GraphicsDevice graphics, Map map, Color baseColour)
        {
            _graphics = graphics;
            _map = map;
            _baseColour = baseColour;
            _random = new Random();
            _gradient = CreateBg();
        }

        public void Draw(SpriteBatch spriteBatch, int progress, Map map)
        {
            progress = progress >= map.Width*map.TileWidth - 25 ? map.Width*map.TileWidth : progress; // hack to manage barrier at the end of the level
            var destinationRectangle = new Rectangle(0,0,progress-Camera.Instance.CameraView.X, Camera.Instance.CameraView.Height);
            var sourceRectangle = new Rectangle(Camera.Instance.CameraView.X, 0, progress - Camera.Instance.CameraView.X,
                                                Camera.Instance.CameraView.Height);
            spriteBatch.Draw(_gradient, destinationRectangle, sourceRectangle, _baseColour);
        }

        private Texture2D CreateBg()
        {
            var width = _map.Width*_map.TileWidth;
            var height = _graphics.Viewport.Height;

            var backgroundTex = new Texture2D(_graphics, width, height);
            var bgc = new Color[height*width];

            var count = width;
            for (int i = 0; i < bgc.Length; i++)
            {
                var texColour = 255 - (200 * count)/width; // Defines the colour of the gradient.
                texColour -= _random.Next(50);
                count--;
                if ((i+1) % width == 0) count = width;
                bgc[i] = new Color(texColour, texColour, texColour, 0);
            }
            backgroundTex.SetData(bgc);
            return backgroundTex;

        }
    }

}
