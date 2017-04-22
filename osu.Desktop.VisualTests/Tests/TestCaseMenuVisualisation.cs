using System;
using OpenTK;
using osu.Framework.Graphics;
using osu.Framework.Testing;
using osu.Framework.MathUtils;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Sprites;
using osu.Game.Screens.Menu;
using OpenTK.Graphics;

namespace osu.Desktop.VisualTests.Tests
{
    class TestCaseMenuVisualisation : TestCase
    {
        public override string Description => @"Main menu music visualizer";

        private MenuVisualisation vis;

        private void ascendingValues()
        {
            float[] data = new float[512];
            for (int i = 0; i < 128; i++)
                data[i] = 0.25f / 128f * i;
            vis.AudioData = data;
        }

        private void descendingValues()
        {
            float[] data = new float[512];
            for (int i = 0; i < 128; i++)
                data[i] = 0.25f / 128f * (128 - i);
            vis.AudioData = data;
        }

        private void fixedRandomValues()
        {
            float[] data = new float[512];
            for (int i = 0; i < 128; i++)
            {
                Random r = new Random(i * i * i / (4 + i) * (i + 5 * (54 + 5 * i) / (i + 1)));
                data[i] = (r.Next(2, 250)) / 1000f;
            }
            vis.AudioData = data;
        }

        private bool random;
        private void totalRandom(bool random)
        {
            this.random = random;
        }

        private void shrink()
        {
            vis.ResizeTo(new Vector2(500, 500), 300, EasingTypes.OutQuad);
        }

        private void expand()
        {
            vis.ResizeTo(new Vector2(951, 500), 300, EasingTypes.OutQuad);
        }

        private void takeLine()
        {
            vis.BarCount.Value--;
        }

        private void addLine()
        {
            vis.BarCount.Value++;
        }

        public override void Reset()
        {
            base.Reset();

            Add(new Box
            {
                ColourInfo = ColourInfo.GradientVertical(Color4.Gray, Color4.WhiteSmoke),
                RelativeSizeAxes = Axes.Both,
            });
            Add(vis = new MenuVisualisation { Scale = Vector2.One });

            AddStep(@"Ascending values", ascendingValues);
            AddStep(@"Descending values", descendingValues);
            AddStep(@"Fixed random values", fixedRandomValues);
            AddStep(@"Smaller", shrink);
            AddStep(@"Normal", expand);
            AddRepeatStep(@"Take lines", takeLine, 10);
            AddRepeatStep(@"Add lines", addLine, 10);
            AddWaitStep(2);
            AddToggleStep(@"Total randomness", totalRandom);
        }

        protected override void Update()
        {
            base.Update();

            if (random)
            {
                float[] data = new float[512];
                for (int j = 0; j < 128; j++)
                    data[j] = (RNG.NextSingle(0.002f, 0.25f));
                vis.AudioData = data;
            }
        }
    }
}
