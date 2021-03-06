﻿using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Windows.Threading;
using UrTQuery.Properties;

namespace UrTQuery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly DispatcherTimer _refreshTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            _ServerListDataInit();
            Address.Text = Settings.Default.LastIP;
            Port = Settings.Default.LastPort;

            Combo.SelectedIndex = 0;

            _mainQuery.InfoResponseEvent += _MainQuery_infoResponseEvent;
            _mainQuery.StatusResponseEvent += _MainQuery_statusResponseEvent;
            _mainQuery.PrintResponseEvent += _MainQuery_printResponseEvent;
            _mainQuery.OtherResponseEvent += _MainQuery_printResponseEvent;
            _mainQuery.ServerResponseEvent += _MainQuery_serverResponseEvent;
            _mainQuery.NewServerEvent += _MainQuery_NewServerEvent;

            _refreshTimer.Tick += _refreshTimer_Tick;
            _refreshTimer.Interval = new System.TimeSpan(0, 0, 1);

            try
            {
                _mainQuery.Master(Dns.GetHostAddresses("master.urbanterror.info")[0], 27900, 68, true, true);
                _mainQuery.Send("", Ip, Port);
            }
            catch (SocketException ex) when (ex.SocketErrorCode == SocketError.HostNotFound)
            {
                // Ignore this one & throw others.
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            Settings.Default.LastIP = Address.Text;
            Settings.Default.LastPort = Port;
            Settings.Default.Save();
            _mainQuery.Close();
        }
    }
}
