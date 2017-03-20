// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics.Containers;
using OpenTK;
using OpenTK.Input;
using osu.Game.Modes.Vitaru.Objects.Drawables;
using osu.Framework.Graphics;
using osu.Framework.Input;
using System.Collections.Generic;
using System;
using OpenTK.Graphics;

namespace osu.Game.Modes.Vitaru.Objects.Characters
{
    public class Boss : Character
    {
        public int StartTime { get; set; }

        public Vector2 bossPosition = new Vector2(0, -160);
        public Vector2 bossSpeed { get; set; } = new Vector2(1, 1);

        private CharacterSprite boss;

        public Boss(Container parent) : base(parent)
        {
            Children = new[]
            {
                boss = new CharacterSprite()
                {
                    Origin = Anchor.Centre,
                    CharacterName = "boss"
                },
            };
            characterHealth = 1000;
            Team = 1;
            Add(hitbox = new Hitbox()
            {
                Alpha = 1,
                HitboxWidth = 32,
                HitboxColor = Color4.Green,
            });
        }
        protected override void Update()
        {
            base.Update();
            float ySpeed = bossSpeed.Y * (float)(Clock.ElapsedFrameTime);
            float xSpeed = bossSpeed.X * (float)(Clock.ElapsedFrameTime);
            Position = bossPosition;
        }
    }
}