// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using osu.Game.Modes.Objects;
using OpenTK;

namespace osu.Game.Modes.Vitaru.Objects
{
    public abstract class VitaruHitObject : HitObject
    {
        //All of this can probably go once we actually get into making this work with maps
        public Vector2 Position { get; set; }

        public float Scale { get; set; } = 1;

        public virtual Vector2 EndPosition => Position;

        [Flags]
        internal enum HitObjectType
        {
            Dart = 1,
            Wave = 2,
            NewCombo = 4,
            DartNewCombo = 5,
            WaveNewCombo = 6,
            Flower = 8,
            ColourHax = 122,
            Hold = 128,
            ManiaLong = 128,
        }
    }
}