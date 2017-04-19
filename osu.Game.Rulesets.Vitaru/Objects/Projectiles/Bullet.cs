﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using OpenTK;
using System;
using OpenTK.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Extensions.Color4Extensions;

namespace osu.Game.Modes.Vitaru.Objects.Projectiles
{
    public class Bullet : Projectile
    {
        //Different stats for Bullet that should always be changed
        public int BulletDamage { get; set; } = 20;
        public Color4 BulletColor { get; set; } = Color4.Red;
        public float BulletSpeed { get; set; } = 20;
        public float BulletWidth { get; set; } = 12f;
        public float BulletAngleDegree { get; set; } = 0;
        public float BulletAngleRadian { get; set; } = -1;
        public Vector4 BulletBounds = new Vector4(-30, -50, 532, 740);

        //Result of bulletSpeed + bulletAngle math, should never be modified outside of this class
        private Vector2 bulletVelocity;
        private BulletPiece bulletSprite;
        

        public Bullet(int team)
        {
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

        public Vector2 GetBulletVelocity()
        {
            if (BulletAngleRadian != -1)
            {
                bulletVelocity.Y = BulletSpeed * (-1 * ((float)Math.Cos(BulletAngleRadian)));
                bulletVelocity.X = BulletSpeed * ((float)Math.Sin(BulletAngleRadian));
                return bulletVelocity;
            }
            else
            {
                bulletVelocity.Y = BulletSpeed * (-1 * ((float)Math.Cos(BulletAngleDegree * (Math.PI / 180))));
                bulletVelocity.X = BulletSpeed * ((float)Math.Sin(BulletAngleDegree * (Math.PI / 180)));
                return bulletVelocity;
            }
        }

        protected override void Update()
        {
            base.Update();
            GetBulletVelocity();
            MoveToOffset(new Vector2(bulletVelocity.X * (float)Clock.ElapsedFrameTime, bulletVelocity.Y * (float)Clock.ElapsedFrameTime));

            if (Position.Y < BulletBounds.Y | Position.X < BulletBounds.X | Position.Y > BulletBounds.W | Position.X > BulletBounds.Z)
            {
                DeleteBullet();
            }

            if (Clock.ElapsedFrameTime > 40)
            {
                DeleteBullet();
            }
        }

        internal void DeleteBullet()
        {
            Dispose();
        }
    }

    public class BulletPiece : Container
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