using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mechanics.Objects
{
    public class Interval 
    {
        public Interval()
        {
        }

        public Interval(float from, float to)
        {
            this.From = from;
            this.To = to;
        }

        public float From { get; set; }

        public float To { get; set; }

        public static Interval operator *(Interval me, float modifier)
        {
            return new Interval(me.From * modifier, me.To * modifier);
        }

        public static Interval operator +(Interval a, Interval b)
        {
            return new Interval( a.From + b.From, a.To + b.To);
        }
    }
}
