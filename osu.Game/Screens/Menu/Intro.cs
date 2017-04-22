// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Threading.Tasks;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Sample;
using osu.Framework.Audio.Track;
using osu.Framework.Configuration;
using osu.Framework.MathUtils;
using osu.Framework.Screens;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Containers;
using osu.Game.Beatmaps;
using osu.Game.Configuration;
using osu.Game.Database;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Screens.Backgrounds;
using OpenTK.Graphics;
using OpenTK;

namespace osu.Game.Screens.Menu
{
    public class Intro : OsuScreen
    {
        private OsuLogo logo;

        private SpriteText w;
        private SpriteText e;
        private SpriteText l;
        private SpriteText c;
        private SpriteText o;
        private SpriteText m;
        private SpriteText e_;

        private int textSize = 60;
        private float textSpace = 1.5f;

        private CircularContainer lowCircleGray;
        private CircularContainer lowCircleBlack;
        private CircularContainer highCircleBlack;
        private CircularContainer highCircleWhite;

        private CircularContainer borderContainer;
        private Box verticalBorderBox;
        private Box horizontalBorderBox;

        private CircularContainer pinkCircle;
        private CircularContainer blueCircle;
        private CircularContainer yellowCircle;
        private CircularContainer purpleCircle;

        private List<Container> lines = new List<Container>();
        private List<Drawable> welcomeWord = new List<Drawable>();

        /// <summary>
        /// Whether we have loaded the menu previously.
        /// </summary>
        internal bool DidLoadMenu;

        MainMenu mainMenu;
        private SampleChannel welcome;
        private SampleChannel seeya;
        private Track bgm;

        internal override bool ShowOverlays => false;

        protected override BackgroundScreen CreateBackground() => new BackgroundScreenEmpty();

