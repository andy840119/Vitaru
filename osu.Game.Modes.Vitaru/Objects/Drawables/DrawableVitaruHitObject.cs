using osu.Game.Modes.Objects.Drawables;
using System;
using System.ComponentModel;
using osu.Game.Modes.Vitaru.Judgements;

namespace osu.Game.Modes.Vitaru.Objects.Drawables
{
    public class DrawableVitaruHitObject : DrawableHitObject<VitaruHitObject, VitaruJudgementInfo>
    {
        public const float TIME_PREEMPT = 600;
        public const float TIME_FADEIN = 400;
        public const float TIME_FADEOUT = 500;

        public DrawableVitaruHitObject(VitaruHitObject hitObject)
            : base(hitObject)
        {

        }

        protected override VitaruJudgementInfo CreateJudgementInfo() => new VitaruJudgementInfo { MaxScore = VitaruScoreResult.Kill1500 };

        protected override void UpdateState(ArmedState state)
        {
            if (!IsLoaded) return;

            Flush();

            UpdateInitialState();

            Delay(HitObject.StartTime - Time.Current - TIME_PREEMPT + Judgement.TimeOffset, true);

            UpdatePreemptState();

            Delay(TIME_PREEMPT, true);
        }

        private void Delay(object p, bool v)
        {
            throw new NotImplementedException();
        }

        protected virtual void UpdatePreemptState()
        {
            FadeIn(TIME_FADEIN);
        }

        protected virtual void UpdateInitialState()
        {
            Alpha = 0;
        }
    }

    public enum ComboResult
    {
        [Description(@"")]
        None,
        [Description(@"Good")]
        Good,
        [Description(@"Amazing")]
        Perfect
    }

    public enum VitaruScoreResult
    {
        [Description(@"Hit")]
        Hit,
        [Description(@"2")]
        Graze2,
        [Description(@"10")]
        Kill10,
        [Description(@"20")]
        Kill20,
        [Description(@"30")]
        Kill30,
        [Description(@"1500")]
        Kill1500,
        [Description(@"")]
        Miss
    }
}
