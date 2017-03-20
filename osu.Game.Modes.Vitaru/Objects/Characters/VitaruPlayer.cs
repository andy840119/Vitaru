// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

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

namespace osu.Game.Modes.Vitaru.Objects.Characters
{
    public class VitaruPlayer : Character
    {
        //stores if a key is pressed or not
        private Dictionary<Key, bool> keys = new Dictionary<Key, bool>();

        //Was used for debugging performance
        public static int velocityCalculation;

        //stores the player position
        public static Vector2 playerPosition = new Vector2(0, 200);

        //(MinX,MaxX,MinY,MaxY)
        public Vector4 PlayerBounds = new Vector4(-200, 200, -200, 300);

        //useful when mods get involved or slow debuffs become a thing, pixels per millisecond, different values for x and y
        public Vector2 playerSpeed { get; set; } = new Vector2(0.5f, 0.5f);

        private bool _kiaiActivated = false;
        public bool KiaiActivated
        {
            get
            {
                return _kiaiActivated;
            }
            set
            {
                _kiaiActivated = value;
                //player.setKiai(value);
            }
        }
        private CharacterSprite player;

        public VitaruPlayer(Container parent) : base(parent)
        {

            //initialize the Dictionary values so it wont throw exceptions
            keys[Key.Up] = false;
            keys[Key.Right] = false;
            keys[Key.Down] = false;
            keys[Key.Left] = false;
            keys[Key.Z] = false;
            keys[Key.X] = false;
            keys[Key.LShift] = false;
            keys[Key.RShift] = false;
            Children = new[]
            {
                player = new CharacterSprite()
                {
                    Origin = Anchor.Centre,
                    CharacterName = "player"
                },

            };
            characterHealth = 100;
            Add(hitbox = new Hitbox()
            {
                HitboxWidth = 8,
                HitboxColor = Color4.Cyan,
            });
            Team = 0;
            OnShoot = Shoot;
        }

        //Kiai toggle
        public void ToggleKiai()
        {
            KiaiActivated = !KiaiActivated;
        }

        //Update Loop
        protected override void Update()
        {
            base.Update();

            //Handles Player Speed
            float ySpeed = playerSpeed.Y * (float)(Clock.ElapsedFrameTime);
            float xSpeed = playerSpeed.X * (float)(Clock.ElapsedFrameTime);

            //All these handle keys and when they are or aren't pressed
            if (keys[Key.LShift] | keys[Key.RShift])
            {
                xSpeed /= 2;
                ySpeed /= 2;
            }
            if (keys[Key.Z])
            {
                Shooting = true;
            }
            if (keys[Key.Z] == false)
            {
                Shooting = false;
            }
            if (keys [Key.X])
            {
                //Bomb();
            }
            if (keys[Key.Up])
            {
                playerPosition.Y -= ySpeed;
            }
            if (keys[Key.Left])
            {
                playerPosition.X -= xSpeed;
            }
            if (keys[Key.Down])
            {
                playerPosition.Y += ySpeed;
            }
            if (keys[Key.Right])
            {
                playerPosition.X += xSpeed;
            }

            //Handles VitaruPlayer Position
            playerPosition = Vector2.ComponentMin(playerPosition, PlayerBounds.Yw);
            playerPosition = Vector2.ComponentMax(playerPosition, PlayerBounds.Xz);
            Position = playerPosition;
        }

        //Shoot function for VitaruPlayer
        private void Shoot()
        {
                Bullet bullet;
                parent.Add(bullet = new Bullet(Team)
                {
                    Depth = 1,
                    Anchor = Anchor.Centre,
                    BulletAngleDegree = 0f,
                    BulletSpeed = 1f,
                    BulletColor = Color4.Green,
                });
                bullet.MoveTo(ToSpaceOfOtherDrawable(new Vector2(0, 0), bullet));
        }

        //saves if key is pressed
        protected override bool OnKeyDown(InputState state, KeyDownEventArgs args)
        {
            keys[args.Key] = true;
            if (args.Key == Key.LShift || args.Key == Key.RShift)
                hitbox.Alpha = 1;
            return base.OnKeyDown(state, args);
        }
        //saves if key is released
        protected override bool OnKeyUp(InputState state, KeyUpEventArgs args)
        {
            keys[args.Key] = false;
            if (args.Key == Key.LShift || args.Key == Key.RShift)
                hitbox.Alpha = 0;
            return base.OnKeyUp(state, args);
        }
    }
}