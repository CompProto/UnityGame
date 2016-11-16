using Mechanics.Enumerations;
using System;
using UnityEngine;

namespace Mechanics.Objects.Abilities
{
    public class AstralPressenceMechanic : DamageMechanic
    {
        private float healed;

        public AstralPressenceMechanic(Character self) : base(self)
        {
            this.damageInterval = DamageRange;
            this.healed = 0f;
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
                    this.healed += drain;
                }
                base.Execute(target, correctedFactor);
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
            if (dmg > DamageRange.From)
            {
                CombatText.instance.Show(dmg.ToString(), Color.yellow);
                this.damage = 0f;
            }

            if (this.healed > DamageRange.From / 2f)
            {
                int heal = (int)this.healed;
                CombatText.instance.Show("+" + heal.ToString(), Color.green);
                this.healed = 0f;
            }

            int recovery = (int)this.recovery;
            if (recovery > 0f)
            {
                CombatText.instance.Show("+" + recovery.ToString(), Color.cyan);
                this.recovery = 0f;
            }
        }
    }
}
