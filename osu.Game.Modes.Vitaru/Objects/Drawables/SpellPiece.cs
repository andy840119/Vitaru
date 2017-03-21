// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics;
using System;

namespace osu.Game.Modes.Vitaru.Objects.Drawables
{
    class SpellPiece : Container
    {
        private Sprite sprite;
        public float DegreesPerSecond = 80;
        public float NormalSize = 200;
        public float sineHeight = 100;
        public float SineSpeed = 0.001f;

        public SpellPiece()
        {
            sprite = new Sprite()
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Alpha = 0.25f,
            };
            Add(sprite);
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            sprite.Texture = textures.Get(@"Play/Vitaru/sign");
        }

        protected override void Update()
        {
            base.Update();

            sprite.ResizeTo((float)Math.Abs(Math.Sin(Clock.CurrentTime * SineSpeed)) * sineHeight + NormalSize);
            sprite.RotateTo((float)((Clock.CurrentTime / 1000) * DegreesPerSecond));
        }
    }
}
