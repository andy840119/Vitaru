﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System;
using System.Globalization;
using System.IO;
using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Sprites;
using osu.Framework.IO.Stores;
using osu.Framework.Localisation;
using osu.Framework.Testing;

namespace osu.Framework.VisualTests.Tests
{
    internal class TestCaseLocalisation : TestCase
    {
        public override string Description => "Localisation engine";

        private LocalisationEngine engine; //keep a reference to avoid GC of the engine

        public override void Reset()
        {
            base.Reset();

            var config = new FakeFrameworkConfigManager();
            engine = new LocalisationEngine(config);

            engine.AddLanguage("en", new FakeStorage());
            engine.AddLanguage("zh-CHS", new FakeStorage());
            engine.AddLanguage("ja", new FakeStorage());

            Add(new FillFlowContainer<SpriteText>
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(0, 10),
                Padding = new MarginPadding(10),
                AutoSizeAxes = Axes.Both,
                Children = new[]
                {
                    new SpriteText
                    {
                        Text = "Not localisable",
                        TextSize = 48,
                        Colour = Color4.White
                    },
                    new SpriteText
                    {
                        Current = engine.GetLocalisedString("localisable"),
                        TextSize = 48,
                        Colour = Color4.White
                    },
                    new SpriteText
                    {
                        Current = engine.GetUnicodePreference("Unicode on", "Unicode off"),
                        TextSize = 48,
                        Colour = Color4.White
                    },
                    new SpriteText
                    {
                        Current = engine.GetUnicodePreference(null, "I miss unicode"),
                        TextSize = 48,
                        Colour = Color4.White
                    },
                    new SpriteText
                    {
                        Current = engine.Format("{0}", DateTime.Now),
                        TextSize = 48,
                        Colour = Color4.White
                    },
                }
            });

            AddStep("English", () => config.Set(FrameworkConfig.Locale, "en"));
            AddStep("Japanese", () => config.Set(FrameworkConfig.Locale, "ja"));
            AddStep("Simplified Chinese", () => config.Set(FrameworkConfig.Locale, "zh-CHS"));
            AddToggleStep("ShowUnicode", b => config.Set(FrameworkConfig.ShowUnicode, b));
        }

        private class FakeFrameworkConfigManager : FrameworkConfigManager
        {
            protected override string Filename => null;

            public FakeFrameworkConfigManager() : base(null) { }

            protected override void InitialiseDefaults()
            {
                Set(FrameworkConfig.Locale, "");
                Set(FrameworkConfig.ShowUnicode, false);
            }
        }

        private class FakeStorage : IResourceStore<string>
        {
            public string Get(string name) => $"{name} in {CultureInfo.CurrentCulture.EnglishName}";
            public Stream GetStream(string name)
            {
                throw new NotSupportedException();
            }
        }
    }
}
