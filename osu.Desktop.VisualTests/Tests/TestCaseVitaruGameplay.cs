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
    class TestCaseVitaruGameplay : TestCase
    {
        public override string Description => @"Showing everything to play osu!vitaru";

        private VitaruPlayer player;
        private Enemy enemy;
        public int Kills;
        public int Combo;
        private SpriteText score;
        private SpriteText combox;

        //Score will probably be changed to reward points based on enemy difficulty
        private int perfect = 30;
        private int good = 20;
        private int bad = 10;
        private int graze = 5;

        public override void Reset()
        {
            base.Reset();
            Kills = 0;
            Combo = 0;
            Enemy.Shoot = true;
            //ensure we are at offset 0
            //Clock = new FramedClock();

            player = new VitaruPlayer(this)
            {
                Anchor = Anchor.Centre,
                Shooting = true,
                OnDeath = NewPlayer
            };
            Add(player);

            enemy = new Enemy(this)
            {
                Anchor = Anchor.TopCentre,
                EnemyPosition = new Vector2(0, 100),
                OnDeath = NewEnemy,
            };
            Add(enemy);

            score = new SpriteText()
            {
                Text = "" + Combo * (Kills * perfect),
                TextSize = 50,
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopRight
            };
            Add(score);

            combox = new SpriteText()
            {
                Text = Combo + "x",
                TextSize = 40,
                Anchor = Anchor.BottomLeft,
                Origin = Anchor.BottomLeft
            };
            Add(combox);
        }
        protected override void Update()
        {
            base.Update();
            score.Text = "" + Combo * (Kills * perfect);
            combox.Text = Combo + "x";
        }

        protected void NewEnemy()
        {
            Kills++;
            Combo++;
            enemy = new Enemy(this)
            {
                Anchor = Anchor.TopCentre,
                EnemyPosition = new Vector2(new Random().Next(-200, 200), new Random().Next (50 , 200)),
                OnDeath = NewEnemy,
            };
            Add(enemy);
        }
        protected void NewPlayer()
        {
            VitaruPlayer.PlayerPosition = new Vector2(0, 200);
            Combo = 0;
            player = new VitaruPlayer(this)
            {
                Anchor = Anchor.Centre,
                OnDeath = NewPlayer,
            };
            Add(player);
        }
    }
}