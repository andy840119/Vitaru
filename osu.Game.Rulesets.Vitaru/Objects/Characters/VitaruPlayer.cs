// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics.Containers;
using OpenTK;
using OpenTK.Input;
using osu.Game.Rulesets.Vitaru.Objects.Drawables;
using osu.Framework.Graphics;
using osu.Framework.Input;
using System.Collections.Generic;
using osu.Game.Rulesets.Vitaru.Objects.Projectiles;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Vitaru.Objects.Characters
{
    public class VitaruPlayer : Character
    {
        /*private bool _kiaiActivated = false;
        public bool KiaiActivated
        {
            get
            {
                return _kiaiActivated;
            }
            set
            {
                _kiaiActivated = value;
                //player.setKiai(value);
            }
        }
        private CharacterSprite player;
        */
        public VitaruPlayer() : base() { }
        public double EndTime { get; set; }
        /*
//Kiai toggle
public void ToggleKiai()
{
KiaiActivated = !KiaiActivated;
}*/

        public override HitObjectType Type => HitObjectType.Player;
    }
}