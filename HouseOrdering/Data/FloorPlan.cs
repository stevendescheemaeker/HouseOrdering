using System;
using System.Collections.Generic;

namespace HouseOrdering.Data
{
    [Serializable]
    public class FloorPlan
    {
        public Floor Floor { get; set; }

        public String Name { get; set; }

        public List<FloorItem> Items { get; private set; }

        public FloorPlan()
        {
            Name = "<unknown>";
            Items = new List<FloorItem>();
        }
    }
}
