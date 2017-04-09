// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics.Containers;
using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Game.Modes.Vitaru.Objects.Drawables;
using System;

namespace osu.Game.Modes.Vitaru.Objects.Projectiles
{
    public abstract class BulletPattern : Container
    {
        public abstract int PatternID { get; }
        public float PatternSpeed { get; set; } = 0.2f;
        public float PatternAngle { get; set; } = MathHelper.Pi;
        public int PatternAngleRadian { get; set; }
        public int PatternAngleDegree { get; set; }
        public int Team { get; set; }
        private Vector2 patternVelocity;

        public Container ParentPattern;

        public int BulletTeam { get; set; } = 1;
        public Color4 PatternColor { get; set; } = Color4.White;
        public int BulletCount { get; set; } = 0;

        public BulletPattern()
        {
            Children = new[]
            {
                ParentPattern = new Container()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
            };
        }

        public Vector2 GetPatternVelocity()
        {
            if (PatternAngleRadian != -1)
            {
                patternVelocity.Y = PatternSpeed * (-1 * ((float)Math.Cos(PatternAngleRadian)));
                patternVelocity.X = PatternSpeed * ((float)Math.Sin(PatternAngleRadian));
                return patternVelocity;
            }
            else
            {
                patternVelocity.Y = PatternSpeed * (-1 * ((float)Math.Cos(PatternAngleDegree * (Math.PI / 180))));
                patternVelocity.X = PatternSpeed * ((float)Math.Sin(PatternAngleDegree * (Math.PI / 180)));
                return patternVelocity;
            }
        }

        protected override void Update()
        {
            base.Update();
            GetPatternVelocity();

            MoveToOffset(new Vector2(patternVelocity.X * (float)Clock.ElapsedFrameTime, patternVelocity.Y * (float)Clock.ElapsedFrameTime));

            if (BulletCount < 1)
                Dispose();
        }
    }
    public class SingleShot : BulletPattern
    {
        public override int PatternID => 0;

        Bullet b;
        public SingleShot(int team)
        {
            Team = team;
            BulletCount = 1;
            ParentPattern.Add(b = new Bullet(BulletTeam)
            {
                Depth = 1,
                Anchor = Anchor.Centre,
                BulletSpeed = 0,
                BulletAngleRadian = 0,
                BulletColor = PatternColor,
            });
            b.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), b));
        }
    }
    public class ConcaveWave : BulletPattern
    {
        public override int PatternID => 1;


    }
    public class ConvexWave : BulletPattern
    {
        public override int PatternID => 2;


    }
    public class DirectStrike : BulletPattern
    {
        public override int PatternID => 3;


    }
}
