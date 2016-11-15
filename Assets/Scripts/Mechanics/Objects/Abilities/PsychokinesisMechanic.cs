using Mechanics.Enumerations;
using System;
using UnityEngine;

namespace Mechanics.Objects.Abilities
{
    public class PsychokinesisMechanic : DamageMechanic
    {
        private float lastUsage;

        public PsychokinesisMechanic(Character self) : base(self)
        {
            this.lastUsage = Time.time;
            this.damageInterval = DamageRange;
        }

        public static float HitRange { get { return MECHANICS.TABLES.AOE.HITRANGE.LOW; } }
        public static float Cooldown { get { return MECHANICS.TABLES.SINGLE_TARGET.COOLDOWN.VERY_LOW; } }
        public static Interval DamageRange { get { return MECHANICS.TABLES.SINGLE_TARGET.DAMAGE.LOW; } }

        public override bool CanApply()
        {
            return (Cooldown * this.self[Stats.ALACRITY]) < Time.time - this.lastUsage;
        }

        public override void Execute(Character target, float factor)
        {
            if (target != null)
            {
                base.Execute(target, factor);
                this.ShowDamage();
            }
            else
            {
                this.lastUsage = Time.time;
            }
        }

        public override void End()
        {
        }


        private void ShowDamage()
        {
            int dmg = (int)this.damage;
            if (dmg > 0)
            {
                CombatText.instance.Show(dmg.ToString(), Color.green);
                this.damage = 0f;
            }
        }
    }
}
