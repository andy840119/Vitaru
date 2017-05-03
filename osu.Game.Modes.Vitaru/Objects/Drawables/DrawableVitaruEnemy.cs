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

        private int difficultypattern = 1; // It will be depending on OD in future
        private float anglecircle = 1f; // Angle of circles currently
        private float direction = 0; // For more bullet hell ! could be remplaced by map seed
        private int bulletPattern = 1;
        private float shootLeniancy = 10f;
        private bool hasShot = false;
        
        protected override void Update()
        {
            bulletPattern = RNG.Next(1, 5);
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
            direction = RNG.Next(-50, 51);
            direction = direction / 100;
            switch (bulletPattern)
            {
                case 1:
                    bulletAddRad(0.2f, direction);
                    bulletAddRad(0.2f, 0.1f + direction);
                    bulletAddRad(0.2f, -0.1f + direction);
                    break;

                case 2:
                    bulletAddRad(0.15f, direction);
                    bulletAddRad(0.175f, direction);
                    bulletAddRad(0.2f, direction);
                    bulletAddRad(0.225f, direction);
                    bulletAddRad(0.25f, direction);
                    break;

                case 3:
                    bulletAddRad(0.15f, 0f + direction);
                    bulletAddRad(0.16f, 0.075f + direction);
                    bulletAddRad(0.16f, -0.075f + direction);
                    bulletAddRad(0.17f, 0.15f + direction);
                    bulletAddRad(0.17f, -0.15f + direction);
                    bulletAddRad(0.18f, 0.225f + direction);
                    bulletAddRad(0.18f, -0.225f + direction);
                    break;

                case 4:
                    difficultypattern = RNG.Next(0, 2);
                    switch (difficultypattern)
                    {
                        case 0:
                            anglecircle = 0;
                            break;
                        case 1:
                            anglecircle = 45;
                            break;
                    }

                    bulletAddDeg(0.2f, 0);
                    bulletAddDeg(0.2f, 90);
                    bulletAddDeg(0.2f, 180);
                    bulletAddDeg(0.2f, 270);
                    bulletAddDeg(0.2f, anglecircle);
                    bulletAddDeg(0.2f, 90 + anglecircle);
                    bulletAddDeg(0.2f, 180 + anglecircle);
                    bulletAddDeg(0.2f, 270 + anglecircle);
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