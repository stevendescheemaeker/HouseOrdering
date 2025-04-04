using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace HouseOrdering.Data
{
    [Serializable]
    public class ItemBaseCircle : ItemBase
    {
        public int Diameter { get; set; }

        public override Image Image
        {
            get
            {
                if (Diameter < 1)
                {
                    return null;
                }

                Bitmap bitmap = new Bitmap(Diameter, Diameter);
                using (Graphics gr = Graphics.FromImage(bitmap))
                {
                    gr.SmoothingMode = SmoothingMode.HighQuality;
                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    gr.FillEllipse(new SolidBrush(BackGround), new Rectangle(new Point(0, 0), new Size(Diameter, Diameter)));
                    gr.DrawEllipse(new Pen(Locked ? Brushes.Black : Brushes.Gray, 1), new Rectangle(new Point(0, 0), new Size(Diameter, Diameter)));
                }

                return bitmap;
            }
        }

        public ItemBaseCircle()
            : base()
        {
            Diameter = 100;
        }
    }
}
