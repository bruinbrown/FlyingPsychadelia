using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyingPsychadelia
{
    public class OscillatingEnemy : BaseEnemy
    {
        protected int _OscillationMax;
        protected int _StartX;
        protected int _StartY;

        public OscillatingEnemy(ContentManager content, int x, int y, int OscillationMax)
            : base(content)
        {
            _OscillationMax = OscillationMax;
            SetTexture("dragon.png", 32, 32);
            SetLocation(x, y);
            _StartX = x;
            _StartY = y;
        }
    }
}
