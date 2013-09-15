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

        private string _warningLine1 = "A very small percentage of people may experience a seizure when";
        private string _warningLine2 = "exposed to certain visual images, including flashing lights or patters";
        private string _warningLine3 = "that may appear in video games. If you or any of your relatives have";
        private string _warningLine4 = "a history of seizures or epilepsy, consult a doctor before playing.";

        private string _warningLine5 = "Immediately stop playing and consult a doctor if you experience any";
        private string _warningLine6 = "symptoms such as lightheadedness, disorientation and twitching.";

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
            Vector2 textPosition = new Vector2(font.GetWidth(_warningLine1) / 10, startY);

            Color color = Color.White;

            // Draw the text.
            spriteBatch.Begin();
            spriteBatch.DrawString(font, _warningLine1, textPosition, color, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);

            startY += 25;
            textPosition = new Vector2(font.GetWidth(_warningLine2) / 12, startY);
            spriteBatch.DrawString(font, _warningLine2, textPosition, color, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);

            startY += 25;
            textPosition = new Vector2(font.GetWidth(_warningLine3) / 12, startY);
            spriteBatch.DrawString(font, _warningLine3, textPosition, color, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);

            startY += 25;
            textPosition = new Vector2(font.GetWidth(_warningLine4) / 10, startY);
            spriteBatch.DrawString(font, _warningLine4, textPosition, color, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);

            startY += 50;
            textPosition = new Vector2(font.GetWidth(_warningLine5) / 12, startY);
            spriteBatch.DrawString(font, _warningLine5, textPosition, color, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);

            startY += 25;
            textPosition = new Vector2(font.GetWidth(_warningLine6) / 9, startY);
            spriteBatch.DrawString(font, _warningLine6, textPosition, color, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);
            spriteBatch.End();

        }
    }
}
