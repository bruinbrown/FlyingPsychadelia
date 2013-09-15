using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
namespace FlyingPsychadelia
{
    public class HorizontallyOscillatingEnemy : OscillatingEnemy
    {
        public HorizontallyOscillatingEnemy(ContentManager content, int x, int y, int OscillationMax)
            : base(content, x, y, OscillationMax)
        {
            Velocity = new Vector2(5, 0);
            if (Random.Next(2) == 0)
                Velocity *= -1;
        }

        public override void Update(GameTime gametime)
        {
            if (Bounds.X < _StartX - (_OscillationMax / 2) || Bounds.X > _StartX + (_OscillationMax / 2))
                Velocity *= -1;
            base.Update(gametime);

        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if(Velocity.X>0)
                _Reversed = false;
            if (Velocity.X < 0)
                _Reversed = true;
            base.Draw(spriteBatch);
        }

    }
}
