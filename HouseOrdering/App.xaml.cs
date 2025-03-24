using System;
using System.Windows;
using System.Windows.Threading;

namespace HouseOrdering
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(String.Format("{0}{1}{2}", e.Exception.Message, Environment.NewLine, e.Exception.StackTrace), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            if (e.Exception.InnerException != null)
            {
                MessageBox.Show(String.Format("{0}{1}{2}", e.Exception.InnerException.Message, Environment.NewLine, e.Exception.InnerException.StackTrace), "Error Inner", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            e.Handled = true;
        }

    }
}
