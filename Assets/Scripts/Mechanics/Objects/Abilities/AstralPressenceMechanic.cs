using Mechanics.Enumerations;
using System;
using UnityEngine;

namespace Mechanics.Objects.Abilities
{
    public class AstralPressenceMechanic : DamageMechanic
    {
        public AstralPressenceMechanic(Character self) : base(self)
        {
            this.damageInterval = DamageRange;
        }
        public static float HitRange { get { return MECHANICS.TABLES.AOE.HITRANGE.VERY_HIGH; } }
        public static float Resource { get { return MECHANICS.TABLES.AOE.RESOURCECOST.MEDIUM; } }
        public static Interval DamageRange { get { return MECHANICS.TABLES.AOE.DAMAGE.LOW; } }

        public override bool CanApply()
        {
            return this.self.SpellPointsLeft > Resource;
        }

        public override void Execute(Character target, float factor)
        {
            float correctedFactor = factor;
            this.self.ConsumedSpellPoints += Resource * factor;
            if (this.self.SpellPointsLeft <= 0f)
            {
                return;
            }

            if (target != null)
            {
                if (GameManager.instance.isDarkMode)
                {
                    correctedFactor *= 0.5f;
                    float drain = this.self.Roll(this.damageInterval) * this.self[Stats.POTENCY] * correctedFactor;
                    this.self.Wounds -= drain;
                }
                base.Execute(target, correctedFactor);
            }
        }

        public override void End()
        {
        }
    }
}
