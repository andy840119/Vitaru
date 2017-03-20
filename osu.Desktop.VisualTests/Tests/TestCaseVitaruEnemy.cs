﻿//Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
//Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Screens.Testing;
using osu.Game.Modes.Vitaru.Objects.Drawables;
using osu.Game.Modes.Vitaru.Objects.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osu.Framework.MathUtils;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Game.Modes.Vitaru.Objects;
using OpenTK;
using osu.Game.Beatmaps;
using osu.Game.Modes.Objects;
using osu.Framework.Timing;

namespace osu.Desktop.VisualTests.Tests
{
    class TestCaseVitaruEnemy : TestCase
    {

        //private WorkingBeatmap beatmap;
        //private List<HitObject> enemysLoaded;

        public override string Name => @"Vitaru Enemy";
        public override string Description => @"Showing Enemy stuff";

        private VitaruPlayer player;
        private Enemy enemy;
        public int kills;
        public int combo;
        private SpriteText score;
        private SpriteText combox;
        private int perfect = 30;
        private int good = 20;
        private int bad = 10;
        private int graze = 5;

        private void loadNewEnemy()
        {
            var v = new Enemy
            {
                Position = new Vector2(RNG.Next(-200, 200), RNG.Next(50, 200)),
            };
            NewEnemy(new DrawableVitaruEnemy(v));
        }

        public override void Reset()
        {
            base.Reset();
            kills = 0;

            /*player = new VitaruPlayer(this)
            {
                Anchor = Anchor.Centre,
                Shooting = true,
            };
            Add(player);*/

            AddButton(@"New Enemy", () => loadNewEnemy());

            score = new SpriteText()
            {
                Text = "" + (combo * (kills * perfect)),
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopRight
            };
            Add(score);

            combox = new SpriteText()
            {
                Text = combo + "X",
                Anchor = Anchor.BottomLeft,
                Origin = Anchor.BottomLeft
            };
            Add(combox);
            loadNewEnemy();
        }
        protected override void Update()
        {
            base.Update();
            score.Text = "" + (combo * (kills * perfect));
            combox.Text = combo + "X";
        }

        int depth;
        protected void NewEnemy(DrawableVitaruCharacter v)
        {
            v.OnDeath = loadNewEnemy;
            kills++;
            combo++;
            v.Anchor = Anchor.Centre;
            v.Depth = depth++;
            Add(v);
        }
    }
}