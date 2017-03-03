using osu.Framework.Graphics.Containers;
using OpenTK;
using OpenTK.Input;
using osu.Game.Modes.Vitaru.Objects.Drawables;
using osu.Framework.Graphics;
using osu.Framework.Input;
using osu.Framework.Graphics.Colour;
using osu.Game.Modes.Vitaru.Objects.Drawables.Pieces;
using OpenTK.Graphics;

namespace osu.Game.Modes.Vitaru.Objects
{
    public class Hitbox : Container
    {
        //Different stats for Hitboxes
        public Color4 hitboxColor { get; set; } = Color4.White;
        public float hitboxHealth { get; set; } = 100;
        public float hitboxWidth { get; set; } = 4f;

        private DrawableHitbox hitbox;

        public Hitbox()
        {
            Children = new[]
            {
                hitbox = new DrawableHitbox(this)
                {
                    Origin = Anchor.Centre,
                },
            };
            Hide();
        }
    }
}
