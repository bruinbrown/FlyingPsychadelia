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
    public class Player1KeyboardController : IController
    {

        public bool DetectFire()
        {
            return Keyboard.GetState().IsKeyDown(Keys.RightControl);
        }
        public bool DetectJump()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Up);
        }
        public bool DetectRight()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Right);
        }
        public bool DetectLeft()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Left);
        }
        public bool DetectUp()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Up);
        }
        public bool DetectDown()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Down);
        }
    }
}
