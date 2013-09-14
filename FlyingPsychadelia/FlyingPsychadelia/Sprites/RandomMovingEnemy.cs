using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyingPsychadelia
{
    public class RandomMovingEnemy : BaseEnemy
    {
        public RandomMovingEnemy(ContentManager content, int x, int y)
            : base(content)
        {
            SetTexture("dragon.png");
            SetLocation(x, y);
        }

        public override void Update(float gametime)
        {
            this.Velocity = new Vector2(Random.Next(-3, 4), Random.Next(-3, 4));
            base.Update(gametime);

        }

    }
}
