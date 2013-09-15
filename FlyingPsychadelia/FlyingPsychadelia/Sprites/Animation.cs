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

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string NextAnimation
        {
            get { return _nextAnimation; }
            set { _nextAnimation = value; }
        }

        public bool LoopAnimation
        {
            get { return _loopAnimation; }
            set { _loopAnimation = value; }
        }
        public bool FinishedPlaying
        {
            get { return _finishedPlaying; }
            set { _finishedPlaying = value; }
        }

        public int FrameCount
        {
            get { return FramesPerRow*FramesPerCol; }
        }

        public float FrameLength 
        {
            get { return _frameDelay; }
            set { _frameDelay = value; }
        }

        public Rectangle FrameRectangle
        {
            get
            {
               return new Rectangle(
                   FrameX,FrameY,FrameWidth,FrameHeight
                   ); 
            }
        }

        private int FrameY
        {
            get
            {
                return (_currentFrame/FramesPerRow) * _frameHeight;
            }
        }

        private int FrameX
        {
            get
            {
                return (_currentFrame % 3) * _frameWidth;
            }
        }

        private int FramesPerRow
        {
            get { return _texture.Width/_frameWidth; }
        }

        private int FramesPerCol {
            get { return _texture.Height/_frameHeight; }
        }

        public Animation(Texture2D texture, int frameWidth, int frameHeight, string name)
        {
            _texture = texture;
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
            _name = name;
        }

        public void Play()
        {
            _currentFrame = 0;
            _finishedPlaying = false;
        }

        public void Update(GameTime gameTime)
        {
            var elapsed = (float) gameTime.ElapsedGameTime.TotalSeconds;
            _frameTimer += elapsed;

            if (!(_frameTimer >= _frameDelay)) return;
            
            _currentFrame++;
            if (_currentFrame >= FrameCount)
            {
                if (_loopAnimation)
                {
                    _currentFrame = 0;
                }
                else
                {
                    _currentFrame = FrameCount - 1;
                    _finishedPlaying = true;
                }
            }
            _frameTimer = 0f;
        }
    }
}