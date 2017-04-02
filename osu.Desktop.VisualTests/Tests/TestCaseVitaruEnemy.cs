// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Screens.Testing;
using osu.Game.Modes.Vitaru.Objects.Characters;
using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using OpenTK;
using osu.Framework.MathUtils;

namespace osu.Desktop.VisualTests.Tests
{
    class TestCaseVitaruEnemy : TestCase
    {

        //private WorkingBeatmap beatmap;
        //private List<HitObject> enemysLoaded;

        public override string Description => @"Showing Enemy stuff";

        private Enemy enemy;
        public int Kills;
        public int Combo;
        private SpriteText score;
        private SpriteText combox;
        private int perfect = 30;
        private int good = 20;
        private int bad = 10;
        private int graze = 5;
        private VitaruPlayer Player;

        public override void Reset()
        {
            base.Reset();
            Kills = 0;

            Player = new VitaruPlayer(this)
            {
                Anchor = Anchor.Centre,
                Shooting = true,
                OnDeath = NewPlayer,
            };
            Add(Player);

            AddButton(@"New Enemy", NewEnemy);

            enemy = new Enemy(this)
            {
                Anchor = Anchor.Centre,
                EnemyPosition = new Vector2(RNG.Next(-190, 190), RNG.Next(-300, 0)),
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

            combox = new SpriteText()
            {
                Text = Combo + "X",
                Anchor = Anchor.BottomLeft,
                Origin = Anchor.BottomLeft
            };
            Add(combox);
        }
        protected override void Update()
        {
            base.Update();
            score.Text = "" + (Combo * (Kills * perfect));
            combox.Text = Combo + "X";
        }

        protected void NewPlayer()
        {
            VitaruPlayer.PlayerPosition = new Vector2(0, 200);
            Player = new VitaruPlayer(this)
            {
                Anchor = Anchor.Centre,
                OnDeath = NewPlayer,
            };
            Add(Player);
        }

        protected void NewEnemy()
        {
            Kills++;
            Combo++;
            enemy = new Enemy(this)
            {
                Anchor = Anchor.Centre,
                EnemyPosition = new Vector2(RNG.Next(-190, 190), RNG.Next(-300, 0)),
                OnDeath = NewEnemy,
            };
            Add(enemy);
        }
    }
}