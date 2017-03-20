// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics.Containers;
using OpenTK;
using osu.Game.Modes.Vitaru.Objects.Drawables;
using osu.Framework.Graphics;
using OpenTK.Graphics;
using osu.Game.Modes.Vitaru.Objects.Projectiles;
using System;

namespace osu.Game.Modes.Vitaru.Objects.Characters
{
    public class Enemy : Character
    {
        public static bool shoot = false;
        public Vector2 enemyPosition = new Vector2(0, -160);
        public Vector2 enemySpeed { get; set; } = new Vector2(0.5f, 0.5f);

        int a = 0;
        private CharacterSprite enemy;

        public Enemy(Container parent) : base(parent)
        {
            Children = new[]
            {
                enemy = new CharacterSprite()
                {
                    Origin = Anchor.Centre,
                    CharacterName = "enemy"
                },
            };
            characterHealth = 100;
            Team = 1;
            Add(hitbox = new Hitbox()
            {
                Alpha = 1,
                HitboxWidth = 20,
                HitboxColor = Color4.Yellow,
            });
        }
        protected override void Update()
        {
            base.Update();
            if (shoot == true)
            {
                enemyShoot();
            }

            float ySpeed = enemySpeed.Y * (float)(Clock.ElapsedFrameTime);
            float xSpeed = enemySpeed.X * (float)(Clock.ElapsedFrameTime);
            Position = enemyPosition;
        }
        private void enemyShoot()
        {
            playerRelativePositionAngle();
            Bullet b;
            parent.Add(b = new Bullet(Team)
            {
                Depth = 1,
                Anchor = Anchor.Centre,
                BulletAngleRadian = a,
                BulletSpeed = 0.2f,
                BulletColor = Color4.Red,
            });
            b.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), b));
        }
        private float playerRelativePositionAngle()
        {
            float a = ((float)Math.Atan2((enemyPosition.X - VitaruPlayer.playerPosition.X), (enemyPosition.Y - VitaruPlayer.playerPosition.Y)) * (float)(180 / Math.PI));
            if (a < 0)
            {
                a = (a + (float)(2 * Math.PI));
                return a;
            }
            else
            {
                return a;
            }
        }
    }
}