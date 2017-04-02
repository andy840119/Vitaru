// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics.Containers;

namespace osu.Game.Modes.Vitaru.Objects
{
    public abstract class BulletPattern : VitaruHitObject
    {
        public int PatternID { get; set; } = 0;
       /* public float patternAngle;
        public int Team;

        public void SlowingStream(Container parent)
        {
            for (int i = 0; i < 6; i++)
            {
                Bullet bullet;
                parent.Add(bullet = new Bullet(Team)
                {
                    Depth = 1,
                    Anchor = Anchor.Centre,
                    BulletAngle = patternAngle,
                    BulletSpeed = i/8,
                    BulletColor = Color4.Green,
                });
                bullet.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), bullet));
            }
        }*/
    }
}
