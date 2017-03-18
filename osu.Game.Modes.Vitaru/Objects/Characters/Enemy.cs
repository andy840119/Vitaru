using osu.Framework.Graphics.Containers;
using OpenTK;
using osu.Game.Modes.Vitaru.Objects.Drawables;
using osu.Framework.Graphics;
using OpenTK.Graphics;
using osu.Game.Modes.Vitaru.Objects.Projectiles;

namespace osu.Game.Modes.Vitaru.Objects.Characters
{
    public class Enemy : Character
    {
        public Vector2 DesiredPosition { get; set; }

        int a = 0;

        protected override void Update()
        {
            base.Update();
            if (shoot == true)
            {
                enemyShoot();
            }

            float ySpeed = enemySpeed.Y * (float)(Clock.ElapsedFrameTime);
            float xSpeed = enemySpeed.X * (float)(Clock.ElapsedFrameTime);
            Position = enemyPosition;
        }
        private void enemyShoot()
        {
            a = (a + 31);
            Bullet b;
            parent.Add(b = new Bullet(1)
            {
                Depth = 1,
                Anchor = Anchor.Centre,
                BulletAngle = a,
                BulletSpeed = 0.2f,
                BulletColor = Color4.Red,
            });
            b.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), b));
        }
    }
}