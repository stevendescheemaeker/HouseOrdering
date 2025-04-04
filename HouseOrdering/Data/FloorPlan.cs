using System;
using System.Collections.Generic;

namespace HouseOrdering.Data
{
    [Serializable]
    public class FloorPlan
    {
        public Floor Floor { get; set; }

        public List<ItemBase> Items { get; private set; }

        public FloorPlan()
        {
            Items = new List<ItemBase>();
        }
    }
}
