﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.Vitaru.Judgements;
using osu.Game.Rulesets.Vitaru.Objects;
using osu.Game.Rulesets.Vitaru.Objects.Drawables;
using osu.Game.Rulesets.Vitaru.Objects.Characters;
using osu.Game.Rulesets.Vitaru.Beatmaps;
using osu.Game.Rulesets.Vitaru.UI;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.Beatmaps;
using OpenTK;
using osu.Game.Rulesets.Vitaru.Scoring;

namespace osu.Game.Rulesets.Vitaru
{
    internal class VitaruHitRenderer : HitRenderer<VitaruHitObject, VitaruJudgement>
    {
        public VitaruHitRenderer(WorkingBeatmap beatmap)
            : base(beatmap)
        {
        }

        public override ScoreProcessor CreateScoreProcessor() => new VitaruScoreProcessor(this);

        protected override BeatmapConverter<VitaruHitObject> CreateBeatmapConverter() => new VitaruBeatmapConverter();

        protected override BeatmapProcessor<VitaruHitObject> CreateBeatmapProcessor() => new VitaruBeatmapProcessor();

        protected override Playfield<VitaruHitObject, VitaruJudgement> CreatePlayfield() => new VitaruPlayfield();

        protected override DrawableHitObject<VitaruHitObject, VitaruJudgement> GetVisualRepresentation(VitaruHitObject h)
        {
            var player = h as VitaruPlayer;
            if (player != null)
                return new DrawableVitaruPlayer(player);
            
            var enemy = h as Enemy;
            if (enemy != null)
                return new DrawableVitaruEnemy(enemy);

            var boss = h as Boss;
            if (boss != null)
                return new DrawableVitaruBoss(boss);
            return null;
        }

        protected override Vector2 GetPlayfieldAspectAdjust() => new Vector2(0.75f);
    }
}
