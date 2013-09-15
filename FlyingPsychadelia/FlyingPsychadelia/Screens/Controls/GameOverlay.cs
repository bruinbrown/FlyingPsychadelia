using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlyingPsychadelia.Sprites;
using FlyingPsychadelia.StateManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FlyingPsychadelia.Screens.Controls
{
    public class GameOverlay
    {
        private Texture2D _fullHeart;
        private Texture2D _emptyHeart;
        private Player _player;

        public GameOverlay()
        {
            
        }

        public void LoadContent(ContentManager contentManager)
        {
            _fullHeart = contentManager.Load<Texture2D>("FullHeart.png");
            _emptyHeart = contentManager.Load<Texture2D>("EmptyHeart.png");
        }

        public void SetPlayer(Player player)
        {
            _player = player;
        }

        public void Draw(SpriteBatch spriteBatch, ScreenManager screenManager)
        {
            SpriteFont font = screenManager.Font;
     
            // Draw Score
            spriteBatch.DrawString(font, "Score:", new Vector2(0,0), Color.White, 0F, Vector2.Zero, .5F, SpriteEffects.None, 0F);


            int offsetX = 30;
            for (int i = Player.MaxHealth; i >= 1; i--)
            {
                if (_player.Health < i)
                {
                    spriteBatch.Draw(_emptyHeart, new Vector2(screenManager.GraphicsDevice.Viewport.Width - offsetX, 5), Color.White);
                }
                else
                {
                    spriteBatch.Draw(_fullHeart, new Vector2(screenManager.GraphicsDevice.Viewport.Width - offsetX, 5), Color.White);
                }
                offsetX += 30;
            }    
        } 
    }
}
