using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HouseOrdering.Data;
using HouseOrdering.Gui.UserControls;
using HouseOrdering.Gui.Windows;

namespace HouseOrdering.Gui.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WindowMain : Window
    {
        Project mProject;

        public WindowMain()
        {
            InitializeComponent();

            mProject = null;
        }

        void OpenProject(Project project)
        {
            mProject = project;
            tbProjectName.Text = project.Name;
            lbFloors.ItemsSource = mProject.Plans;
            lbFloors.Items.Refresh();
        }

        void btnNewProject_Click(object sender, RoutedEventArgs e)
        {
            Project newproject = new Project("<noname>");
            OpenProject(newproject);
        }

        void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        void btnOpen_Click(object sender, RoutedEventArgs e)
        {

        }

        void tbProjectName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (mProject != null)
            {
                WindowRename windowRename = new WindowRename(mProject.Name);
                windowRename.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                windowRename.ShowDialog();

                if (windowRename.DialogResult == true)
                {
                    mProject.Name = windowRename.Text;
                    tbProjectName.Text = mProject.Name;
                }
            }
        }

        void tbProjectName_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as TextBlock).Foreground = new SolidColorBrush(Colors.LightGray);
        }

        void tbProjectName_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as TextBlock).Foreground = new SolidColorBrush(Colors.Black);
        }

        void lbFloors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pnlFloor.Children.Count == 1)
            {
                (pnlFloor.Children[0] as UserControlFloorPlan).OnClosing();
            }

            pnlFloor.Children.Clear();

            if (lbFloors.SelectedItem != null)
            {
                FloorPlan plan = lbFloors.SelectedItem as FloorPlan;
                pnlFloor.Children.Add(new UserControlFloorPlan(plan));
            }
        }

        void FloorRename_Click(object sender, RoutedEventArgs e)
        {
            FloorPlan plan = (sender as Button).DataContext as FloorPlan;
            if (plan != null)
            {
                WindowRename windowRename = new WindowRename(plan.Name);
                windowRename.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                windowRename.ShowDialog();

                if (windowRename.DialogResult == true)
                {
                    plan.Name = windowRename.Text;
                    lbFloors.Items.Refresh();
                }
            }
        }


        void FloorRemove_Click(object sender, RoutedEventArgs e)
        {
            FloorPlan plan = (sender as Button).DataContext as FloorPlan;
            if (plan != null && mProject != null)
            {
                mProject.Plans.Remove(plan);
                lbFloors.Items.Refresh();
            }
        }

        void addFloor_Click(object sender, RoutedEventArgs e)
        {
            if (mProject != null)
            {
                FloorPlan floorPlan = new FloorPlan();
                Floor floor = new Floor();
                floorPlan.Floor = floor;
                mProject.Plans.Add(floorPlan);

                WindowItemBase plan = new WindowItemBase(floor);
                plan.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                plan.ShowDialog();

                OpenProject(mProject);
            }
        }
    }
}