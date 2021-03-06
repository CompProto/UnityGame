﻿using Mechanics.Enumerations;
using System;
using UnityEngine;

namespace Mechanics.Objects.Abilities
{
    public class BarrierMechanic : AbilityBase
    {
        private float lastUsage;
        private bool isRunning;

        public BarrierMechanic(Character self) : base(self)
        {
            this.lastUsage = Time.time;
            this.isRunning = false;
        }

        public static float Cooldown { get { return MECHANICS.TABLES.SINGLE_TARGET.COOLDOWN.VERY_HIGH; } }
        public static float Duration { get { return MECHANICS.TABLES.SINGLE_TARGET.DURATION.HIGH; } }
        public static Interval BarrierRange { get { return MECHANICS.TABLES.SINGLE_TARGET.DAMAGE.MEDIUM; } }

        public override bool CanApply()
        {
            return (Cooldown * this.self[Stats.ALACRITY]) < Time.time - this.lastUsage;
        }
        public override void Execute(Character target, float factor)
        {
            if (!this.isRunning)
            {
                this.lastUsage = Time.time;
                this.isRunning = true;
            }
            self.Absorb = self.Roll(BarrierRange) * this.self[Stats.BARRIER_POTENCY];
        }
        public override void End()
        {
            this.isRunning = false;
            this.self.Absorb = 0f;
        }
    }
}
