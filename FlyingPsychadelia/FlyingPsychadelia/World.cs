using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlyingPsychadelia.Sprites;
using FuncWorks.XNA.XTiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace FlyingPsychadelia
{
    public class World
    {
        private MapObjectWrapper[] _blockingObjects;
        private readonly List<Player> _players;
        private readonly Map _map;
        private readonly List<BaseEnemy> _enemies;
        private readonly ContentManager _content;
        public int Progression { get; set; }
        public int Time { get; set; }
        private const int MaxTime = 500;

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
            _map = map;
            _players = new List<Player>();
            _players.Add(new Player(_content, new Player1KeyboardController()));
            Time = 500;

            ObjectLayer StartLayer = GetLayerOrNull("StartLayer");
            if (StartLayer != null)
            {
                Players[0].SetLocation(StartLayer.MapObjects[0].Bounds.X, StartLayer.MapObjects[0].Bounds.Y);
            }
            var EnemyObjects = GetLayerObjectsOrNull("Enemies");
            if (EnemyObjects == null)
            {
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
            else
            {
                foreach (MapObject mapObject in EnemyObjects)
                {
                    var x = mapObject.Bounds.X;
                    var y = mapObject.Bounds.Y;
                    string EnemyType = mapObject.Properties["Type"].Value;
                    switch (EnemyType)
                    {

                        case "V":
                            _enemies.Add(new VerticallyOscillatingEnemy(_content, x, y, 150));
                            break;
                        case "H":
                            _enemies.Add(new HorizontallyOscillatingEnemy(_content, x, y, 150));
                            break;
                        case "S":
                        case "":
                            _enemies.Add(new StaticEnemy(_content, x, y));
                            break;
                    }
                }
            }
        }
        private IEnumerable<MapObject> GetLayerObjectsOrNull(string LayerName)
        {
            IEnumerable<MapObject> Objects = null;
            try
            {
                ObjectLayer Layer = _map.ObjectLayers[LayerName];
                int index = _map.ObjectLayers.IndexOf(Layer);
                Objects = _map.ObjectLayers[LayerName].MapObjects;
            }
            catch (Exception ex)
            {

            }
            return Objects;
        }
        private IEnumerable<MapObject> LayerObjectsOrNull(string LayerName, Rectangle Region)
        {
            IEnumerable<MapObject> Objects = null;
            try
            {
                ObjectLayer Layer = _map.ObjectLayers[LayerName];
                int index = _map.ObjectLayers.IndexOf(Layer);
                Objects = _map.GetObjectsInRegion(index, Region);
            }
            catch (Exception ex)
            {

            }
            return Objects;
        }
        private ObjectLayer GetLayerOrNull(string LayerName)
        {
            ObjectLayer Layer = null;
            try
            {
                Layer = _map.ObjectLayers[LayerName];
            }
            catch (Exception ex)
            {

            }
            return Layer;
        }
        public void Update(GameTime gameTime)
        {
            Time = MaxTime - gameTime.TotalGameTime.Seconds;
            foreach (Player player in Players)
            {
                // Add gravity
                float ms = gameTime.ElapsedGameTime.Milliseconds;
                float friction = -0.004f;
                if (player.Velocity.X < 0) friction *= 2f; //why??

                player.AddVeocity(new Vector2(player.Velocity.X * friction * ms, 0.02f * ms));

                // Add Directional Velocity
                player.DetectMovement(gameTime);
                player.Update(gameTime);
            }
            foreach (BaseEnemy enemy in _enemies)
            {
                enemy.Update(gameTime);
            }
            _blockingObjects = LayerObjectsOrNull("GroundCollision",
                                                               Camera.Instance.CameraView).Select(p => new MapObjectWrapper(p)).ToArray();
            CheckForPlayerDeath(gameTime);
            ResolveStaticCollisions();
            ResolveEnemyCollisions(gameTime);
            BulletCollisions();
            CalculateMaxProgression();
            CheckForWorldReset();
        }

        private void ResolveEnemyCollisions(GameTime gameTime)
        {
            var enemys = _enemies.Where(p => p.Bounds.Intersects(Camera.Instance.CameraView)).ToArray();

            foreach (var enemy in enemys)
            {
                foreach (var player in _players)
                {
                    if (enemy.Bounds.Intersects(player.Bounds))
                    {
                        player.PlayHasBeenHurt(gameTime);
                        _enemies.Remove(enemy);
                    }
                }

            }
        }

        private void BulletCollisions()
        {
            foreach (Player player in _players)
            {
                var Charms = player.Charms;

                Charm[] CharmArray = Charms.ToArray();
                foreach (Charm charm in CharmArray)
                {
                    BaseEnemy[] EnemyArray = _enemies.ToArray();
                    foreach (var Enemy in EnemyArray)
                    {
                        if (charm.Bounds.Intersects(Enemy.Bounds))
                        {
                            // do enemy interaction
                            _enemies.Remove(Enemy);
                        }
                    }
                    foreach (var obj in _blockingObjects)
                    {
                        if (obj.Bounds.Intersects(charm.Bounds))
                        {
                            player.Charms.Remove(charm);
                        }
                    }
                    if (!charm.Bounds.Intersects(Camera.Instance.CameraView))
                        player.Charms.Remove(charm);
                }
            }

        }
        private void CheckForPlayerDeath(GameTime gameTime)
        {
            var deathObjects = LayerObjectsOrNull("DeathLayer", Camera.Instance.CameraView);
            if (deathObjects == null) return;
            foreach (var deathObject in deathObjects)
            {
                foreach (var player in _players)
                {
                    if (player.Bounds.Intersects(deathObject.Bounds))
                    {
                        player.PlayHasBeenKilled(gameTime);
                    }
                }
            }
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

        private void CheckForWorldReset()
        {
            bool resetLevel = _players.Count(p => p.PlayerState == Player.PlayerStates.Dead && !p.PlayDeathAnimation) == _players.Count();

            if (resetLevel)
            {
                foreach (var player in Players)
                {
                    player.ResetPlayer();
                }
            }
        }

        private void ResolveStaticCollisions()
        {
            foreach (Player player in Players)
            {
                foreach (var Object in _blockingObjects)
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

            if (Math.Abs(dx) > Math.Abs(dy) + 0.01f)
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
                        player.IsLanded = true;
                }
                Object1.Velocity = new Vector2(Object1.Velocity.X, 0.0f);
            }
            else if (Math.Abs(dy) > Math.Abs(dx) + 0.01f)
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
