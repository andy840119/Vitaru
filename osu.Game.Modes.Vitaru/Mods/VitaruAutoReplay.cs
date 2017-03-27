// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

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