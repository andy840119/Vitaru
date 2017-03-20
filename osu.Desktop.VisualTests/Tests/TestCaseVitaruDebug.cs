// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Screens.Testing;
using osu.Game.Modes.Vitaru.Objects.Characters;
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
using osu.Framework.MathUtils;

namespace osu.Desktop.VisualTests.Tests
{
    class TestCaseVitaruDebug : TestCase
    {
        public override string Description => @"Debug info";

        private VitaruPlayer player;
        private Enemy enemy;

        private SpriteText DebugInfo;

        public override void Reset()
        {
            base.Reset();
            Enemy.shoot = true;

            player = new VitaruPlayer(this)
            {
                Anchor = Anchor.Centre,
                Shooting = true,
                OnDeath = NewPlayer
            };
            Add(player);

            enemy = new Enemy(this)
            {
                Anchor = Anchor.Centre,
                enemyPosition = new Vector2(0, -200),
                OnDeath = NewEnemy,
                randomMovement = true,
            };
            Add(enemy);

            DebugInfo = new SpriteText()
            {
                Text = "PlayerPos " + VitaruPlayer.playerPosition.X + " , " + VitaruPlayer.playerPosition.Y,
                TextSize = 25,
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopRight
            };
            Add(DebugInfo);
        }
        protected override void Update()
        {
            base.Update();
            DebugInfo.Text = "PlayerPos " + VitaruPlayer.playerPosition.X + " , " + VitaruPlayer.playerPosition.Y;
        }

        protected void NewEnemy()
        {
            enemy = new Enemy(this)
            {
                Anchor = Anchor.Centre,
                enemyPosition = new Vector2(RNG.Next(-190, 190), RNG.Next(-300, 0)),
                OnDeath = NewEnemy,
                randomMovement = true,
            };
            Add(enemy);
        }
        protected void NewPlayer()
        {
            VitaruPlayer.playerPosition = new Vector2(0, 200);
            player = new VitaruPlayer(this)
            {
                Anchor = Anchor.Centre,
                OnDeath = NewPlayer,
            };
            Add(player);
        }
    }
}