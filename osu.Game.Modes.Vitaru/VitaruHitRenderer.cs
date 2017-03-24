// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using osu.Game.Modes.Objects;
using osu.Game.Modes.UI;

namespace osu.Game.Modes.Vitaru
{
    internal class VitaruHitRenderer : HitRenderer
    {
        public List<HitObject> Enemy { get; set; }

        protected override bool AllObjectsJudged
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override ScoreProcessor CreateScoreProcessor()
        {
            throw new NotImplementedException();
        }
    }
}