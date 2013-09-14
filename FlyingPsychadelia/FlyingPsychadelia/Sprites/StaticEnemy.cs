using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyingPsychadelia
{
    public class StaticEnemy:BaseEnemy
    {
        public StaticEnemy(ContentManager content, int x, int y):base(content)
        {
            SetTexture("dragon.png");
            SetLocation(x,y);
        }
    }
}
