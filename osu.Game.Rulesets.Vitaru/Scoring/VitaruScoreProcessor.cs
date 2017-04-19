// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Modes.Vitaru.Judgements;
using osu.Game.Modes.Vitaru.Objects;
using osu.Game.Modes.UI;
using System;
using osu.Game.Modes.Scoring;
using osu.Game.Modes.Objects.Drawables;

namespace osu.Game.Modes.Vitaru
{
    internal class VitaruScoreProcessor : ScoreProcessor<VitaruHitObject, VitaruJudgement>
    {
        public VitaruScoreProcessor()
        {
        }

        public VitaruScoreProcessor(HitRenderer<VitaruHitObject, VitaruJudgement> hitRenderer)
            : base(hitRenderer)
        {
        }

        protected override void Reset()
        {
            base.Reset();

            Health.Value = 1;
            Accuracy.Value = 1;
        }

        protected override void OnNewJudgement(VitaruJudgement judgement)
        {
            if (judgement != null)
            {
                switch (judgement.Result)
                {
                    //In this case, "Hit" does not mean you were hit, it means you hit or killed an enemy / boss
                    case HitResult.Hit:
                        Combo.Value++;
                        Health.Value += 0.1f;
                        break;

                        //In this case, "Miss" does not mean you missed a shot or avoided one coming from an enemy,
                        //it means you took damage or an enemy got away
                    case HitResult.Miss:
                        Combo.Value = 0;
                        Health.Value -= 0.2f;
                        break;
                }
            }

            int score = 0;
            int maxScore = 0;

            foreach (var j in Judgements)
            {
                score += j.ScoreValue;
                maxScore += j.MaxScoreValue;
            }

            TotalScore.Value = score;
            Accuracy.Value = (double)score / maxScore;
        }
    }
}
