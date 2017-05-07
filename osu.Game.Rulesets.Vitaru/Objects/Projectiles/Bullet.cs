// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using OpenTK;
using System;
using OpenTK.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Extensions.Color4Extensions;

namespace osu.Game.Rulesets.Vitaru.Objects.Projectiles
{
    public class Bullet : Projectile
    {
        //Different stats for Bullet that should always be changed
        public int BulletDamage { get; set; } = 10;
        public Color4 BulletColor { get; set; } = Color4.White;
        public float BulletSpeed { get; set; } = 1f;
        public float BulletWidth { get; set; } = 12f;
        public float BulletAngleDegree { get; set; } = 0;
        public float BulletAngleRadian { get; set; } = -10;

        private Vector4 BulletBounds = new Vector4(-30, -100, 532, 740);
        private Vector2 PlayfieldOffset = new Vector2(175, 380);
        private bool fadingOut = false;

        public static int BulletCount = 0;

        //Result of bulletSpeed + bulletAngle math, should never be modified outside of this class
        private Vector2 bulletVelocity;

        private Container bulletRing;
        private CircularContainer bulletCircle;


        public Bullet(int team)
        {
            Team = team;

        }

        protected override void LoadComplete()
        {
            BulletCount++;
            GetBulletVelocity();
            Children = new Drawable[]
            {
                bulletRing = new Container
                {
                    Masking = true,
                    AutoSizeAxes = Axes.Both,
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    BorderThickness = 3,
                    Depth = 1,
                    AlwaysPresent = true,
                    BorderColour = BulletColor,
                    Alpha = 1f,
                    CornerRadius = BulletWidth,
                    Children = new[]
                    {
                        new Box
                        {
                            Colour = Color4.White,
                            Alpha = 1,
                            Width = BulletWidth * 2,
                            Height = BulletWidth * 2,
                        },
                    },
                },
                bulletCircle = new CircularContainer
                {
                        Origin = Anchor.Centre,
                        Anchor = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Scale = new Vector2(BulletWidth * 2),
                        Depth = 2,
                        AlwaysPresent = true,
                        Masking = true,
                        EdgeEffect = new EdgeEffect
                        {
                            Type = EdgeEffectType.Shadow,
                            Colour = (BulletColor).Opacity(0.5f),
                            Radius = 2f,
                        }
                }
            };
        }

        public Vector2 GetBulletVelocity()
        {
            if (BulletAngleRadian != -10)
            {
                bulletVelocity.X = BulletSpeed * (((float)Math.Cos(BulletAngleRadian)));
                bulletVelocity.Y = BulletSpeed * ((float)Math.Sin(BulletAngleRadian));
                return bulletVelocity;
            }
            else
            {
                BulletAngleDegree = BulletAngleDegree - 90;
                bulletVelocity.X = BulletSpeed * (((float)Math.Cos(BulletAngleDegree * (Math.PI / 180))));
                bulletVelocity.Y = BulletSpeed * ((float)Math.Sin(BulletAngleDegree * (Math.PI / 180)));
                return bulletVelocity;
            }
        }

        protected override void Update()
        {
            base.Update();
            MoveToOffset(new Vector2(bulletVelocity.X * (float)Clock.ElapsedFrameTime, bulletVelocity.Y * (float)Clock.ElapsedFrameTime));

            if (Alpha < 0.05)
                DeleteBullet();

            if (Position.Y < BulletBounds.Y | Position.X < BulletBounds.X | Position.Y > BulletBounds.W | Position.X > BulletBounds.Z)
            {
                if (Team == 0 && fadingOut == false)
                    fadeOut();
            }

            if (Position.Y < (BulletBounds.Y + PlayfieldOffset.Y) | Position.X < (BulletBounds.X + PlayfieldOffset.X) | Position.Y > (BulletBounds.W + PlayfieldOffset.Y) | Position.X > (BulletBounds.Z + PlayfieldOffset.X))
            {
                if (Team == 1 && fadingOut == false)
                    fadeOut();
            }
        }

        private void fadeOut()
        {
            fadingOut = true;
            FadeOut((200), EasingTypes.OutBounce);
        }

        internal void DeleteBullet()
        {
            Dispose();
        }
    }
}