using System.ComponentModel;
using System.Net;
using System.Windows;
using UrTQueryWpf.Properties;

namespace UrTQueryWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            _ServerListDataInit();
            Address.Text = Settings.Default.LastIP;
            Port.Text = Settings.Default.LastPort;
            CheckIpPort();

            Combo.SelectedIndex = 0;

            _mainQuery.InfoResponseEvent += _MainQuery_infoResponseEvent;
            _mainQuery.StatusResponseEvent += _MainQuery_statusResponseEvent;
            _mainQuery.PrintResponseEvent += _MainQuery_printResponseEvent;
            _mainQuery.OtherResponseEvent += _MainQuery_printResponseEvent;
            _mainQuery.ServerResponseEvent += _MainQuery_serverResponseEvent;
            _mainQuery.NewServerEvent += _MainQuery_NewServerEvent;

            _refreshTimer.Tick += _refreshTimer_Tick;
            _refreshTimer.Interval = new System.TimeSpan(0, 0, 1);

            _mainQuery.Master(Dns.GetHostAddresses("master.urbanterror.info")[0], 27900, 68, true, true);
            _mainQuery.Send("",_ip, _port);
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            Settings.Default.LastIP = Address.Text;
            Settings.Default.LastPort = Port.Text;
            Settings.Default.Save();
            _mainQuery.Close();
        }
    }
}
