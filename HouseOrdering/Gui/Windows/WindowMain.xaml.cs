using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using HouseOrdering.Data;
using HouseOrdering.Gui.UserControls;
using Microsoft.Win32;

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
            if (mProject != null)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = String.Format("{0} (*.{1})|*.{1}", "HouseOrdening file", "ho");

                if (save.ShowDialog() == true)
                {
                    if (!Utilities.Utilities.SaveObjectToFile<Project>(save.FileName, mProject))
                    {
                        MessageBox.Show("Opslaan mislukt", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = String.Format("{0} (*.{1})|*.{1}", "HouseOrdening file", "ho");

            if (open.ShowDialog() == true)
            {
                if (Utilities.Utilities.GetObjectFromFile<Project>(open.FileName, out Project output))
                {
                    OpenProject(output);
                }
                else
                {
                    MessageBox.Show("Openen mislukt", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
            pnlFloor.Children.Clear();

            if (lbFloors.SelectedItem != null)
            {
                FloorPlan plan = lbFloors.SelectedItem as FloorPlan;
                pnlFloor.Children.Add(new UserControlFloorPlan(plan));
            }
        }

        void FloorEdit_Click(object sender, RoutedEventArgs e)
        {
            FloorPlan plan = (sender as Button).DataContext as FloorPlan;
            if (plan != null)
            {
                WindowItemBasePolygon windowEdit = new WindowItemBasePolygon(plan.Floor);
                windowEdit.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                windowEdit.ShowDialog();

                if (windowEdit.DialogResult == true)
                {
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

                WindowItemBasePolygon plan = new WindowItemBasePolygon(floor);
                plan.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                plan.ShowDialog();

                OpenProject(mProject);
            }
        }

        void btnCloneFloor_Click(object sender, RoutedEventArgs e)
        {
            if (mProject == null)
                return;

            if (lbFloors.SelectedItem == null)
                return;

            mProject.Plans.Add(Utilities.Utilities.CloneObject<FloorPlan>(lbFloors.SelectedItem as FloorPlan));
            OpenProject(mProject);
        }

        void btnAddFloor_Click(object sender, RoutedEventArgs e)
        {
            if (mProject != null)
            {
                FloorPlan floorPlan = new FloorPlan();
                Floor floor = new Floor();
                floorPlan.Floor = floor;
                mProject.Plans.Add(floorPlan);

                WindowItemBasePolygon plan = new WindowItemBasePolygon(floor);
                plan.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                plan.ShowDialog();

                OpenProject(mProject);
            }
        }
    }
}