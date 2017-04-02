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

namespace osu.Desktop.VisualTests.Tests
{
    class TestCaseVitaruDebug : TestCase
    {
        public override string Description => @"Debug info";

        private DrawableVitaruPlayer player;
        private DrawableEnemy enemy;

        //Debug info, Change at will
        private SpriteText debugSpriteTextTopLeft;
        private SpriteText debugSpriteTextTopRight;
        private string debugTextTopLeft = "Player Position (x,y): ";
        private string debugTextTopRight;

        //Gross Shit, ignore it
        public bool RandomMovement = false;
        private Vector2 randomPlace = new Vector2(RNG.Next(-190, 190), RNG.Next(-300, 0));
        private float t = RNG.Next(1, 3);
        private bool enemyMoving;

        //Reset function (runs when you start this testcase)
        public override void Reset()
        {
            base.Reset();
            Enemy.Shoot = true;

            //Player
            player = new DrawableVitaruPlayer(new VitaruPlayer());
            Add(player);

            //Enemy
            enemy = new DrawableEnemy(this)
            {
                Anchor = Anchor.Centre,
                EnemyPosition = new Vector2(0, -200),
                OnDeath = NewEnemy,
            };
            Add(enemy);

            //Debug stats, change to whatever you need to debug
            debugSpriteTextTopLeft = new SpriteText()
            {
                Text = debugTextTopLeft + VitaruPlayer.PlayerPosition.X + " , " + VitaruPlayer.PlayerPosition.Y,
                TextSize = 25,
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopRight
            };
            Add(debugSpriteTextTopLeft);
        }

        //Update loop
        protected override void Update()
        {
            if (RandomMovement == true)
            {
                randomMovements();
            }
            base.Update();
            debugSpriteTextTopLeft.Text = debugTextTopLeft + VitaruPlayer.PlayerPosition.X + " , " + VitaruPlayer.PlayerPosition.Y;
        }

        //New Enemy Function
        protected void NewEnemy()
        {
            enemy = new Enemy(this)
            {
                Anchor = Anchor.Centre,
                EnemyPosition = new Vector2(RNG.Next(-190, 190), RNG.Next(-300, 0)),
                OnDeath = NewEnemy,
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
        private void randomMovements()
        {
            if (enemyMoving == false)
            {
                t = RNG.Next(100, 500);
                randomPlace = new Vector2(RNG.Next(-190, 190), RNG.Next(-300, 0));
                enemy.MoveTo(randomPlace, t, EasingTypes.InOutQuad);
            }
            if (Enemy.EnemyPos == randomPlace)
            {
                enemyMoving = false;
            }
        }
    }
}