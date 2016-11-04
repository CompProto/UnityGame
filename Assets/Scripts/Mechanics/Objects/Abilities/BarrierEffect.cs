using Mechanics.Enumerations;
using System;

namespace Mechanics.Objects.Abilities
{
    public class BarrierEffect : AbilityBase
    {
        private int lastUsage;
        private float absorbAmount;

        public BarrierEffect(Character self) : base(self)
        {
            this.absorbAmount = self.Roll(MECHANICS.TABLES.AOE.DAMAGEVALUES.MEDIUM) * this.self[Stats.BARRIER_POTENCY]; 
            this.lastUsage = 0;
        }
        public override bool CanApply()
        {
            return this.lastUsage < Environment.TickCount + (MECHANICS.TABLES.SINGLE_TARGET.COOLDOWNVALUES.MEDIUM * this.self[Stats.ALACRITY]); 
        }
        public override void Execute(Character target, float factor)
        {
            target.Barrier = this.absorbAmount;
        }
        public override void End()
        {
            this.lastUsage = Environment.TickCount;
        }
    }
}
