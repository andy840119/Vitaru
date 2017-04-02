// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Game.Modes.UI;
using System;
using osu.Game.Graphics;
using osu.Game.Beatmaps;
using osu.Game.Screens.Play;
using osu.Game.Modes.Mods;
using osu.Game.Modes.Vitaru.Mods;
using OpenTK.Input;

namespace osu.Game.Modes.Vitaru
{
    public class VitaruRuleset : Ruleset
    {

        public override string Description => "osu!vitaru";

        public override DifficultyCalculator CreateDifficultyCalculator(Beatmap beatmap) => new VitaruDifficultyCalculator(beatmap);

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
                        new MultiMod
                        {
                            Mods = new Mod[]
                            {
                                new VitaruModMirror(),
                                new VitaruModHardRock(),
                            },
                        },
                        new VitaruModSuddenDeath(),
                        new MultiMod
                        {
                            Mods = new Mod[]
                            {
                                new VitaruModDoubleTime(),
                                new VitaruModNightcore(),
                            },
                        },
                        new MultiMod
                        {
                            Mods = new Mod[]
                            {
                                new VitaruModHidden(),
                                new VitaruModFlashlight(),
                            },
                        },
                        new VitaruModDoubleTrouble(),
                    };

                case ModType.Special:
                    return new Mod[]
                    {
                        new MultiMod
                        {
                            Mods = new Mod[]
                            {
                                new VitaruModCoop(),
                                new VitaruMod1V1(),
                            }
                        },
                        new VitaruRelax(),
                        new MultiMod
                        {
                            Mods = new Mod[]
                            {
                                new ModAutoplay(),
                                new ModCinema(),
                            },
                        },
                    };
                default : return new Mod[] { };
            }
        }

        public override HitRenderer CreateHitRendererWith(WorkingBeatmap beatmap) => new VitaruHitRenderer(beatmap);

        public override ScoreProcessor CreateScoreProcessor() => new VitaruScoreProcessor();

        public override IEnumerable<KeyCounter> CreateGameplayKeys() => new KeyCounter[]
        {
            new KeyCounterKeyboard(Key.Up),
            new KeyCounterKeyboard(Key.Right),
            new KeyCounterKeyboard(Key.Left),
            new KeyCounterKeyboard(Key.Down),
            new KeyCounterKeyboard(Key.X),
            new KeyCounterKeyboard(Key.Z),
            new KeyCounterKeyboard(Key.LShift),
        };

        public override FontAwesome Icon => FontAwesome.fa_osu_vitaru_o;

        protected override PlayMode PlayMode => PlayMode.Vitaru;
    }
}
