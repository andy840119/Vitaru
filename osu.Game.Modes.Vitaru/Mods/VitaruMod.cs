// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Beatmaps;
using osu.Game.Graphics;
using osu.Game.Modes.Mods;
using osu.Game.Modes.Osu.Objects;
using System;
using System.Linq;

namespace osu.Game.Modes.Vitaru.Mods
{
    public class VitaruModNoFail : ModNoFail
    {

    }

    public class VitaruModEasy : ModEasy
    {

    }

    public class VitaruModHidden : ModHidden
    {
        public override string Description => @"Play with bullets dissapearing once they are close to you.";
        public override double ScoreMultiplier => 1.18;
    }

    public class VitaruModHardRock : ModHardRock
    {
        public override double ScoreMultiplier => 1.08;
        public override bool Ranked => true;
    }

    public class VitaruModSuddenDeath : ModSuddenDeath
    {
        public override bool Ranked => true;
    }

    public class VitaruModDoubleTime : ModDoubleTime
    {
        public override double ScoreMultiplier => 1.36;
    }

    public class VitaruModHalfTime : ModHalfTime
    {
        public override double ScoreMultiplier => 0.5;
    }

    public class VitaruModNightcore : ModNightcore
    {
        public override double ScoreMultiplier => 1.36;
    }

    public class VitaruModFlashlight : ModFlashlight
    {
        public override string Description => @"Good Luck.";
        public override double ScoreMultiplier => 1.18;
    }

    public class VitaruModDoubleTrouble : ModDoubleTrouble
    {
        public override double ScoreMultiplier => 1.18;
    }

    public class VitaruModMirror : ModMirror
    {
        public override double ScoreMultiplier => 1.18;
    }

    public class VitaruModCoop : ModCoop
    {
        public override double ScoreMultiplier => 0.5;
        public override bool Ranked => true;
    }

    public class VitaruMod1v1 : Mod1v1
    {
        public override double ScoreMultiplier => 0.5;
        public override bool Ranked => false;
    }

    public class VitaruRelax : ModRelax
    {
        public override bool Ranked => false;
    }

    public class VitaruModAutoplay : ModAutoplay<OsuHitObject>
    {
        protected override Score CreateReplayScore(Beatmap<OsuHitObject> beatmap) => new Score
        {
            Replay = new VitaruAutoReplay(beatmap)
        };
    }

    public class OsuModTarget : Mod
    {
        public override string Name => "Target";
        public override FontAwesome Icon => FontAwesome.fa_osu_mod_target;
        public override string Description => @"";
        public override double ScoreMultiplier => 1;
    }
}
