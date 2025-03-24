using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using HouseOrdering.Data;
using HouseOrdering.Gui.UserControls;

namespace HouseOrdering.Gui.Windows
{
    /// <summary>
    /// Interaction logic for WindowItemBase.xaml
    /// </summary>
    public partial class WindowItemBase : Window
    {
        ItemBase mItem;

        public WindowItemBase(ItemBase item)
        {
            InitializeComponent();

            mItem = item;

            sliderR.Value = mItem.BackGround.R;
            sliderG.Value = mItem.BackGround.G;
            sliderB.Value = mItem.BackGround.B;
            sliderA.Value = mItem.BackGround.A;


            DrawItem();
            SetPoints();

            this.Closing += WindowItemBase_Closing;
        }

        private void WindowItemBase_Closing(object sender, CancelEventArgs e)
        {
            canItem.Children.Clear();
        }


        void DrawItem()
        {
            canItem.Children.Clear();
            canItem.Children.Add(mItem.DrawItem);
        }

        void RemoveItem(PointDirection direction)
        {
            mItem.Directions.Remove(direction);
            DrawItem();
            SetPoints();
        }

        void SetPoints()
        {
            pnlPoints.Children.Clear();
            foreach (PointDirection direction in mItem.Directions)
            {
                pnlPoints.Children.Add(new UserControlPolygonePoint(direction, DrawItem, RemoveItem));
            }
        }

        void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mItem.BackGround = Color.FromArgb((byte)sliderA.Value, (byte)sliderR.Value, (byte)sliderG.Value, (byte)sliderB.Value);
        }

        void btnAddPoint_Click(object sender, RoutedEventArgs e)
        {
            mItem.Directions.Add(new PointDirection());
            SetPoints();
        }

        void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
