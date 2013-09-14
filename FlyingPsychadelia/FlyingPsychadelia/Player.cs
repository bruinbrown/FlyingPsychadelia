using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
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
        private Body _physicsBody;

        public Player(Texture2D texture, int x, int y, World world)
        {
            _Rectangle = new Rectangle(x, y, texture.Width, texture.Height);
            _Texture = texture;

            //We create a body object and make it dynamic (movable)


            _physicsBody = BodyFactory.CreateRectangle(world, _Rectangle.Width, _Rectangle.Height, 1.0f);
            _physicsBody.BodyType = BodyType.Dynamic;

        }
        public void MoveLeft()
        {
            _Rectangle.X -= 10;
        }


        public Rectangle Bounds
        {
            get
            {
                return _Rectangle;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_Texture, _physicsBody.Position, Color.White);

        }
        public void MoveRight()
        {
            _Rectangle.X += 10;
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
