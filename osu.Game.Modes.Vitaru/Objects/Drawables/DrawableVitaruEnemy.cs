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
        float playerPos;

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

        private int bulletPattern = 1;
        private float shootLeniancy = 10f;
        private bool hasShot = false;
        protected override void Update()
        {
            bulletPattern = RNG.Next(1, 4);
            if (HitObject.StartTime < (Time.Current + (shootLeniancy * 2)) && HitObject.StartTime > (Time.Current - (shootLeniancy / 4)) && hasShot == false)
            {
                enemyShoot();
                FadeOut(Math.Min(TIME_FADEOUT * 2, TIME_PREEMPT));
                hasShot = true;
            }
            playerPos = (float)Math.Atan2((DrawableVitaruPlayer.PlayerPosition.X - enemy.Position.X), -1 * (DrawableVitaruPlayer.PlayerPosition.Y - enemy.Position.Y));
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

            Alpha = 0.001f;
            Scale = new Vector2(0.25f);
        }

        protected override void UpdatePreemptState()
        {
            base.UpdatePreemptState();

            FadeIn(Math.Min(TIME_FADEIN * 2, TIME_PREEMPT));
            ScaleTo(1f, TIME_PREEMPT);
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

        private void enemyShoot()
        {
            if (bulletPattern == 1)
            {
                Bullet B1;
                Bullet B2;
                Bullet B3;
                MainParent.Add(B1 = new Bullet(1)
                {
                    Origin = Anchor.Centre,
                    Depth = 1,
                    BulletColor = Color4.Cyan,
                    BulletAngleRadian = playerPos,
                    BulletSpeed = 0.2f,
                });
                MainParent.Add(B2 = new Bullet(1)
                {
                    Origin = Anchor.Centre,
                    Depth = 1,
                    BulletColor = Color4.Cyan,
                    BulletAngleRadian = playerPos - 0.1f,
                    BulletSpeed = 0.2f,
                });
                MainParent.Add(B3 = new Bullet(1)
                {
                    Origin = Anchor.Centre,
                    Depth = 1,
                    BulletColor = Color4.Cyan,
                    BulletAngleRadian = playerPos + 0.1f,
                    BulletSpeed = 0.2f,
                });
                B1.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), B1));
                B2.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), B2));
                B3.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), B3));
            }

            if (bulletPattern == 2)
            {
                Bullet B1;
                Bullet B2;
                Bullet B3;
                Bullet B4;
                Bullet B5;
                MainParent.Add(B1 = new Bullet(1)
                {
                    Origin = Anchor.Centre,
                    Depth = 1,
                    BulletColor = Color4.Cyan,
                    BulletAngleRadian = playerPos,
                    BulletSpeed = 0.05f,
                });
                MainParent.Add(B2 = new Bullet(1)
                {
                    Origin = Anchor.Centre,
                    Depth = 1,
                    BulletColor = Color4.Cyan,
                    BulletAngleRadian = playerPos,
                    BulletSpeed = 0.1f,
                });
                MainParent.Add(B3 = new Bullet(1)
                {
                    Origin = Anchor.Centre,
                    Depth = 1,
                    BulletColor = Color4.Cyan,
                    BulletAngleRadian = playerPos,
                    BulletSpeed = 0.15f,
                });
                MainParent.Add(B4 = new Bullet(1)
                {
                    Origin = Anchor.Centre,
                    Depth = 1,
                    BulletColor = Color4.Cyan,
                    BulletAngleRadian = playerPos,
                    BulletSpeed = 0.2f,
                });
                MainParent.Add(B5 = new Bullet(1)
                {
                    Origin = Anchor.Centre,
                    Depth = 1,
                    BulletColor = Color4.Cyan,
                    BulletAngleRadian = playerPos,
                    BulletSpeed = 0.25f,
                });
                B1.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), B1));
                B2.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), B2));
                B3.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), B3));
                B4.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), B4));
                B5.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), B5));
            }
            if(bulletPattern == 3)
            {
                Bullet B1;
                Bullet B2;
                Bullet B3;
                Bullet B4;
                Bullet B5;
                MainParent.Add(B1 = new Bullet(1)
                {
                    Origin = Anchor.Centre,
                    Depth = 1,
                    BulletColor = Color4.Cyan,
                    BulletAngleRadian = playerPos,
                    BulletSpeed = 0.15f,
                });
                MainParent.Add(B2 = new Bullet(1)
                {
                    Origin = Anchor.Centre,
                    Depth = 1,
                    BulletColor = Color4.Cyan,
                    BulletAngleRadian = playerPos + 0.1f,
                    BulletSpeed = 0.175f,
                });
                MainParent.Add(B3 = new Bullet(1)
                {
                    Origin = Anchor.Centre,
                    Depth = 1,
                    BulletColor = Color4.Cyan,
                    BulletAngleRadian = playerPos - 0.1f,
                    BulletSpeed = 0.175f,
                });
                MainParent.Add(B4 = new Bullet(1)
                {
                    Origin = Anchor.Centre,
                    Depth = 1,
                    BulletColor = Color4.Cyan,
                    BulletAngleRadian = playerPos + 0.2f,
                    BulletSpeed = 0.17f,
                });
                MainParent.Add(B5 = new Bullet(1)
                {
                    Origin = Anchor.Centre,
                    Depth = 1,
                    BulletColor = Color4.Cyan,
                    BulletAngleRadian = playerPos - 0.2f,
                    BulletSpeed = 0.17f,
                });
                B1.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), B1));
                B2.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), B2));
                B3.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), B3));
                B4.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), B4));
                B5.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), B5));
            }

        }
        public float playerRelativePositionAngle()
        {
            return (float)Math.Atan2((DrawableVitaruPlayer.PlayerPosition.X - Position.X), -1 * (DrawableVitaruPlayer.PlayerPosition.Y - Position.Y));
        }
    }
}
