using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace FlyingPsychadelia
{
    public class MovableSprite
    {
        protected Rectangle _bounds;
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
        public MovableSprite(Texture2D texture, int X, int Y)
        {
            _bounds = new Rectangle(X, Y, texture.Width, texture.Height);
            _Texture = texture;
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
