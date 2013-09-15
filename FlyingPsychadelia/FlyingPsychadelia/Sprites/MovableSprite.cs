using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using FlyingPsychadelia.Sprites;

namespace FlyingPsychadelia
{
    public class MovableSprite : ICollidable
    {
        protected Rectangle _bounds;
        private ContentManager _Content;
        //protected Texture2D _Texture;

        protected Animation _Animation;

        protected bool _Reversed;

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
            _Animation = new Animation(_Content.Load<Texture2D>(TextureName), 32, 32, "leprechaunanimation.png");
            //_Texture = _Content.Load<Texture2D>(TextureName);
            SetLocation(10,10);
        }
        public void SetLocation(int X, int Y)
        {
            _bounds = new Rectangle(X, Y, _Animation.FrameWidth, _Animation.FrameHeight);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            _Animation.Draw(spriteBatch, GetBoundsAdjustedForCamera(Bounds), _Reversed);
            //spriteBatch.Draw(_Texture, GetBoundsAdjustedForCamera(Bounds), Color.White);
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
            _Animation.Update(gametime);
            _bounds.Offset((int)Math.Floor(Velocity.X), (int)Math.Floor(Velocity.Y));
        }
        public Vector2 LocationAsVector()
        {
            return new Vector2(Bounds.X,Bounds.Y);
        }

    }
}
