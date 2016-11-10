using Mechanics.Enumerations;
using System;
using UnityEngine;

namespace Mechanics.Objects.Abilities
{
    public class DimensionDoorMechanic : DamageMechanic
    {
        private float lastUsage;

        public DimensionDoorMechanic(Character self) : base(self)
        {
            this.lastUsage = Time.time;
            this.damageInterval = DamageRange;
        }

        public static float Cooldown { get { return MECHANICS.TABLES.AOE.COOLDOWN.LOW; } }
        public static float Duration { get { return MECHANICS.TABLES.AOE.DURATION.HIGH; } }
        public static Interval DamageRange { get { return MECHANICS.TABLES.AOE.DAMAGE.NONE; } }

        public override bool CanApply()
        {
            return (Cooldown * this.self[Stats.ALACRITY]) < Time.time - this.lastUsage;
        }

        public override void Execute(Character target, float factor)
        {
        }

        public override void End()
        {
            this.lastUsage = Time.time;
        }
    }
}
