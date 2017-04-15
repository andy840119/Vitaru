using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics.Backgrounds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Game.Modes.Vitaru.Objects.Drawables.Pieces
{
    class PlayerPiece : Container
    {
        private float playerSize = 24f;

        public PlayerPiece()
        {
            Children = new Drawable[]
            {
                new Container
                {
                    Position = new Vector2(0,-4),
                    Masking = true,
                    AutoSizeAxes = Axes.Both,
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    BorderThickness = 3,
                    Depth = 1,
                    BorderColour = Color4.GreenYellow,
                    Alpha = 1f,
                    CornerRadius = playerSize / 2,
                    EdgeEffect = new EdgeEffect
                    {
                        Type = EdgeEffectType.Shadow,
                        Colour = Color4.GreenYellow.Opacity(0.25f),
                        Radius = (playerSize / 6),
                    },
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Depth = -1,
                            Colour = Color4.White.Opacity(0),
                            Alpha = 1,
                            Width = playerSize,
                            Height = playerSize,
                        },
                        new Triangles
                        {
                            Depth = 5,
                            TriangleScale = 4,
                            ColourLight = Color4.Yellow,
                            ColourDark = Color4.Green,
                            RelativeSizeAxes = Axes.Both,
                        },
                    },
                },
                new Container
                {
                    Position = new Vector2(-5,5),
                    Masking = true,
                    AutoSizeAxes = Axes.Both,
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    BorderThickness = 3,
                    Depth = 1,
                    BorderColour = Color4.GreenYellow,
                    Alpha = 1f,
                    CornerRadius = playerSize / 2,
                    EdgeEffect = new EdgeEffect
                    {
                        Type = EdgeEffectType.Shadow,
                        Colour = Color4.GreenYellow.Opacity(0.25f),
                        Radius = (playerSize / 6),
                    },
                    Children = new[]
                    {
                        new Box
                        {
                            Depth = -1,
                            Colour = Color4.White.Opacity(0),
                            Alpha = 1,
                            Width = playerSize,
                            Height = playerSize,
                        },
                    },
                },
                new Container
                {
                    Position = new Vector2(5,5),
                    Masking = true,
                    AutoSizeAxes = Axes.Both,
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    BorderThickness = 3,
                    Depth = 1,
                    BorderColour = Color4.GreenYellow,
                    Alpha = 1f,
                    CornerRadius = playerSize / 2,
                    EdgeEffect = new EdgeEffect
                    {
                        Type = EdgeEffectType.Shadow,
                        Colour = Color4.GreenYellow.Opacity(0.25f),
                        Radius = (playerSize / 6),
                    },
                    Children = new[]
                    {
                        new Box
                        {
                            Depth = -1,
                            Colour = Color4.White.Opacity(0),
                            Alpha = 1,
                            Width = playerSize,
                            Height = playerSize,
                        },
                    },
                },
            };
        }
    }
}