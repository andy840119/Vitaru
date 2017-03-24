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

        internal VitaruPlayer Player;
        private Boss boss;
        public int Kills;
        private SpriteText score;

        public override void Reset()
        {
            base.Reset();
            Kills = 0;

            Player = new VitaruPlayer(this)
            {
                Anchor = Anchor.Centre,
                Shooting = true,
            };
            Add(Player);

            AddButton(@"New Boss", NewBoss);

            boss = new Boss(this)
            {
                Anchor = Anchor.TopCentre,
                BossPosition = new Vector2(0, 100),
                OnDeath = NewBoss,
            };
            Add(boss);

            score = new SpriteText()
            {
                Text = "" + Kills,
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopRight
            };
            Add(score);
        }
        protected override void Update()
        {
            base.Update();
            score.Text = "" + Kills;
        }

        protected void NewBoss()
        {
            Kills++;
            boss = new Boss(this)
            {
                Anchor = Anchor.TopCentre,
                BossPosition = new Vector2(new Random().Next(-200, 200), new Random() .Next (50 , 200)),
                OnDeath = NewBoss,
            };
            Add(boss);
        }
    }
}