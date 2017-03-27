﻿using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Modes.Vitaru.Objects.Drawables
{
    public class DrawableVitaruBoss : DrawableVitaruCharacter
    {
        public DrawableVitaruBoss(VitaruHitObject hitObject) : base(hitObject)
        {
            Speed = new Vector2(0, -160);
            Position = hitObject.Position;
            CharacterType = CharacterType.Boss;
            CharacterHealth = 1000;
            Team = 1;
            HitboxColor = Color4.Green;
            HitboxWidth = 32;
        }

        protected override void Update()
        {
            base.Update();
            float ySpeed = Speed.Y * (float)Clock.ElapsedFrameTime;
            float xSpeed = Speed.X * (float)Clock.ElapsedFrameTime;
        }
    }
}
