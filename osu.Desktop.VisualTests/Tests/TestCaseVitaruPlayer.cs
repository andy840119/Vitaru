//Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
//Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Screens.Testing;
using osu.Framework.Graphics;
using OpenTK.Input;
using osu.Game.Modes.Vitaru.Objects.Characters;
using osu.Framework.Graphics.Sprites;
using osu.Game.Screens.Play;
using osu.Game.Modes.Vitaru.Objects.Projectiles;

namespace osu.Desktop.VisualTests.Tests
{
    class TestCaseVitaruPlayer : TestCase
    {
        //private WorkingBeatmap beatmap;
        //private List<HitObject> enemys;

        public override string Description => @"Showing Player";

        private SpriteText health;
        private VitaruPlayer player;
        //private Hitbox hitbox;

        public override void Reset()
        {
            base.Reset();

            player = new VitaruPlayer(this)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            };
            Add(player);

            //AddToggle(@"Toggle Kiai", player.ToggleKiai);

           health = new SpriteText()
            {
                Text = "velocity calculations " + VitaruPlayer.velocityCalculation,
                Anchor = Anchor.TopLeft,
                Origin = Anchor.TopLeft
            };
            Add(health);
        }

        /*private void AddToggle(string v, Action toggleKiai)
        {
            throw new NotImplementedException();
        }*/

        protected override void Update()
        {
            base.Update();
            health.Text = "velocity calculations: " + VitaruPlayer.velocityCalculation;
        }
    }
}