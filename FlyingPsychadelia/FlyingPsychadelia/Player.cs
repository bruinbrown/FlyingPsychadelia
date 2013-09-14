using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FuncWorks.XNA.XTiled;
using Microsoft.Xna.Framework.Graphics;

namespace FlyingPsychadelia
{
    public class Player : MovableSprite, ICollidable
    {
        private readonly IController _controller;

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


        public Player(Texture2D texture, int X, int Y, IController Controller):base(texture,X,Y)
        {
            _controller = Controller;
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
    public class Enemy
    {
        
        public Enemy()
        {
            
        }
    }
}
