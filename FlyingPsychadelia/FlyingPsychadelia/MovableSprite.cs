using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace FlyingPsychadelia
{
    public class MovableSprite
    {
        protected Rectangle _bounds;
        private ContentManager _Content;
        protected Texture2D _Texture;
        public Rectangle Bounds
        {
            get
            {
                return _bounds;
            }
            set
            {
                _bounds = value;
            }
        }
        public Vector2 Velocity { get; set; }
        public MovableSprite(ContentManager content)
        {
            _Content = content;
        }
        protected void SetTexture(string TextureName)
        {
            _Texture = _Content.Load<Texture2D>(TextureName);
        }
        public void SetLocation(int X, int Y)
        {
            _bounds = new Rectangle(X, Y, _Texture.Width, _Texture.Height);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_Texture, Bounds, Color.White);
        }
        public void Update(float gametime)
        {
            // Apply current velocity to rectangle X and Y
            _bounds.Offset((int)Math.Floor(Velocity.X), (int)Math.Floor(Velocity.Y));
        }

    }
}
