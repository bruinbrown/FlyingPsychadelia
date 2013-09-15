using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FuncWorks.XNA.XTiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlyingPsychadelia
{
    public class World
    {
        private readonly List<Player> _players;
        private readonly Map _map;
        private readonly List<BaseEnemy> _enemies;
        private List<MapObjectWrapper> _BlockingObjects { get; set; }
        public int Progression { get; set; }

        public World(Map map, List<Player> Players, List<BaseEnemy> Enemies)
        {
            _enemies = Enemies;
            _BlockingObjects = map.ObjectLayers[0].MapObjects.Select(p => new MapObjectWrapper(p)).ToList();
            _players = Players;
            _map = map;
        }
        public void Update(GameTime gameTime)
        {
            foreach (Player player in _players)
            {
                // Add gravity
                float ms = gameTime.ElapsedGameTime.Milliseconds;
                float friction = -0.004f;
                if (player.Velocity.X < 0) friction *= 2f; //why??

                player.AddVeocity(new Vector2(player.Velocity.X * friction * ms, 0.02f * ms));

                // Add Directional Velocity
                player.DetectMovement(gameTime);
                // Move player based on cumulative velocity
                player.Update(gameTime);  // 1 doesnothing. Fix this for varying framerates.
            }
            foreach (BaseEnemy enemy in _enemies)
            {
                enemy.Update(1);
            }
            ResolveCollisions();
            CalculateMaxProgression();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Player player in _players)
            {
                player.Draw(spriteBatch);
            }
            foreach (BaseEnemy enemy in _enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }
        private void CalculateMaxProgression()
        {
            var currentProgress = _players.Select(p => p.Bounds.Center.X).Max();
            Progression = Math.Max(Progression, currentProgress);
        }

        private void ResolveCollisions()
        {
            List<MapObjectWrapper> _BlockingObjects1 = _BlockingObjects;
            foreach (Player player in _players)
            {
                foreach (var Object in _BlockingObjects1)
                {
                    if (player.Bounds.Intersects(Object.Bounds))
                        FixUpCollision(player, Object);
                }
            }

            foreach (var player in _players)
            {
                foreach (var player1 in _players.Except(new[] {player}))
                {
                    FixUpCollision(player, player1);
                }
            }
        }

        private void FixUpCollision(ICollidable Object1, ICollidable Object2)
        {
            var Overlap = Rectangle.Intersect(Object1.Bounds, Object2.Bounds);
            var dx = Overlap.Width;
            var dy = Overlap.Height;
            Player player = Object1 as Player;

            if (Math.Abs(dx) > Math.Abs(dy) + 0.01f )
            {
                // Correct Vertical
                if (Object1.Velocity.Y < 0)
                {
                    // Adjust down
                    OffsetPlayerBounds(Object1, 0, dy);
                }

                if (Object1.Velocity.Y > 0)
                {
                    // Adjust up
                    OffsetPlayerBounds(Object1, 0, -dy);
                    if (player != null)
                        player._IsLanded = true;
                }
                Object1.Velocity = new Vector2(Object1.Velocity.X, 0.0f);
            }
            else if (Math.Abs(dy) > Math.Abs(dx) + 0.01f )
            {
                // Correct Horizontal
                if (Object1.Velocity.X < 0) // Moving Left
                {
                    // Adjust Right
                    OffsetPlayerBounds(Object1, dx, 0);

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
