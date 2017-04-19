﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Game.Rulesets.Vitaru.Objects;
using osu.Game.Rulesets.Vitaru.Objects.Drawables;
using osu.Game.Rulesets.UI;
using OpenTK;
using osu.Game.Rulesets.Vitaru.Judgements;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Osu.Objects.Drawables.Connections;
using osu.Framework.Graphics.Sprites;

namespace osu.Game.Rulesets.Vitaru.UI
{
    public class VitaruPlayfield : Playfield<VitaruHitObject, VitaruJudgement>
    {
        public Container MainPlayfield;
        private Box playfieldBackground;

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
            Position = new Vector2(-166, -200);
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Scale = new Vector2(1.6f);

            Add(new Drawable[]
            {
                MainPlayfield = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Depth = -1,
                },
            });
            DrawableCharacter.playfield = MainPlayfield;
        }

        public override void Add(DrawableHitObject<VitaruHitObject, VitaruJudgement> h)
        {
            h.Depth = (float)h.HitObject.StartTime;

            IDrawableHitObjectWithProxiedApproach c = h as IDrawableHitObjectWithProxiedApproach;
            if (c != null)
                MainPlayfield.Add(c.ProxiedLayer.CreateProxy());

            base.Add(h);
        }
    }
}
