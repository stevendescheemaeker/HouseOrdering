using System;
using System.Windows;

namespace HouseOrdering.Gui.Windows
{
    /// <summary>
    /// Interaction logic for WindowRename.xaml
    /// </summary>
    public partial class WindowRename : Window
    {
        public String Text
        {
            get
            {
                return tbName.Text;
            }
        }

        public WindowRename(String current)
        {
            InitializeComponent();

            tbName.Text = current;
        }

        void btnApply_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
