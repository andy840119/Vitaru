using osu.Game.Beatmaps;
using osu.Game.Modes.Osu.Objects;

namespace osu.Game.Modes.Vitaru.Mods
{
    internal class VitaruAutoReplay : Replay
    {
        private Beatmap<OsuHitObject> beatmap;

        public VitaruAutoReplay(Beatmap<OsuHitObject> beatmap)
        {
            this.beatmap = beatmap;
        }
    }
}