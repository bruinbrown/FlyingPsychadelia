using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace FlyingPsychadelia.Sprites
{
    public class PotOfGold : MovableSprite
    {
        public PotOfGold(ContentManager content, Rectangle bounds) : base(content)
        {
            SetTexture("PotOfGold.png", 32, 32);
            SetLocation(bounds.X, bounds.Y);
        }
    }
}
