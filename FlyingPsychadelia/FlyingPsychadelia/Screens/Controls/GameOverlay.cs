using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlyingPsychadelia.Extensions;
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
        private string _score = "Score:";
        private string _time = "Time:";
        private World _world;

        public GameOverlay()
        {
            
        }

        public void LoadContent(ContentManager contentManager)
        {
            _fullHeart = contentManager.Load<Texture2D>("FullHeart.png");
            _emptyHeart = contentManager.Load<Texture2D>("EmptyHeart.png");
        }

        public void SetWorld(World world)
        {
            _world = world;
            _player = world.Players[0];
        }

        public void Draw(SpriteBatch spriteBatch, ScreenManager screenManager)
        {
            SpriteFont font = screenManager.Font;
     
            // Draw Score
            spriteBatch.DrawString(font, _score, new Vector2(5, 0), Color.DarkGreen, 0F, Vector2.Zero, .6F, SpriteEffects.None, 0F);
            spriteBatch.DrawString(font, _player.Score.ToString("D8"), new Vector2(font.GetWidth(_player.Score.ToString()) + 50, 0), Color.DarkGreen, 0F, Vector2.Zero, .6F, SpriteEffects.None, 0F);

            // Draw Time
            spriteBatch.DrawString(font, _time, new Vector2(screenManager.GraphicsDevice.Viewport.Width / 2 - 50, 0), Color.DarkGreen, 0F, Vector2.Zero, .6F, SpriteEffects.None, 0F);
            spriteBatch.DrawString(font, _world.Time.ToString("D3"), new Vector2(screenManager.GraphicsDevice.Viewport.Width / 2, 0), Color.DarkGreen, 0F, Vector2.Zero, .6F, SpriteEffects.None, 0F);


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
