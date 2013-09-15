using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlyingPsychadelia.Sprites
{
    public class Animation
    {
        private bool _loopAnimation = true;
        private float _frameDelay = 0.05f;
        private float _frameTimer;
        private string _name;
        private string _nextAnimation;
        private bool _finishedPlaying;
        private int _currentFrame;
        private Texture2D _texture;
        private int _frameHeight;
        private int _frameWidth;
        private int _currentFrameX;

        public int FrameWidth
        {
            get { return _frameWidth; }
            set { _frameWidth = value; }
        }

        public int FrameHeight
        {
            get { return _frameHeight; }
            set { _frameHeight = value; }
        }

        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public int FrameCount
        {
            get { return _frameCount; }
        }

        public Rectangle FrameRectangle
        {
            get
            {
               return new Rectangle(
                   _currentFrameX * FrameWidth,_currentFrameY * FrameHeight,FrameWidth,FrameHeight
                   ); 
            }
        }

        public int ColumnsCount { get; private set; }
        public int RowsCount { get; private set; }

        public Animation(Texture2D texture, int frameWidth, int frameHeight, string name)
        {
            _texture = texture;
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
            _name = name;
            ColumnsCount = _texture.Width/frameWidth;
            RowsCount = _texture.Height/frameHeight;
            _frameCount = ColumnsCount*RowsCount;
            _frameDelay = 0.1f;
        }

        public void Update(GameTime gameTime)
        {
            var elapsed = (float) gameTime.ElapsedGameTime.TotalSeconds;
            _frameTimer += elapsed;

            if (!(_frameTimer >= _frameDelay)) return;
            
            _currentFrame++;
            if (_currentFrame >= FrameCount)
            {
                _currentFrame = 0;
            }
            _currentFrameX = _currentFrame%ColumnsCount;
            if (_currentFrameX == 0)
            {
                _currentFrameY++;
            }
            if (_currentFrameY == RowsCount)
            {
                _currentFrameY = 0;
            }
            _frameTimer = 0f;
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle rectangle, bool _Reversed)
        {
            SpriteEffects fx = SpriteEffects.None;
            if(_Reversed)
                fx = SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(_texture, rectangle, FrameRectangle, Color.White, 0f, Vector2.Zero, fx, 1.0f );
        }

        public int _frameCount { get; set; }

        public int _currentFrameY { get; set; }
    }
}