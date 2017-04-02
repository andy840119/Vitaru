// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Game.Modes.Vitaru.Objects;
using osu.Game.Modes.Vitaru.Objects.Drawables;
using osu.Game.Modes.UI;
using OpenTK;
using osu.Game.Modes.Vitaru.Judgements;
using osu.Framework.Graphics.Containers;
using osu.Game.Modes.Objects.Drawables;

namespace osu.Game.Modes.Vitaru.UI
{
    public class VitaruPlayfield : Playfield<VitaruHitObject, VitaruJudgementInfo>
    {
        internal Container characters;
        private Container hitboxes;
        internal Container projectiles;

        public override Vector2 Size
        {
            get
            {
                var parentSize = Parent.DrawSize;
                var aspectSize = parentSize.X * 0.75f < parentSize.Y ? new Vector2(parentSize.X, parentSize.X * 0.75f) : new Vector2(parentSize.Y * 9f / 16f, parentSize.Y);

                return new Vector2(aspectSize.X / parentSize.X, aspectSize.Y / parentSize.Y) * base.Size;
            }
        }

        public VitaruPlayfield() : base(512)
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(0.75f);

            Add(new Drawable[]
            {
                hitboxes = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Depth = 1,
                },
                projectiles = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Depth = 0,
                },
                characters = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Depth = -1,
                }
            });
        }

        public override void Add(DrawableHitObject<VitaruHitObject, VitaruJudgementInfo> h)
        {
            h.Depth = (float)h.HitObject.StartTime;

            DrawableEnemy e = h as DrawableEnemy;
            if (e != null)
            {
                e.MainParent = characters;
            }

            base.Add(e);
        }
    }
}
