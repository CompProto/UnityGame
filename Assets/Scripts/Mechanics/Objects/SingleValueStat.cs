using Mechanics.Enumerations;

namespace Mechanics.Objects
{
    public class SingleValueStat : StatBase
    {
        public SingleValueStat(Stats stattype, float value, float multiplier) : base(stattype)
        {
            this.Value = value;
            this.Multiplier = multiplier;
        }


        public float Value { get; set; }

        public float Multiplier { get; set; }

        public float ActualValue { get { return this.Value * (this.Multiplier + 1.0f); } }

        public static SingleValueStat operator +(SingleValueStat a, SingleValueStat b)
        {
            return new SingleValueStat(a.StatType, a.Value + b.Value, a.Multiplier + b.Multiplier);
        }

        public static SingleValueStat operator *(SingleValueStat me, float modifier)
        {
            return new SingleValueStat(me.StatType, me.Value * modifier, me.Multiplier * modifier);
        }

        public SingleValueStat ConvertTo(Stats newtype, float factor = 1f)
        {
            return new SingleValueStat(newtype, this.Value * factor, this.Multiplier);
        }

    }
}
