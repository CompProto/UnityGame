using Mechanics.Enumerations;
using System;
using UnityEngine;

namespace Mechanics.Objects.Abilities
{
    public class EnemyRangedMechanic : DamageMechanic
    {
        private float lastUsage;
        

        public EnemyRangedMechanic(Character self) : base(self)
        {
            this.lastUsage = Time.time;
            this.damageInterval = DamageRange;
        }

        public static float HitRange { get { return MECHANICS.TABLES.AOE.HITRANGE.LOW; } }
        public static float Cooldown { get { return MECHANICS.TABLES.SINGLE_TARGET.COOLDOWN.HIGH; } }
        public static Interval DamageRange { get { return MECHANICS.TABLES.SINGLE_TARGET.DAMAGE.LOW; } }

        public override bool CanApply()
        {
            return (Cooldown * this.self[Stats.ALACRITY]) < Time.time - this.lastUsage;
        }

        public override void Execute(Character target, float factor)
        {
            this.lastUsage = Time.time;

            if (target != null)
            {
                base.Execute(target, factor);
                if (Time.frameCount % 30 == 0)
                {
                    this.ShowDamage();
                }
            }
        }

        public override void End()
        {
            this.ShowDamage();
        }


        private void ShowDamage()
        {
            int dmg = (int)this.damage;
            if (dmg > 0)
            {
                CombatText.instance.Show(dmg.ToString(), Color.red);
                this.damage = 0f;
            }
        }
    }
}
