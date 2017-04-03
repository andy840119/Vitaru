// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Screens.Testing;
using osu.Game.Modes.Vitaru.Objects.Characters;
using osu.Game.Modes.Vitaru.Objects.Drawables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using OpenTK;
using osu.Framework.MathUtils;
using osu.Framework.Graphics.Transforms;
using osu.Framework.Timing;
using osu.Game.Modes.Vitaru.Objects;
using osu.Framework.Graphics.Containers;
using osu.Game.Modes.Objects.Drawables;
using osu.Game.Modes.Vitaru.Judgements;
using osu.Framework.Configuration;

namespace osu.Desktop.VisualTests.Tests
{
    class TestCaseVitaruDebug : TestCase
    {
        private FramedClock framedClock;

        private bool auto;

        public TestCaseVitaruDebug()
        {
            var rateAdjustClock = new StopwatchClock(true);
            framedClock = new FramedClock(rateAdjustClock);
        }

        private HitObjectType mode = HitObjectType.Enemy;

        private Container playfieldContainer;

        private void load(HitObjectType mode)
        {
            this.mode = mode;

            switch (mode)
            {
                case HitObjectType.Player:
                    add(new DrawableVitaruPlayer(new VitaruPlayer
                    {
                        StartTime = framedClock.CurrentTime + 600,
                        EndTime = framedClock.CurrentTime + 1600,
                        Position = new Vector2(0, 0),
                    }));
                    break;
                case HitObjectType.Enemy:
                    add(new DrawableEnemy(new Enemy
                    {
                        StartTime = framedClock.CurrentTime + 600,
                        EndTime = framedClock.CurrentTime + 1600,
                        Position = new Vector2(0, 0),
                    }));
                    break;
                case HitObjectType.Boss:
                    add(new DrawableVitaruBoss(new Boss
                    {
                        StartTime = framedClock.CurrentTime + 600,
                        EndTime = framedClock.CurrentTime + 1600,
                        Position = new Vector2(0, 0),
                    }));
                    break;
            }
        }

        public override void Reset()
        {
            base.Reset();

            AddButton(@"player", () => load(HitObjectType.Player));
            AddButton(@"enemy", () => load(HitObjectType.Enemy));
            AddButton(@"boss", () => load(HitObjectType.Boss));

            framedClock.ProcessFrame();

            var clockAdjustContainer = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Clock = framedClock,
                Children = new[]
                {
                    playfieldContainer = new Container { RelativeSizeAxes = Axes.Both },
                }
            };

            Add(clockAdjustContainer);

            load(mode);
        }

        private int depth;

        private void add(DrawableVitaruHitObject h)
        {
            h.Anchor = Anchor.Centre;
            h.Depth = depth++;

            playfieldContainer.Add(h);
            var proxyable = h as IDrawableHitObjectWithProxiedApproach;
        }
    }
}
