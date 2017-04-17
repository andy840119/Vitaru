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

namespace osu.Game.Modes.Vitaru.Objects.Drawables
{
    public class DrawableVitaruEnemy : DrawableCharacter
    {
        private readonly Enemy enemy;
        public bool Shoot = false;

        public DrawableVitaruEnemy(Enemy enemy) : base(enemy)
        {
            Origin = Anchor.Centre;
            Position = enemy.Position;
            CharacterType = HitObjectType.Enemy;
            CharacterHealth = 60;
            Team = 1;
            HitboxWidth = 20;
            HitboxColor = Color4.Yellow;
        }

        protected override void Update()
        {
            base.Update();
            if (Shoot == true)
            {
                Shooting = true;
                OnShoot = enemyShoot;
            }
            float ySpeed = 0.5f * (float)Clock.ElapsedFrameTime;
            float xSpeed = 0.5f * (float)Clock.ElapsedFrameTime;
        }
        
        protected override void CheckJudgement(bool userTriggered)
        {
            if (!userTriggered)
            {
                if (Judgement.TimeOffset > enemy.HitWindowKill10)
                    Judgement.Result = HitResult.Miss;
                return;
            }

            double hitOffset = Math.Abs(Judgement.TimeOffset);

            if (hitOffset > enemy.HitWindowMiss)
                return;

            else if (hitOffset < enemy.HitWindowKill10)
            {
                Judgement.Result = HitResult.Hit;
                Judgement.Score = hitOffset < enemy.HitWindowKill30 ? VitaruScoreResult.Kill30 : VitaruScoreResult.Kill10;
            }
            else
                Judgement.Result = HitResult.Miss;
        }

        protected override void UpdateState(ArmedState state)
        {
            Delay(HitObject.StartTime - Time.Current + Judgement.TimeOffset, true);

            switch (State)
            {
                case ArmedState.Idle:
                    Delay(enemy.HitWindowMiss);
                    break;
                case ArmedState.Miss:
                    FadeOut(100);
                    break;
                case ArmedState.Hit:
                    
                    FadeOut(600);
                    break;
            }

            Expire();
        }

        private void enemyShoot()
        {
            ConcaveWave Wave;
            MainParent.Add(Wave = new ConcaveWave()
            {
                Origin = Anchor.Centre,
                Depth = 1,
            });
            Wave.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), Wave));
        }
        public float playerRelativePositionAngle()
        {
            return (float)Math.Atan2((DrawableVitaruPlayer.PlayerPosition.X - Position.X), -1 * (DrawableVitaruPlayer.PlayerPosition.Y - Position.Y));
        }
    }
}
