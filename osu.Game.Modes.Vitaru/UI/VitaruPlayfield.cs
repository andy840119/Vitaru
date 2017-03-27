// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Game.Modes.Vitaru.Objects;
using osu.Game.Modes.UI;
using OpenTK;
using osu.Game.Modes.Vitaru.Judgements;
using osu.Framework.Graphics.Containers;

namespace osu.Game.Modes.Vitaru.UI
{
    public class VitaruPlayfield : Playfield<VitaruHitObject, VitaruJudgementInfo>
    {
        internal Container characters;
        private Container judgementLayer;

        public override Vector2 Size
        {
            get
            {
                var parentSize = Parent.DrawSize;
                var aspectSize = parentSize.X * 0.75 < parentSize.Y ? new Vector2(parentSize.X, parentSize.X * 0.75f) : new Vector2(parentSize.Y * 4f / 3f, parentSize.Y);

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
                judgementLayer = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Depth = 1,
                },
                characters = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Depth = -1,
                }
            });
        }
    }
}