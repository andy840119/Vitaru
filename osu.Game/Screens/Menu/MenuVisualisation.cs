// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Framework.Configuration;
using osu.Framework.Graphics.Containers;
using OpenTK;

namespace osu.Game.Screens.Menu
{
    public class MenuVisualisation : Container
    {
        public Bindable<int> BarCount = new Bindable<int>();
        public float[] AudioData;
        private Container visBars = new Container { RelativeSizeAxes = Axes.Both };
        private int barCount;

        public MenuVisualisation(int barCount = 130)
        {
            AudioData = new float[512];
            BarCount.Value = barCount;
            Size = new Vector2(921, 500);
            Scale = new Vector2(1, 0);
            Anchor = Anchor.Centre;
            Origin = Anchor.BottomCentre;
            BlendingMode = BlendingMode.Additive;

            this.barCount = BarCount.Value;
            BarCount.ValueChanged += recalcBars;
        }

        private void recalcBars(object sender, System.EventArgs e)
        {
            if (BarCount > barCount)
                for (int i = 0; i < (BarCount - barCount); i++)
                    visBars.Add(new VisualisationBar
                    {
                        Width = (Size.X - ((BarCount - 1) * 2)) / BarCount,
                        Position = new Vector2(((Width / 2) + (BarCount * 2) + (BarCount * Width)) - (Size.X / 2), 0),
                    });
            else if (BarCount < barCount)
            {
                for (int i = 0; i < (barCount - BarCount); i++)
                {
                    int j = 0;
                    foreach (Drawable d in visBars.Children)
                    {
                        if (d is VisualisationBar && j == barCount - 1)
                            d.Dispose();
                        j++;
                    }
                }
            }
            barCount = BarCount.Value;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            float barWidth = (Size.X - ((barCount - 1) * 2)) / barCount;
            for (int i = 0; i < barCount; i++)
            {
                visBars.Add(new VisualisationBar
                {
                    Width = barWidth,
                    Position = new Vector2(((barWidth / 2) + (i * 2) + (i * barWidth)) - (Size.X / 2), 0),
                    Scale = (new Vector2(1, 0)),
                });
            }
            Add(visBars);
        }
        
        protected override void Update()
        {
            base.Update();

            int i = 0;
            foreach (Drawable d in visBars.Children)
            {
                if (d is VisualisationBar)
                {
                    d.Width = (Size.X - ((BarCount - 1) * 2)) / BarCount;
                    d.MoveToX(((d.Width / 2) + (i * 2) + (i * d.Width)) - (Size.X / 2), 30);
                    if (AudioData != null)
                    {
                        if (AudioData[i] * 3 >= d.Scale.Y)
                            d.ScaleTo(new Vector2(1, AudioData[i] * 3), 30);
                        else if (d.Scale.Y > 0f)
                            d.ScaleTo(new Vector2(1, d.Scale.Y - (d.Scale.Y * 0.2f)), 50);
                        else d.Scale = new Vector2(1, 0);
                    }
                    d.Alpha = MathHelper.Clamp(d.Scale.Y * 10f, 0f, 1f);
                    i++;
                }

            }

        }
    }
}
