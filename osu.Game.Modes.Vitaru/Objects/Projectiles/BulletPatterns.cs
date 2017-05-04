// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics.Containers;
using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Vitaru.Objects.Drawables;
using System;

namespace osu.Game.Rulesets.Vitaru.Objects.Projectiles
{
    public abstract class BulletPattern : Container
    {
        public abstract int PatternID { get; }
        public float PatternSpeed { get; set; } = 0.2f;
        public float PatternAngle { get; set; } = MathHelper.Pi;
        public int PatternAngleRadian { get; set; }
        public int PatternAngleDegree { get; set; }
        public int Team { get; set; }

        public int BulletTeam { get; set; } = 1;
        public Color4 PatternColor { get; set; } = Color4.White;
        public int BulletCount { get; set; } = 0;

        public BulletPattern()
        {

        }

        protected override void Update()
        {
            base.Update();

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
