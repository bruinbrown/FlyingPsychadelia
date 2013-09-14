using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FuncWorks.XNA.XTiled;
using Microsoft.Xna.Framework;

namespace FlyingPsychadelia
{
    public class World
    {
        private readonly Player _player;
        public static MapObject[] BlockingObjects { get; set; }
        public World(Player player)
        {
            _player = player;            
        }
        public void ResolveCollisions()
        {
            foreach (MapObject Object in BlockingObjects)
            {
                if (_player.Bounds.Intersects(Object.Bounds))
                    FixUpCollision(_player, Object);
            }
        }
        private void FixUpCollision(ICollidable Object1, MapObject Object2)
        {
            var Overlap = Rectangle.Intersect(Object1.Bounds, Object2.Bounds);
            var dx = Overlap.Width;
            var dy = Overlap.Height;

            if (Math.Abs(dx) > Math.Abs(dy))
            {
                // Correct Vertical
                if (Object1.Velocity.Y < 0)
                {
                    // Adjust down
                    OffsetPlayerBounds(Object1,
                   0,
                   dy);
                }

                if (Object1.Velocity.Y > 0)
                {
                    // Adjust up
                    OffsetPlayerBounds(Object1,
                   0, -dy);
                }
                Object1.Velocity = new Vector2(Object1.Velocity.X, 0.0f);
            }
            else
            {
                // Correct Horizontal
                if (Object1.Velocity.X < 0) // Moving Left
                {
                    // Adjust Right
                    OffsetPlayerBounds(Object1,

                   dx, 0);

                }
                if (Object1.Velocity.X > 0)
                {
                    // Adjust Left

                    OffsetPlayerBounds(Object1, -dx, 0);

                }
                Object1.Velocity = new Vector2(0.0f, Object1.Velocity.Y);
            }

        }
        private void OffsetPlayerBounds(ICollidable player, int myDx, int Mydy)
        {
            player.Bounds = new Rectangle(player.Bounds.X + myDx, player.Bounds.Y + Mydy, player.Bounds.Width, player.Bounds.Height);
        }

    }
}
