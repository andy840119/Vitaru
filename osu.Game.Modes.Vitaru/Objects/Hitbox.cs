using osu.Framework.Graphics.Containers;
using osu.Game.Modes.Vitaru.Objects.Drawables;
using osu.Framework.Graphics;
using OpenTK.Graphics;

namespace osu.Game.Modes.Vitaru.Objects
{
    public class Hitbox : Container
    {
        //Different stats for Hitboxes
        public Color4 HitboxColor { get; set; } = Color4.Cyan;
        public float HitboxHealth { get; set; } = 100;
        public float HitboxWidth { get; set; } = 8f;

        private HitboxPiece hitbox;

        public Hitbox()
        {
            Children = new[]
            {
                hitbox = new HitboxPiece(this)
                {
                    Origin = Anchor.Centre,
                },
            };
            Hide();
        }
    }
}
