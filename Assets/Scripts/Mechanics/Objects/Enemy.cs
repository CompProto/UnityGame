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
        public Enemy(EnemyType type, Interval levelRange)
        {
            this.characterStats = MECHANICS.Convert(this.Generate(type, levelRange));
            this.UpdateStats();
        }

        private StatBase[] Generate(EnemyType type, Interval levelRange)
        {
            float level = Utility.GetRandomFromInterval(levelRange);
            SingleValueStat[] baseStats = new SingleValueStat[]
            {
                new SingleValueStat(Stats.ALL_PRIMARY_STATS, 5f, 0f)*level,
                new SingleValueStat(Stats.CRITICAL_HIT_CHANCE, 10f, 0f),
                new SingleValueStat(Stats.CRITICAL_HIT_DAMAGE, 0f, 0f),
            };

            SingleValueStat[] typeStats = null;
            switch (type)
            {
                case EnemyType.TANK:
                    typeStats = new SingleValueStat[]
                    {
                        new SingleValueStat(Stats.LIFEFORCE, 10f, 0f)*level,
                    };
                    break;
                case EnemyType.LONGRANGE:
                    typeStats = new SingleValueStat[]
                    {
                        new SingleValueStat(Stats.KNOWLEDGE, 10f, 0f)*level,
                    };
                    break;
                case EnemyType.CLOSERANGE:
                    typeStats = new SingleValueStat[]
                    {
                        new SingleValueStat(Stats.POTENCY, 10f, 0f)*level,
                    };
                    break;
            }
            return MECHANICS.CombineStats(baseStats, typeStats);
        }

    }
}
