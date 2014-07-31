using QuakeQueryDll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace UrTQueryWpf
{
    public partial class MainWindow
    {
        private Dictionary<string, tmp_server> _tmpServers = new Dictionary<string, tmp_server>();
        private DataTable _ServerListDataTable = new DataTable();

        private void _ServerListDataInit()
        {
            _ServerListDataTable.Columns.Add("ID", typeof(ushort));
            _ServerListDataTable.Columns[0].AutoIncrement = true;
            _ServerListDataTable.Columns[0].AutoIncrementSeed = 1;
            _ServerListDataTable.Columns[0].AutoIncrementStep = 1;
            _ServerListDataTable.Columns.Add("Version", typeof(string));
            _ServerListDataTable.Columns.Add("Hostname", typeof(string));
            _ServerListDataTable.Columns.Add("Map Name", typeof(string));
            _ServerListDataTable.Columns.Add("Game Type", typeof(string));
            _ServerListDataTable.Columns.Add("Clients", typeof(ushort));
            _ServerListDataTable.Columns.Add("Max Clients", typeof(ushort));
            _ServerListDataTable.Columns.Add("IP Address", typeof(string));
            _ServerListDataTable.Columns.Add("Port", typeof(ushort));
            _ServerListDataTable.Columns.Add("Ping", typeof(ushort));
            _ServerListDataTable.PrimaryKey = new DataColumn[2] { _ServerListDataTable.Columns[7], _ServerListDataTable.Columns[8] };

            _ServerListDataGrid.ItemsSource = _ServerListDataTable.DefaultView;
        }
        
        private void UpdateStatus()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                Done.Text = _ServerListDataTable.Rows.Count.ToString();
                Pending.Text = _tmpServers.Count.ToString();
                Total.Text = _MainQuery.Servers.Count.ToString();
            }));
        }
        private class tmp_server
        {
            private ushort _atempts = 0;
            public string IP = string.Empty;
            public ushort Port = new ushort();
            public ushort Attempts { get { return _atempts++; } }
        }
        private DispatcherTimer _refreshTimer = new DispatcherTimer();
        void _refreshTimer_Tick(object sender, System.EventArgs e)
        {
            if (_tmpServers.Count == 0)
                _refreshTimer.Stop();
            foreach (var server in _tmpServers.ToList())
            {
                _MainQuery.GetInfo(server.Value.IP, server.Value.Port);
                if (server.Value.Attempts > 10)
                {
                    _tmpServers.Remove(server.Key);
                    UpdateStatus();
                }
            }
        }

        private void _MainQuery_NewServerEvent(Server sender)
        {
            _MainQuery.GetInfo(sender.IP, sender.Port);
            this.Dispatcher.Invoke((Action)(() =>
            {
                tmp_server tmp = new tmp_server() { IP = sender.IP, Port = sender.Port };
                _tmpServers[sender.ToString()] = tmp;
            }));
            if (!_refreshTimer.IsEnabled)
                _refreshTimer.Start();
        }

        private void GetNewList_Click(object sender, RoutedEventArgs e)
        {
            _refreshTimer.Stop();
            _MainQuery.ClearList();
            Refresh_Click(this, e);
            _MainQuery.Master(Dns.GetHostAddresses("master.urbanterror.info")[0], 27900, 68, true, true);
        }
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            _refreshTimer.Stop();
            _tmpServers.Clear();
            _ServerListDataTable.Clear();
            _ServerListDataTable.Columns[0].AutoIncrementSeed = -1;
            _ServerListDataTable.Columns[0].AutoIncrementStep = -1;
            _ServerListDataTable.Columns[0].AutoIncrementSeed = 1;
            _ServerListDataTable.Columns[0].AutoIncrementStep = 1;
            foreach (var server in _MainQuery.Servers.ToList())
            {
                _tmpServers[server.Key] = new tmp_server { IP = server.Value.IP.ToString(), Port = server.Value.Port };
            }
            if (!_refreshTimer.IsEnabled)
                _refreshTimer.Start();
            _ServerListDataGrid.Items.Refresh();
            UpdateStatus();
        }

        private void _ServerListDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var CurrentRow = ((DataRowView)((DataGrid)sender).CurrentItem).Row;
            if (_ip.ToString() != CurrentRow[7].ToString() && _port.ToString() != CurrentRow[8].ToString())
            {
                this.Address.Text = CurrentRow[7].ToString();
                this.Port.Text = CurrentRow[8].ToString();
                CheckIpPort();
                Clear_Click(sender, e);
            }

            Tabs.SelectedIndex = 0;
            if (e != null)
                e.Handled = true;
        }
        private void Info_RightClick(object sender, RoutedEventArgs e)
        {
            var CurrentRow = ((DataRowView)_ServerListDataGrid.CurrentItem).Row;
            _MainQuery.GetInfo(CurrentRow[7].ToString(), ushort.Parse(CurrentRow[8].ToString()));
        }
        private void Status_RightClick(object sender, RoutedEventArgs e)
        {
            _ServerListDataGrid_MouseDoubleClick(_ServerListDataGrid, null);
            GetStatus_Click(sender, e);
        }
    }
}
