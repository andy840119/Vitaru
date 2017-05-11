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
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Vitaru.Scoring;
using osu.Game.Rulesets.Vitaru.UI;
using osu.Game.Rulesets.Vitaru.Objects.Characters;

namespace osu.Game.Rulesets.Vitaru.Objects.Drawables
{
    public class DrawableVitaruPlayer : DrawableCharacter
    {
        private readonly VitaruPlayer player;
        private Dictionary<Key, bool> keys = new Dictionary<Key, bool>();

        //(MinX,MaxX,MinY,MaxY)
        private Vector4 playerBounds = new Vector4(0, 512, 0, 820);

        public DrawableVitaruPlayer(VitaruPlayer player) : base(player)
        {
            this.player = player;
            keys[Key.Up] = false;
            keys[Key.Right] = false;
            keys[Key.Down] = false;
            keys[Key.Left] = false;
            keys[Key.Z] = false;
            keys[Key.X] = false;
            keys[Key.LShift] = false;
            keys[Key.RShift] = false;
            Origin = Anchor.Centre;
            Position = player.Position;
            CharacterType = HitObjectType.Player;
            CharacterHealth = 100;
            Team = 0;
            HitboxColor = Color4.Yellow;
            HitboxWidth = 4;
            OnShoot = shoot;
        }

        protected override void CheckJudgement(bool userTriggered)
        {
        }

        private const float playerSpeed = 0.5f;
        private Vector2 positionChange = Vector2.Zero;
        public static float Energy;
        public static float Health;

        protected override void Update()
        {
            base.Update();

            HitDetect();

            playerMovement();
        }

        private void playerMovement()
        {
            //Handles Player Speed
            var pos = Position;
            float ySpeed = playerSpeed * (float)(Clock.ElapsedFrameTime);
            float xSpeed = playerSpeed * (float)(Clock.ElapsedFrameTime);

            //All these handle keys and when they are or aren't pressed
            if (keys[Key.LShift] | keys[Key.RShift])
            {
                xSpeed /= 4;
                ySpeed /= 4;
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
            VitaruPlayer.PlayerPosition = pos;
        }

        private void shoot()
        {
            Bullet b;
            VitaruPlayfield.vitaruPlayfield.Add(b = new Bullet(Team)
            {
                Depth = 1,
                Origin = Anchor.Centre,
                BulletSpeed = 1f,
                BulletColor = Color4.Red,
                BulletAngleDegree = 0,
                BulletWidth = 6,
            });
            b.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), b));
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
