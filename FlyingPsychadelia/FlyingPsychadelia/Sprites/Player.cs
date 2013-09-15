using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace FlyingPsychadelia.Sprites
{
    public class Player : MovableSprite
    {
        public List<Charm> Charms = new List<Charm>();
        private readonly IController _controller;
        private Direction _Direction = Direction.Right;
        private readonly SoundEffect[] _jump;
        private readonly SoundEffect[] _shootSound;
        public bool IsLanded;
        private readonly Random _random;
        private int _health;
        private double _timePlayerSetToInvincible;
        private double _timeInSecondsPlayDied;
        public bool PlayDeathAnimation { get; set; }
        public float _timeUntilCanShoot;

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

        private void ShootSound()
        {
            _shootSound[_random.Next() % 3].Play();
        }

        public void DetectMovement(GameTime gameTime)
        {
            float moveMagnitude = 0.02f * gameTime.ElapsedGameTime.Milliseconds;
            float moveMagnitudeX = moveMagnitude;
            if (!IsLanded) moveMagnitudeX *= 0.7f; // dampen in air

            if (_controller.DetectRight())
            {
                AddVeocity(new Vector2(moveMagnitudeX, 0));
                _Direction = Direction.Right;
            }
            if (_controller.DetectLeft())
            {
                AddVeocity(new Vector2(-moveMagnitudeX, 0));
                _Direction = Direction.Left;
            }
            if (_controller.DetectDown())
            {
                AddVeocity(new Vector2(0, moveMagnitude));
            }
           
            if (_controller.DetectFire())
            {
                // Spawn new Charm
                var speed = 0.7f * gameTime.ElapsedGameTime.Milliseconds;
                var DirectionVector = _Direction == Direction.Left ? new Vector2(-speed, 0) : new Vector2(speed, 0);
                var SpawnX = _Direction == Direction.Left ? Bounds.X - 1 : Bounds.Right + 1;
                var SpawnY = Bounds.Y + (Bounds.Height / 2);
                if (Charms.Count < 5 && _timeUntilCanShoot <= 0 )
                {
                    Charms.Add(new Charm(_Content, SpawnX, SpawnY, DirectionVector));
                    ShootSound();
                    _timeUntilCanShoot = 180;
                }
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

            _shootSound = new SoundEffect[3];
            _shootSound[0] = content.Load<SoundEffect>("Gold1");
            _shootSound[1] = content.Load<SoundEffect>("Gold2");
            _shootSound[2] = content.Load<SoundEffect>("Gold3");

            _controller = controller;
            SetTexture("leprechaunanimation.png", 32, 32);
            IsLanded = true;
            _random = new Random();
            PlayerState = PlayerStates.Alive;
            Health = MaxHealth;
            Score = 0;
            _timeUntilCanShoot = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (PlayerState == PlayerStates.Invincible && _timePlayerSetToInvincible < gameTime.TotalGameTime.Seconds)
            {
                PlayerState = PlayerStates.Alive;
            }
            if (PlayerState == PlayerStates.Dead && _timeInSecondsPlayDied < gameTime.TotalGameTime.Seconds)
            {
                PlayDeathAnimation = false;
            }
            Charms.ForEach(c => c.Update(gameTime));

            _timeUntilCanShoot -= gameTime.ElapsedGameTime.Milliseconds;
            if (_timeUntilCanShoot < 0) _timeUntilCanShoot = 0;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (Velocity.X > 0)
            {
                _Reversed = false;
            }
            if (Velocity.X < 0)
            {
                _Reversed = true;
            }

            Charms.ForEach(c => c.Draw(spriteBatch));
        }
        public bool MovingUp()
        {
            return Velocity.Y < 0;
        }
        public bool MovingRight()
        {
            return Velocity.X > 0;
        }

        public void ResetPlayer()
        {
            SetLocation(10, 10);
            PlayerState = PlayerStates.Alive;
            Health = MaxHealth;
            PlayDeathAnimation = false;
        }

        public void PlayHasBeenKilled(GameTime gameTime)
        {
            Health = 0;
            PlayerState = PlayerStates.Dead;
            PlayDeathAnimation = true;
            _timeInSecondsPlayDied = gameTime.TotalGameTime.TotalSeconds + .2;
        }

        public void PlayHasBeenHurt(GameTime gameTime)
        {
            if (PlayerState == PlayerStates.Alive)
            {
                Health--;
                if (Health > 0)
                {
                    PlayerState = PlayerStates.Invincible;
                    _timePlayerSetToInvincible = gameTime.TotalGameTime.TotalSeconds + 1;
                }
            }
        }
    }
}
