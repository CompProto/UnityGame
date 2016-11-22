using Mechanics.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mechanics.Objects
{
    public static class MECHANICS
    {
        public static SingleValueStat EMPTY = new SingleValueStat(Stats.UNKNOWN, 0f, 0f);
        public static StatBase[] CombineStats(StatBase[] currentsStats, StatBase[] newStats)
        {
            Dictionary<Stats, StatBase> stats = new Dictionary<Stats, StatBase>();

            if (currentsStats != null)
            {
                foreach (StatBase stat in currentsStats)
                {
                    if (!stats.ContainsKey(stat.StatType))
                    {
                        stats.Add(stat.StatType, stat);
                    }
                }
            }
            if (newStats != null)
            {
                foreach (StatBase stat in newStats)
                {
                    if (!stats.ContainsKey(stat.StatType))
                    {
                        stats.Add(stat.StatType, stat);
                    }
                    else
                    {
                        if (stat is SingleValueStat)
                        {
                            stats[stat.StatType] = ((SingleValueStat)stats[stat.StatType]) + ((SingleValueStat)stat);
                        }
                    }
                }
            }
            return stats.Values.ToArray();
        }
        public static Dictionary<Stats, StatBase> Convert(StatBase[] stats)
        {
            Dictionary<Stats, StatBase> statsTable = new Dictionary<Stats, StatBase>();

            if (stats != null)
            {
                foreach (StatBase stat in stats)
                {
                    if (!statsTable.ContainsKey(stat.StatType))
                    {
                        statsTable.Add(stat.StatType, stat);
                    }
                }
            }

            return statsTable;
        }
        public static StatBase[] ModifyStats(StatBase[] stats, float quality_modifier, float itemlevel_modifier, float variation)
        {
            List<StatBase> newstats = new List<StatBase>();
            float modifier = ((1.0f * variation) + quality_modifier) * itemlevel_modifier;

            foreach (StatBase stat in stats)
            {
                if (stat is SingleValueStat)
                {
                    newstats.Add(((SingleValueStat)stat) * modifier);
                }
            }

            return newstats.ToArray();
        }
        public static float EvaluateStat(SingleValueStat actualStat)
        {
            if (actualStat != null)
            {
                float range = 0f;
                switch (actualStat.StatType)
                {
                    case Stats.POTENCY:
                        range = CONSTRAINTS.MAX_POTENCY - CONSTRAINTS.MIN_POTENCY;
                        return CONSTRAINTS.MIN_POTENCY + range * Translate(GROWTH.SLOW, CEILINGS.POTENCY, actualStat.ActualValue);

                    case Stats.RECOVERY:
                        range = CONSTRAINTS.MAX_RECOVERY - CONSTRAINTS.MIN_RECOVERY;
                        return CONSTRAINTS.MIN_RECOVERY + range * Translate(GROWTH.SLOW, CEILINGS.RECOVERY, actualStat.ActualValue);

                    case Stats.LIFEFORCE:
                        return actualStat.ActualValue * SCALINGS.LIFEFORCE_HITPOINTS;

                    case Stats.ENERGY:
                        return actualStat.ActualValue * SCALINGS.ENERGY_SPELLPOINTS;

                    case Stats.PARITY:
                        range = CONSTRAINTS.MAX_PARITY - CONSTRAINTS.MIN_PARITY;
                        return CONSTRAINTS.MIN_PARITY + range * Translate(GROWTH.SLOW, CEILINGS.PARITY, actualStat.ActualValue);

                    case Stats.KNOWLEDGE:
                        range = CONSTRAINTS.MAX_KNOWLEDGE - CONSTRAINTS.MIN_KNOWLEDGE;
                        return CONSTRAINTS.MIN_KNOWLEDGE + range * Translate(GROWTH.SLOW, CEILINGS.KNOWLEDGE, actualStat.ActualValue);

                    case Stats.INTUITION:
                        range = CONSTRAINTS.MAX_INTUITION - CONSTRAINTS.MIN_INTUITION;
                        return CONSTRAINTS.MIN_INTUITION + range * Translate(GROWTH.SLOW, CEILINGS.INTUITION, actualStat.ActualValue);

                    case Stats.FORTUITY:
                        range = CONSTRAINTS.MAX_FORTUITY - CONSTRAINTS.MIN_FORTUITY;
                        return CONSTRAINTS.MIN_FORTUITY + range * Translate(GROWTH.SLOW, CEILINGS.FORTUITY, actualStat.ActualValue);

                    case Stats.BARRIER_BLOCK_CHANCE:
                        range = CONSTRAINTS.MAX_BARRIER_BLOCK_CHANCE - CONSTRAINTS.MIN_BARRIER_BLOCK_CHANCE;
                        return CONSTRAINTS.MIN_BARRIER_BLOCK_CHANCE + range * Translate(GROWTH.SLOW, CEILINGS.BARRIER_BLOCK_CHANCE, actualStat.ActualValue);

                    case Stats.BARRIER_POTENCY:
                        range = CONSTRAINTS.MAX_BARRIER_POTENCY - CONSTRAINTS.MIN_BARRIER_POTENCY;
                        return CONSTRAINTS.MIN_BARRIER_POTENCY + range * Translate(GROWTH.SLOW, CEILINGS.BARRIER_POTENCY, actualStat.ActualValue);

                    case Stats.CRITICAL_HIT_CHANCE:
                        return actualStat.ActualValue / 100.0f;

                    case Stats.CRITICAL_HIT_DAMAGE:
                        return 1.5f;

                    case Stats.ALACRITY:
                        range = CONSTRAINTS.MAX_ALACRITY - CONSTRAINTS.MIN_ALACRITY;
                        return CONSTRAINTS.MAX_ALACRITY - range * Translate(GROWTH.SLOW, CEILINGS.ALACRITY, actualStat.ActualValue);

                    default:
                        return actualStat.ActualValue;
                }
            }
            return 0.0f;
        }
        public static float Translate(GROWTH type, float ceiling, float value)
        {
            //http://www.mathe-fa.de/en#result
            // 1-e^(-x/100) blue
            // e^(-100/x) red
            // 1-x^(-x/100) green
            switch (type)
            {
                case GROWTH.FAST: // Can never exceed 1.0f
                    return 1.0f - (float)Math.Exp((-value / (ceiling / 4.605f)));

                case GROWTH.MEDIUM: // Can never exceed 1.0f
                    return 1.0f - (float)Math.Pow(value, (-value / ceiling));

                case GROWTH.SLOW: // Can never exceed 1.25f
                    return 1.25f * (float)Math.Exp(-(ceiling / 4.28f) / value);

                case GROWTH.STEADY:
                    return value / ceiling;

            }
            return 0f;
        }
        public static SingleValueStat[] GetContribution(SingleValueStat stat)
        {
            switch (stat.StatType)
            {
                case Stats.ALL_PRIMARY_STATS:
                    return new SingleValueStat[]
                    {
                        stat.ConvertTo(Stats.POWER, SCALINGS.ALL_PRIMARY_STAT_CONTRIBUTION),
                        stat.ConvertTo(Stats.ESSENCE, SCALINGS.ALL_PRIMARY_STAT_CONTRIBUTION),
                        stat.ConvertTo(Stats.PERCEPTION, SCALINGS.ALL_PRIMARY_STAT_CONTRIBUTION),
                        stat.ConvertTo(Stats.LUCK, SCALINGS.ALL_PRIMARY_STAT_CONTRIBUTION)
                    };
                case Stats.POWER:
                    return new SingleValueStat[]
                    {
                        stat.ConvertTo(Stats.POTENCY, SCALINGS.PRIMARY_STAT_CONTRIBUTION),
                        stat.ConvertTo(Stats.RECOVERY, SCALINGS.PRIMARY_STAT_CONTRIBUTION)
                    };
                case Stats.ESSENCE:
                    return new SingleValueStat[]
                    {
                        stat.ConvertTo(Stats.LIFEFORCE, SCALINGS.PRIMARY_STAT_CONTRIBUTION),
                        stat.ConvertTo(Stats.ENERGY, SCALINGS.PRIMARY_STAT_CONTRIBUTION)
                    };
                case Stats.PERCEPTION:
                    return new SingleValueStat[]
                    {
                        stat.ConvertTo(Stats.PARITY, SCALINGS.PRIMARY_STAT_CONTRIBUTION),
                        stat.ConvertTo(Stats.KNOWLEDGE, SCALINGS.PRIMARY_STAT_CONTRIBUTION)
                    };
                case Stats.LUCK:
                    return new SingleValueStat[]
                    {
                        stat.ConvertTo(Stats.INTUITION, SCALINGS.PRIMARY_STAT_CONTRIBUTION),
                        stat.ConvertTo(Stats.FORTUITY, SCALINGS.PRIMARY_STAT_CONTRIBUTION)
                    };
                default:
                    return null;
            }
        }

        public enum GROWTH
        {
            FAST,
            MEDIUM,
            SLOW,
            STEADY,
        }
        private static class CONSTRAINTS
        {
            public readonly static float MIN_POTENCY = 1f;
            public readonly static float MAX_POTENCY = 10f;
            public readonly static float MIN_RECOVERY = 0.2f;
            public readonly static float MAX_RECOVERY = 1f;
            public readonly static float MIN_PARITY = 0f;
            public readonly static float MAX_PARITY = 1f;
            public readonly static float MIN_KNOWLEDGE = 0f;
            public readonly static float MAX_KNOWLEDGE = 1f;
            public readonly static float MIN_INTUITION = 0f;
            public readonly static float MAX_INTUITION = 1f;
            public readonly static float MIN_FORTUITY = 0f;
            public readonly static float MAX_FORTUITY = 1f;
            public readonly static float MIN_BARRIER_BLOCK_CHANCE = 0f;
            public readonly static float MAX_BARRIER_BLOCK_CHANCE = 1f;
            public readonly static float MIN_BARRIER_POTENCY = 1f;
            public readonly static float MAX_BARRIER_POTENCY = 5f;
            public readonly static float MIN_ALACRITY = 0f;
            public readonly static float MAX_ALACRITY = 1f;
        }
        private static class CEILINGS
        {
            public readonly static float POTENCY = 1000f;
            public readonly static float RECOVERY = 1000f;
            public readonly static float PARITY = 1000f;
            public readonly static float KNOWLEDGE = 1000f;
            public readonly static float INTUITION = 1000f;
            public readonly static float FORTUITY = 1000f;
            public readonly static float BARRIER_BLOCK_CHANCE = 1000f;
            public readonly static float BARRIER_POTENCY = 1000f;
            public readonly static float ALACRITY = 1000f;
        }
        private static class SCALINGS
        {
            public readonly static float ALL_PRIMARY_STAT_CONTRIBUTION = 1f;
            public readonly static float PRIMARY_STAT_CONTRIBUTION = 3f;
            public readonly static float LIFEFORCE_HITPOINTS = 30f;
            public readonly static float ENERGY_SPELLPOINTS = 10f;
        }
        public static class TABLES
        {
            public static class AOE
            {
                public static class DAMAGE
                {
                    public static readonly Interval NONE = new Interval(0f, 0f);
                    public static readonly Interval VERY_LOW = new Interval(10f, 25f);
                    public static readonly Interval LOW = new Interval(20f, 55f);
                    public static readonly Interval MEDIUM = new Interval(50f, 110f);
                    public static readonly Interval HIGH = new Interval(100f, 150f);
                }
                public static class COOLDOWN
                {
                    public static readonly float NONE = 0;
                    public static readonly float VERY_LOW = 1.6f;
                    public static readonly float LOW = 2.5f;
                    public static readonly float MEDIUM = 5.0f;
                    public static readonly float HIGH = 12.0f;
                    public static readonly float VERY_HIGH = 28.0f;
                }
                public static class DURATION
                {
                    public static readonly float NONE = 0;
                    public static readonly float INSTANT = 0.01f;
                    public static readonly float VERY_LOW = 1.6f;
                    public static readonly float LOW = 2.5f;
                    public static readonly float MEDIUM = 5.0f;
                    public static readonly float HIGH = 12.0f;
                    public static readonly float VERY_HIGH = 28.0f;
                }
                public static class RESOURCECOST
                {
                    public static readonly float NONE = 0f;
                    public static readonly float VERY_LOW = 75f;
                    public static readonly float LOW = 125f;
                    public static readonly float MEDIUM = 187f;
                    public static readonly float HIGH = 250f;
                    public static readonly float VERY_HIGH = 450f;
                }
                public static class CASTINGRANGE
                {
                    public static readonly float NONE = 0f;
                    public static readonly float VERY_LOW = 75f;
                    public static readonly float LOW = 125f;
                    public static readonly float MEDIUM = 187f;
                    public static readonly float HIGH = 250f;
                    public static readonly float VERY_HIGH = 450f;
                }
                public static class HITRANGE
                {
                    public static readonly float NONE = 0f;
                    public static readonly float VERY_LOW = 1f;
                    public static readonly float LOW = 2.5f;
                    public static readonly float MEDIUM = 5f;
                    public static readonly float HIGH = 7.5f;
                    public static readonly float VERY_HIGH = 10f;
                }
            }
            public static class SINGLE_TARGET
            {
                public class DAMAGE
                {
                    public static readonly Interval NONE = new Interval(0f, 0f);
                    public static readonly Interval LOW = new Interval(25f, 65f);
                    public static readonly Interval MEDIUM = new Interval(55f, 115f);
                    public static readonly Interval HIGH = new Interval(105f, 165f);
                }
                public static class COOLDOWN
                {
                    public static readonly float NONE = 0;
                    public static readonly float VERY_LOW = 0.5f;
                    public static readonly float LOW = 1.0f;
                    public static readonly float MEDIUM = 3.0f;
                    public static readonly float HIGH = 8.0f;
                    public static readonly float VERY_HIGH = 15.0f;
                }
                public static class DURATION
                {
                    public static readonly float NONE = 0;
                    public static readonly float INSTANT = 0.01f;
                    public static readonly float VERY_LOW = 0.75f;
                    public static readonly float LOW = 1.0f;
                    public static readonly float MEDIUM = 3.0f;
                    public static readonly float HIGH = 8.0f;
                    public static readonly float VERY_HIGH = 15.0f;
                }
                public static class RESOURCECOST
                {
                    public static readonly float NONE = 0f;
                    public static readonly float VERY_LOW = 25f;
                    public static readonly float LOW = 50f;
                    public static readonly float MEDIUM = 75f;
                    public static readonly float HIGH = 100f;
                    public static readonly float VERY_HIGH = 150f;
                }
                public static class CASTINGRANGE
                {
                    public static readonly float NONE = 0f;
                    public static readonly float VERY_LOW = 75f;
                    public static readonly float LOW = 125f;
                    public static readonly float MEDIUM = 187f;
                    public static readonly float HIGH = 250f;
                    public static readonly float VERY_HIGH = 450f;
                }
            }
            public static class SPECIALS
            {
                public static readonly float HEALTH_PR_SECOND = 0.01f;
                public static readonly float SPELLPOINTS_PR_SECOND = 0.01f;
                public static readonly float RECOVERY_PROC_VALUE = 0.02f;
                public static readonly int BASE_EXP_PR_LEVEL = 500;
                public static readonly int BASE_EXP_PR_STAT = 10;
                public static readonly float EXP_LEVEL_RATE = 2f;
                public static readonly int BASE_EXP_LEVEL = 5000;
                public static readonly int STATS_PR_LEVEL = 5;
            }
        }
        public enum ABILITIES
        {
            NONE,
            // Player abilities
            CHARGE,
            BLACKHOLE,
            ASTRAL_PRESENCE,
            DIMENSION_DOOR,
            ENERGY_BARRIER,
            PSYCHO_KINESIS,
            ENERGY_BOMB,
            ENEMY_RANGED_ATTACK,
            ENEMY_MELEE_ATTACK
            // Enemy abilities
        }
    }
}
