using System.Globalization;
using System.Net.Sockets;
using System.Windows.Documents;
using QuakeQueryDll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace UrTQuery
{
    public partial class MainWindow
    {
        private readonly Dictionary<string, TmpServer> _tmpServers = new Dictionary<string, TmpServer>();
        private static readonly DataTable ServerListDataTable = new DataTable();
        private class TmpServer
        {
            private ushort _atempts;
            public string Ip = string.Empty;
            public ushort Port;
            public ushort Attempts => _atempts++;
        }

        private void ServerListDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "ID")
            {
                e.Cancel = true;
            }
            else if (e.PropertyType == typeof (FlowDocument))
            {
                var templateColumn = new DataGridTemplateColumn
                {
                    Header = e.PropertyName,
                    CellTemplate = (DataTemplate) Resources["RichTextBoxCellTemplate"]
                };
                e.Column = templateColumn;
            }
        }
        private void _ServerListDataInit()
        {
            ServerListDataTable.TableName = "_serverListDataTable";
            ServerListDataTable.Columns.Add("ID", typeof(ushort));
            ServerListDataTable.Columns[0].AutoIncrement = true;
            ServerListDataTable.Columns[0].AutoIncrementSeed = 1;
            ServerListDataTable.Columns[0].AutoIncrementStep = 1;
            ServerListDataTable.Columns.Add("Version", typeof(string));
            //ServerListDataTable.Columns.Add("test", typeof(RichTextBox));
            ServerListDataTable.Columns.Add("Hostname", typeof(string));
            //ServerListDataTable.Columns[2].
            ServerListDataTable.Columns.Add("Map Name", typeof(string));
            ServerListDataTable.Columns.Add("Game Type", typeof(string));
            ServerListDataTable.Columns.Add("Clients", typeof(string));
            ServerListDataTable.Columns.Add("Max Clients", typeof(short));
            ServerListDataTable.Columns.Add("IP Address", typeof(string));
            ServerListDataTable.Columns.Add("Port", typeof(ushort));
            ServerListDataTable.Columns.Add("Ping", typeof(ushort));
            //ServerListDataTable.Columns.Add("Hostname", typeof(string));
            ServerListDataTable.PrimaryKey = new[] { ServerListDataTable.Columns[7], ServerListDataTable.Columns[8] };

            ServerListDataGrid.ItemsSource = ServerListDataTable.DefaultView;
        }
        
        private void UpdateStatus()
        {
            Dispatcher.Invoke(() =>
            {
                Done.Text = ServerListDataTable.Rows.Count.ToString(CultureInfo.InvariantCulture);
                Pending.Text = _tmpServers.Count.ToString(CultureInfo.InvariantCulture);
                Total.Text = _mainQuery.Servers.Count.ToString(CultureInfo.InvariantCulture);
                TaskbarItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Normal;
                TaskbarItemInfo.ProgressState = _tmpServers.Count == 0 ? System.Windows.Shell.TaskbarItemProgressState.None : System.Windows.Shell.TaskbarItemProgressState.Indeterminate;
            });
        }

        private void _refreshTimer_Tick(object sender, EventArgs e)
        {
            if (_tmpServers.Count == 0)
                _refreshTimer.Stop();
            foreach (var server in _tmpServers.ToList())
            {
                _mainQuery.GetInfo(server.Value.Ip, server.Value.Port);
                if (server.Value.Attempts <= 5) continue;
                _tmpServers.Remove(server.Key);
                UpdateStatus();
            }
        }

        private void _MainQuery_NewServerEvent(Server sender)
        {
            _mainQuery.GetInfo(sender.IP, sender.Port);
            Dispatcher.Invoke(() =>
            {
                var tmp = new TmpServer { Ip = sender.IP, Port = (ushort)sender.Port };
                _tmpServers[sender.ToString()] = tmp;
            });
            if (!_refreshTimer.IsEnabled)
                _refreshTimer.Start();
        }

        private void GetNewList_Click(object sender, RoutedEventArgs e)
        {
            _refreshTimer.Stop();
            _mainQuery.ClearList();
            Refresh_Click(this, e);
            try
            {
                _mainQuery.Master(Dns.GetHostAddresses("master.urbanterror.info")[0], 27900, 68, true, true);
                _mainQuery.Send("", Ip, Port);
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode != SocketError.HostNotFound)
                    throw;
            }
        }
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            _refreshTimer.Stop();
            _tmpServers.Clear();
            ServerListDataTable.Clear();
            ServerListDataTable.Columns[0].AutoIncrementSeed = -1;
            ServerListDataTable.Columns[0].AutoIncrementStep = -1;
            ServerListDataTable.Columns[0].AutoIncrementSeed = 1;
            ServerListDataTable.Columns[0].AutoIncrementStep = 1;
            foreach (var server in _mainQuery.Servers.ToList())
            {
                _tmpServers[server.Key] = new TmpServer { Ip = server.Value.IP, Port = (ushort)server.Value.Port };
            }
            if (!_refreshTimer.IsEnabled)
                _refreshTimer.Start();
            ServerListDataGrid.Items.Refresh();
            UpdateStatus();
        }

        private void _ServerListDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var currentItem = ((DataGrid)sender).CurrentItem;
            if (currentItem == null) return;
            var currentRow = ((DataRowView)currentItem).Row;

            if (Ip.ToString() != currentRow[7].ToString() && Port.ToString(CultureInfo.InvariantCulture) != currentRow[8].ToString())
            {
                Address.Text = currentRow[7].ToString();
                Port = (ushort)currentRow[8];
                Clear_Click(sender, e);
            }

            Tabs.SelectedIndex = 0;
            ((DataGrid)sender).CurrentItem = null;
            if (e != null) e.Handled = true;
        }
        private void Info_RightClick(object sender, RoutedEventArgs e)
        {
            var currentRow = ((DataRowView)ServerListDataGrid.CurrentItem).Row;
            _mainQuery.GetInfo(currentRow[7].ToString(), ushort.Parse(currentRow[8].ToString()));
        }
        private void Status_RightClick(object sender, RoutedEventArgs e)
        {
            _ServerListDataGrid_MouseDoubleClick(ServerListDataGrid, null);
            GetStatus_Click(sender, e);
        }
    }
}
