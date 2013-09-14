using System;
using System.Collections.Generic;
using FuncWorks.XNA.XTiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace FlyingPsychadelia
{
    public class Player2KeyboardController : IController
    {

        public bool DetectFire()
        {
           return Keyboard.GetState().IsKeyDown(Keys.Space );
        }
        public bool DetectJump()
        {
            return Keyboard.GetState().IsKeyDown(Keys.W);
        }
        public bool DetectRight()
        {
            return Keyboard.GetState().IsKeyDown(Keys.D);
        }
        public bool DetectLeft()
        {
            return Keyboard.GetState().IsKeyDown(Keys.A);
        }
        public bool DetectUp()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Q);
        }
        public bool DetectDown()
        {
            return Keyboard.GetState().IsKeyDown(Keys.S);
        }
    }
}
