// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics.Containers;
using System;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Vitaru.Objects.Projectiles;
using OpenTK;

namespace osu.Game.Rulesets.Vitaru.Objects.Characters
{
    public abstract class Character : VitaruHitObject
    {
        public float CharacterHealth { get; set; } = 100;
        public float Armor { get; internal set; } = 1; //All damage taken should be divided by this number. During kiai player will only take half damage so [2]
        public int Team { get; set; } = 0; // 0 = Player, 1 = Ememies + Boss(s) in Singleplayer
        public int ProjectileDamage { get; set; }
        public Spawn SpawnPoint { get; set; }

        public Character() { }
    }

    /// <summary>
    /// Where the <see cref="Character"/> will spawn, based on the area it can be on.
    /// </summary>
    [Flags]
    public enum Spawn
    {
        TopLeft = y0 | x0,
        TopCentre = y0 | x1,
        TopRight = y0 | x2,

        CentreLeft = y1 | x0,
        Centre = y1 | x1,
        CentreRight = y1 | x2,

        BottomLeft = y2 | x0,
        BottomCentre = y2 | x1,
        BottomRight = y2 | x2,
        y0 = 1 << 0,
        y1 = 1 << 1,
        y2 = 1 << 2,
        x0 = 1 << 3,
        x1 = 1 << 4,
        x2 = 1 << 5,

        /// <summary>
        /// The spawn will be set at the position of the <see cref="Character"/>
        /// </summary>
        Self = 1 << 6,
    }
}
