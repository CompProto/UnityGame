using Mechanics.Enumerations;
using Mechanics.Objects.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mechanics.Objects
{
    public class Player : Character
    {
        public void Update()
        {
            // update resources pr. second
        }

        public Player(SingleValueStat[] stats) : base(stats)
        {            
            this.lastUsage.Add(MECHANICS.ABILITIES.BLACKHOLE, new BlackHoleEffect(this));
            this.lastUsage.Add(MECHANICS.ABILITIES.ENERGY_BARRIER, new BarrierEffect(this));
            //this.lastUsage.Add(MECHANICS.ABILITIES.CHARGE, 0);
            //this.lastUsage.Add(MECHANICS.ABILITIES.DIMENSION_DOOR, 0);
            //this.lastUsage.Add(MECHANICS.ABILITIES.ENERGY_BARRIER, 0);
            //this.lastUsage.Add(MECHANICS.ABILITIES.ENERGY_BOMB, 0);
            //this.lastUsage.Add(MECHANICS.ABILITIES.PSYCHO_KINESIS, 0);
        }

    }
}
