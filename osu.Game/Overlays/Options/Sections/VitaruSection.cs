using osu.Framework.Graphics;
using osu.Game.Graphics;
using osu.Game.Overlays.Options.Sections.Vitaru;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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