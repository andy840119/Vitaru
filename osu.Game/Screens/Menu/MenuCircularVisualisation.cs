// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Framework.Configuration;
using osu.Framework.Graphics.Containers;
using OpenTK;
using System;

namespace osu.Game.Screens.Menu
{
    public class MenuCircularVisualisation : Container
    {
        public Bindable<int> BarCount = new Bindable<int>();
        public float[] AudioData;
        private float[] usableAudioData;
        private Container visBars = new Container
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            RelativeSizeAxes = Axes.Both
        };
        private int barCount;

        public MenuCircularVisualisation(int barCount = 130)
        {
            usableAudioData = new float[512];
            AudioData = new float[512];
            BarCount.Value = barCount;
            Size = new Vector2(300);
            Scale = Vector2.One;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            BlendingMode = BlendingMode.Additive;

            this.barCount = BarCount.Value;

        }

        private float barWidth;

        protected override void LoadComplete()
        {
            barWidth = Size.X * (float)Math.Sqrt(2f * (1f - Math.Cos(MathHelper.DegreesToRadians(360f / BarCount.Value)))) / 2f;
            for (int i = 0; i < barCount; i++)
            {
                visBars.Add(new VisualisationBar
                {
                    RelativePositionAxes = Axes.Both,
                    Width = barWidth,
                    Rotation = (360f / BarCount.Value * i) - 180f,
                    Position = new Vector2(
                        -(float)Math.Sin((float)i / BarCount * 2 * MathHelper.Pi) / 2,
                        -0.5f + (float)Math.Cos((float)i / BarCount * 2 * MathHelper.Pi) / 2
                    ),
                    Scale = (new Vector2(1, 0)),
                });
            }
            Add(visBars);
        }

        protected override void Update()
        {
            base.Update();

            usableAudioData = new float[512];
            for(int j = 0; j < BarCount.Value; j+= 26)
            {
                for(int k = 0; k < BarCount.Value; k++)
                    usableAudioData[k + j > BarCount.Value - 1 ? k + j - BarCount.Value : k + j] += (float)Math.Pow(AudioData[k], 1.3);
            }


            int i = 0;
            foreach (Drawable d in visBars.Children)
            {
                if (d is VisualisationBar)
                {
                    if (usableAudioData != null)
                    {
                        if (usableAudioData[i] * 3 >= d.Scale.Y)
                            d.ScaleTo(new Vector2(1, usableAudioData[i] * 3), 30);
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
