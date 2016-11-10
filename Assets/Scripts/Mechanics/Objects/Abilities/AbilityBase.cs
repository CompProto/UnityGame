using Mechanics.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mechanics.Objects.Abilities
{
    public abstract class AbilityBase
    {
        protected Character self;

        public AbilityBase(Character self)
        {
            this.self = self;
        }

        public abstract bool CanApply();
        public abstract void Execute(Character target, float factor);
        public abstract void End();

    }


}
