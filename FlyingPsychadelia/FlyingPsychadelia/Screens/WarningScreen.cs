using System;
using FlyingPsychadelia.Extensions;
using FlyingPsychadelia.Screens.GameStateManagement;
using FlyingPsychadelia.StateManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlyingPsychadelia.Screens
{
    public class WarningScreen : GameScreen
    {
        private bool _skipScreen;

        private const string WarningLine1 = "A very small percentage of people may experience a seizure when";
        private const string WarningLine2 = "exposed to certain visual images, including flashing lights or patterns";
        private const string WarningLine3 = "that may appear in video games. If you or any of your relatives have";
        private const string WarningLine4 = "a history of seizures or epilepsy, consult a doctor before playing.";

        private const string WarningLine5 = "Immediately stop playing and consult a doctor if you experience any";
        private const string WarningLine6 = "symptoms such as lightheadedness, disorientation or twitching.";

        public override void HandleInput(InputState input)
        {
            if (input.CurrentKeyboardStates[0].IsKeyDown(Keys.Escape))
            {
                _skipScreen = true;
            }
        }
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (gameTime.TotalGameTime.Seconds > 5 || _skipScreen)
            {
                ScreenManager.RemoveScreen(this);
                ScreenManager.AddScreen(new BackgroundScreen(), null);
                ScreenManager.AddScreen(new MainMenuScreen(), null);
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            GraphicsDevice graphics = ScreenManager.GraphicsDevice;
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            graphics.Clear(Color.Black);

            int startY = 125;
            // Center the text in the viewport.
            Vector2 textPosition = new Vector2(font.GetWidth(WarningLine1) / 10, startY);

            Color color = Color.White;

            // Draw the text.
            spriteBatch.Begin();
            spriteBatch.DrawString(font, WarningLine1, textPosition, color, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);

            startY += 25;
            textPosition = new Vector2(font.GetWidth(WarningLine2) / 12, startY);
            spriteBatch.DrawString(font, WarningLine2, textPosition, color, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);

            startY += 25;
            textPosition = new Vector2(font.GetWidth(WarningLine3) / 12, startY);
            spriteBatch.DrawString(font, WarningLine3, textPosition, color, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);

            startY += 25;
            textPosition = new Vector2(font.GetWidth(WarningLine4) / 10, startY);
            spriteBatch.DrawString(font, WarningLine4, textPosition, color, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);

            startY += 50;
            textPosition = new Vector2(font.GetWidth(WarningLine5) / 12, startY);
            spriteBatch.DrawString(font, WarningLine5, textPosition, color, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);

            startY += 25;
            textPosition = new Vector2(font.GetWidth(WarningLine6) / 9, startY);
            spriteBatch.DrawString(font, WarningLine6, textPosition, color, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);
            spriteBatch.End();

        }
    }
}
