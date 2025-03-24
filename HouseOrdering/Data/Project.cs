using System;
using System.Collections.Generic;
using System.Text;

namespace HouseOrdering.Data
{
    [Serializable]
    public class Project
    {
        public String Name { get; set; }
        public List<FloorPlan> Plans { get; private set; }

        public Project(String name)
        {
            Plans = new List<FloorPlan>();
            Name = name;
        }
    }
}
