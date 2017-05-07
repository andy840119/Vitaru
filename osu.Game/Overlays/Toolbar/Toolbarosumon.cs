using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace osu.Game.Overlays.Toolbar
{
    class ToolbarOsuMon : ToolbarButton
    {
        private ToolbarButton button;
        private Sprite icon;
        public ToolbarOsuMon()
        {
            Children = new Drawable[]
            {
                icon = new Sprite()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Alpha = 1,
                    Colour = Color4.Pink,
                    Depth = -10,
                    Scale = new Vector2(0.5f),
                },
                button = new ToolbarButton()
                {
                    Depth = 1,
                    TooltipMain = "osu!mon",
                    TooltipSub = "Open osu!mon UI",
                },
            };
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            icon.Texture = textures.Get(@"Menu/osumonIcon");
        }
    }
}
