using Mechanics.Enumerations;
using System;
using UnityEngine;

namespace Mechanics.Objects.Abilities
{
    public class AstralPressenceMechanic : DamageMechanic
    {
        private bool isRunning;

        public AstralPressenceMechanic(Character self) : base(self)
        {
            this.isRunning = false;
            this.damageInterval = DamageRange;
        }
        public static float HitRange { get { return MECHANICS.TABLES.AOE.HITRANGE.VERY_HIGH; } }
        public static float Resource { get { return MECHANICS.TABLES.AOE.RESOURCECOST.MEDIUM; } }
        public static Interval DamageRange { get { return MECHANICS.TABLES.AOE.DAMAGE.LOW; } }

        public override bool CanApply()
        {
            return !isRunning && this.self.SpellPointsLeft > Resource;
        }

        public override void Execute(Character target, float factor)
        {
            if (!this.isRunning)
            {
                this.isRunning = true;
            }
            this.self.ConsumedSpellPoints += Resource * factor;
            if(this.self.SpellPointsLeft <= 0f)
            {
                this.isRunning = false;
            }
            if (GameManager.instance.isDarkMode)
            {
                float drain = this.self.Roll(this.damageInterval) * this.self[Stats.POTENCY] * factor;
                this.self.Wounds -= drain;
            }
            else
            {
                if (target != null)
                {
                    base.Execute(target, factor);
                }
            }
        }

        public override void End()
        {
            this.isRunning = false;
        }
    }
}
