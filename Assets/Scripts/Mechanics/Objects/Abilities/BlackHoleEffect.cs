using Mechanics.Enumerations;
using System;
using UnityEngine;

namespace Mechanics.Objects.Abilities
{
    public class BlackHoleEffect : DamageEffect
    {
        private float lastUsage;
        private bool isRunning;

        public BlackHoleEffect(Character self) : base(self)
        {
            this.lastUsage = Time.time;
            this.isRunning = false;
            this.damageInterval = DamageRange;
        }

        public static float Cooldown { get { return MECHANICS.TABLES.AOE.COOLDOWNVALUES.MEDIUM; } }
        public static float Duration { get { return MECHANICS.TABLES.AOE.DURATIONVALUES.MEDIUM; } }
        public static Interval DamageRange { get { return MECHANICS.TABLES.AOE.DAMAGEVALUES.MEDIUM; } }

        public override bool CanApply()
        {
            return (Cooldown * this.self[Stats.ALACRITY]) < Time.time - this.lastUsage;
        }

        public override void Execute(Character target, float factor)
        {
            if (!this.isRunning)
            {
                this.lastUsage = Time.time;
                this.isRunning = true;
            }
            if (target != null)
            {
                base.Execute(target, factor);
            }
        }

        public override void End()
        {
            this.isRunning = false;
        }
    }
}
