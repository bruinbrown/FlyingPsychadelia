#region Using Statements
using System;
using System.Collections.Generic;
using FuncWorks.XNA.XTiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace FlyingPsychadelia
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Map _map;
        private Player _player;
        private MapObject[] BlockingObjects;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Map.InitObjectDrawing(GraphicsDevice);
            _map = Content.Load<Map>("map1");
            BlockingObjects = _map.ObjectLayers[0].MapObjects;
            Texture2D _PlayerTexture = Content.Load<Texture2D>("Player.png");
            _player = new Player(_PlayerTexture, 0, 0);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Process Gravity
            foreach (MapObject Object in BlockingObjects)
            {
                if (_player.Bounds.Intersects(Object.Bounds))
                    FixUpPlayer(_player, Object);
            }

            _player.Velocity = new Vector2(0,0);
            _player.AddVeocity(new Vector2(0, 10));
            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _player.AddVeocity(new Vector2(10,0));
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _player.AddVeocity(new Vector2(-10, 0));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _player.AddVeocity(new Vector2(0, -10));
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _player.AddVeocity(new Vector2(0, 10));
            }
            _player.Update(1);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            _map.Draw(spriteBatch, new Rectangle(0, 0, 320, 320));
            _player.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
        private void FixUpPlayer(Player player, MapObject TheObject)
        {
            int dy = 1000;
            int dx = 1000;

            // Determine if overlap is primarily in X or Y
            if (player.Velocity.X > 0)
                dx = player.Bounds.Right - TheObject.Bounds.Left;
            if (player.Velocity.X < 0)
                dx = player.Bounds.Left - TheObject.Bounds.Right;

            if (player.Velocity.Y < 0)
                dy = player.Bounds.Top - TheObject.Bounds.Bottom;
            if (player.Velocity.Y > 0)
                dy = player.Bounds.Bottom - TheObject.Bounds.Top;

            if (Math.Abs(dx) > Math.Abs(dy))
            {
                // Correct Vertical
                if (player.MovingUp())
                {
                    // Adjust down
                    player.Bounds.Offset(0, dy + 1);
                }
                else
                {
                    // Adjust up
                    int Mydy = -(dy + 1);
                    int newVariable = 0;
                    player.Bounds = new Rectangle(player.Bounds.X + newVariable, player.Bounds.Y + Mydy, player.Bounds.Width, player.Bounds.Height);
                }
                player.Velocity.Y = 0.0f;
            }
            else
            {
                // Correct Horizontal
                if (player.MovingRight())
                {
                    // Adjust Left
                    player.Bounds.Offset(dx - 1, 0);
                }
                else
                {
                    // Adjust Right
                    player.Bounds.Offset(dx + 1, 0);
                }
                player.Velocity.X = 0.0f;
            }

        }
    }
}
