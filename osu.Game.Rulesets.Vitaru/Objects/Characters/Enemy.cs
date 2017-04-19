// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics.Containers;
using OpenTK;
using osu.Game.Rulesets.Vitaru.Objects.Drawables;
using osu.Framework.Graphics;
using OpenTK.Graphics;
using osu.Game.Rulesets.Vitaru.Objects.Projectiles;
using System;
using System.Collections.Generic;
using osu.Framework.MathUtils;
using osu.Framework.Graphics.Transforms;
using osu.Framework.Audio.Sample; 
using osu.Game.Beatmaps.Timing;
using osu.Game.Database;

namespace osu.Game.Rulesets.Vitaru.Objects.Characters
{
    public class Enemy : Character
    {
        public bool Shoot = false;
        public Vector2 EnemyPosition = new Vector2(0, 0);
        public Vector2 EnemySpeed { get; set; } = new Vector2(0.5f, 0.5f);
        public BulletPattern Pattern { get; set; }

        public Vector2 EnemyVelocity;
        public float EnemyAngle;
        public Action OnShoot;

        public static Vector2 EnemyPos;
        

        //Main Enemy Function
        public Enemy() : base () { }
        public override HitObjectType Type => HitObjectType.Enemy;

        public double HitWindowKill10 = 35;
        public double HitWindowKill30 = 80;
        public double HitWindowMiss = 95;

        public override void ApplyDefaults(TimingInfo timing, BeatmapDifficulty difficulty)
        {
            base.ApplyDefaults(timing, difficulty);

            HitWindowKill10 = BeatmapDifficulty.DifficultyRange(difficulty.OverallDifficulty, 50, 35, 20);
            HitWindowKill30 = BeatmapDifficulty.DifficultyRange(difficulty.OverallDifficulty, 120, 80, 50);
            HitWindowMiss = BeatmapDifficulty.DifficultyRange(difficulty.OverallDifficulty, 135, 95, 70);
        }

        public double EndTime { get; set; }
    }
}