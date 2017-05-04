using osu.Framework.Graphics.Containers;
using OpenTK;
using OpenTK.Input;
using osu.Game.Rulesets.Vitaru.Objects.Drawables;
using osu.Framework.Graphics;
using osu.Framework.Input;
using System.Collections.Generic;
using System;
using osu.Game.Rulesets.Vitaru.Objects.Projectiles;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Vitaru.Objects.Drawables
{
    public class DrawableVitaruPlayer : DrawableCharacter
    {
        private Dictionary<Key, bool> keys = new Dictionary<Key, bool>();
        
        public static Vector2 PlayerPosition;

        //(MinX,MaxX,MinY,MaxY)
        private Vector4 playerBounds = new Vector4(0, 512, -20, 720);

        public DrawableVitaruPlayer(VitaruHitObject hitObject) : base(hitObject)
        {
            keys[Key.Up] = false;
            keys[Key.Right] = false;
            keys[Key.Down] = false;
            keys[Key.Left] = false;
            keys[Key.Z] = false;
            keys[Key.X] = false;
            keys[Key.LShift] = false;
            keys[Key.RShift] = false;
            Origin = Anchor.Centre;
            Position = PlayerPosition;
            CharacterType = HitObjectType.Player;
            CharacterHealth = 100;
            Team = 0;
            HitboxColor = Color4.Cyan;
            HitboxWidth = 8;
            OnShoot = shoot;
            Anchor = Anchor.Centre;
        }

        private const float playerSpeed = 0.25f;
        private Vector2 positionChange = Vector2.Zero;

        protected override void Update()
        {
            base.Update();

            //Handles Player Speed
            var pos = Position;
            float ySpeed = 0.5f * (float)(Clock.ElapsedFrameTime);
            float xSpeed = 0.5f * (float)(Clock.ElapsedFrameTime);

            //All these handle keys and when they are or aren't pressed
            if (keys[Key.LShift] | keys[Key.RShift])
            {
                xSpeed /= 2;
                ySpeed /= 2;
            }
            if (keys[Key.Z])
            {
                Shooting = true;
            }
            if (keys[Key.Z] == false)
            {
                Shooting = false;
            }
            if (keys[Key.X])
            {
                //Bass();
            }
            if (keys[Key.Up])
            {
                pos.Y -= ySpeed;
            }
            if (keys[Key.Left])
            {
                pos.X -= xSpeed;
            }
            if (keys[Key.Down])
            {
                pos.Y += ySpeed;
            }
            if (keys[Key.Right])
            {
                pos.X += xSpeed;
            }

            pos = Vector2.ComponentMin(pos, playerBounds.Yw);
            pos = Vector2.ComponentMax(pos, playerBounds.Xz);
            Position = pos;
            //PlayerPosition = pos;
        }

        private void shoot()
        {
            if (MainParent == null)
            {
                throw new Exception();
            }
            if (MainParent != null)
            {
                Bullet b;
                MainParent.Add(b = new Bullet(Team)
                {
                    Depth = 1,
                    Anchor = Anchor.Centre,
                    BulletSpeed = 0.1f,
                    BulletAngleRadian = 0,
                });
                b.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), b));
            }
        }

        protected override bool OnKeyDown(InputState state, KeyDownEventArgs args)
        {
            keys[args.Key] = true;
            if (args.Key == Key.LShift || args.Key == Key.RShift)
                Hitbox.Alpha = 1;
            return base.OnKeyDown(state, args);
        }
        protected override bool OnKeyUp(InputState state, KeyUpEventArgs args)
        {
            keys[args.Key] = false;
            if (args.Key == Key.LShift || args.Key == Key.RShift)
                Hitbox.Alpha = 0;
            return base.OnKeyUp(state, args);
        }
    }
}
