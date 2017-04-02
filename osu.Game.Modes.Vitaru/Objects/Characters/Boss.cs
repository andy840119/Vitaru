﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics.Containers;
using OpenTK;
using osu.Game.Modes.Vitaru.Objects.Drawables;
using osu.Framework.Graphics;
using OpenTK.Graphics;
using System.Collections.Generic;

namespace osu.Game.Modes.Vitaru.Objects.Characters
{
    public class Boss : Character
    {
        public Vector2 BossPosition = new Vector2(0, -160);
        public List<BulletPattern> Patterns { get; set; }

        public Boss() { }
        public override HitObjectType Type => HitObjectType.Boss;
    }
}