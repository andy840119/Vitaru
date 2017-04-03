// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Modes.Vitaru.Judgements;
using osu.Game.Modes.Vitaru.Objects;
using osu.Game.Modes.UI;
using System;

namespace osu.Game.Modes.Vitaru
{
    internal class VitaruScoreProcessor : ScoreProcessor<VitaruHitObject, VitaruJudgementInfo>
    {
        public VitaruScoreProcessor()
        {
        }

        public VitaruScoreProcessor(HitRenderer<VitaruHitObject, VitaruJudgementInfo> hitRenderer)
            : base(hitRenderer)
        {
        }

        protected override void Reset()
        {
            base.Reset();

            Health.Value = 1;
            Accuracy.Value = 1;
        }

        protected override void UpdateCalculations(VitaruJudgementInfo judgement)
        {
            int score = 0;

            foreach (var judgementInfo in Judgements)
            {
                var j = judgementInfo;
                score += j.ScoreValue;
            }
            TotalScore.Value = score;
        }
    }
}