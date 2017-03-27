// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics.Containers;
using OpenTK;
using osu.Game.Modes.Vitaru.Objects.Drawables;
using osu.Framework.Graphics;
using OpenTK.Graphics;

namespace osu.Game.Modes.Vitaru.Objects.Characters
{
    public class Boss : Character
    {
        public int StartTime { get; set; }

        public Vector2 BossPosition = new Vector2(0, -160);
        public Vector2 BossSpeed { get; set; } = new Vector2(1, 1);

        public Boss(Container parent) : base(parent)
        {
            Children = new[]
            {
                new CharacterSprite()
                {
                    Origin = Anchor.Centre,
                    CharacterName = "boss"
                },
            };
            CharacterHealth = 1000;
            Team = 1;
            Add(Hitbox = new Hitbox()
            {
                Alpha = 1,
                HitboxWidth = 32,
                HitboxColor = Color4.Green,
            });
        }
        protected override void Update()
        {
            base.Update();
            float ySpeed = BossSpeed.Y * (float)Clock.ElapsedFrameTime;
            float xSpeed = BossSpeed.X * (float)Clock.ElapsedFrameTime;
            Position = BossPosition;
        }
    }
}