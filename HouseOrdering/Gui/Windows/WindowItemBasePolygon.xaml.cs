using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HouseOrdering.Data;
using HouseOrdering.Gui.UserControls;

namespace HouseOrdering.Gui.Windows
{
    /// <summary>
    /// Interaction logic for WindowItemBase.xaml
    /// </summary>
    public partial class WindowItemBasePolygon : Window
    {
        ItemBasePolygon mItem;

        public WindowItemBasePolygon(ItemBasePolygon item)
        {
            InitializeComponent();

            mItem = item;

            tbName.Text = item.Name;
            tbAngle.Text = item.RotateAngle.ToString();

            sliderR.Value = mItem.BackGround.R;
            sliderG.Value = mItem.BackGround.G;
            sliderB.Value = mItem.BackGround.B;
            sliderA.Value = mItem.BackGround.A;

            sliderR.ValueChanged += Slider_ValueChanged;
            sliderG.ValueChanged += Slider_ValueChanged;
            sliderB.ValueChanged += Slider_ValueChanged;
            sliderA.ValueChanged += Slider_ValueChanged;

            DrawItem();
            SetPoints();
        }

        void DrawItem()
        {
            imgPreview.Source = Utilities.Utilities.GetImage(mItem.Image);
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

        void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            mItem.Name = tbName.Text;
        }

        void tbAngle_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        void tbAngle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.TryParse(tbAngle.Text, out int result))
            {
                mItem.RotateAngle = result;
                SetPoints();
            }
        }

        void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mItem.BackGround = Color.FromArgb((byte)sliderA.Value, (byte)sliderR.Value, (byte)sliderG.Value, (byte)sliderB.Value);
            DrawItem();
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
