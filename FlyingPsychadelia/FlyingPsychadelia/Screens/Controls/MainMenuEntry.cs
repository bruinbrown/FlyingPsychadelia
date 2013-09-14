using System;
using FlyingPsychadelia.Screens.Events;
using FlyingPsychadelia.StateManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlyingPsychadelia.Screens.Controls
{
    public class MainMenuEntry
    {
        private Random _rand;

        /// <summary>
        /// Gets or sets the text of this menu entry.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the position at which to draw this menu entry.
        /// </summary>
        public Vector2 Position { get; set; }
        public bool UseOffset { get; set; }
        public Color Color { get; set; }
        private double _previousUpdate;

        /// <summary>
        /// Constructs a new menu entry with the specified text.
        /// </summary>
        public MainMenuEntry(string text, bool useOffset)
        {
            Text = text;
            UseOffset = useOffset;
             _rand = new Random(DateTime.Now.Millisecond);
        }

        /// <summary>
        /// Updates the menu entry.
        /// </summary>
        public virtual void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds > _previousUpdate)
            {
                Color = new Color(_rand.Next(255), _rand.Next(255), _rand.Next(255));
                _previousUpdate = gameTime.TotalGameTime.TotalMilliseconds + 125;
            }
        }

        /// <summary>
        /// Draws the menu entry. This can be overridden to customize the appearance.
        /// </summary>
        public virtual void Draw(MenuScreen screen, GameTime gameTime, float transitionOffset)
        {
            // Draw the selected entry in yellow, otherwise white.
            ScreenManager screenManager = screen.ScreenManager;
            SpriteBatch spriteBatch = screenManager.SpriteBatch;
            SpriteFont font = screenManager.Font;

            var titleOrigin = font.MeasureString(Text) / 2;
            const float titleScale = 1.25f;

            if (UseOffset)
            {
                Position = new Vector2(Position.X - 140, Position.Y);
            }

            spriteBatch.DrawString(font, Text, Position, Color, 0,
                                   titleOrigin, titleScale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Queries how much space this menu entry requires.
        /// </summary>
        public virtual int GetHeight(MenuScreen screen)
        {
            return screen.ScreenManager.Font.LineSpacing;
        }

        /// <summary>
        /// Queries how wide the entry is, used for centering on the screen.
        /// </summary>
        public virtual int GetWidth(MenuScreen screen)
        {
            return (int)screen.ScreenManager.Font.MeasureString(Text).X;
        }
    }
}
