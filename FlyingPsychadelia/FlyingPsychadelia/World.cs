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
        private readonly Player[] _players;
        private List<MapObjectWrapper> _BlockingObjects { get; set; }
        public World(Player[] player, MapObject[] BlockingObjects)
        {
            _BlockingObjects = BlockingObjects.Select(p => new MapObjectWrapper(p)).ToList();
            _players = player;
        }
        public void ResolveCollisions()
        {
            List<MapObjectWrapper> _BlockingObjects1 = _BlockingObjects;
            foreach (Player player in _players)
            {
                var ToCheck = new List<ICollidable>();
                ToCheck.AddRange(_BlockingObjects1);
                ToCheck.AddRange(_players);
                ToCheck.Remove(player);
                foreach (var Object in ToCheck)
                {
                    if (player.Bounds.Intersects(Object.Bounds))
                        FixUpCollision(player, Object);
                }
            }
        }
        private void FixUpCollision(ICollidable Object1, ICollidable Object2)
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
    public class MapObjectWrapper : ICollidable
    {

        private readonly MapObject _mapObject;
        public MapObjectWrapper(MapObject mapObject)
        {
            _mapObject = mapObject;
        }
        public Rectangle Bounds
        {
            get
            {
                return _mapObject.Bounds;
            }
            set
            {
                _mapObject.Bounds = value;
            }
        }
        public Vector2 Velocity
        {
            get
            {
                return new Vector2();
            }
            set
            {

            }
        }

    }
}
