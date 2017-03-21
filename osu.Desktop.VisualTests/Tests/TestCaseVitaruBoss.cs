// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Screens.Testing;
using osu.Game.Modes.Vitaru.Objects.Characters;
using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using OpenTK;

namespace osu.Desktop.VisualTests.Tests
{
    class TestCaseVitaruBoss : TestCase
    {

        //private WorkingBeatmap beatmap;
        //private List<HitObject> enemysLoaded;

        public override string Description => @"Showing Boss stuff";

        internal VitaruPlayer player;
        private Boss boss;
        public int kills;
        private SpriteText score;

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

            AddButton(@"New Boss", NewBoss);

            boss = new Boss(this)
            {
                Anchor = Anchor.TopCentre,
                bossPosition = new Vector2(0, 100),
                OnDeath = NewBoss,
            };
            Add(boss);

            score = new SpriteText()
            {
                Text = "" + kills,
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopRight
            };
            Add(score);
        }
        protected override void Update()
        {
            base.Update();
            score.Text = "" + kills;
        }

        protected void NewBoss()
        {
            kills++;
            boss = new Boss(this)
            {
                Anchor = Anchor.TopCentre,
                bossPosition = new Vector2(new Random().Next(-200, 200), new Random() .Next (50 , 200)),
                OnDeath = NewBoss,
            };
            Add(boss);
        }
    }
}