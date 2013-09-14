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
        private int _OscillationWidth = 150;
        private int _StartX;
        private int _StartY;
        public OscillatingEnemy(ContentManager content, int x, int y)
            : base(content)
        {
            SetTexture("Player.png");
            SetLocation(x, y);
            _StartX = x;
            _StartY = y;
            Velocity = new Vector2(5, 0);
            if (Random.Next(2) == 0)
                Velocity *= -1;
        }

        public override void Update(float gametime)
        {
            if (Bounds.X < _StartX - (_OscillationWidth / 2) || Bounds.X > _StartX + (_OscillationWidth / 2))
                Velocity *= -1;
            //this.Velocity = new Vector2(Random.Next(-3, 4), Random.Next(-3, 4));
            base.Update(gametime);

        }

    }
}
