using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FuncWorks.XNA.XTiled;
using Microsoft.Xna.Framework.Graphics;

namespace FlyingPsychadelia
{
    public class Player
    {
        private Rectangle _Rectangle;
        private Texture2D _Texture;
        public Vector2 Velocity;
        public Rectangle Bounds
        {
            get
            {
                return _Rectangle;
            }
        }
        public Player(Texture2D texture, int X, int Y)
        {
            _Rectangle = new Rectangle(X,Y,texture.Width, texture.Height);
            _Texture = texture;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_Texture,Bounds, Color.White);
        }
        public void Update(float gametime)
        {
            // Apply current velocity to rectangle X and Y
            _Rectangle.Offset((int)Math.Floor(Velocity.X), (int)Math.Floor(Velocity.Y));
        }
        public void AddVeocity(Vector2 vector2)
        {
            Velocity = new Vector2(Velocity.X + vector2.X, Velocity.Y + vector2.Y);
        }
        public bool MovingUp()
        {
            return Velocity.Y < 0;
        }
        public bool MovingRight()
        {
            return Velocity.X > 0;
            
        }
    }
}
