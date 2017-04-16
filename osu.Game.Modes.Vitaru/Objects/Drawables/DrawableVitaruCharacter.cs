using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using System;
using osu.Game.Modes.Vitaru.Objects.Projectiles;
using osu.Framework.Audio;
using osu.Framework.Audio.Sample;

namespace osu.Game.Modes.Vitaru.Objects.Drawables
{
    public abstract class DrawableVitaruCharacter : DrawableVitaruHitObject
    {
        public HitObjectType CharacterType;

        protected Sprite CharacterSprite;
        protected Sprite CharacterKiaiSprite;

        public Vector2 Speed { get; set; } = Vector2.Zero;
        public float CharacterHealth { get; set; } = 100;
        public float Armor { get; internal set; } = 1; //All damage taken should be divided by this number. During kiai player will only take half damage so [2]
        public int Team { get; set; } = 0; // 0 = Player, 1 = Ememies + Boss(s) in Singleplayer
        public int ProjectileDamage { get; set; }
        public int BPM { get; set; } = 190;
        private SampleChannel sampleShoot;
        private SampleChannel sampleDeath;

        public static Container playfield;
        protected Hitbox Hitbox;
        public Container MainParent = playfield;

        public bool Shooting { get; set; } = false;

        private double timeSinceLastShoot;

        protected Color4 HitboxColor = Color4.White;
        protected int HitboxWidth = 16;

        public Action OnDeath { get; set; }
        public Action OnShoot { get; set; }

        public DrawableVitaruCharacter(VitaruHitObject hitObject) : base(hitObject)
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Children = new Drawable[]
            {
                CharacterSprite = new Sprite()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre
                },
                CharacterKiaiSprite = new Sprite()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Alpha = 0,
                },
                Hitbox = new Hitbox()
                {
                    Alpha = 0,
                    HitboxWidth = HitboxWidth,
                    HitboxColor = HitboxColor,
                }
            };
        }

        /// <summary>
        /// The <see cref="Character"/> gets damaged, with a multiplier of <see cref="DamageMultiplier"/>
        /// </summary>
        /// <param name="damage">Damage without the Resistance applied</param>
        /// <returns>If the Character died</returns>
        public bool TakeDamage(int damage)
        {
            CharacterHealth -= (int)(damage * Armor);
            if (CharacterHealth <= 0)
            {
                Dispose();
                sampleDeath.Play();
                //OnDeath();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Heals the <see cref="Character"/> by the specified amount
        /// </summary>
        /// <param name="healing">Amount of health to be healed</param>
        public void Heal(int healing)
        {
            CharacterHealth = 100 >= healing + CharacterHealth ? 100 : CharacterHealth + healing;
        }

        protected override void Update()
        {
            MainParent = playfield;
            base.Update();
            if (MainParent?.Children != null)
            foreach (Drawable draw in MainParent.Children)
            {
                if (draw is Bullet)
                {
                    Bullet bullet = draw as Bullet;
                    if (bullet.Team != Team)
                    {
                        Vector2 bulletPos = bullet.ToSpaceOfOtherDrawable(Vector2.Zero, this);
                        float distance = (float)Math.Sqrt(Math.Pow(bulletPos.X, 2) + Math.Pow(bulletPos.Y, 2));
                        float minDist = Hitbox.HitboxWidth + bullet.BulletWidth;
                        if (distance < minDist)
                        {
                            bullet.DeleteBullet();
                            if (TakeDamage(bullet.BulletDamage))
                                break;
                        }
                    }
                }
            }
            if (MainParent != null)
            foreach (Drawable draw in MainParent.Children)
            {
                if (draw is Bullet)
                {
                    Bullet bullet = draw as Bullet;
                    if (bullet.Team != Team)
                    {
                        Vector2 bulletPos = bullet.ToSpaceOfOtherDrawable(Vector2.Zero, this);
                        float distance = (float)Math.Sqrt(Math.Pow(bulletPos.X, 2) + Math.Pow(bulletPos.Y, 2));
                        float minDist = Hitbox.HitboxWidth + bullet.BulletWidth;
                        if (distance < minDist)
                        {
                            bullet.DeleteBullet();
                            if (TakeDamage(bullet.BulletDamage))
                                break;
                        }
                    }
                }
            }
            if (Shooting)
            {
                timeSinceLastShoot += Clock.ElapsedFrameTime;
                if (timeSinceLastShoot / 1000.0 > 1 / BPM / 30.0)
                {
                    sampleShoot.Play();
                    OnShoot?.Invoke();
                    timeSinceLastShoot -= 1 / (BPM / 30.0) * 1000.0;
                }
            }
        }

        protected void ShowHitbox()
        {
            Hitbox.FadeIn();
        }

        protected void HideHitbox()
        {
            Hitbox.FadeOut();
        }

        [BackgroundDependencyLoader]
        private void load(AudioManager audio, TextureStore textures)
        {
            string characterType = "null";
            switch(CharacterType)
            {
                case HitObjectType.Player:
                    characterType = "player";
                    break;
                case HitObjectType.Enemy:
                    characterType = "enemy";
                    break;
                case HitObjectType.Boss:
                    characterType = "boss";
                    break;
            }

            sampleDeath = audio.Sample.Get(@"Vitaru/deathSound");
            sampleShoot = audio.Sample.Get(@"Vitaru/shootSound");
            CharacterSprite.Texture = textures.Get(@"Play/Vitaru/" + characterType);
            CharacterKiaiSprite.Texture = textures.Get(@"Play/Vitaru/" + characterType + "Kiai");
        }
    }
}