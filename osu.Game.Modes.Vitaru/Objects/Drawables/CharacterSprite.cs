// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace osu.Game.Modes.Vitaru.Objects.Drawables
{
    public class CharacterSprite : Container
    {
        public Sprite Sprite;
        public string CharacterName;

        public CharacterSprite()
        {
            Sprite = new Sprite()
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            };
            Add(Sprite);
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            Sprite.Texture = textures.Get(@"Play/Vitaru/" + CharacterName);
        }
    }
}