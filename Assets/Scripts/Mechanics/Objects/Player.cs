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
            this.abilities.Add(MECHANICS.ABILITIES.BLACKHOLE, new BlackHoleMechanic(this));
            this.abilities.Add(MECHANICS.ABILITIES.ENERGY_BARRIER, new BarrierMechanic(this));
            this.abilities.Add(MECHANICS.ABILITIES.ASTRAL_PRESENCE, new AstralPressenceMechanic(this));
            this.abilities.Add(MECHANICS.ABILITIES.ENERGY_BOMB, new EnergyBombMechanic(this));
            this.abilities.Add(MECHANICS.ABILITIES.PSYCHO_KINESIS, new PsychokinesisMechanic(this));
            this.abilities.Add(MECHANICS.ABILITIES.DIMENSION_DOOR, new DimensionDoorMechanic(this));
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
            this.abilities.Add(MECHANICS.ABILITIES.BLACKHOLE, new BlackHoleMechanic(this));
            this.abilities.Add(MECHANICS.ABILITIES.ENERGY_BARRIER, new BarrierMechanic(this));
            this.abilities.Add(MECHANICS.ABILITIES.ASTRAL_PRESENCE, new AstralPressenceMechanic(this));
            this.abilities.Add(MECHANICS.ABILITIES.ENERGY_BOMB, new EnergyBombMechanic(this));
            this.abilities.Add(MECHANICS.ABILITIES.PSYCHO_KINESIS, new PsychokinesisMechanic(this));
            this.abilities.Add(MECHANICS.ABILITIES.DIMENSION_DOOR, new DimensionDoorMechanic(this));
            this.UpdateStats();
        }



    }
}
