using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics;

namespace osu.Game.Modes.Vitaru.Objects.Drawables.Pieces
{
    class HitboxPiece : Container
    {
        //Different stats for Hitboxes
        public Color4 hitboxColor { get; set; } = Color4.White;
        public float hitboxWidth { get; set; } = 4f;

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
                    BorderColour = hitboxColor,
                    Alpha = 1f,
                    CornerRadius = hitboxWidth / 2,
                    Children = new[]
                    {
                        new Box
                        {
                            Colour = Color4.White,
                            Alpha = 1,
                            Width = hitboxWidth,
                            Height = hitboxWidth,
                        },
                    },
                },
                hitboxContainer = new CircularContainer
                {
                        Origin = Anchor.Centre,
                        Anchor = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Scale = new Vector2(hitboxWidth),
                        Depth = 2,
                        Masking = true,
                        EdgeEffect = new EdgeEffect
                        {
                            Type = EdgeEffectType.Shadow,
                            Colour = (hitboxColor).Opacity(0.4f),
                            Radius = 2f,
                        }
                }
            };
        }
    }
}