        public Intro()
        {
            Children = new Drawable[]
            {
                new ParallaxContainer
                {
                    ParallaxAmount = 0.01f,
                    Children = new Drawable[]
                    {
                        lowCircleGray = new CircularContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(0),
                            Masking = true,
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = new Color4(180,180,180,100),
                                },
                            },
                        },
                        lowCircleBlack = new CircularContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(0),
                            Masking = true,
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = Color4.Black,
                                },
                            },
                        },
                        highCircleWhite = new CircularContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(0),
                            Masking = true,
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = Color4.White,
                                },
                            },
                        },
                        highCircleBlack = new CircularContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(0),
                            Masking = true,
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = Color4.Black,
                                },
                            },
                        },
                        w = new SpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Font = @"Exo2.0-regular",
                            Text = "w",
                            TextSize = textSize,
                            Position = new Vector2(-60 * textSpace, 0),
                        },
                        e = new SpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Font = @"Exo2.0-regular",
                            Text = "e",
                            TextSize = textSize,
                            Position = new Vector2(-37.5f * textSpace, 0),
                        },
                        l = new SpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Font = @"Exo2.0-regular",
                            Text = "l",
                            TextSize = textSize,
                            Position = new Vector2(-19 * textSpace, 0),
                        },
                        c = new SpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Font = @"Exo2.0-regular",
                            Text = "c",
                            TextSize = textSize,
                            Position = new Vector2(-3f * textSpace, 0),
                        },
                        o = new SpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Font = @"Exo2.0-regular",
                            Text = "o",
                            TextSize = textSize,
                            Position = new Vector2(16.5f * textSpace, 0),
                        },
                        m = new SpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Font = @"Exo2.0-regular",
                            Text = "m",
                            TextSize = textSize,
                            Position = new Vector2(38f * textSpace, 0),
                        },
                        e_ = new SpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Font = @"Exo2.0-regular",
                            Text = "e",
                            TextSize = textSize,
                            Position = new Vector2(60 * textSpace, 0),
                        },
                        borderContainer = new CircularContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(463),
                            Masking = true,
                            Children = new Drawable[]
                            {
                                horizontalBorderBox = new Box
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Size = new Vector2(463, 0),
                                    Colour = Color4.White,
                                    Alpha = 0.25f,
                                },
                                verticalBorderBox = new Box
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Size = new Vector2(0, 463),
                                    Colour = Color4.White,
                                    Alpha = 0.4f,
                                },
                            },
                        },
                        purpleCircle = new CircularContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(0),
                            Masking = true,
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = OsuColour.FromHex(@"aa88ff"),
                                },
                            },
                        },
                        yellowCircle = new CircularContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(0),
                            Masking = true,
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = OsuColour.FromHex(@"ffdd55"),
                                },
                            },
                        },
                        blueCircle = new CircularContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(0),
                            Masking = true,
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = OsuColour.FromHex(@"99eeff"),
                                },
                            },
                        },
                        pinkCircle = new CircularContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Masking = true,
                            Size = new Vector2(0),
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = OsuColour.FromHex(@"e967a1"),
                                },
                            },
                        },
                        logo = new OsuLogo
                        {
                            Alpha = 0,
                            Triangles = true,
                            BlendingMode = BlendingMode.Additive,
                            Interactive = false,
                            //Colour = Color4.DarkGray,
                            Ripple = false,
                        },
                    }
                }
            };
            for (int i = 0; i < 4; i++)
            {
                lines.Add(new Container
                {
                    Size = new Vector2(100, 2),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.CentreLeft,
                    Children = new[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Color4.White,
                        }
                    }
                });
                lines[i].Rotation = i * 90 + 45;
                lines[i].Position = new Vector2(i == 0 || i == 3 ? -90 : 90, i < 2 ? -90 : 90);
                lines[i].Colour = i == 1 ? new Color4(1f, 1f, 1f, 0.45f) : Color4.White;
                Content.Add(lines[i]);
            }
            welcomeWord.Add(w);
            welcomeWord.Add(e);
            welcomeWord.Add(l);
            welcomeWord.Add(c);
            welcomeWord.Add(o);
            welcomeWord.Add(m);
            welcomeWord.Add(e_);
        }

        private Bindable<bool> menuVoice;
        private Bindable<bool> menuMusic;
        private TrackManager trackManager;
        private BeatmapInfo beatmap;
        private WorkingBeatmap song;
        private int choosableBeatmapsetAmmout;

        [BackgroundDependencyLoader]
        private void load(OsuGameBase game, AudioManager audio, OsuConfigManager config, BeatmapDatabase beatmaps)
        {
            menuVoice = config.GetBindable<bool>(OsuConfig.MenuVoice);
            menuMusic = config.GetBindable<bool>(OsuConfig.MenuMusic);
            if (!menuMusic)
            {
                trackManager = game.Audio.Track;
                choosableBeatmapsetAmmout = beatmaps.Query<BeatmapSetInfo>().Count();
                if (choosableBeatmapsetAmmout > 0)
                {
                    beatmap = beatmaps.GetWithChildren<BeatmapSetInfo>(RNG.Next(1, choosableBeatmapsetAmmout)).Beatmaps[0];
                    song = beatmaps.GetWorkingBeatmap(beatmap);
                    Beatmap = song;
                }
            }

            bgm = audio.Track.Get(@"circles");
            bgm.Looping = true;

            welcome = audio.Sample.Get(@"welcome");
            seeya = audio.Sample.Get(@"seeya");

        }

        protected override void OnEntering(Screen last)
        {
            base.OnEntering(last);

            logo.ScaleTo(1f);
            logo.FadeOut();
            logo.Delay(2300);
            logo.FadeIn(200);

            foreach (var let in welcomeWord)
            {
                let.FadeOut();
                let.Delay(200);
                let.MoveToX(let.Position.X * 1.5f, 1200, EasingTypes.OutCirc);
                let.FadeIn(500, EasingTypes.OutQuad);
                let.Delay(2300);
                let.FadeOut();
            }
            foreach (var l in lines)
            {
                l.FadeOut();
                l.Delay(175);
                l.ResizeTo(new Vector2(0, 2), 600, EasingTypes.OutCubic);
                l.MoveTo(l.Position * 1.3f, 1000, EasingTypes.OutCirc);
                l.FadeIn(200);
            }

            lowCircleGray.ResizeTo(125, 350, EasingTypes.InOutQuint);
            lowCircleGray.Delay(360);
            lowCircleGray.FadeOut();
            lowCircleBlack.ResizeTo(125, 350, EasingTypes.InOutQuad);
            lowCircleBlack.Delay(360);
            lowCircleBlack.FadeOut();

            highCircleWhite.ResizeTo(55, 550, EasingTypes.InOutQuint);
            highCircleWhite.Delay(1100);
            highCircleWhite.ResizeTo(0);
            highCircleWhite.Delay(100);
            highCircleWhite.ResizeTo(400, 500, EasingTypes.OutQuart);
            highCircleWhite.Delay(510);
            highCircleWhite.FadeOut();
            highCircleBlack.ResizeTo(56, 550, EasingTypes.InOutQuart);
            highCircleBlack.Delay(1100);
            highCircleBlack.ResizeTo(0);
            highCircleBlack.Delay(100);
            highCircleBlack.ResizeTo(400, 500, EasingTypes.OutQuad);
            highCircleBlack.Delay(510);
            highCircleBlack.FadeOut();

            horizontalBorderBox.Delay(1400);
            horizontalBorderBox.ResizeTo(463, 433, EasingTypes.InCirc);
            horizontalBorderBox.RotateTo(-90, 433, EasingTypes.InCirc);
            horizontalBorderBox.FadeIn(433, EasingTypes.InCirc);
            horizontalBorderBox.Delay(1200);
            horizontalBorderBox.FadeOut();
            verticalBorderBox.Delay(1400);
            verticalBorderBox.ResizeTo(463, 433, EasingTypes.InCirc);
            verticalBorderBox.RotateTo(-90, 433, EasingTypes.InCirc);
            verticalBorderBox.FadeIn(433, EasingTypes.InCirc);
            verticalBorderBox.Delay(1200);
            verticalBorderBox.FadeOut();

            purpleCircle.Delay(1450);
            purpleCircle.ResizeTo(420, 150, EasingTypes.OutQuart);
            purpleCircle.Delay(800);
            purpleCircle.FadeOut();
            yellowCircle.Delay(1600);
            yellowCircle.ResizeTo(420, 150, EasingTypes.OutQuart);
            yellowCircle.Delay(650);
            yellowCircle.FadeOut();
            blueCircle.Delay(1750);
            blueCircle.ResizeTo(420, 150, EasingTypes.OutQuart);
            blueCircle.Delay(500);
            blueCircle.FadeOut();
            pinkCircle.Delay(1900);
            pinkCircle.ResizeTo(420, 150, EasingTypes.OutQuart);
            pinkCircle.Delay(350);
            pinkCircle.FadeOut();

            /*logo.ScaleTo(0.4f);
            logo.FadeOut();

            logo.ScaleTo(1, 4400, EasingTypes.OutQuint);
            logo.FadeIn(20000, EasingTypes.OutQuint);*/
            if (menuVoice)
                welcome.Play();

            Scheduler.AddDelayed(delegate
            {
                if (menuMusic)
                    bgm.Start();
                else if (song != null)
                {
                    Task.Run(() =>
                    {
                        trackManager.SetExclusive(song.Track);
                        song.Track.Seek(beatmap.Metadata.PreviewTime);
                        if (beatmap.Metadata.PreviewTime == -1)
                            song.Track.Seek(song.Track.Length * .4f);
                    });
                }

                LoadComponentAsync(mainMenu = new MainMenu());

                Scheduler.AddDelayed(delegate
                {
                    if (!menuMusic && song != null)
                        Task.Run(() => song.Track.Start());
                    DidLoadMenu = true;
                    Push(mainMenu);
                }, 2300);
            }, 600);
        }

        protected override void OnSuspending(Screen next)
        {
            Content.FadeOut(300);
            base.OnSuspending(next);
        }

        protected override bool OnExiting(Screen next)
        {
            //cancel exiting if we haven't loaded the menu yet.
            return !DidLoadMenu;
        }

        protected override void OnResuming(Screen last)
        {
            if (!(last is MainMenu))
                Content.FadeIn(300);

            double fadeOutTime = 2000;
            //we also handle the exit transition.
            if (menuVoice)
                seeya.Play();
            else
                fadeOutTime = 500;

            Scheduler.AddDelayed(Exit, fadeOutTime);

            //don't want to fade out completely else we will stop running updates and shit will hit the fan.
            Game.FadeTo(0.01f, fadeOutTime);

            base.OnResuming(last);
        }

        protected override void Update()
        {
            base.Update();
            if (bgm.IsRunning)
            {
                mainMenu.buttons.VisualisationData = bgm.GetChannelData();
                mainMenu.buttons.osuLogo.visualiser.AudioData = bgm.GetChannelData();
                logo.visualiser.AudioData = bgm.GetChannelData();
            }
        }
    }
}
