﻿using Mechanics.Enumerations;
using Mechanics.Objects.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mechanics.Objects
{
    public abstract class Character
    {
        private float wounds;
        private float consumedSpellpoints;
        protected int level;
        protected int statSum;
        protected Dictionary<Stats, StatBase> characterStats;
        private Dictionary<Stats, StatBase> combinedStats;
        private List<Item> equippedItems;
        protected Dictionary<MECHANICS.ABILITIES, AbilityBase> abilities;
        private float timer;

        public Character(SingleValueStat[] baseStats = null)
        {
            this.equippedItems = new List<Item>() { };
            this.characterStats = MECHANICS.Convert(baseStats);
            this.combinedStats = new Dictionary<Stats, StatBase>();
            this.abilities = new Dictionary<MECHANICS.ABILITIES, AbilityBase>();
            this.UpdateStats();
            this.timer = 0;
        }

        // Stats
        public float SpellPoints { get { return this[Stats.ENERGY]; } }
        public float ConsumedSpellPoints
        {
            get { return this.consumedSpellpoints; }
            set { this.consumedSpellpoints = value; if (this.consumedSpellpoints < 0f) this.consumedSpellpoints = 0f; }
        }
        public float SpellPointsLeft { get { return this.SpellPoints - this.ConsumedSpellPoints; } }
        public float Life { get { return this[Stats.LIFEFORCE]; } }
        public float Wounds
        {
            get { return this.wounds; }
            set { this.wounds = value; if (this.wounds < 0f) this.wounds = 0f; }
        }
        public int ExpValue { get { return this.level * MECHANICS.TABLES.SPECIALS.BASE_EXP_PR_LEVEL + this.statSum * MECHANICS.TABLES.SPECIALS.BASE_EXP_PR_STAT; } }
        public int Level { get { return this.level; } }
        public float Absorb { get; set; }
        public bool IsDead { get; private set; }
        // For getting translated values
        public float this[Stats index]
        {
            get
            {
                return MECHANICS.EvaluateStat(this.GetStat(index));
            }
        }

        // Not working properly yet
        public void EquipRune(Item item)
        {
            this.equippedItems.Add(item);
            this.UpdateStats();
        }

        public void UnEquipRune(Item item)
        {
            this.equippedItems.Remove(item);
            this.UpdateStats();
        }

        public void ApplyDamage(float damage, float reduction)
        {
            SingleValueStat parity = this.GetStat(Stats.PARITY).ConvertTo(Stats.PARITY, 1f - reduction);
            float mitigation = MECHANICS.EvaluateStat(parity);
            float incomingDamage = damage * (1f - mitigation);
            if (this.Absorb > 0)
            {
                if (this.Roll(this[Stats.BARRIER_BLOCK_CHANCE]))
                {
                    this.Absorb -= incomingDamage;
                    incomingDamage = 0;
                    if (this.Absorb < 0)
                    {
                        incomingDamage = -this.Absorb;
                        this.Absorb = 0;
                    }
                }
            }
            this.Wounds += incomingDamage;
            this.IsDead = this.Wounds > this.Life;
        }

        public void Update()
        {
            this.timer += Time.fixedDeltaTime;
            if (this.timer >= 1.0f)
            {
                this.timer = 0f;
                this.Wounds -= this.Life * MECHANICS.TABLES.SPECIALS.HEALTH_PR_SECOND;
                this.ConsumedSpellPoints -= this.SpellPoints * MECHANICS.TABLES.SPECIALS.SPELLPOINTS_PR_SECOND;
            }
            List<Item> remove = new List<Item>();
            this.equippedItems.ForEach(x => { x.Update(); if (x.Duration < 0f) remove.Add(x); });
            remove.ForEach(x => this.equippedItems.Remove(x));
            this.UpdateStats();
        }

        public bool CanUse(MECHANICS.ABILITIES ability)
        {
            if (this.abilities.ContainsKey(ability))
            {
                return this.abilities[ability].CanApply();
            }

            return false;
        }

        /// <summary>
        /// Used when starting, or executing an ability multiple times.
        /// </summary>
        /// <param name="ability">Ability name</param>
        /// <param name="target">The target of the ability effect</param>
        /// <param name="factor">The time factor of the effect. For instant effects the factor should be 1.0f; 
        /// For effects that work over a duration, the factor should be framecount / duration. </param>
        public void UseAbility(MECHANICS.ABILITIES ability, Character target, float factor)
        {
            if (this.abilities.ContainsKey(ability))
            {
                this.abilities[ability].Execute(target, factor);
            }
        }
        /// <summary>
        /// Used when an ability ends.
        /// </summary>
        /// <param name="ability"></param>
        public void EndAbility(MECHANICS.ABILITIES ability)
        {
            if (this.abilities.ContainsKey(ability))
            {
                this.abilities[ability].End();
            }
        }
        public float Roll(Interval interval)
        {
            float roll = Utility.GetRandomFromInterval(interval);
            if (Utility.Chance(this[Stats.INTUITION]))
            {
                return Math.Max(roll, Utility.GetRandomFromInterval(interval));
            }
            return roll;
        }
        public bool Roll(float threshold)
        {
            bool roll = Utility.Chance(threshold);
            if (Utility.Chance(this[Stats.INTUITION]))
            {
                return roll || Utility.Chance(threshold);
            }
            return roll;
        }
        public SingleValueStat GetStat(Stats stat)
        {
            return this.combinedStats.ContainsKey(stat) ? this.combinedStats[stat].As<SingleValueStat>() : MECHANICS.EMPTY;
        }

        protected void UpdateStats()
        {
            var characterStats = this.characterStats.Values.Select(x => x).ToArray();
            var equippedStats = this.equippedItems.SelectMany(x => x.Stats).ToArray();
            var total = MECHANICS.CombineStats(characterStats, equippedStats);

            // Add all primary stats contributions 
            StatBase allStatsPrime = total.Where(x => x.StatType == Stats.ALL_PRIMARY_STATS).FirstOrDefault();
            if (allStatsPrime != null)
            {
                total = MECHANICS.CombineStats(total, MECHANICS.GetContribution(allStatsPrime.As<SingleValueStat>()));
            }

            foreach (var stat in total.Where(x => x.StatType == Stats.POWER || x.StatType == Stats.ESSENCE || x.StatType == Stats.PERCEPTION || x.StatType == Stats.LUCK))
            {
                total = MECHANICS.CombineStats(total, MECHANICS.GetContribution(stat.As<SingleValueStat>()));
            }

            this.combinedStats = MECHANICS.Convert(total);
            foreach (var stat in this.combinedStats.Values)
            {
                this.statSum += (int)stat.As<SingleValueStat>().Value;
            }
        }
    }
}
