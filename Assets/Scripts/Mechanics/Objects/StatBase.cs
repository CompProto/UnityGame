using Mechanics.Enumerations;

namespace Mechanics.Objects
{
    public abstract class StatBase
    {

        public StatBase(Stats stattype)
        {
            this.StatType = stattype;

        }

        public Stats StatType { get; private set; }

        public T As<T>() where T: StatBase
        {
            return (T)this;
        }

    }
}
