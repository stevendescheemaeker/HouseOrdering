using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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

        void DrawFloor()
        {
            canFloor.Children.Clear();

            /* plan */

            Image floor = new Image();
            floor.Source = Utilities.Utilities.GetImage(mPlan.Floor.Image);
            RenderOptions.SetBitmapScalingMode(floor, BitmapScalingMode.HighQuality);
            canFloor.Children.Add(floor);

            /* items */

            foreach (ItemBase item in mPlan.Items)
            {
                Image itemImage = new Image();
                itemImage.ToolTip = item.Name;
                itemImage.Source = Utilities.Utilities.GetImage(item.Image);
                itemImage.Tag = item;
                canFloor.Children.Add(itemImage);
                Canvas.SetLeft(itemImage, item.X);
                Canvas.SetTop(itemImage, item.Y);

                RotateTransform rotateTransform = new RotateTransform(item.RotateAngle);
                itemImage.LayoutTransform = rotateTransform;
            }
        }

        void canFloor_MouseMove(object sender, MouseEventArgs e)
        {
            ItemBase dragging = mPlan.Items.FirstOrDefault(f => f.Dragging);
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

                foreach (Image image in canFloor.Children)
                {
                    if (image.Tag != null)
                    {
                        if ((image.Tag as ItemBase).Dragging)
                        {
                            Canvas.SetLeft(image, dragging.X);
                            Canvas.SetTop(image, dragging.Y);
                            break;
                        }
                    }
                }
            }
        }

        void canFloor_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ItemBase item = null;
            Point newPoint = new Point((float)(e.GetPosition(canFloor).X), (float)(e.GetPosition(canFloor).Y));

            foreach (Image image in canFloor.Children)
            {
                if (image.Tag != null)
                {
                    IInputElement element = canFloor.InputHitTest(e.GetPosition(canFloor));
                    if (element != null)
                    {
                        if (element.Equals(image))
                        {
                            item = image.Tag as ItemBase;
                            break;
                        }
                    }
                }
            }

            if (item != null)
            {
                if (e.ClickCount == 2)
                {
                    canFloor.Children.Clear();

                    if (item as ItemBasePolygon != null)
                    {
                        WindowItemBasePolygon windowItemBase = new WindowItemBasePolygon(item as ItemBasePolygon);
                        windowItemBase.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        windowItemBase.ShowDialog();
                    }

                    if (item as ItemBaseCircle != null)
                    {
                        WindowItemBaseCircle windowItemBase = new WindowItemBaseCircle(item as ItemBaseCircle);
                        windowItemBase.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        windowItemBase.ShowDialog();
                    }

                    DrawFloor();
                }
                else if (!item.Locked)
                {
                    item.Dragging = true;
                }
            }

            canFloor.CaptureMouse();
        }

        void canFloor_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ItemBase item = null;
            Image img = null;
            Point newPoint = new Point((float)(e.GetPosition(canFloor).X), (float)(e.GetPosition(canFloor).Y));

            foreach (Image image in canFloor.Children)
            {
                if (image.Tag != null)
                {
                    if (canFloor.InputHitTest(e.GetPosition(canFloor)).Equals(image))
                    {
                        item = image.Tag as ItemBase;
                        img = image;
                       
                        break;
                    }
                }
            }

            if (item != null)
            {
                if (e.ClickCount == 2)
                {
                    if (MessageBox.Show(string.Format("You want to remove {0}?", item.Name), "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                    {
                        mPlan.Items.Remove(item);
                        canFloor.Children.Remove(img);
                    }
                }
                else
                {
                    item.Locked = !item.Locked;
                    img.Source = Utilities.Utilities.GetImage(item.Image);
                }
            }
        }

        void canFloor_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ItemBase item = mPlan.Items.FirstOrDefault(fi => fi.Dragging);
            if (item != null)
            {
                item.Dragging = false;
            }

            canFloor.ReleaseMouseCapture();
        }

        void addPolygon_Click(object sender, RoutedEventArgs e)
        {
            ItemBasePolygon item = new ItemBasePolygon();
            mPlan.Items.Add(item);

            WindowItemBasePolygon windowItemBase = new WindowItemBasePolygon(item);
            windowItemBase.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            windowItemBase.ShowDialog();

            DrawFloor();
        }

        void addCircle_Click(object sender, RoutedEventArgs e)
        {
            ItemBaseCircle item = new ItemBaseCircle();
            mPlan.Items.Add(item);

            WindowItemBaseCircle windowItemBase = new WindowItemBaseCircle(item);
            windowItemBase.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            windowItemBase.ShowDialog();

            DrawFloor();
        }


    }
}
