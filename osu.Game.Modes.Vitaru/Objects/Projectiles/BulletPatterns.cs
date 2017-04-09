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
        public Container MainParent = DrawableVitaruCharacter.playfield;
        public int BulletTeam { get; set; } = 1;
        public Color4 PatternColor { get; set; } = Color4.White;
    }
    public class SingleShot : BulletPattern
    {
        public override int PatternID => 0;

        Bullet b;
        public SingleShot()
        {

            MainParent.Add(b = new Bullet(BulletTeam)
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


/*
 *             Bullet b;
            MainParent.Add(b = new Bullet(Team)
            {
                Depth = 1,
                Anchor = Anchor.Centre,
                BulletSpeed = 0.2f,
                BulletAngleRadian = playerRelativePositionAngle(),
                BulletColor = Color4.Red,
            });
            b.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), b));

*/