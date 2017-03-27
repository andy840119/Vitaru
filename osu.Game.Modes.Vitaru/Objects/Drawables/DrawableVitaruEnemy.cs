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
            Position = hitObject.Position;
            CharacterType = CharacterType.Enemy;
            CharacterHealth = 100;
            Team = 1;
            HitboxWidth = 20;
            HitboxColor = Color4.Yellow;
        }

        protected override void Update()
        {
            base.Update();
        }
        

        private void enemyShoot()
        {
            Bullet b;
            parent.Add(b = new Bullet(1)
            {
                Depth = 1,
                Anchor = Anchor.Centre,
                BulletSpeed = 0.2f,
                BulletColor = Color4.Red,
            });
            b.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), b));
        }
    }
}
