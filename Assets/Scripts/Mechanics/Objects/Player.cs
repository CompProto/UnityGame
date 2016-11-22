using Mechanics.Enumerations;
using Mechanics.Objects.Abilities;
using RPG.Assets.Scripts.Mechanics.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mechanics.Objects
{
    public class Player : Character
    {
        public int CurrentExp { get; private set; }

        public Player(SingleValueStat[] stats) : base(stats)
        {
            this.level = 1;
            this.CurrentExp = 0;
            this.abilities.Add(MECHANICS.ABILITIES.BLACKHOLE, new BlackHoleMechanic(this));
            this.abilities.Add(MECHANICS.ABILITIES.ENERGY_BARRIER, new BarrierMechanic(this));
            this.abilities.Add(MECHANICS.ABILITIES.ASTRAL_PRESENCE, new AstralPressenceMechanic(this));
            this.abilities.Add(MECHANICS.ABILITIES.ENERGY_BOMB, new EnergyBombMechanic(this));
            this.abilities.Add(MECHANICS.ABILITIES.PSYCHO_KINESIS, new PsychokinesisMechanic(this));
            this.abilities.Add(MECHANICS.ABILITIES.DIMENSION_DOOR, new DimensionDoorMechanic(this));
            this.abilities.Add(MECHANICS.ABILITIES.DIMENSION_DOOR, new ChargeMechanic(this));
            this.UpdateStats();
        }

        public Player()
        {
            this.level = 1;
            this.CurrentExp = 0;
            SingleValueStat[] baseStats = new SingleValueStat[]
            {
                new SingleValueStat(Stats.ALL_PRIMARY_STATS, 1f, 0f),
                new SingleValueStat(Stats.CRITICAL_HIT_CHANCE, 5f, 0f),
                new SingleValueStat(Stats.CRITICAL_HIT_DAMAGE, 0f, 0f),
                new SingleValueStat(Stats.ALACRITY, 1f, 0f),
                new SingleValueStat(Stats.BARRIER_BLOCK_CHANCE, 250f, 0f),
                new SingleValueStat(Stats.BARRIER_POTENCY, 150f, 0f),
                new SingleValueStat(Stats.POTENCY, 160f, 0f),
                new SingleValueStat(Stats.ENERGY, 100f, 0f),
                new SingleValueStat(Stats.KNOWLEDGE, 20f, 0f),
                new SingleValueStat(Stats.LIFEFORCE, 15f, 0f),



            };
            this.characterStats = MECHANICS.Convert(baseStats);
            this.abilities.Add(MECHANICS.ABILITIES.BLACKHOLE, new BlackHoleMechanic(this));
            this.abilities.Add(MECHANICS.ABILITIES.ENERGY_BARRIER, new BarrierMechanic(this));
            this.abilities.Add(MECHANICS.ABILITIES.ASTRAL_PRESENCE, new AstralPressenceMechanic(this));
            this.abilities.Add(MECHANICS.ABILITIES.ENERGY_BOMB, new EnergyBombMechanic(this));
            this.abilities.Add(MECHANICS.ABILITIES.PSYCHO_KINESIS, new PsychokinesisMechanic(this));
            this.abilities.Add(MECHANICS.ABILITIES.DIMENSION_DOOR, new DimensionDoorMechanic(this));
            this.abilities.Add(MECHANICS.ABILITIES.CHARGE, new ChargeMechanic(this));
            this.UpdateStats();
        }
        public int AvailableStatPoints { get; private set; }

        public void AwardExp(int exp)
        {
            CombatText.instance.Show("+" + exp.ToString() + " exp", Color.white, 14);
            this.CurrentExp += exp;
            float factor = (float)this.CurrentExp / (float)MECHANICS.TABLES.SPECIALS.BASE_EXP_LEVEL;
            int projectedLevel = this.CurrentExp < MECHANICS.TABLES.SPECIALS.BASE_EXP_LEVEL ? 1 : (int)Math.Log(factor, MECHANICS.TABLES.SPECIALS.EXP_LEVEL_RATE) + 2;
            if (projectedLevel > this.level)
            {
                this.AvailableStatPoints += MECHANICS.TABLES.SPECIALS.STATS_PR_LEVEL * (projectedLevel - this.level);
                this.level = projectedLevel;
                CombatText.instance.Show("+Level", Color.yellow, 80);
            }
        }

        public int ExpNextLevel(int levelMod)
        {
            int l = level + levelMod - 2;
            float exp = l < 0 ? 0 : (float)Math.Pow(MECHANICS.TABLES.SPECIALS.EXP_LEVEL_RATE, l) * MECHANICS.TABLES.SPECIALS.BASE_EXP_LEVEL;
            return (int)exp;
        }

        public void AwardStat(Stats stat, float count)
        {
            if (this.AvailableStatPoints > 0)
            {
                if (this.characterStats.ContainsKey(stat))
                {
                    this.characterStats[stat].As<SingleValueStat>().Value += count;
                }
                else
                {
                    this.characterStats.Add(stat, new SingleValueStat(stat, count, 0f));
                }
                this.AvailableStatPoints--;
                this.UpdateStats();
            }
        }

    }
}
