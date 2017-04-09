﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using OpenTK.Graphics;
using OpenTK;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Extensions.Color4Extensions;

namespace osu.Game.Modes.Vitaru.Objects
{
    public class Hitbox : Container
    {
        //Different stats for Hitboxes
        public Color4 HitboxColor { get; set; } = Color4.White;
        public float HitboxHealth { get; set; } = 100;
        public float HitboxWidth { get; set; } = 8f;

        private Container hitboxContainer;
        
        public Hitbox()
        {
            Children = new Drawable[]
            {
                new Container
                {
                    Masking = true,
                    AutoSizeAxes = Axes.Both,
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    BorderThickness = 3,
                    Depth = 1,
                    BorderColour = HitboxColor,
                    Alpha = 1f,
                    CornerRadius = HitboxWidth / 2,
                    Children = new[]
                    {
                        new Box
                        {
                            Colour = Color4.White,
                            Alpha = 1,
                            Width = HitboxWidth,
                            Height = HitboxWidth,
                        },
                    },
                },
                hitboxContainer = new CircularContainer
                {
                        Origin = Anchor.Centre,
                        Anchor = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Scale = new Vector2(HitboxWidth),
                        Depth = 2,
                        Masking = true,
                        EdgeEffect = new EdgeEffect
                        {
                            Type = EdgeEffectType.Shadow,
                            Colour = (HitboxColor).Opacity(0.4f),
                            Radius = 2f,
                        }
                }
            };
        }
    }
}
