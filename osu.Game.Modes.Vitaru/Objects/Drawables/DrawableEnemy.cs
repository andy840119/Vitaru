using OpenTK.Graphics;
using OpenTK;
using System;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Modes.Vitaru.Objects.Projectiles;
using osu.Game.Modes.Vitaru.Objects.Drawables;
using osu.Game.Modes.Vitaru.Objects;
using osu.Game.Modes.Vitaru.Objects.Characters;

namespace osu.Game.Modes.Vitaru.Objects.Drawables
{
    public class DrawableEnemy : DrawableVitaruCharacter
    {
        public bool Shoot = false;

        public DrawableEnemy(VitaruHitObject hitObject) : base(hitObject)
        {
            Origin = Anchor.Centre;
            Position = hitObject.Position;
            CharacterType = HitObjectType.Enemy;
            CharacterHealth = 100;
            Team = 1;
            HitboxWidth = 20;
            HitboxColor = Color4.Yellow;
        }

        protected override void Update()
        {
            base.Update();
            if (Shoot == true)
            {
                Shooting = true;
                OnShoot = enemyShoot;
            }
            float ySpeed = 0.5f * (float)Clock.ElapsedFrameTime;
            float xSpeed = 0.5f * (float)Clock.ElapsedFrameTime;
        }


        private void enemyShoot()
        {
            Bullet b;
            MainParent.Add(b = new Bullet(Team)
            {
                Depth = 1,
                Anchor = Anchor.Centre,
                BulletSpeed = 0.2f,
                BulletAngleRadian = playerRelativePositionAngle(),
                BulletColor = Color4.Red,
            });
            b.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), b));
        }
        public float playerRelativePositionAngle()
        {
            return (float)Math.Atan2((DrawableVitaruPlayer.PlayerPosition.X - Position.X), -1 * (DrawableVitaruPlayer.PlayerPosition.Y - Position.Y));
        }
    }
}
