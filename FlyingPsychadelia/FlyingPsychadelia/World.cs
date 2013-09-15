using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FuncWorks.XNA.XTiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace FlyingPsychadelia
{
    public class World
    {
        private readonly List<Player> _players;
        private readonly Map _map;
        private readonly List<BaseEnemy> _enemies;
        private readonly ContentManager _content;
        private List<MapObjectWrapper> _BlockingObjects { get; set; }
        public int Progression { get; set; }

        public List<Player> Players
        {
            get
            {
                return _players;
            }
        }
        public World(Map map, ContentManager content)
        {
            _content = content;
            _enemies = new List<BaseEnemy>();
            _BlockingObjects = map.ObjectLayers["GroundCollision"].MapObjects.Select(p => new MapObjectWrapper(p)).ToList();
            _players = new List<Player>();
            _map = map;

            _players.Add(new Player(_content, new Player1KeyboardController()));
            _players.Add(new Player(_content, new Player2KeyboardController()));
            for (int i = 0; i < Players.Count; i++)
            {
                _players[i].SetLocation((i + 1) * 400, 350);
            }

            var Random = new System.Random();
            for (int i = 0; i < 50; i++)
            {
                var x = Random.Next(_map.Width * _map.TileWidth);
                var y = Random.Next(_map.Height * _map.TileHeight);
                _enemies.Add(new HorizontallyOscillatingEnemy(_content, x, y, 150));
            }
            for (int i = 0; i < 50; i++)
            {
                var x = Random.Next(_map.Width * _map.TileWidth);
                var y = Random.Next(_map.Height * _map.TileHeight);
                _enemies.Add(new VerticallyOscillatingEnemy(_content, x, y, 150));
            }

        }

        public void Update()
        {
            foreach (Player player in Players)
            {
                player.Velocity = Vector2.Zero;
                // Add gravity
                player.AddVeocity(new Vector2(player.Velocity.X * -0.2f, 1));
                // Add Directional Velocity
                player.DetectMovement();
                // Move player based on cumulative velocity
                player.Update(1);  // 1 doesnothing. Fix this for varying framerates.
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
            foreach (Player player in Players)
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
            var currentProgress = Players.Select(p => p.Bounds.Center.X).Max();
            Progression = Math.Max(Progression, currentProgress);
        }

        private void ResolveCollisions()
        {
            List<MapObjectWrapper> _BlockingObjects1 = _BlockingObjects;
            foreach (Player player in Players)
            {
                foreach (var Object in _BlockingObjects1)
                {
                    if (player.Bounds.Intersects(Object.Bounds))
                        FixUpCollision(player, Object);
                }
            }

            foreach (var player in Players)
            {
                foreach (var player1 in Players.Except(new[] { player }))
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
