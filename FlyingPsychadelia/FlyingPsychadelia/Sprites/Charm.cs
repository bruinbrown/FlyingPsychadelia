using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyingPsychadelia.Sprites
{
    public class Charm : MovableSprite, ICollidable
    {
        public Charm(ContentManager content, int x, int y, Vector2 Direction)
            : base(content)
        {
            SetTexture("charm.png", 7, 6);
            SetLocation(x, y);
            AddVeocity(Direction);
        }
    }
}
