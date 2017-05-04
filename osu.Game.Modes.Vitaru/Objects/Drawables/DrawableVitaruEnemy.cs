using OpenTK.Graphics;
using OpenTK;
using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Modes.Vitaru.Objects.Projectiles;
using osu.Game.Modes.Vitaru.Objects.Drawables;
using osu.Game.Modes.Vitaru.Objects;
using osu.Game.Modes.Vitaru.Objects.Characters;
using osu.Framework.Audio.Sample;
using osu.Game.Modes.Objects.Drawables;
using osu.Game.Modes.Objects.Types;
using osu.Game.Modes.Vitaru.Judgements;
using osu.Framework.MathUtils;
using System.Timers;

namespace osu.Game.Modes.Vitaru.Objects.Drawables
{
    public class DrawableVitaruEnemy : DrawableCharacter
    {
        private readonly Enemy enemy;
        public bool Shoot = false;
        private float playerPos;
        private Color4 enemyColor = Color4.Green;

        public DrawableVitaruEnemy(Enemy enemy) : base(enemy)
        {
            this.enemy = enemy;
            AlwaysPresent = true;
            Origin = Anchor.Centre;
            Position = enemy.Position;
            CharacterType = HitObjectType.Enemy;
            CharacterHealth = 60;
            Team = 1;
            HitboxWidth = 20;
            HitboxColor = Color4.Yellow;
            Alpha = 1;
            Judgement = new VitaruJudgement { Result = HitResult.Hit };
        }

        private int patternDifficulty = 1; // It will be depending on OD in future
        private float circleAngle = 1f; // Angle of circles currently in degree
        private float randomDirection = 0; // For more bullet hell !
        private int bulletPattern = 1;
        private float shootLeniancy = 10f;
        private bool hasShot = false;
        
        protected override void Update()
        {
            bulletPattern = RNG.Next(1, 3); // could be remplaced by map seed, with stackleniency
            if (HitObject.StartTime < (Time.Current + (shootLeniancy * 2)) && HitObject.StartTime > (Time.Current - (shootLeniancy / 4)) && hasShot == false)
            {
                enemyShoot();
                FadeOut(Math.Min(TIME_FADEOUT * 2, TIME_PREEMPT));
                hasShot = true;
            }
            playerRelativePositionAngle();
        }

        protected override void CheckJudgement(bool userTriggered)
        {
            double hitOffset = Math.Abs(Judgement.TimeOffset);

            if (CharacterHealth < 1)
            {
                Judgement.Result = HitResult.Hit;
            }
            else
                Judgement.Result = HitResult.Miss;
        }

        protected override void UpdateInitialState()
        {
            base.UpdateInitialState();

            Alpha = 0f;
            Scale = new Vector2(0.5f);
        }

        protected override void UpdatePreemptState()
        {
            base.UpdatePreemptState();

            FadeIn(Math.Min(TIME_FADEIN * 2, TIME_PREEMPT), EasingTypes.OutQuart);
            ScaleTo(1f, TIME_PREEMPT, EasingTypes.OutQuart);
        }

        protected override void UpdateState(ArmedState state)
        {
            base.UpdateState(state);

            double endTime = (HitObject as IHasEndTime)?.EndTime ?? HitObject.StartTime;
            double duration = endTime - HitObject.StartTime;



            Delay(HitObject.StartTime - Time.Current + Judgement.TimeOffset, true);

            switch (State)
            {
                case ArmedState.Idle:
                    Delay(duration + TIME_PREEMPT);
                    //FadeOut(TIME_FADEOUT);
                    Expire(true);
                    break;
                case ArmedState.Miss:
                    //FadeOut(TIME_FADEOUT / 2);
                    Expire();
                    break;
                case ArmedState.Hit:
                    //FadeOut(TIME_FADEOUT / 4);
                    Expire();
                    break;
            }

            Expire();
        }

        private void bulletAddDeg(float speed, float degree)
        {
            Bullet bullet;
            MainParent.Add(bullet = new Bullet(1)
            {
                Origin = Anchor.Centre,
                Depth = 1,
                BulletColor = Color4.Cyan,
                BulletAngleDegree = playerPos + degree,
                BulletSpeed = speed,
            });
            bullet.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), bullet));
        }
        private void bulletAddRad(float speed, float degree)
        {
            Bullet bullet;
            MainParent.Add(bullet = new Bullet(1)
            {
                Origin = Anchor.Centre,
                Depth = 1,
                BulletColor = Color4.Cyan,
                BulletAngleRadian = playerPos + degree,
                BulletSpeed = speed,
            });
            bullet.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), bullet));
        }

        private void enemyShoot()
        {
            patternDifficulty = RNG.Next(0, 5); // For circle currently
            randomDirection = RNG.Next(-50, 51); // Between -0.05f and 0.05f
            randomDirection = randomDirection / 100; // It seems that add / 100 after the random breaks randomDirection idk why
            float speedModifier;
            float directionModifier;
            switch (bulletPattern)
            {
                case 1: // Wave of three bullets
                    directionModifier = -0.1f;
                    for(int i = 0; i<=2; i++)
                    {
                        bulletAddRad(0.2f, randomDirection + directionModifier);
                        directionModifier += 0.1f;
                    }
                    break;

                case 2: // Cool wave
                    speedModifier = 0;
                    for(int i = 0; i <= 4; i++)
                    {
                        bulletAddRad(0.15f + speedModifier, randomDirection);
                        speedModifier += 0.025f;
                    }
                    break;

                case 3: // Line
                    speedModifier = 0.03f;
                    directionModifier = -0.225f;
                    for(int i = 0; i <= 6; i++)
                    {

                        bulletAddRad(
                            0.15f + Math.Abs(speedModifier),
                            directionModifier + randomDirection
                        );
                        speedModifier -= 0.01f;
                        directionModifier += 0.075f;
                    }
                    break;

                case 4: // Circle
                    directionModifier = (float)(90 / Math.Pow(2, patternDifficulty));
                    circleAngle = 0;
                   for(int j = 1; j <= Math.Pow(2,patternDifficulty + 2); j++)
                    {
                        bulletAddDeg(0.2f, circleAngle);
                        circleAngle += directionModifier;
                    }
                    break;

                case 5: // Spinner
                    circleAngle = 0;
                    int density = 2 * RNG.Next(1, 4);
                    for (int i = 1; i <= (360 / density); i++)
                    {
                        bulletAddDeg(0.2f, circleAngle);
                        // ADD TIMER HERE
                        circleAngle += density;
                    }
                    break;
            }
        }
        public float playerRelativePositionAngle()
        {
            //Returns Something?
            playerPos = (float)Math.Atan2((DrawableVitaruPlayer.PlayerPosition.X - Position.X), -1 * (DrawableVitaruPlayer.PlayerPosition.Y - Position.Y));
            return playerPos;
        }
    }
}