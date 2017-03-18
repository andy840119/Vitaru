using OpenTK.Graphics;
using OpenTK;
using osu.Framework.Graphics;
using osu.Game.Modes.Vitaru.Objects.Projectiles;

namespace osu.Game.Modes.Vitaru.Objects.Drawables
{
    public class DrawableVitaruEnemy : DrawableVitaruCharacter
    {
        public DrawableVitaruEnemy(VitaruHitObject hitObject) : base(hitObject)
        {
            CharacterType = CharacterType.Enemy;
            characterHealth = 100;
            Team = 1;
            HitboxWidth = 20;
            HitboxColor = Color4.Yellow;
        }

        protected override void Update()
        {
            base.Update();
        }

        int a;

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
