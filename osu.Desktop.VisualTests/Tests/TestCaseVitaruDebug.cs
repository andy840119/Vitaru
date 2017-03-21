// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Screens.Testing;
using osu.Game.Modes.Vitaru.Objects.Characters;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using OpenTK;
using osu.Framework.MathUtils;

namespace osu.Desktop.VisualTests.Tests
{
    class TestCaseVitaruDebug : TestCase
    {
        public override string Description => @"Debug info";

        private VitaruPlayer player;
        private Enemy enemy;

        private SpriteText DebugInfo;

        //Reset function (runs when you start this testcase)
        public override void Reset()
        {
            base.Reset();
            Enemy.Shoot = true;


            //Player
            player = new VitaruPlayer(this)
            {
                Anchor = Anchor.Centre,
                Shooting = true,
                OnDeath = NewPlayer
            };
            Add(player);

            //Enemy
            enemy = new Enemy(this)
            {
                Anchor = Anchor.Centre,
                EnemyPosition = new Vector2(0, -200),
                OnDeath = NewEnemy,
                RandomMovement = true,
            };
            Add(enemy);

            //Debug stats, change to whatever you need to debug
            DebugInfo = new SpriteText()
            {
                Text = "PlayerPos " + VitaruPlayer.PlayerPosition.X + " , " + VitaruPlayer.PlayerPosition.Y,
                TextSize = 25,
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopRight
            };
            Add(DebugInfo);
        }

        //Update loop
        protected override void Update()
        {
            base.Update();
            DebugInfo.Text = "PlayerPos " + VitaruPlayer.PlayerPosition.X + " , " + VitaruPlayer.PlayerPosition.Y;
        }

        //New Enemy Function
        protected void NewEnemy()
        {
            enemy = new Enemy(this)
            {
                Anchor = Anchor.Centre,
                EnemyPosition = new Vector2(RNG.Next(-190, 190), RNG.Next(-300, 0)),
                OnDeath = NewEnemy,
                RandomMovement = true,
            };
            Add(enemy);
        }

        //New Player (VitaruPlayer) Function
        protected void NewPlayer()
        {
            VitaruPlayer.PlayerPosition = new Vector2(0, 200);
            player = new VitaruPlayer(this)
            {
                Anchor = Anchor.Centre,
                OnDeath = NewPlayer,
            };
            Add(player);
        }
    }
}