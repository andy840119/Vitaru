// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using System.Collections.Generic;
using osu.Game.Modes.Objects;
using osu.Game.Modes.UI;
using osu.Game.Modes.Vitaru.Judgements;
using osu.Game.Modes.Vitaru.Objects;
using osu.Game.Modes.Vitaru.Objects.Drawables;
using osu.Game.Modes.Vitaru.Objects.Characters;
using osu.Game.Modes.Vitaru.Beatmaps;
using osu.Game.Modes.Vitaru.UI;
using osu.Game.Beatmaps;
using osu.Game.Modes.Objects.Drawables;

namespace osu.Game.Modes.Vitaru
{
    internal class VitaruHitRenderer : HitRenderer<VitaruHitObject, VitaruJudgementInfo>
    {
        public static bool playerLoaded = false;
        public List<HitObject> Enemy { get; set; }
        public VitaruHitRenderer(WorkingBeatmap beatmap)
            : base(beatmap)
        {
        }
        public override ScoreProcessor CreateScoreProcessor() => new VitaruScoreProcessor(this);

        protected override IBeatmapConverter<VitaruHitObject> CreateBeatmapConverter() => new VitaruBeatmapConverter();

        protected override IBeatmapProcessor<VitaruHitObject> CreateBeatmapProcessor() => new VitaruBeatmapProcessor();

        protected override Playfield<VitaruHitObject, VitaruJudgementInfo> CreatePlayfield() => new VitaruPlayfield();

        protected override DrawableHitObject<VitaruHitObject, VitaruJudgementInfo> GetVisualRepresentation(VitaruHitObject h)
        {
            var player = h as VitaruPlayer;
            if (player != null)
                return new DrawableVitaruPlayer(player);

            var enemy = h as Enemy;
            if (enemy != null)
                return new DrawableEnemy(enemy);

            var boss = h as Boss;
            if (boss != null)
                return new DrawableEnemy(boss);

            return null;
        }

        protected override bool AllObjectsJudged
        {
            get
            {
                //Placeholder
                return true;
            }
        }

    }
}