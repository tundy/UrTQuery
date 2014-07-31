using System.ComponentModel;
using System.Data;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using UrTQueryWpf.Properties;

namespace UrTQueryWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _ServerListDataInit();
            Address.Text = Settings.Default.LastIP;
            Port.Text = Settings.Default.LastPort;
            CheckIpPort();

            Combo.SelectedIndex = 0;

            _MainQuery.infoResponseEvent += _MainQuery_infoResponseEvent;
            _MainQuery.statusResponseEvent += _MainQuery_statusResponseEvent;
            _MainQuery.printResponseEvent += _MainQuery_printResponseEvent;
            _MainQuery.otherResponseEvent += _MainQuery_printResponseEvent;
            _MainQuery.serverResponseEvent += _MainQuery_serverResponseEvent;
            _MainQuery.NewServerEvent += _MainQuery_NewServerEvent;

            _refreshTimer.Tick += _refreshTimer_Tick;
            _refreshTimer.Interval = new System.TimeSpan(0, 0, 1);

            _MainQuery.Master(Dns.GetHostAddresses("master.urbanterror.info")[0], 27900, 68, true, true);
            _MainQuery.Send("",_ip, _port);

        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            Settings.Default.LastIP = Address.Text;
            Settings.Default.LastPort = Port.Text;
            Settings.Default.Save();
            _MainQuery.Close();
        }
    }
}
