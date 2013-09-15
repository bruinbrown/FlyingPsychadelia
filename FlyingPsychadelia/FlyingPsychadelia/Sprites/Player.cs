using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FuncWorks.XNA.XTiled;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace FlyingPsychadelia
{
    public class Player : MovableSprite, ICollidable
    {
        private readonly IController _controller;
        private SoundEffect[] _Jump;
        public bool _IsLanded;
        private Random _random;
        private void JumpSound()
        {
            _Jump[ _random.Next()%3 ].Play();
        }

        public void DetectMovement(GameTime gameTime)
        {
            float MoveMagnitude = 0.02f * gameTime.ElapsedGameTime.Milliseconds;
            float MoveMagnitudeX = MoveMagnitude;
            if (!_IsLanded) MoveMagnitudeX *= 0.7f; // dampen in air

            if (_controller.DetectRight())
            {
                AddVeocity(new Vector2(MoveMagnitudeX, 0));
            }
            else if (_controller.DetectLeft())
            {
                AddVeocity(new Vector2(-MoveMagnitudeX, 0));
            }
            else if (_controller.DetectDown())
            {
                AddVeocity(new Vector2(0, MoveMagnitude));
            }
            else if (_controller.DetectFire())
            {
                
            }

            if (_controller.DetectJump())
            {

                if (_IsLanded)
                {
                    AddVeocity(new Vector2(0, -MoveMagnitude * 25f));
                    _IsLanded = false;
                    JumpSound();
                }
            }
            //
            if (Math.Abs(Velocity.X) < MoveMagnitude * 0.2f)
                AddVeocity( new Vector2(-Velocity.X, 0) );
        }
        public Player(ContentManager content, IController Controller)
            : base(content)
        {
            _Jump = new SoundEffect[3];
            _Jump[0] = content.Load<SoundEffect>("Jump1");
            _Jump[1] = content.Load<SoundEffect>("Jump2");
            _Jump[2] = content.Load<SoundEffect>("Jump3");

            _controller = Controller;
            SetTexture("leprechaun.png");
            _IsLanded = true;
            _random = new Random();
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
