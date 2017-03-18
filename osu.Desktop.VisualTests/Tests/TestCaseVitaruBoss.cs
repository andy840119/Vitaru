//Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
//Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Screens.Testing;
using osu.Game.Modes.Vitaru.Objects.Characters;
using osu.Game.Modes.Vitaru.Objects.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Game.Modes.Vitaru.Objects;
using OpenTK;
using osu.Game.Beatmaps;
using osu.Game.Modes.Objects;
using osu.Framework.Timing;

namespace osu.Desktop.VisualTests.Tests
{
    class TestCaseVitaruBoss : TestCase
    {

        //private WorkingBeatmap beatmap;
        //private List<HitObject> enemysLoaded;

        public override string Name => @"Vitaru Boss";
        public override string Description => @"Showing Boss stuff";

        private VitaruPlayer player;
        private Boss boss;
        public int kills;
        private SpriteText score;

        private void loadNewBoss()
        {
            var v = new Boss
            {
                Position = new Vector2(new Random().Next(-200, 200), new Random().Next(50, 200)),
            };
        }

        public override void Reset()
        {
            base.Reset();
            kills = 0;

            player = new VitaruPlayer(this)
            {
                Anchor = Anchor.Centre,
                Shooting = true,
            };
            Add(player);

            AddButton(@"New Boss", () => loadNewBoss());

            score = new SpriteText()
            {
                Text = "" + kills,
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopRight
            };
            Add(score);
            loadNewBoss();
        }
        protected override void Update()
        {
            base.Update();
            score.Text = "" + kills;
        }

        protected void NewBoss(DrawableVitaruCharacter v)
        {
            kills++;
            v.Anchor = Anchor.TopCentre;
            v.OnDeath = loadNewBoss;
            Add(v);
        }
    }
}