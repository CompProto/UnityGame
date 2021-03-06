﻿using Mechanics.Enumerations;
using System;
using UnityEngine;

namespace Mechanics.Objects.Abilities
{
    public class EnergyBombMechanic : DamageMechanic
    {
        public EnergyBombMechanic(Character self) : base(self)
        {
            this.damageInterval = DamageRange;
        }

        public static float HitRange { get { return MECHANICS.TABLES.AOE.HITRANGE.MEDIUM; } }
        public static float Resource { get { return MECHANICS.TABLES.AOE.RESOURCECOST.VERY_HIGH; } }
        public static Interval DamageRange { get { return MECHANICS.TABLES.AOE.DAMAGE.HIGH; } }

        public override bool CanApply()
        {
            return this.self.SpellPointsLeft > Resource;
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
                this.self.ConsumedSpellPoints += Resource;
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
                CombatText.instance.Show(dmg.ToString(), Color.yellow);
                this.damage = 0f;
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
