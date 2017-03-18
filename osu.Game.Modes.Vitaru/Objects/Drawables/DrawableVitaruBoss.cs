using OpenTK.Graphics;

namespace osu.Game.Modes.Vitaru.Objects.Drawables
{
    class DrawableVitaruBoss : DrawableVitaruCharacter
    {

        public DrawableVitaruBoss(VitaruHitObject hitObject) : base(hitObject)
        {
            CharacterType = CharacterType.Boss;
            characterHealth = 1000;
            Team = 1;
            HitboxColor = Color4.Green;
            HitboxWidth = 32;
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}
