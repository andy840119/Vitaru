// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics.Containers;
using OpenTK;
using osu.Game.Modes.Vitaru.Objects.Drawables;
using osu.Framework.Graphics;
using OpenTK.Graphics;
using osu.Game.Modes.Vitaru.Objects.Projectiles;
using System;
using osu.Framework.MathUtils;
using osu.Framework.Graphics.Transforms;

namespace osu.Game.Modes.Vitaru.Objects.Characters
{
    public class Enemy : Character
    {
        public static bool Shoot = false;
        public Vector2 EnemyPosition = new Vector2(0, -160);
        public Vector2 EnemySpeed { get; set; } = new Vector2(0.5f, 0.5f);
        public Vector2 EnemyVelocity;
        public float EnemyAngle;

        private float playerAngleRadian = 0;

        private CharacterSprite enemy;

        public bool RandomMovement { get; set; } = false;

        //Main Enemy Function
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
            CharacterHealth = 100;
            Team = 1;
            Add(Hitbox = new Hitbox()
            {
                Alpha = 1,
                HitboxWidth = 20,
                HitboxColor = Color4.Yellow,
            });
        }

        //Main Update Loop
        protected override void Update()
        {
            base.Update();
            if (RandomMovement == true)
            {
                randomMovements();
            }
            if (Shoot == true)
            {
                Shooting = true;
                OnShoot = enemyShoot;
            }
            float ySpeed = EnemySpeed.Y * (float)Clock.ElapsedFrameTime;
            float xSpeed = EnemySpeed.X * (float)Clock.ElapsedFrameTime;
            Position = EnemyPosition;
        }

        //Shoot Function for enemy
        private void enemyShoot()
        {
            playerRelativePositionAngle();
            Bullet b;
            MainParent.Add(b = new Bullet(Team)
            {
                Depth = 1,
                Anchor = Anchor.Centre,
                BulletAngleRadian = playerAngleRadian,
                BulletSpeed = 0.5f,
                BulletColor = Color4.Red,
            });
            b.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), b));
        }

        //Finds Player angle from Enemy position (only works with one player and enemy ATM*)
        private float playerRelativePositionAngle()
        {
            playerAngleRadian = (float)Math.Atan2((VitaruPlayer.PlayerPosition.X - EnemyPosition.X) , -1 * (VitaruPlayer.PlayerPosition.Y - EnemyPosition.Y));
            return playerAngleRadian;
        }
        private Vector2 randomPlace = new Vector2(RNG.Next(-190, 190), RNG.Next(-300, 0));
        private float v = RNG.Next(1, 3);
        private float t = RNG.Next(1, 3);
        private bool enemyMoving;

        private void randomMovements()
        {
            if (enemyMoving == false)
            {
                v = RNG.Next(1, 5);
                t = RNG.Next(1, 4);
                randomPlace = new Vector2(RNG.Next(-190, 190), RNG.Next(-300, 0));
                MoveTo(Direction.Horizontal, randomPlace.X, 3 , EasingTypes.InOutQuad);
                MoveTo(Direction.Vertical, randomPlace.Y, 3, EasingTypes.InOutQuad);
            }
            if(EnemyPosition == randomPlace)
            {
                enemyMoving = false;
            }
        }
    }
}