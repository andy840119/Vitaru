using osu.Framework.Graphics;
using OpenTK;
using System;
using OpenTK.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics;

namespace osu.Game.Modes.Vitaru.Objects.Projectiles
{
    public class Bullet : Projectile
    {
        //Different stats for Bullet that should always be changed
        public int BulletDamage { get; set; } = 5;
        public Color4 BulletColor { get; set; } = Color4.Red;
        public float BulletSpeed { get; set; } = 20;
        public float BulletWidth { get; set; } = 12f;
        public float BulletAngle { get; set; } = 0;

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
            base.Update();
            getBulletVelocity();
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
            BulletVelocity.Y = BulletSpeed * (-1 * ((float)Math.Cos(BulletAngle * (Math.PI / 180))));
            BulletVelocity.X = BulletSpeed * ((float)Math.Sin(BulletAngle * (Math.PI / 180)));
            return BulletVelocity;
        }

        internal void deleteBullet()
        {
            bulletsLoaded--;
            Dispose();
        }
    }


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