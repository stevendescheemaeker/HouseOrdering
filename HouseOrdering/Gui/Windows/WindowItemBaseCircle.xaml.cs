using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HouseOrdering.Data;

namespace HouseOrdering.Gui.Windows
{
    /// <summary>
    /// Interaction logic for WindowItemBaseCircle.xaml
    /// </summary>
    public partial class WindowItemBaseCircle : Window
    {
        ItemBaseCircle mItem;

        public WindowItemBaseCircle(ItemBaseCircle item)
        {
            InitializeComponent();

            mItem = item;

            tbCircle.Text = mItem.Diameter.ToString();
            tbName.Text = item.Name;

            sliderR.Value = mItem.BackGround.R;
            sliderG.Value = mItem.BackGround.G;
            sliderB.Value = mItem.BackGround.B;
            sliderA.Value = mItem.BackGround.A;

            DrawItem();
        }

        void DrawItem()
        {
            imgPreview.Source = Utilities.Utilities.GetImage(mItem.Image);
        }

        void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            mItem.Name = tbName.Text;
        }

        void tbCircle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(tbCircle.Text))
            {
                mItem.Diameter = Int32.Parse(tbCircle.Text);
            }
        }

        void tbCircle_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mItem.BackGround = Color.FromArgb((byte)sliderA.Value, (byte)sliderR.Value, (byte)sliderG.Value, (byte)sliderB.Value);
            DrawItem();
        }

        void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
