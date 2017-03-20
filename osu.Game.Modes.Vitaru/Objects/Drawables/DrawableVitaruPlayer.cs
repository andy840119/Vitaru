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

namespace osu.Game.Modes.Vitaru.Objects.Drawables
{
    public class DrawableVitaruPlayer : DrawableVitaruCharacter
    {
        public DrawableVitaruPlayer(VitaruHitObject hitObject) : base(hitObject)
        {
            Position = new Vector2(200);
            CharacterType = CharacterType.Boss;
            characterHealth = 100;
            Team = 0;
            HitboxColor = Color4.Cyan;
            HitboxWidth = 8;
        }

        private const float playerSpeed = 0.5f;
        private Vector2 positionChange = Vector2.Zero;
        private bool isHalfSpeed = false;

        protected override void Update()
        {
            base.Update();
            if (isHalfSpeed)
            {
                ShowHitbox();
            }
            else
                HideHitbox();
            Position = new Vector2
            (
                MathHelper.Clamp(Position.X + positionChange.X * (float)Clock.ElapsedFrameTime, -200, 300),
                MathHelper.Clamp(Position.Y + positionChange.Y * (float)Clock.ElapsedFrameTime, 0, 300)
            );
        }
        protected override bool OnKeyDown(InputState state, KeyDownEventArgs args)
        {
            if (args.Key == Key.Up)
                positionChange.Y = -playerSpeed;
            if (args.Key == Key.Left)
                positionChange.X = -playerSpeed;
            if (args.Key == Key.Down)
                positionChange.Y = playerSpeed;
            if (args.Key == Key.Right)
                positionChange.X = playerSpeed;
            if (args.Key == Key.LShift)
                isHalfSpeed = true;
            return base.OnKeyDown(state, args);
        }
        protected override bool OnKeyUp(InputState state, KeyUpEventArgs args)
        {
                if (args.Key == Key.Up)
                    positionChange.Y -= positionChange.Y;
                if (args.Key == Key.Left)
                    positionChange.X -= positionChange.X;
                if (args.Key == Key.Down)
                    positionChange.Y -= positionChange.Y;
                if (args.Key == Key.Right)
                    positionChange.X -= positionChange.X;
                if (args.Key == Key.LShift)
                    isHalfSpeed = false;
            return base.OnKeyUp(state, args);
        }
    }
}
