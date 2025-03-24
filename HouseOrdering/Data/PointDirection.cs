using System;

namespace HouseOrdering.Data
{
    [Serializable]
    public class PointDirection
    {
        public int Length { get; set; }
        public Direction Direction { get; set; }

        public PointDirection()
        {
            Length = 0;
            Direction = Direction.NONE;
        }
    }
}
