using System;

namespace HouseOrdering.Data
{
    [Serializable]
    public class Floor : ItemBasePolygon
    {
        public Floor()
            : base()
        {
            Locked = true;
        }
    }
}
