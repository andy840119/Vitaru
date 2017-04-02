using osu.Game.Beatmaps;
using osu.Game.Modes.Vitaru.Objects;
using osu.Game.Modes.Vitaru.Beatmaps;
using osu.Game.Modes.Objects;
using System;
using System.Collections.Generic;

namespace osu.Game.Modes.Vitaru
{
    class VitaruDifficultyCalculator : DifficultyCalculator<VitaruHitObject>
    {
        public VitaruDifficultyCalculator(Beatmap beatmap) : base(beatmap)
        {
        }
        
        protected override IBeatmapConverter<VitaruHitObject> CreateBeatmapConverter() => new VitaruBeatmapConverter();

        protected override double CalculateInternal(Dictionary<string, string> categoryDifficulty)
        {
            return 69;
        }
        public enum DifficultyType
        {
            Speed = 0,
            Aim,
        };
    }
}
