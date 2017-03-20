using osu.Framework.Graphics.Containers;
using OpenTK;
using OpenTK.Input;
using osu.Game.Modes.Vitaru.Objects.Drawables;
using osu.Framework.Graphics;
using osu.Framework.Input;
using System.Collections.Generic;
using System;
using osu.Game.Modes.Vitaru.Objects.Projectiles;
using OpenTK.Graphics;

namespace osu.Game.Modes.Vitaru.Objects.Characters
{
    public class VitaruPlayer : Character
    {
        /*public void ToggleShoot()
        {
            Shooting = !Shooting;
        }

        public void ToggleKiai()
        {
            KiaiActivated = !KiaiActivated;
        }

        private void Shoot()
        {
                Bullet bullet;
                parent.Add(bullet = new Bullet(Team)
                {
                    Depth = 1,
                    Anchor = Anchor.Centre,
                    BulletAngle = 0f,
                    BulletSpeed = 1f,
                    BulletColor = Color4.Green,
                });
                bullet.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), bullet));
        }

        //saves if key is pressed
        protected override bool OnKeyDown(InputState state, KeyDownEventArgs args)
        {
            keys[args.Key] = true;
            if (args.Key == Key.LShift || args.Key == Key.RShift)
                hitbox.Alpha = 1;
            return base.OnKeyDown(state, args);
        }
        //saves if key is released
        protected override bool OnKeyUp(InputState state, KeyUpEventArgs args)
        {
            keys[args.Key] = false;
            if (args.Key == Key.LShift || args.Key == Key.RShift)
                hitbox.Alpha = 0;
            return base.OnKeyUp(state, args);
        }*/
    }
}