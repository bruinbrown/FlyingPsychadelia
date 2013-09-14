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
        private Player _player2;
        private World _world;
        GraphicsDeviceManager graphics;
        private List<Player> Players = new List<Player>();
        SpriteBatch spriteBatch;
        private Map _map;
        private Player _player;

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
            Players.Add(new Player(Content, new Player1KeyboardController()));
            Players.Add(new Player(Content, new Player2KeyboardController()));
            for (int i = 0; i < Players.Count; i++)
            {
                Players[i].SetLocation(i * 50, 0);
            }
            _world = new World(Players.ToArray(), _map.ObjectLayers[0].MapObjects);
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
            foreach (Player player in Players)
            {
                // Reset velocity for this frame
                player.Velocity = new Vector2(0, 0);
                // Add gravity
                player.AddVeocity(new Vector2(0, 1));
                // Add Directional Velocity
                player.DetectMovement();
                // Move player based on cumulative velocity
                player.Update(1);  // 1 doesnothing. Fix this for varying framerates.
            }

            // Resolve Collisions 
            _world.ResolveCollisions();
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
            foreach (Player player in Players)
            {
                player.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
        

    }

}

