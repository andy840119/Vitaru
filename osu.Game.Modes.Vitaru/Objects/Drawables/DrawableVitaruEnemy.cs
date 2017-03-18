using OpenTK.Graphics;

namespace osu.Game.Modes.Vitaru.Objects.Drawables
{
    class DrawableVitaruEnemy : DrawableVitaruCharacter
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

        private void enemyShoot()
        {

        }
    }
}
