using Mechanics.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mechanics.Objects.Abilities
{
    public abstract class DamageMechanic : AbilityBase
    {
        protected Interval damageInterval;

        public DamageMechanic(Character self) : base(self)
        {
        }

        public abstract override bool CanApply();
        public abstract override void End();
        public override void Execute(Character target, float factor)
        {
            float damage = this.self.Roll(this.damageInterval) * this.self[Stats.POTENCY] * factor;

            if (this.self.Roll(this.self[Stats.CRITICAL_HIT_CHANCE]))
            {
                damage *= this.self[Stats.CRITICAL_HIT_DAMAGE];
            }

            target.ApplyDamage(damage, this.self[Stats.KNOWLEDGE]);

            if (this.self.Roll(this.self[Stats.RECOVERY]))
            {
                this.self.ConsumedSpellPoints -= this.self.SpellPoints * MECHANICS.TABLES.SPECIALS.RECOVERY_PROC_VALUE * factor;
            }

        }


    }
}
