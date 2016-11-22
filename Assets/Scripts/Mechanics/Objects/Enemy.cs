using Mechanics.Enumerations;
using Mechanics.Objects.Abilities;
using RPG.Assets.Scripts.Mechanics.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mechanics.Objects
{
    public class Enemy : Character
    {

        public EnemyType Type { get; private set; }
        public Enemy(EnemyType type, Interval levelRange)
        {
            this.characterStats = MECHANICS.Convert(this.Generate(type, levelRange));
            this.UpdateStats();
            this.Type = type;

            switch(type)
            {
                case EnemyType.LONGRANGE:
                    this.abilities.Add(MECHANICS.ABILITIES.ENEMY_RANGED_ATTACK, new EnemyRangedMechanic(this));
                    break;
                case EnemyType.CLOSERANGE:
                    this.abilities.Add(MECHANICS.ABILITIES.ENEMY_MELEE_ATTACK, new EnemyMeleeMechanic(this));
                    break;
                case EnemyType.BOSS:
                    this.abilities.Add(MECHANICS.ABILITIES.ENEMY_MELEE_ATTACK, new EnemyMeleeMechanic(this));
                    this.abilities.Add(MECHANICS.ABILITIES.ENEMY_RANGED_ATTACK, new EnemyRangedMechanic(this));
                    break;
            }
        }

        private StatBase[] Generate(EnemyType type, Interval levelRange)
        {
            float level = Utility.GetRandomFromInterval(levelRange);
            this.level = (int)level;
            SingleValueStat[] baseStats = new SingleValueStat[]
            {
                new SingleValueStat(Stats.ALL_PRIMARY_STATS, 1f, 0f)*level,
                new SingleValueStat(Stats.CRITICAL_HIT_CHANCE, 2f, 0f) * level,
                new SingleValueStat(Stats.CRITICAL_HIT_DAMAGE, 0f, 0f),
                new SingleValueStat(Stats.ALACRITY, 1f, 0f) * level,
                new SingleValueStat(Stats.PARITY, 10f, 0f) * level,
                new SingleValueStat(Stats.KNOWLEDGE, 10f, 0f) * level,
                new SingleValueStat(Stats.POTENCY, 5f, 0f)*level,
                new SingleValueStat(Stats.LIFEFORCE, 4f, 0f)*level,

            };

            SingleValueStat[] typeStats = null;
            switch (type)
            {
                case EnemyType.BOSS:
                    typeStats = new SingleValueStat[]
                    {
                        new SingleValueStat(Stats.LIFEFORCE, 5f, 0f)*level,
                        new SingleValueStat(Stats.KNOWLEDGE, 1f, 0f)*level,
                        new SingleValueStat(Stats.POTENCY, 1f, 0f)*level,
                        new SingleValueStat(Stats.CRITICAL_HIT_CHANCE, 2f, 0f) * level,
                    };
                    break;
                case EnemyType.LONGRANGE:
                    typeStats = new SingleValueStat[]
                    {
                        new SingleValueStat(Stats.KNOWLEDGE, 1f, 0f)*level,
                    };
                    break;
                case EnemyType.CLOSERANGE:
                    typeStats = new SingleValueStat[]
                    {
                        new SingleValueStat(Stats.POTENCY, 1f, 0f)*level,
                    };
                    break;
            }
            return MECHANICS.CombineStats(baseStats, typeStats);
        }

    }
}
