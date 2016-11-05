using System;

namespace Mechanics.Objects
{
    public static class Utility
    {
        private static Random random = new Random(Environment.TickCount);
        public static float GetVariation(int variation_percent = 15)
        {
            return 1.0f + (float)((random.NextDouble() * variation_percent) / 100f);
        }
        public static float GetRandomFromInterval(Interval interval)
        {
            float range = interval.To - interval.From;
            return ((float)random.NextDouble() * range) + interval.From;
        }

        public static bool Chance(float chance)
        {
            return (float)random.NextDouble() <= chance;
        }
    }
}
