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
        protected ContentManager _Content;
        
        protected Animation _Animation;

        protected bool _Reversed;

        public enum PlayerStates
        {
            Alive,
            Dead,
            Invincible
        }

        public PlayerStates PlayerState;

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
        private double _nextBlinkTime;
        private bool _modelVisibility;

        public MovableSprite(ContentManager content)
        {
            _Content = content;
        }
        protected void SetTexture(string TextureName, int FrameWidth, int FrameHeight)
        {
            _Animation = new Animation(_Content.Load<Texture2D>(TextureName), FrameWidth, FrameHeight, "leprechaunanimation.png");
            //_Texture = _Content.Load<Texture2D>(TextureName);
            SetLocation(10, 10);
        }
        public void SetLocation(int X, int Y)
        {
            _bounds = new Rectangle(X, Y, _Animation.FrameWidth, _Animation.FrameHeight);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_modelVisibility)
            {
                _Animation.Draw(spriteBatch, GetBoundsAdjustedForCamera(Bounds), _Reversed);
                //spriteBatch.Draw(_Texture, GetBoundsAdjustedForCamera(Bounds), Color.White);
            }
        }
        protected Rectangle GetBoundsAdjustedForCamera(Rectangle bounds)
        {
            Rectangle ViewRect = Camera.Instance.CameraView;
            var Temp = bounds;
            Temp.Offset(-ViewRect.X, -ViewRect.Y);
            return Temp;
        }
        public virtual void Update(GameTime gameTime)
        {
            // Apply current velocity to rectangle X and Y
            _Animation.Update(gameTime);
            _bounds.Offset((int)Math.Floor(Velocity.X), (int)Math.Floor(Velocity.Y));

            if (PlayerState == PlayerStates.Invincible)
            {
                if (gameTime.TotalGameTime.TotalMilliseconds >= _nextBlinkTime)
                {
                    _modelVisibility = !_modelVisibility;

                    _nextBlinkTime = gameTime.TotalGameTime.TotalMilliseconds + 50;
                }
            }
            if (PlayerState != PlayerStates.Invincible)
            {
                _modelVisibility = true;
            }
        }
        public Vector2 LocationAsVector()
        {
                
            return new Vector2(Bounds.X,Bounds.Y);
        }
        public void AddVeocity(Vector2 vector2)
        {
            Velocity = new Vector2(Velocity.X + vector2.X, Velocity.Y + vector2.Y);
        }
    }
}
