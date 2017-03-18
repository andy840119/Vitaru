using osu.Framework.Graphics;
using osu.Game.Graphics.UserInterface;
using osu.Game.Modes.UI;
using osu.Game.Modes.Osu.UI;
using OpenTK;
using OpenTK.Input;
using osu.Framework.Graphics.Primitives;
using osu.Game.Screens.Play;

namespace osu.Game.Modes.Vitaru
{
    internal class VitaruScoreOverlay : ScoreOverlay
    {
        protected override PercentageCounter CreateAccuracyCounter() => new PercentageCounter()
        {
            Anchor = Anchor.TopCentre,
            Origin = Anchor.TopCentre,
            Position = new Vector2(0, 65),
            TextSize = 20,
            Margin = new MarginPadding { Right = 5 },
        };

        protected override ComboCounter CreateComboCounter() => new OsuComboCounter()
        {
            Anchor = Anchor.BottomLeft,
            Origin = Anchor.BottomLeft,
        };

        protected override KeyCounterCollection CreateKeyCounter() => new KeyCounterCollection
        {
            IsCounting = true,
            FadeTime = 50,
            Anchor = Anchor.BottomRight,
            Origin = Anchor.BottomRight,
            Margin = new MarginPadding(10),
            Children = new KeyCounter[]
            {
                new KeyCounterKeyboard(@"W", Key.W),
                new KeyCounterKeyboard(@"A", Key.A),
                new KeyCounterKeyboard(@"S", Key.S),
                new KeyCounterKeyboard(@"D", Key.D),
            }
        };

        protected override ScoreCounter CreateScoreCounter() => new ScoreCounter(6)
        {
            Anchor = Anchor.TopCentre,
            Origin = Anchor.TopCentre,
            TextSize = 40,
            Position = new Vector2(0, 30),
            Margin = new MarginPadding { Right = 5 },
        };
    }
}