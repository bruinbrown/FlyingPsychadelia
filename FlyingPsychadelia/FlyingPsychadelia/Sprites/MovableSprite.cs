using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace FlyingPsychadelia
{
    public class MovableSprite : ICollidable
    {
        protected Rectangle _bounds;
        private ContentManager _Content;
        protected Texture2D _Texture;
        public Rectangle Bounds
        {
            get
            {
                return _bounds;
            }
            set
            {
                _bounds = value;
            }
        }
        public Vector2 Velocity { get; set; }
        protected static System.Random Random = new System.Random();

        public MovableSprite(ContentManager content)
        {
            _Content = content;
        }
        protected void SetTexture(string TextureName)
        {
            _Texture = _Content.Load<Texture2D>(TextureName);
            SetLocation(10,10);
        }
        public void SetLocation(int X, int Y)
        {
            _bounds = new Rectangle(X, Y, _Texture.Width, _Texture.Height);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_Texture, GetBoundsAdjustedForCamera(Bounds), Color.White);
        }
        protected Rectangle GetBoundsAdjustedForCamera(Rectangle bounds)
        {
            Rectangle ViewRect = Camera.Instance.CameraView;
            var Temp = bounds;
            Temp.Offset(-ViewRect.X, -ViewRect.Y);
            return Temp;
        }
        public virtual void Update(GameTime gametime)
        {
            // Apply current velocity to rectangle X and Y
            _bounds.Offset((int)Math.Floor(Velocity.X), (int)Math.Floor(Velocity.Y));
        }
        public Vector2 LocationAsVector()
        {
            return new Vector2(Bounds.X,Bounds.Y);
        }
        protected void DrawReversed(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_Texture,
                             GetBoundsAdjustedForCamera(Bounds),
                             null,
                             Color.White,
                             0.0f,
                             new Vector2(),
                             SpriteEffects.FlipHorizontally,
                             0);
        }

    }
}
