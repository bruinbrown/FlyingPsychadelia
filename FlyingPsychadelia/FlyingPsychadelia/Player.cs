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
        private SoundEffect _Jump;

        private void JumpSound()
        {
        //    SoundEffect soundEffect;
        //    soundEffect = _Content.Load<SoundEffect>("Jump1");
                _Jump.Play();
        }
        public void DetectMovement()
        {
            int MoveMagnitude = 1;
            if (_controller.DetectRight())
            {
                AddVeocity(new Vector2(MoveMagnitude, 0));
            }
            else if (_controller.DetectLeft())
            {
                AddVeocity(new Vector2(-MoveMagnitude, 0));
            }
            if (_controller.DetectUp())
            {
                if (Velocity.Y == 1)
                    JumpSound();
                AddVeocity(new Vector2(0, -MoveMagnitude * 2));
            }
            else if (_controller.DetectDown())
            {
                AddVeocity(new Vector2(0, MoveMagnitude));
            }
            if (_controller.DetectJump())
            {
                AddVeocity(new Vector2(0, -20));
            }
        }
        public Player(ContentManager content, IController Controller)
            : base(content)
        {
            _Jump = content.Load<SoundEffect>("Jump1");
            _controller = Controller;
            SetTexture("Player.png");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_Texture,Bounds, Color.White);
        }
        public void Update(float gametime)
        {
            // Apply current velocity to rectangle X and Y
            _bounds.Offset((int)Math.Floor(Velocity.X), (int)Math.Floor(Velocity.Y));
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
