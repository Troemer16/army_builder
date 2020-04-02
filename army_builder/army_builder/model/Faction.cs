using System;
using System.Collections.Generic;
using System.Text;

namespace army_builder.model
{
    public class Faction
    {
        private int id;
        public string Name { get; }
        private string tier;

        public Faction(int id, string name, string tier)
        {
            this.id = id;
            this.Name = name;
            this.tier = tier;
        }
    }
}
