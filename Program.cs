using Mechanics.Enumerations;
using Mechanics.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanics
{
    class Program
    {
        static void Main(string[] args)
        {
            SingleValueStat[] baseStats = new SingleValueStat[]
            {
                new SingleValueStat(Stats.ALL_PRIMARY_STATS, 33f, 0f),
                new SingleValueStat(Stats.POWER, 0f, 0f),
                new SingleValueStat(Stats.BARRIER_BLOCK_CHANCE, 25f, 0f),
                new SingleValueStat(Stats.BARRIER_POTENCY, 0f, 0f),
                new SingleValueStat(Stats.CRITICAL_HIT_CHANCE, 10f, 0f),
                new SingleValueStat(Stats.CRITICAL_HIT_DAMAGE, 0f, 0f),
                new SingleValueStat(Stats.POTENCY, 1f, 0f),
            };

            Player c = new Player(baseStats);
            ItemGenerator ig = new ItemGenerator();
            ig.Initialize();
            Item item1 = ig.MakeRandom(1);
            Item item2 = ig.MakeRandom(1);
            Item item3 = ig.MakeRandom(1);
            Item item4 = ig.MakeRandom(1);
            c.EquipRune(0, item1);
            c.EquipRune(1, item2);
            c.EquipRune(2, item3);
            c.EquipRune(3, item4);
            float f1 = MECHANICS.Translate(MECHANICS.GROWTH.FAST, 100f, 30);
            float f2 = MECHANICS.Translate(MECHANICS.GROWTH.MEDIUM, 100f, 30);
            float f3 = MECHANICS.Translate(MECHANICS.GROWTH.SLOW, 100f, 30);
            float f4 = MECHANICS.Translate(MECHANICS.GROWTH.STEADY, 100f, 30);
            
        }
    }
}
