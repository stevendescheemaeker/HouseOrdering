using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using HouseOrdering.Data;

namespace HouseOrdering.Gui.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlPolygonePoint.xaml
    /// </summary>
    public partial class UserControlPolygonePoint : UserControl
    {
        internal class BorderSelection
        {
            public bool Selected { get; set; }
            public Border Border { get; private set; }
            public Direction Direction { get; private set; }

            public BorderSelection(Border border, Direction direction)
            {
                Border = border;
                Selected = false;
                Direction = direction;
            }
        }

        PointDirection mPointDirection;
        List<BorderSelection> mDirection;
        Action mCallBack;
        Action<PointDirection> mRemove;

        public UserControlPolygonePoint(PointDirection pointDirection, Action callback, Action<PointDirection> removeItem)
        {
            InitializeComponent();

            mPointDirection = pointDirection;
            mCallBack = callback;
            mRemove = removeItem;

            mDirection = new List<BorderSelection>();
            mDirection.Add(new BorderSelection(borderLeft, Direction.LEFT));
            mDirection.Add(new BorderSelection(borderLeftUp, Direction.LEFT_UP));
            mDirection.Add(new BorderSelection(borderUp, Direction.UP));
            mDirection.Add(new BorderSelection(borderUpRight, Direction.UP_RIGHT));
            mDirection.Add(new BorderSelection(borderRight, Direction.RIGHT));
            mDirection.Add(new BorderSelection(borderRightDown, Direction.RIGHT_DOWN));
            mDirection.Add(new BorderSelection(borderDown, Direction.DOWN));
            mDirection.Add(new BorderSelection(borderDownLeft, Direction.DOWN_LEFT));

            tbLength.Text = pointDirection.Length.ToString();
            BorderSelection dir = mDirection.FirstOrDefault(d => d.Direction == pointDirection.Direction);
            if (dir != null)
            {
                dir.Selected = true;
                UpdateDirections();
            }
        }
        void UpdateDirections()
        {
            foreach (BorderSelection borderSelection in mDirection)
            {
                borderSelection.Border.Background = new SolidColorBrush(borderSelection.Selected ? Colors.LightGreen : Colors.White);
            }
        }

        void tbLength_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        void border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Border border = sender as Border;

            foreach (BorderSelection borderSelection in mDirection)
            {
                if (border == borderSelection.Border)
                {
                    borderSelection.Selected = true;
                    mPointDirection.Direction = borderSelection.Direction;
                }
                else
                {
                    borderSelection.Selected = false;
                }
            }

            UpdateDirections();
            mCallBack();
        }

        void tbLength_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.TryParse(tbLength.Text, out int result))
            {
                mPointDirection.Length = result;
                mCallBack();
            }
        }

        void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            mRemove(mPointDirection);
        }
    }
}
