using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyingPsychadelia
{
    public class BaseEnemy : MovableSprite
    {
        public BaseEnemy(Texture2D texture, int X, int Y)
            : base(texture, X, Y)
        {

        }

    }
}
