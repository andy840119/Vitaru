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

namespace osu.Game.Modes.Vitaru.Objects.Drawables
{
    public class DrawableVitaruEnemy : DrawableCharacter
    {
        //private CharacterSprite EnemySprite;
        private readonly Enemy enemy;
        public bool Shoot = false;

        public DrawableVitaruEnemy(Enemy enemy) : base(enemy)
        {
            this.enemy = enemy;
            Origin = Anchor.Centre;
            Position = enemy.Position;
            CharacterType = HitObjectType.Enemy;
            CharacterHealth = 60;
            Team = 1;
            HitboxWidth = 20;
            HitboxColor = Color4.Yellow;
            Alpha = 0;
        }

        protected override void CheckJudgement(bool userTriggered)
        {
            double hitOffset = Math.Abs(Judgement.TimeOffset);

            if (hitOffset < HitObject.HitWindowFor(VitaruScoreResult.Kill20))
            {
                Judgement.Result = HitResult.Hit;
            }
            else
                Judgement.Result = HitResult.Miss;
        }

        protected override void UpdateInitialState()
        {
            base.UpdateInitialState();

            Alpha = 1;
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
                    FadeOut(TIME_FADEOUT);
                    Expire(true);
                    break;
                case ArmedState.Miss:
                    FadeOut(TIME_FADEOUT / 5);
                    Expire();
                    break;
                case ArmedState.Hit:
                    FadeOut(20);
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
