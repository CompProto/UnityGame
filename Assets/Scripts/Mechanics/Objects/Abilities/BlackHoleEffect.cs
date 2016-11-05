using Mechanics.Enumerations;
using System;

namespace Mechanics.Objects.Abilities
{
    public class BlackHoleEffect : DamageEffect
    {
        private int lastUsage;

        public BlackHoleEffect(Character self) : base(self)
        {
            this.damageInterval = MECHANICS.TABLES.AOE.DAMAGEVALUES.MEDIUM;
            this.lastUsage = 0;
        }

        public override bool CanApply()
        {
            return this.lastUsage < Environment.TickCount + (MECHANICS.TABLES.AOE.COOLDOWNVALUES.MEDIUM * this.self[Stats.ALACRITY]);
        }

        public override void Execute(Character target, float factor)
        {
            if (this.CanApply())
            {
                base.Execute(target, factor);                        
            }
        }

        public override void End()
        {
            this.lastUsage = Environment.TickCount;
        }
    }
}
