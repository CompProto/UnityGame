using Mechanics.Enumerations;
using Mechanics.Objects.Abilities;
using RPG.Assets.Scripts.Mechanics.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mechanics.Objects
{
    public class Player : Character
    {

        public Player(SingleValueStat[] stats) : base(stats)
        {            
            this.abilities.Add(MECHANICS.ABILITIES.BLACKHOLE, new BlackHoleEffect(this));
            this.abilities.Add(MECHANICS.ABILITIES.ENERGY_BARRIER, new BarrierEffect(this));
            //this.abilities.Add(MECHANICS.ABILITIES.CHARGE, 0);
            //this.abilities.Add(MECHANICS.ABILITIES.DIMENSION_DOOR, 0);
            //this.abilities.Add(MECHANICS.ABILITIES.ENERGY_BARRIER, 0);
            //this.abilities.Add(MECHANICS.ABILITIES.ENERGY_BOMB, 0);
            //this.abilities.Add(MECHANICS.ABILITIES.PSYCHO_KINESIS, 0);
            this.UpdateStats();
        }

        public Player()
        {
            SingleValueStat[] baseStats = new SingleValueStat[]
            {
                new SingleValueStat(Stats.ALL_PRIMARY_STATS, 111f, 0f),
                new SingleValueStat(Stats.CRITICAL_HIT_CHANCE, 5f, 0f),
                new SingleValueStat(Stats.CRITICAL_HIT_DAMAGE, 0f, 0f),
                new SingleValueStat(Stats.ALACRITY, 1f, 0f),
                new SingleValueStat(Stats.BARRIER_BLOCK_CHANCE, 25f, 0f),
                new SingleValueStat(Stats.BARRIER_POTENCY, 1f, 0f),
                new SingleValueStat(Stats.RECOVERY, 1f, 0f),
            };
            this.characterStats = MECHANICS.Convert(baseStats);
            this.abilities.Add(MECHANICS.ABILITIES.BLACKHOLE, new BlackHoleEffect(this));
            this.abilities.Add(MECHANICS.ABILITIES.ENERGY_BARRIER, new BarrierEffect(this));
            //this.abilities.Add(MECHANICS.ABILITIES.CHARGE, 0);
            //this.abilities.Add(MECHANICS.ABILITIES.DIMENSION_DOOR, 0);
            //this.abilities.Add(MECHANICS.ABILITIES.ENERGY_BARRIER, 0);
            //this.abilities.Add(MECHANICS.ABILITIES.ENERGY_BOMB, 0);
            //this.abilities.Add(MECHANICS.ABILITIES.PSYCHO_KINESIS, 0);
            this.UpdateStats();
        }



    }
}
