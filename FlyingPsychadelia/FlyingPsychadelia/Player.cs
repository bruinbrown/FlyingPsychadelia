using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using FuncWorks.XNA.XTiled;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlyingPsychadelia
{
    public class Player
    {
        private Texture2D _Texture;
        public Vector2 Velocity;
        private Body _body;

        public Player(Texture2D texture, int x, int y, World world)
        {
            _Texture = texture;

            //We create a body object and make it dynamic (movable)


            _body = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(texture.Width), ConvertUnits.ToSimUnits(texture.Height), 10.0f);
            _body.BodyType = BodyType.Dynamic;
            _body.Position = ConvertUnits.ToSimUnits(new Vector2(30, 0));
            _body.SleepingAllowed = false;
            _body.Restitution = .7f;
            _body.Friction = .2f;

            _body.Mass = 1;
            //_physicsBody.LocalCenter = new Vector2(ConvertUnits.ToSimUnits(texture.Width), ConvertUnits.ToSimUnits(texture.Height / 2));
            //_physicsBody.UserData = this;
            //_physicsBody.CollisionCategories = Category.All;
            //_physicsBody.CollidesWith = Category.All;
            //_physicsBody.IgnoreGravity = false;

        }
        public void MoveLeft()
        {
            _body.Position = new Vector2(_body.Position.X - ConvertUnits.ToSimUnits(5), _body.Position.Y);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(_Texture, ConvertUnits.ToDisplayUnits(_body.Position), Color.White);
            spriteBatch.Draw(_Texture, ConvertUnits.ToDisplayUnits(_body.Position), null, Color.White, _body.Rotation, new Vector2(16, 16), 1f, SpriteEffects.None, 0f);
            

        }
        public void MoveRight()
        {
            _body.Position = new Vector2(_body.Position.X + ConvertUnits.ToSimUnits(5), _body.Position.Y);
        }
        public bool MovingUp()
        {
            return Velocity.Y < 0;
        }
        public bool MovingRight()
        {
            return Velocity.X > 0;
            
        }

        public void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                MoveRight();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                MoveLeft();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                _body.Position = ConvertUnits.ToSimUnits(new Vector2(0, 0));
            }
        }
    }
}
