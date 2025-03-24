using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HouseOrdering.Data
{
    [Serializable]
    public abstract class ItemBase
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Color BackGround
        {
            get
            {
                return (mPolygon.Fill as SolidColorBrush).Color;
            }
            set
            {
                mPolygon.Fill = new SolidColorBrush(value);
            }
        }

        public Polygon DrawItem
        {
            get
            {
                int index = 0;
                double length;
                PointCollection points = new PointCollection();
                points.Add(new Point(0, 0));

                foreach (PointDirection pointDirection in Directions)
                {
                    Point point = new Point(0, 0);

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

                mPolygon.Points = points;

                return mPolygon;
            }
        }

        public List<PointDirection> Directions { get; private set; }

        protected Polygon mPolygon;

        protected ItemBase()
        {
            X = 0;
            Y = 0;

            Directions = new List<PointDirection>();

            mPolygon = new Polygon();
            mPolygon.Stroke = new SolidColorBrush(Colors.Gray);
            mPolygon.StrokeThickness = 1;

            BackGround = Colors.LawnGreen;
        }
    }
}
