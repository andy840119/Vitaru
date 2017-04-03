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
using osu.Game.Modes.Osu.Objects.Drawables.Connections;
using osu.Framework.Graphics.Sprites;

namespace osu.Game.Modes.Vitaru.UI
{
    public class VitaruPlayfield : Playfield<VitaruHitObject, VitaruJudgementInfo>
    {
        private Container characters;
        private Box playfield;

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
            Position = new Vector2(-182, -200);
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            Size = new Vector2(1f);

            Add(new Drawable[]
            {
                characters = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Depth = -1,
                },
                playfield = new Box
                {
                    Depth = -2,
                    Size = new Vector2 (1.2f , 1f),
                    Position = new Vector2(212 , 338),
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0.25f
                }
            });
        }

        public override void Add(DrawableHitObject<VitaruHitObject, VitaruJudgementInfo> h)
        {
            h.Depth = (float)h.HitObject.StartTime;

            IDrawableHitObjectWithProxiedApproach c = h as IDrawableHitObjectWithProxiedApproach;
            if (c != null)
                characters.Add(c.ProxiedLayer.CreateProxy());

            base.Add(h);
        }
    }
}
