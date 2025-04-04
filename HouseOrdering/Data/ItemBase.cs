using System;
using System.Drawing;

namespace HouseOrdering.Data
{
    [Serializable]
    public abstract class ItemBase
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Locked { get; set; }
        public bool Dragging { get; set; }

        public String Name { get; set; }

        public abstract Image Image { get; }

        public Color BackGround { get; set; }

        protected ItemBase()
        {
            X = 0;
            Y = 0;

            Locked = false;
            Dragging = false;

            Name = "<unknown>";
            BackGround = Color.LightBlue;
        }
    }
}
