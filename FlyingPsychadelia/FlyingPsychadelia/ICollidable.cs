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
    public interface ICollidable
    {
        Rectangle Bounds { get; set; }
        Vector2 Velocity { get; set; }
    }
}
