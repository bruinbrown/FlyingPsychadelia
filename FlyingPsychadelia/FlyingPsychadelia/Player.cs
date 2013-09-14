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
        public void MoveLeft()
        {
            _Rectangle.X  -= 10;
        }
                                     
                                     
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
        public void MoveRight()
        {
            _Rectangle.X += 10;
        }
    }
}
