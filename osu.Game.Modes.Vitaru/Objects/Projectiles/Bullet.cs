// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using OpenTK;
using System;
using OpenTK.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics;
using osu.Game.Modes.Vitaru.Objects.Characters;
using osu.Framework.Extensions.Color4Extensions;

namespace osu.Game.Modes.Vitaru.Objects.Projectiles
{
    public class Bullet : Projectile
    {
        //Different stats for Bullet that should always be changed
        public int BulletDamage { get; set; } = 5;
        public Color4 BulletColor { get; set; } = Color4.Red;
        public float BulletSpeed { get; set; } = 20;
        public float BulletWidth { get; set; } = 12f;
        public float BulletAngleDegree { get; set; } = 0;
        public float BulletAngleRadian { get; set; } = -1;

        //Result of bulletSpeed + bulletAngle math, should never be modified outside of this class
        private Vector2 BulletVelocity;

        //Debug info
        public static int bulletsLoaded = 0;
        public static int bulletCapHit = 0;

        private BulletPiece bulletSprite;
        

        public Bullet(int team)
        {

            bulletsLoaded++;
            Team = team;
            Children = new[]
            {
                bulletSprite = new BulletPiece(this)
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
            };
        }

        protected override void Update()
        {
            getBulletVelocity();
            base.Update();
            MoveToOffset(new Vector2(BulletVelocity.X * (float)Clock.ElapsedFrameTime, BulletVelocity.Y * (float)Clock.ElapsedFrameTime));
            if (Position.Y < -375 | Position.X < -225 | Position.Y > 375 | Position.X > 225)
            {
                deleteBullet();
            }

            if (Clock.ElapsedFrameTime > 40)
            {
                bulletCapHit++;
                deleteBullet();
            }
        }
        public Vector2 getBulletVelocity()
        {
            if (BulletAngleRadian != -1)
            {
                BulletVelocity.Y = BulletSpeed * (-1 * ((float)Math.Cos(BulletAngleRadian)));
                BulletVelocity.X = BulletSpeed * ((float)Math.Sin(BulletAngleRadian));
                VitaruPlayer.velocityCalculation++;
                return BulletVelocity;
            }
            else
            {
                BulletVelocity.Y = BulletSpeed * (-1 * ((float)Math.Cos(BulletAngleDegree * (Math.PI / 180))));
                BulletVelocity.X = BulletSpeed * ((float)Math.Sin(BulletAngleDegree * (Math.PI / 180)));
                VitaruPlayer.velocityCalculation++;
                return BulletVelocity;
            }
        }

        internal void deleteBullet()
        {
            bulletsLoaded--;
            Dispose();
        }
    }

/*using osu.Framework.Graphics;
using OpenTK;
using System;
using OpenTK.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics;
using osu.Game.Modes.Vitaru.Objects.Characters;

namespace osu.Game.Modes.Vitaru.Objects.Projectiles
{
    public class Bullet : Projectile
    {
        //Different stats for Bullet that should always be changed
        public int BulletDamage { get; set; } = 5;
        public Color4 BulletColor { get; set; } = Color4.Red;
        public float BulletWidth { get; set; } = 12f;
        private float bulletSpeed;
        public float BulletSpeed
        {
            get
            {
                return bulletSpeed;
            }
            set
            {
                bulletSpeed = value;
                calculateBulletVelocity();
            }
        }

        private float bulletAngle;
        public float BulletAngle
        {
            get
            {
                return bulletAngle;
            }
            set
            {
                bulletAngle = value;
                calculateBulletVelocity();
            }
        }


        //Result of bulletSpeed + bulletAngle math, should never be modified outside of this class
        private Vector2 bulletVelocity;

        //Debug info
        public static int BulletsLoaded = 0;
        public static int BulletCapHit = 0;

        private BulletPiece bulletSprite;


        public Bullet(int team)
        {
            BulletsLoaded++;
            Team = team;
            Children = new[]
            {
                bulletSprite = new BulletPiece(this)
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
            };
        }

        protected override void Update()
        {
            base.Update();
            MoveToOffset(new Vector2(bulletVelocity.X * (float)Clock.ElapsedFrameTime, bulletVelocity.Y * (float)Clock.ElapsedFrameTime));
            if (Position.Y < -375 | Position.X < -225 | Position.Y > 375 | Position.X > 225)
            {
                Dispose();
            }

            if (Clock.ElapsedFrameTime > 40)
            {
                BulletCapHit++;
                Dispose();
            }
        }
        private void calculateBulletVelocity()
        {
            bulletVelocity.Y = BulletSpeed * (-1 * ((float)Math.Cos(BulletAngle * (Math.PI / 180))));
            bulletVelocity.X = BulletSpeed * ((float)Math.Sin(BulletAngle * (Math.PI / 180)));
            VitaruPlayer.velocityCalculation++;
        }

        protected override void Dispose(bool isDisposing)
        {
            BulletsLoaded--;
            base.Dispose(isDisposing);
        }

        internal void deleteBullet()
        {
            Dispose();
        }
    }*/

    class BulletPiece : Container
    {
        private CircularContainer bulletContainer;
        private object bullet;
        public BulletPiece(Bullet bullet)
        {
            this.bullet = bullet;
            Children = new Drawable[]
            {
                new Container
                {
                    Masking = true,
                    AutoSizeAxes = Axes.Both,
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    BorderThickness = 3,
                    Depth = 1,
                    BorderColour = bullet.BulletColor,
                    Alpha = 1f,
                    CornerRadius = bullet.BulletWidth / 2,
                    Children = new[]
                    {
                        new Box
                        {
                            Colour = Color4.White,
                            Alpha = 1,
                            Width = bullet.BulletWidth,
                            Height = bullet.BulletWidth,
                        },
                    },
                },
                bulletContainer = new CircularContainer
                {
                        Origin = Anchor.Centre,
                        Anchor = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Scale = new Vector2(bullet.BulletWidth),
                        Depth = 2,
                        Masking = true,
                        EdgeEffect = new EdgeEffect
                        {
                            Type = EdgeEffectType.Shadow,
                            Colour = (bullet.BulletColor).Opacity(0.75f),
                            Radius = 2f,
                        }
                }
            };
        }
    }
}