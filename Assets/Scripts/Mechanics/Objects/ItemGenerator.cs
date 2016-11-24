using Mechanics.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mechanics.Objects
{
    public class ItemGenerator
    {
        private Random random;
        public Dictionary<Quality, float> QualityModifiers;
        public Dictionary<RuneTypes, StatBase[]> ItemBaseStats;
        public Dictionary<Prefix, StatBase[]> PrefixBaseStats;
        public Dictionary<Suffix, StatBase[]> SuffixBaseStats;

        public ItemGenerator()
        {
            this.random = new Random(Environment.TickCount);
            this.QualityModifiers = new Dictionary<Quality, float>();
            this.ItemBaseStats = new Dictionary<RuneTypes, StatBase[]>();
            this.PrefixBaseStats = new Dictionary<Prefix, StatBase[]>();
            this.SuffixBaseStats = new Dictionary<Suffix, StatBase[]>();
        }

        public float GetItemLevelModifier(int itemLevel)
        {
            return 1.0f + (((float)itemLevel - 1.0f) / 10f);
        }
        public Prefix GetRandomPrefix(float modifier)
        {
            float roll = 100f * (1f + modifier);
            int value = (this.random.Next((int)roll) + 1);
            if (value >= 50)
            {
                return (Prefix)(this.random.Next(Enum.GetValues(typeof(Prefix)).Length));
            }
            return Prefix.None;
        }
        public Suffix GetRandomSuffix(float modifier)
        {
            float roll = 100f * (1f + modifier);
            int value = (this.random.Next((int)roll) + 1);
            if (value >= 50)
            {
                return (Suffix)(this.random.Next(Enum.GetValues(typeof(Suffix)).Length));
            }
            return Suffix.None;
        }
        public Quality GetRandomItemQuality(float modifier)
        {
            float roll = 100f * (1f + modifier);
            int value = (this.random.Next((int)roll) + 1);
            if (value > 50)
            {
                if (value > 75)
                {
                    if (value > 94)
                    {
                        if (value > 99)
                        {
                            return Quality.ARTIFACT;
                        }
                        return Quality.LEGENDARY;
                    }
                    return Quality.EXOTIC;
                }
                return Quality.MAGICAL;
            }
            return Quality.NORMAL;
        }
        public string MakeName(Prefix prefix, RuneTypes itemType, Suffix suffix)
        {
            string prefixName = prefix != Prefix.None ? prefix.ToString() : string.Empty;
            string suffixName = suffix != Suffix.None ? "of the " + suffix.ToString() : string.Empty;
            return string.Format("{0} {1} {2}", prefixName, itemType.ToString(), suffixName);
        }
        public Item MakeRandomByType(RuneTypes itemType, int itemlevel, float modifier)
        {
            Quality quality = this.GetRandomItemQuality(modifier);
            Prefix prefix = this.GetRandomPrefix(modifier);
            Suffix suffix = this.GetRandomSuffix(modifier);

            string name = this.MakeName(prefix, itemType, suffix);
            float quality_Modifier = this.QualityModifiers[quality];
            float itemlevel_Modifier = this.GetItemLevelModifier(itemlevel);
            float variation = Utility.GetVariation(15);

            StatBase[] stats = MECHANICS.CombineStats(null, this.ItemBaseStats[itemType]);
            stats = MECHANICS.CombineStats(stats, this.PrefixBaseStats[prefix]);
            stats = MECHANICS.CombineStats(stats, this.SuffixBaseStats[suffix]);
            stats = MECHANICS.ModifyStats(stats, quality_Modifier, itemlevel_Modifier, variation);

            return new Item(itemType, name, itemlevel, quality, stats);
        }
        public Item MakeRandom(int itemlevel, float modifier)
        {
            RuneTypes type = (RuneTypes)this.random.Next(7);
            return this.MakeRandomByType(type, itemlevel, modifier);
        }

        public void Initialize()
        {
            this.QualityModifiers.Add(Quality.ARTIFACT, 1.1f);
            this.QualityModifiers.Add(Quality.LEGENDARY, 0.75f);
            this.QualityModifiers.Add(Quality.EXOTIC, 0.45f);
            this.QualityModifiers.Add(Quality.MAGICAL, 1.2f);
            this.QualityModifiers.Add(Quality.NORMAL, 0f);

            // RUNES
            this.ItemBaseStats.Add(RuneTypes.RUNE1, new StatBase[] {
                new SingleValueStat(Stats.LUCK, 1, 1f)
            });

            this.ItemBaseStats.Add(RuneTypes.RUNE2, new StatBase[] {
                new SingleValueStat(Stats.POWER, 1, 1f)
            });

            this.ItemBaseStats.Add(RuneTypes.RUNE3, new StatBase[] {
                new SingleValueStat(Stats.ESSENCE, 1, 1f)
            });

            this.ItemBaseStats.Add(RuneTypes.RUNE4, new StatBase[] {
                new SingleValueStat(Stats.PERCEPTION, 1, 1f)
            });

            this.ItemBaseStats.Add(RuneTypes.RUNE5, new StatBase[] {
                new SingleValueStat(Stats.POTENCY, 1, 1f)
            });

            this.ItemBaseStats.Add(RuneTypes.RUNE6, new StatBase[] {
                new SingleValueStat(Stats.RECOVERY, 1, 1f)
            });

            this.ItemBaseStats.Add(RuneTypes.RUNE7, new StatBase[] {
                new SingleValueStat(Stats.ALACRITY, 1, 1f)
            });

            this.ItemBaseStats.Add(RuneTypes.RUNE8, new StatBase[] {
                new SingleValueStat(Stats.CRITICAL_HIT_CHANCE, 1, 1f)
            });

            this.ItemBaseStats.Add(RuneTypes.RUNE9, new StatBase[] {
                new SingleValueStat(Stats.FORTUITY, 1, 1f)
            });

            // PREFIXES
            this.PrefixBaseStats.Add(Prefix.None, new StatBase[] {
                new SingleValueStat(Stats.POTENCY, 0, 0f)
            });

            this.PrefixBaseStats.Add(Prefix.Prefix1, new StatBase[] {
                new SingleValueStat(Stats.RECOVERY, 250, 0f)
            });

            this.PrefixBaseStats.Add(Prefix.Prefix2, new StatBase[] {
                new SingleValueStat(Stats.LIFEFORCE, 250, 0f)
            });

            this.PrefixBaseStats.Add(Prefix.Prefix3, new StatBase[] {
                new SingleValueStat(Stats.ENERGY, 250, 0f)
            });

            this.PrefixBaseStats.Add(Prefix.Prefix4, new StatBase[] {
                new SingleValueStat(Stats.PARITY, 250, 0f)
            });

            this.PrefixBaseStats.Add(Prefix.Prefix5, new StatBase[] {
                new SingleValueStat(Stats.KNOWLEDGE, 250, 0f)
            });

            this.PrefixBaseStats.Add(Prefix.Prefix6, new StatBase[] {
                new SingleValueStat(Stats.INTUITION, 250, 0f)
            });

            this.PrefixBaseStats.Add(Prefix.Prefix7, new StatBase[] {
                new SingleValueStat(Stats.FORTUITY, 250, 0f),
            });

            this.PrefixBaseStats.Add(Prefix.Prefix8, new StatBase[] {
                new SingleValueStat(Stats.BARRIER_BLOCK_CHANCE, 50, 0f),
                new SingleValueStat(Stats.BARRIER_POTENCY, 50, 0f)
            });

            this.PrefixBaseStats.Add(Prefix.Prefix9, new StatBase[] {
                new SingleValueStat(Stats.ALACRITY, 50, 0f)
            });

            // SUFFIXES
            this.SuffixBaseStats.Add(Suffix.None, new StatBase[] {
                new SingleValueStat(Stats.ALACRITY, 0, 0f)
            });

            this.SuffixBaseStats.Add(Suffix.Suffix1, new StatBase[] {
                new SingleValueStat(Stats.CRITICAL_HIT_CHANCE, 25, 0f),
                new SingleValueStat(Stats.POWER, 100, 0f),
                new SingleValueStat(Stats.KNOWLEDGE, 100, 0f)
            });

            this.SuffixBaseStats.Add(Suffix.Suffix2, new StatBase[] {
                new SingleValueStat(Stats.FORTUITY, 100, 0f),
                new SingleValueStat(Stats.ALACRITY, 100, 0f),
            });

            this.SuffixBaseStats.Add(Suffix.Suffix3, new StatBase[] {
                new SingleValueStat(Stats.KNOWLEDGE, 100, 0f),
                new SingleValueStat(Stats.PARITY, 100, 0f)
            });

            this.SuffixBaseStats.Add(Suffix.Suffix4, new StatBase[] {
                new SingleValueStat(Stats.LIFEFORCE, 100, 0f),
                new SingleValueStat(Stats.POTENCY, 100, 0f)
            });

            this.SuffixBaseStats.Add(Suffix.Suffix5, new StatBase[] {
                new SingleValueStat(Stats.ENERGY, 50, 2f)
            });

            this.SuffixBaseStats.Add(Suffix.Suffix6, new StatBase[] {
                new SingleValueStat(Stats.LIFEFORCE, 50, 2f),
            });

            this.SuffixBaseStats.Add(Suffix.Suffix7, new StatBase[] {
                new SingleValueStat(Stats.POWER, 50, 0f)
            });

            this.SuffixBaseStats.Add(Suffix.Suffix8, new StatBase[] {
                new SingleValueStat(Stats.ESSENCE, 50, 0f)
            });

            this.SuffixBaseStats.Add(Suffix.Suffix9, new StatBase[] {
                new SingleValueStat(Stats.LUCK, 50, 0f)
            });
        }

    }
}
