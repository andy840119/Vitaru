using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics;

namespace osu.Game.Rulesets.Vitaru.Objects.Drawables
{
    public class DrawableVitaruBoss : DrawableCharacter
    {
        public DrawableVitaruBoss(VitaruHitObject hitObject) : base(hitObject)
        {
            Anchor = Anchor.TopCentre;
            Speed = new Vector2(0, -160);
            Position = hitObject.Position;
            CharacterType = HitObjectType.Boss;
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
