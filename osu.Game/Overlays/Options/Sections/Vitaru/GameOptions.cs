// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Game.Configuration;
using osu.Game.Graphics.UserInterface;

namespace osu.Game.Overlays.Options.Sections.Vitaru
{
    class GameOptions : OptionsSubsection
    {
        protected override string Header => "Gameplay";

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager osuConfig, FrameworkConfigManager config)
        {
            Children = new Drawable[]
            {
                new OsuCheckbox
                {
                    LabelText = "Stop PC Death",
                    Bindable = osuConfig.GetBindable<bool>(OsuConfig.StopPcDeath),
                },
            };
        }
    }
}