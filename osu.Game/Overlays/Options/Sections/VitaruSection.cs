// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Game.Graphics;
using osu.Game.Overlays.Options.Sections.Vitaru;

namespace osu.Game.Overlays.Options.Sections
{
    class VitaruSection : OptionsSection
    {
        public override string Header => "Vitaru";
        public override FontAwesome Icon => FontAwesome.fa_osu_vitaru_o;

        public VitaruSection()
        {
            Children = new Drawable[]
            {
                new GameOptions(),
                new OnlineOptions(),
            };
        }
    }
}