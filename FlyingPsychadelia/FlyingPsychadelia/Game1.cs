#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using FarseerPhysics;
using FarseerPhysics.DebugView;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
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
        private World _world;
        private Body _groundBody;
        private Texture2D _base;
        private DebugViewXNA _debugView;
        private MapObject _groundCollision;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _world = new World(new Vector2(0f, 1f));

            spriteBatch = new SpriteBatch(GraphicsDevice);

            Map.InitObjectDrawing(GraphicsDevice);
            _map = Content.Load<Map>("map1");

            Texture2D _PlayerTexture = Content.Load<Texture2D>("Player.png");
            _player = new Player(_PlayerTexture, 0, 0, _world);

            ConvertUnits.SetDisplayUnitToSimUnitRatio(100f);
            _debugView = new DebugViewXNA(_world);

            _debugView.RemoveFlags(DebugViewFlags.Controllers);
            _debugView.RemoveFlags(DebugViewFlags.Joint);

            _debugView.LoadContent(GraphicsDevice, Content);


            var collisionLayer = _map.ObjectLayers["GroundCollision"];
            _groundCollision = collisionLayer.MapObjects.Single();

            _groundBody = BodyFactory.CreateRectangle(_world, ConvertUnits.ToSimUnits(_groundCollision.Bounds.Width), ConvertUnits.ToSimUnits(_groundCollision.Bounds.Height), 1f);
            _groundBody.BodyType = BodyType.Static;
            _groundBody.Position = new Vector2(ConvertUnits.ToSimUnits(_groundCollision.Bounds.X), ConvertUnits.ToSimUnits(_groundCollision.Bounds.Y));

            _groundCollision.Bounds = new Rectangle(0, _groundCollision.Bounds.Height,
                                                    _groundCollision.Bounds.Height, _groundCollision.Bounds.Width);

            //_groundBody = BodyFactory.CreateRectangle(_world, ConvertUnits.ToSimUnits(150), ConvertUnits.ToSimUnits(50), 10f);
            //_groundBody.Position = ConvertUnits.ToSimUnits(0, 250);
            //_groundBody.IsStatic = true;
            //_groundBody.Restitution = 0.2f;
            //_groundBody.Friction = 0.2f;

            //var body = BodyFactory.CreateRectangle(_world, ConvertUnits.ToSimUnits(260), ConvertUnits.ToSimUnits(50), 1f);
            //body.BodyType = BodyType.Static;
            //body.Position = ConvertUnits.ToSimUnits(new Vector2(0, 160));
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

            _player.Update(gameTime);

            _world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshort of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            //spriteBatch.Draw(_map.Tilesets[0].Texture, new Rectangle(0, 0, 320, 320), Color.White);
            //_map.Draw(spriteBatch, ConvertUnits.ToDisplayUnits(_groundBody.Position));

            //_map.Draw(spriteBatch, new Rectangle(0, 0, (int) ConvertUnits.ToDisplayUnits(_groundBody.Position.X), (int) ConvertUnits.ToDisplayUnits(_groundBody.Position.Y)));

            //spriteBatch.Draw(_map.Tilesets[0].Texture, ConvertUnits.ToDisplayUnits(_groundBody.Position), Color.White);

            var destination = new Rectangle
               (
                   (int)_groundBody.Position.X,
                   (int)_groundBody.Position.Y,
                   (int)_groundCollision.Bounds.X,
                   (int)_groundCollision.Bounds.Y
               );

            _map.Draw(spriteBatch, _groundCollision.Bounds);

            //for (int i = 0; i < _map.ObjectLayers.Count; i++)
            //{
            //    _map.DrawObjectLayer(spriteBatch, i, new Rectangle(0, 0, 320, 320), i);
            //}
            _player.Draw(spriteBatch);
            var projection = Matrix.CreateOrthographicOffCenter(
                0f,
                ConvertUnits.ToSimUnits(GraphicsDevice.Viewport.Width),
                ConvertUnits.ToSimUnits(GraphicsDevice.Viewport.Height), 0f, 0f,
                1f);
            _debugView.RenderDebugData(ref projection);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
