// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using osu.Framework.Graphics.Containers;
using osu.Game.Modes.Vitaru.Objects.Projectiles;

namespace osu.Game.Modes.Vitaru.Objects
{
    public abstract class BulletPattern : Container
    {
        public int PatternID { get; set; } = 0;
    }
    public class ConcaveWave : BulletPattern
    {

    }
    public class ConvexWave : BulletPattern
    {

    }
    public class DirectStrike : BulletPattern
    {

    }
}
