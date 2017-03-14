using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using OpenTK.Graphics;
using OpenTK;
using osu.Game.Graphics;
using osu.Framework.Graphics.Sprites;

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


    class HitboxPiece : Container
    {
        private CircularContainer hitboxContainer;
        private object hitbox;

        public HitboxPiece(Hitbox hitbox)
        {
            this.hitbox = hitbox;
            Children = new Drawable[]
            {
                new Container
                {
                    Masking = true,
                    AutoSizeAxes = Axes.Both,
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    BorderThickness = 3,
                    Depth = 1,
                    BorderColour = hitbox.HitboxColor,
                    Alpha = 1f,
                    CornerRadius = hitbox.HitboxWidth / 2,
                    Children = new[]
                    {
                        new Box
                        {
                            Colour = Color4.White,
                            Alpha = 1,
                            Width = hitbox.HitboxWidth,
                            Height = hitbox.HitboxWidth,
                        },
                    },
                },
                hitboxContainer = new CircularContainer
                {
                        Origin = Anchor.Centre,
                        Anchor = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Scale = new Vector2(hitbox.HitboxWidth),
                        Depth = 2,
                        Masking = true,
                        EdgeEffect = new EdgeEffect
                        {
                            Type = EdgeEffectType.Shadow,
                            Colour = (hitbox.HitboxColor).Opacity(0.4f),
                            Radius = 2f,
                        }
                }
            };
        }
    }
}
