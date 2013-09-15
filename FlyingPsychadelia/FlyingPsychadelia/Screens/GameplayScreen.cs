using System;
using System.Collections.Generic;
using System.Threading;
using FlyingPsychadelia.Screens.Controls;
using FlyingPsychadelia.StateManager;
using FuncWorks.XNA.XTiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlyingPsychadelia.Screens
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    public class GameplayScreen : GameScreen
    {
        private ContentManager _content;
        private SpriteFont _gameFont;

        private Random _random = new Random();

        private float _pauseAlpha;
        private Map _map;

        private World _world;
        private ProgressionShader _progressionShader;
        private PsychShader _psychShader;
        private GameOverlay _gameOverlay;

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            _gameOverlay = new GameOverlay();
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (_content == null)
            {
                _content = new ContentManager(ScreenManager.Game.Services, "Content");
            }
            _gameOverlay.LoadContent(_content);

            _gameFont = _content.Load<SpriteFont>("gamefont");

            _map = _content.Load<Map>("map2");

            _world = new World(_map, _content);
            _gameOverlay.SetWorld(_world);

            Camera.Instance.SetMap(_map, _world.Players[0].LocationAsVector(), ScreenManager.GraphicsDevice.Viewport);

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();

            _progressionShader = new ProgressionShader(ScreenManager.GraphicsDevice, _map, Color.Blue);
            _psychShader = new PsychShader(ScreenManager.GraphicsDevice);
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            _content.Unload();
        }


        #endregion

        #region Update and Draw

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


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            ObjectLayer StartLayer = GetLayerOrNull("StartLayer");
            if (_world.Players[0].Bounds.Left > StartLayer.MapObjects[1].Bounds.X )
            {
                LoadingScreen.Load(ScreenManager, "Yay! Nice Trip!", 0, new MainMenuScreen());
            }

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                _pauseAlpha = Math.Min(_pauseAlpha + 1f / 32, 1);
            else
                _pauseAlpha = Math.Max(_pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                _world.Update(gameTime);
                Camera.Instance.SetCamera(_world.Players[0].LocationAsVector());
            }
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {
                //foreach (Player player in Players)
                //{
                //    player.DetectMovement();
                //}               
            }
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.Gray, 0, 0);

            var spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            _progressionShader.Draw(spriteBatch, _world.Progression, _map);
            if( ! _world.Players[0].IsLanded )
                _psychShader.Draw(spriteBatch);

            //Rectangle Camera = new Rectangle(0, 0, ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height);
            _map.Draw(spriteBatch, Camera.Instance.CameraView);
            _world.Draw(spriteBatch);

            _gameOverlay.Draw(spriteBatch, ScreenManager);
            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || _pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, _pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }


        #endregion
    }
}
