using osu.Game.Beatmaps;
using osu.Game.Rulesets.Vitaru.Objects;
using osu.Game.Rulesets.Vitaru.Beatmaps;
using System.Collections.Generic;
using osu.Game.Rulesets.Beatmaps;

namespace osu.Game.Rulesets.Vitaru
{
    class VitaruDifficultyCalculator : DifficultyCalculator<VitaruHitObject>
    {
        public VitaruDifficultyCalculator(Beatmap beatmap) : base(beatmap)
        {
        }
        
        protected override BeatmapConverter<VitaruHitObject> CreateBeatmapConverter() => new VitaruBeatmapConverter();

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
