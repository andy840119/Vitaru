// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using OpenTK;
using osu.Game.Rulesets.Vitaru.Objects.Projectiles;
using System;

namespace osu.Game.Rulesets.Vitaru.Objects.Characters
{
    public class Enemy : Character
    {
        public bool Shoot = false;
        public Vector2 EnemyPosition = new Vector2(0, 0);
        public Vector2 EnemySpeed { get; set; } = new Vector2(0.5f, 0.5f);
        public BulletPattern Pattern { get; set; }
        public double kill30 = 30;
        public double HitWindowMiss = 1000;
        
        public Vector2 EnemyVelocity;
        public float EnemyAngle;
        public Action OnShoot;

        public static Vector2 EnemyPos;

        //Main Enemy Function
        public Enemy() : base () { }
        public override HitObjectType Type => HitObjectType.Enemy;

        public double EndTime { get; set; }
        public object Sample { get; internal set; }
    }
}