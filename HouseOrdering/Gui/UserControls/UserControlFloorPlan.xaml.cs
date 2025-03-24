using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HouseOrdering.Data;
using HouseOrdering.Gui.Windows;

namespace HouseOrdering.Gui.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlFloorPlan.xaml
    /// </summary>
    public partial class UserControlFloorPlan : UserControl
    {
        FloorPlan mPlan;

        public UserControlFloorPlan(FloorPlan plan)
        {
            InitializeComponent();

            mPlan = plan;

            DrawFloor();
        }

        public void OnClosing()
        {
            canFloor.Children.Clear();
        }

        void DrawFloor()
        {
            canFloor.Children.Clear();

            /* plan */

            canFloor.Children.Add(mPlan.Floor.DrawItem);

            /* items */

            foreach (FloorItem item in mPlan.Items)
            {
                canFloor.Children.Add(item.DrawItem);

                Canvas.SetLeft(item.DrawItem, item.X);
                Canvas.SetTop(item.DrawItem, item.Y);
            }
        }

        void canFloor_MouseMove(object sender, MouseEventArgs e)
        {
            FloorItem dragging = mPlan.Items.FirstOrDefault(f => f.Dragging);
            if (dragging != null)
            {
                int eX = (int)e.GetPosition(canFloor).X;
                int eY = (int)e.GetPosition(canFloor).Y;
                if (eX < 0) eX = 0;
                if (eX > canFloor.Width) eX = (int)canFloor.Width;
                if (eY < 0) eY = 0;
                if (eY > canFloor.Height) eY = (int)canFloor.Height;

                dragging.X = eX;
                dragging.Y = eY;

                Canvas.SetLeft(dragging.DrawItem, dragging.X);
                Canvas.SetTop(dragging.DrawItem, dragging.Y);
            }
        }

        void canFloor_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point newPoint = new Point((float)(e.GetPosition(canFloor).X), (float)(e.GetPosition(canFloor).Y));
            FloorItem item = mPlan.Items.FirstOrDefault(it => canFloor.InputHitTest(e.GetPosition(canFloor)).Equals(it.DrawItem));
            if (item != null)
            {
                if (e.ClickCount == 2)
                {
                    canFloor.Children.Clear();

                    WindowItemBase windowItemBase = new WindowItemBase(item);
                    windowItemBase.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    windowItemBase.ShowDialog();

                    DrawFloor();
                }
                else
                {
                    item.Dragging = true;
                }
            }

            canFloor.CaptureMouse();
        }

        void canFloor_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            FloorItem item = mPlan.Items.FirstOrDefault(fi => fi.Dragging);
            if (item != null)
            {
                item.Dragging = false;
            }

            canFloor.ReleaseMouseCapture();
        }

        void addItem_Click(object sender, RoutedEventArgs e)
        {
            FloorItem item = new FloorItem();
            mPlan.Items.Add(item);

            WindowItemBase windowItemBase = new WindowItemBase(item);
            windowItemBase.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            windowItemBase.ShowDialog();

            DrawFloor();
        }
    }
}
