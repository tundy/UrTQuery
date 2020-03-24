using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace UrTQuery
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            const string oldVersion = "3.4.3.2";
            const string compiledVersion = "3.6.0.0";

            //Disable shutdown when the dialog closes
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            if (!File.Exists("QuakeQueryDll.dll"))
            {
                MessageBox.Show("Quake Query Dll not found !", "Missing Dll", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown(-1);
            }
            else if (string.CompareOrdinal(FileVersionInfo.GetVersionInfo("QuakeQueryDll.dll").ProductVersion, oldVersion) < 0)
            {
                MessageBox.Show($"Quake Query Dll is too old !{Environment.NewLine}You need at least version {oldVersion}.", "Wrong version", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown(-1);
            }
            else
            {
                if (!string.Equals(FileVersionInfo.GetVersionInfo("QuakeQueryDll.dll").ProductVersion, compiledVersion, StringComparison.Ordinal))
                    MessageBox.Show($"Quake Query Dll is different than version that was used for compiling !{Environment.NewLine}Compiled with version {compiledVersion}", "Wrong version", MessageBoxButton.OK, MessageBoxImage.Warning);
                //Re-enable normal shutdown mode.
                Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                var mainWindow = new MainWindow();
                Current.MainWindow = mainWindow;
                mainWindow.Show();
            }
        }
    }
}
