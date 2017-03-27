using System.Collections.Generic;
using osu.Game.Beatmaps;
using osu.Game.Modes.Vitaru.Objects;

namespace osu.Game.Modes.Vitaru.Beatmaps
{
    internal class VitaruBeatmapConverter : IBeatmapConverter<VitaruHitObject>
    {
        public Beatmap<VitaruHitObject> Convert(Beatmap original)
        {
            return new Beatmap<VitaruHitObject>(original)
            {
                HitObjects = new List<VitaruHitObject>() //TODO: Do this for real this time
            };
        }
    }
}
