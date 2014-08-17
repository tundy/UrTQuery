using System.Diagnostics;
using System.IO;
using System.Windows;

namespace UrTQueryWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //Disable shutdown when the dialog closes
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            if (!File.Exists("QuakeQueryDll.dll"))
            {
                MessageBox.Show("Quake Query Dll not found !", "Missing Dll", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown(-1);
            }
            else if (System.String.Compare(FileVersionInfo.GetVersionInfo("QuakeQueryDll.dll").ProductVersion, "3.4.2.0", System.StringComparison.Ordinal) < 0)
            {
                MessageBox.Show("Quake Query Dll is too old !", "Wrong version", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown(-1);
            }
            else
            {
                if (System.String.Compare(FileVersionInfo.GetVersionInfo("QuakeQueryDll.dll").ProductVersion, "3.4.4.2", System.StringComparison.Ordinal) != 0)
                    MessageBox.Show("Quake Query Dll is different than version that was used for compiling !", "Wrong version", MessageBoxButton.OK, MessageBoxImage.Warning);
                //Re-enable normal shutdown mode.
                Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                var mainWindow = new MainWindow();
                Current.MainWindow = mainWindow;
                mainWindow.Show();
            }
        }
    }
}
