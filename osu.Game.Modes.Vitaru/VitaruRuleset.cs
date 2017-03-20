// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Game.Modes.Objects;
using osu.Game.Modes.UI;
using osu.Game.Modes.Vitaru.Objects;
using System;
using osu.Game.Modes.Vitaru.UI;
using osu.Game.Graphics;
using osu.Game.Beatmaps;
using osu.Game.Screens.Play;
using osu.Game.Modes.Mods;
using osu.Game.Modes.Vitaru.Mods;

namespace osu.Game.Modes.Vitaru
{
    public class VitaruRuleset : Ruleset
    {

        public override string Description => "osu!vitaru";

        public override DifficultyCalculator CreateDifficultyCalculator(Beatmap beatmap)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Mod> GetModsFor(ModType type)
        {
            switch (type)
            {
                case ModType.DifficultyReduction:
                    return new Mod[]
                    {
                        new VitaruModEasy(),
                        new VitaruModNoFail(),
                        new VitaruModHalfTime(),
                    };

                case ModType.DifficultyIncrease:
                    return new Mod[]
                    {
                        new VitaruModHardRock(),
                        new VitaruModSuddenDeath(),
                        new MultiMod
                        {
                            Mods = new Mod[]
                            {
                                new VitaruModDoubleTime(),
                                new VitaruModNightcore(),
                            },
                        },
                        new VitaruModHidden(),
                        new VitaruModDoubleTrouble(),
                    };

                case ModType.Special:
                    return new Mod[]
                    {
                        new MultiMod
                        {
                            Mods = new Mod[]
                            {
                                new ModAutoplay(),
                                new ModCinema(),
                            },
                        },
                    };

                default:
                    return new Mod[] { };
            }
        }

        public override HitRenderer CreateHitRendererWith(WorkingBeatmap beatmap)
        {
            throw new NotImplementedException();
        }

        public override ScoreProcessor CreateScoreProcessor()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<KeyCounter> CreateGameplayKeys()
        {
            throw new NotImplementedException();
        }

        public override FontAwesome Icon => FontAwesome.fa_osu_vitaru_o;

        protected override PlayMode PlayMode => PlayMode.Vitaru;
    }
}
