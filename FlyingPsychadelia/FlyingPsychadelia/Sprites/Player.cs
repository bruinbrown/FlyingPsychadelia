using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace FlyingPsychadelia.Sprites
{
    public class Player : MovableSprite, ICollidable
    {
        public enum PlayerStates
        {
            Alive,
            Dead,
            Invincible
        }

        public PlayerStates PlayerState;
        private readonly IController _controller;
        private readonly SoundEffect[] _jump;
        public bool IsLanded;
        private readonly Random _random;
        private int _health;
        public int Health
        {
            get { return _health; }
            set
            {
                _health = value;
                if (Health <= 0)
                {
                    PlayerState = PlayerStates.Dead;
                }
            }
        }

        public int Score { get; set; }

        public const int MaxHealth = 3;

        private void JumpSound()
        {
            _jump[_random.Next() % 3].Play();
        }

        public void DetectMovement(GameTime gameTime)
        {
            float moveMagnitude = 0.02f * gameTime.ElapsedGameTime.Milliseconds;
            float moveMagnitudeX = moveMagnitude;
            if (!IsLanded) moveMagnitudeX *= 0.7f; // dampen in air

            if (_controller.DetectRight())
            {
                AddVeocity(new Vector2(moveMagnitudeX, 0));
            }
            else if (_controller.DetectLeft())
            {
                AddVeocity(new Vector2(-moveMagnitudeX, 0));
            }
            else if (_controller.DetectDown())
            {
                AddVeocity(new Vector2(0, moveMagnitude));
            }
            else if (_controller.DetectFire())
            {

            }

            if (_controller.DetectJump())
            {

                if (IsLanded)
                {
                    AddVeocity(new Vector2(0, -moveMagnitude * 25f));
                    IsLanded = false;
                    JumpSound();
                }
            }
            //
            if (Math.Abs(Velocity.X) < moveMagnitude * 0.2f)
                AddVeocity(new Vector2(-Velocity.X, 0));
        }
        public Player(ContentManager content, IController controller)
            : base(content)
        {
            _jump = new SoundEffect[3];
            _jump[0] = content.Load<SoundEffect>("Jump1");
            _jump[1] = content.Load<SoundEffect>("Jump2");
            _jump[2] = content.Load<SoundEffect>("Jump3");

            _controller = controller;
            SetTexture("leprechaun.png");
            IsLanded = true;
            _random = new Random();
            PlayerState = PlayerStates.Alive;
            Health = MaxHealth;
            Score = 0;
        }
        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if (Velocity.X < 0)
                DrawReversed(spriteBatch);
            else
                base.Draw(spriteBatch);
        }
        public void AddVeocity(Vector2 vector2)
        {
            Velocity = new Vector2(Velocity.X + vector2.X, Velocity.Y + vector2.Y);
        }
        public bool MovingUp()
        {
            return Velocity.Y < 0;
        }
        public bool MovingRight()
        {
            return Velocity.X > 0;

        }
    }
}
