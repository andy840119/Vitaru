using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Game.Configuration;
using osu.Game.Graphics.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Overlays.Options.Sections.Vitaru
{
    class OnlineOptions : OptionsSubsection
    {
        protected override string Header => "Online";

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager osuConfig, FrameworkConfigManager config)
        {
            Children = new Drawable[]
            {
                new OsuCheckbox
                {
                    LabelText = "Allow player skins",
                    Bindable = osuConfig.GetBindable<bool>(OsuConfig.AllowVitaruSkins),
                },
            };
        }
    }
}