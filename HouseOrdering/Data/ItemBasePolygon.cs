using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Shapes;

namespace HouseOrdering.Data
{
    [Serializable]
    public class ItemBasePolygon : ItemBase
    {
        public List<PointDirection> Directions { get; private set; }

        public override Image Image
        {
            get
            {
                Polygon polygon = Polygon;
                System.Windows.Media.PointCollection points = Points;

                polygon.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));

                if (polygon.DesiredSize.Width < 1 || polygon.DesiredSize.Height < 1)
                {
                    return null;
                }

                PointF[] drawPoints = new PointF[points.Count];
                                            
                for (int i = 0; i < points.Count; i++)
                {
                    drawPoints[i] = new PointF((float)points[i].X, (float)points[i].Y);
                }

                Bitmap bitmap = new Bitmap((int)polygon.DesiredSize.Width, (int)polygon.DesiredSize.Height);
                using (Graphics gr = Graphics.FromImage(bitmap))
                {
                    gr.SmoothingMode = SmoothingMode.HighQuality;
                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    gr.FillPolygon(new SolidBrush(BackGround), drawPoints);
                    gr.DrawPolygon(new Pen(Locked ? Brushes.Gray : Brushes.LightGreen, 2), drawPoints);
                }

                return bitmap;
            }
        }

        Polygon Polygon
        {
            get
            {
                Polygon polygon = new Polygon();

                polygon.Points = Points;
                polygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(BackGround.A, BackGround.R, BackGround.G, BackGround.B));
                polygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Gray);
                polygon.StrokeThickness = 1;

                return polygon;
            }
        }

        System.Windows.Media.PointCollection Points
        {
            get
            {
                double length;
                int index = 0;
                System.Windows.Media.PointCollection points = new System.Windows.Media.PointCollection();
                points.Add(new System.Windows.Point(0, 0));
                foreach (PointDirection pointDirection in Directions)
                {
                    System.Windows.Point point = new System.Windows.Point(0, 0);

                    switch (pointDirection.Direction)
                    {
                        case Direction.LEFT:

                            point.X = points[index].X - pointDirection.Length;
                            point.Y = points[index].Y;
                            break;

                        case Direction.LEFT_UP:

                            length = Math.Sqrt((Math.Pow(pointDirection.Length, 2) / 2));
                            point.X = points[index].X - length;
                            point.Y = points[index].Y - length;
                            break;

                        case Direction.UP:

                            point.X = points[index].X;
                            point.Y = points[index].Y - pointDirection.Length;
                            break;

                        case Direction.UP_RIGHT:

                            length = Math.Sqrt((Math.Pow(pointDirection.Length, 2) / 2));
                            point.X = points[index].X + length;
                            point.Y = points[index].Y - length;
                            break;

                        case Direction.RIGHT:

                            point.X = points[index].X + pointDirection.Length;
                            point.Y = points[index].Y;
                            break;

                        case Direction.RIGHT_DOWN:

                            length = Math.Sqrt((Math.Pow(pointDirection.Length, 2) / 2));
                            point.X = points[index].X + length;
                            point.Y = points[index].Y + length;
                            break;

                        case Direction.DOWN:

                            point.X = points[index].X;
                            point.Y = points[index].Y + pointDirection.Length;
                            break;

                        case Direction.DOWN_LEFT:

                            length = Math.Sqrt((Math.Pow(pointDirection.Length, 2) / 2));
                            point.X = points[index].X - length;
                            point.Y = points[index].Y + length;
                            break;
                    }

                    points.Add(point);
                    index++;
                }

                return points;
            }
        }

        public ItemBasePolygon()
            : base()
        {
            Directions = new List<PointDirection>();
        }
    }
}
