using Mechanics.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mechanics.Objects
{
    public class Item
    {
        public Item(RuneTypes type, string name, int itemLevel, Quality quality, params StatBase[] stats)
        {
            this.ItemType = type;
            this.Name = name;
            this.ItemLevel = itemLevel;
            this.Quality = quality;
            this.Stats = new List<StatBase>(stats);
        }

        public string Name { get; private set; }

        public RuneTypes ItemType { get; private set; }

        public int ItemLevel { get; private set; }

        public List<StatBase> Stats { get; private set; }

        public Quality Quality { get; private set; }



    }
}
