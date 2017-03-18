using osu.Game.Beatmaps;
using osu.Game.Modes.Vitaru.Objects;
using osu.Game.Modes.Objects;
using System;
using System.Collections.Generic;

namespace osu.Game.Modes.Vitaru
{
    class VitaruDifficultyCalculator : DifficultyCalculator<VitaruHitObject>
    {
        protected override PlayMode PlayMode => PlayMode.Mania;

        public VitaruDifficultyCalculator(Beatmap beatmap) : base(beatmap) { }

        protected override HitObjectConverter<VitaruHitObject> Converter => new VitaruConverter();

        protected override double CalculateInternal(Dictionary<string, string> categoryDifficulty)
        {
            return 0;
        }
    }
}
