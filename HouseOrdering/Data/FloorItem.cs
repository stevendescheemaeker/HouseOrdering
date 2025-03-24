using System;

namespace HouseOrdering.Data
{
    [Serializable]
    public class FloorItem : ItemBase
    {
        public bool Dragging { get; set; }

        public FloorItem()
            : base()
        {
            Dragging = false;
        }
    }
}
