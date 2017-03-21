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
    class TestCaseVitaruEnemy : TestCase
    {

        //private WorkingBeatmap beatmap;
        //private List<HitObject> enemysLoaded;

        public override string Description => @"Showing Enemy stuff";

        internal VitaruPlayer player;
        private Enemy enemy;
        public int Kills;
        public int Combo;
        private SpriteText score;
        private SpriteText Combox;
        private int perfect = 30;
        private int good = 20;
        private int bad = 10;
        private int graze = 5;

        public override void Reset()
        {
            base.Reset();
            Kills = 0;

            player = new VitaruPlayer(this)
            {
                Anchor = Anchor.Centre,
                Shooting = true,
            };
            Add(player);

            AddButton(@"New Enemy", NewEnemy);

            enemy = new Enemy(this)
            {
                Anchor = Anchor.TopCentre,
                EnemyPosition = new Vector2(0, -500),
                OnDeath = NewEnemy,
            };
            Add(enemy);

            score = new SpriteText()
            {
                Text = "" + (Combo * (Kills * perfect)),
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopRight
            };
            Add(score);

            Combox = new SpriteText()
            {
                Text = Combo + "X",
                Anchor = Anchor.BottomLeft,
                Origin = Anchor.BottomLeft
            };
            Add(Combox);
        }
        protected override void Update()
        {
            base.Update();
            score.Text = "" + (Combo * (Kills * perfect));
            Combox.Text = Combo + "X";
        }

        protected void NewEnemy()
        {
            Kills++;
            Combo++;
            enemy = new Enemy(this)
            {
                Anchor = Anchor.TopCentre,
                EnemyPosition = new Vector2(new Random().Next(-200, 200), new Random() .Next(50 , 200)),
                OnDeath = NewEnemy,
            };
            Add(enemy);
        }
    }
}