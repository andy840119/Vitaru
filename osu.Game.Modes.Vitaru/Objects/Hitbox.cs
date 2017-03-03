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
        public Color4 HitboxColor { get; set; } = Color4.White;
        public float HitboxHealth { get; set; } = 100;
        public float HitboxWidth { get; set; } = 4f;

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
