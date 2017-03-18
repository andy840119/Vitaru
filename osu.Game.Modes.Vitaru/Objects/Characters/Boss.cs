using osu.Framework.Graphics.Containers;
using OpenTK;
using OpenTK.Input;
using osu.Game.Modes.Vitaru.Objects.Drawables;
using osu.Framework.Graphics;
using osu.Framework.Input;
using System.Collections.Generic;
using System;
using OpenTK.Graphics;

namespace osu.Game.Modes.Vitaru.Objects.Characters
{
    public class Boss : Character
    {
        public override Vector2 Speed => new Vector2(1, 1);
    }
}