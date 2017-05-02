// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Modes.UI;
using osu.Game.Modes.Vitaru.Judgements;
using osu.Game.Modes.Vitaru.Objects;
using osu.Game.Modes.Vitaru.Objects.Drawables;
using osu.Game.Modes.Vitaru.Objects.Characters;
using osu.Game.Modes.Vitaru.Beatmaps;
using osu.Game.Modes.Vitaru.UI;
using osu.Game.Beatmaps;
using osu.Game.Modes.Objects.Drawables;
using osu.Game.Modes.Scoring;

namespace osu.Game.Modes.Vitaru
{
    internal class VitaruHitRenderer : HitRenderer<VitaruHitObject, VitaruJudgement>
    {
        public VitaruHitRenderer(WorkingBeatmap beatmap)
            : base(beatmap)
        {
        }
        public override ScoreProcessor CreateScoreProcessor() => new VitaruScoreProcessor(this);

        protected override IBeatmapConverter<VitaruHitObject> CreateBeatmapConverter() => new VitaruBeatmapConverter();

        protected override IBeatmapProcessor<VitaruHitObject> CreateBeatmapProcessor() => new VitaruBeatmapProcessor();

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
    }
}
