using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyingPsychadelia
{
    public class VerticallyOscillatingEnemy : OscillatingEnemy 
    {
        public VerticallyOscillatingEnemy(ContentManager content, int x, int y, int OscillationMax)
            : base(content, x, y, OscillationMax)
        {
            Velocity = new Vector2(0, 5);
            if (Random.Next(2) == 0)
                Velocity *= -1;
        }
        public override void Update(float gametime)
        {
            if (Bounds.Y < _StartY - (_OscillationMax / 2) || Bounds.Y > _StartY + (_OscillationMax / 2))
                Velocity *= -1;
            //this.Velocity = new Vector2(Random.Next(-3, 4), Random.Next(-3, 4));
            base.Update(gametime);

        }
    }
}
