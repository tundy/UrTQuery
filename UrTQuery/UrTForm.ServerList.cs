using QuakeQueryDll;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace UrTQuery
{
    partial class MainForm
    {
        private List<Server> _ServerList = ServerListSettings.LoadServerList();

        private static DataTable _ServerListData = new DataTable();
        private DataGridView _ServerListTable = new DataGridView();

        private BackgroundWorker _GetListWorker = new BackgroundWorker();
        private BackgroundWorker _RefreshWorker = new BackgroundWorker();

        private void InitializeServerList()
        {
            _ServerListDataInit();
            _ServerListTableInit();
            _GetListWorker.WorkerSupportsCancellation = true;
            _RefreshWorker.WorkerSupportsCancellation = true;

            if (_ServerList == null)
                GetNewListButton_Click(this, null);
            else
                RefreshButton_Click(this, null);
        }

        private void _ServerListDataInit()
        {
            _ServerListData.Columns.Add("ID", typeof(int));
            _ServerListData.Columns[0].AutoIncrement = true;
            _ServerListData.Columns[0].AutoIncrementSeed = 1;
            _ServerListData.Columns[0].AutoIncrementStep = 1;
            _ServerListData.Columns.Add("Version", typeof(string));
            _ServerListData.Columns.Add("Hostname", typeof(string));
            _ServerListData.Columns.Add("Map Name", typeof(string));
            _ServerListData.Columns.Add("Game Type", typeof(string));
            _ServerListData.Columns.Add("Clients", typeof(int));
            _ServerListData.Columns.Add("Max Players", typeof(int));
            _ServerListData.Columns.Add("IP Address", typeof(string));
            _ServerListData.Columns.Add("Port", typeof(ushort));
            _ServerListData.PrimaryKey = new DataColumn[2] { _ServerListData.Columns[7], _ServerListData.Columns[8] };
        }
        private void _ServerListTableInit()
        {
            _ServerListTable.ReadOnly = true;
            _ServerListTable.AllowUserToAddRows = false;
            _ServerListTable.AllowUserToDeleteRows = false;
            _ServerListTable.AllowUserToOrderColumns = false;
            _ServerListTable.AllowUserToResizeColumns = false;
            _ServerListTable.AllowUserToResizeRows = false;
            _ServerListTable.DataSource = _ServerListData;
            _ServerListTable.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            _ServerListTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            _ServerListTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            _ServerListTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            _ServerListTable.Location = new Point(3, this.GetNewListButton.Location.Y + this.GetNewListButton.Height + 3);
            _ServerListTable.Width = this.ServerList.Width - 6;
            _ServerListTable.Height = this.ServerList.Height - _ServerListTable.Location.Y - 3;
            _ServerListTable.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right);
            _ServerListTable.MouseDoubleClick += _ServerListTable_MouseDoubleClick;
            _ServerListTable.PerformLayout();
            _ServerListTable.DataError += _ServerListTable_DataError;
            _ServerListTable.Scroll += _ServerListTable_Scroll;

            this.ServerList.Controls.Add(_ServerListTable);
        }
        private void _ServerListTable_Scroll(object sender, ScrollEventArgs e)
        {
            _ServerListTable.PerformLayout();
        }
        private void _ServerListTable_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Do fucking nothing !
        }
        private void _ServerListTable_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var CurrentRow = (DataGridView)sender;
            this.Address.Text = CurrentRow.CurrentRow.Cells[7].Value.ToString();
            this.Port.Text = CurrentRow.CurrentRow.Cells[8].Value.ToString();
            this.TabPage.SelectedIndex = 0;
            this.Refresh();
            this.GetStatus_Click(sender, null);
        }
        private void RefreshButton_Click(object sender, System.EventArgs e)
        {
            _RefreshWorker.CancelAsync();
            _GetListWorker.CancelAsync();
            RefreshButton.Enabled = false;
            GetNewListButton.Enabled = false;
            BackGroundWorkersCheck.Enabled = true;
        }
        private void GetNewListButton_Click(object sender, System.EventArgs e)
        {
            _ServerList = GetNewList();
            RefreshButton_Click(sender, e);
        }

        private void _GetListWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            bool data = false;
            if (_ServerList != null)
            {
                foreach (var server in _ServerList)
                {
                    if (_GetListWorker.CancellationPending)
                    {
                        _GetListWorker.CancelAsync();
                        break;
                    }
                    else
                        for (int i = 0; i < 3; i++)
                            if (_GetListWorker.CancellationPending)
                            {
                                _GetListWorker.CancelAsync();
                                break;
                            }
                            else if (RefreshServerStatus(server, 1))
                            {
                                i = 3;
                                data = true;
                            }

                    if (_GetListWorker.CancellationPending)
                    {
                        _GetListWorker.CancelAsync();
                        break;
                    }
                    else if (!data)
                    {
                        _RefreshWorker.DoWork += new DoWorkEventHandler(
                            delegate(object RefreshWorkerO, DoWorkEventArgs RefreshWorkerArgs)
                            {
                                RefreshServerStatus(server, 4);
                            });
                        if (!_RefreshWorker.IsBusy)
                            _RefreshWorker.RunWorkerAsync();
                        _RefreshWorker.RunWorkerCompleted += _RefreshWorker_RunWorkerCompleted;
                    }
                }
            }
        }
        private void _RefreshWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _RefreshWorker.Dispose();
        }
        private void _GetListWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _ServerListData.AcceptChanges();
            _GetListWorker.Dispose();
        }
        private void BackGroundWorkersCheck_Tick(object sender, System.EventArgs e)
        {
            if (!_GetListWorker.IsBusy && !_RefreshWorker.IsBusy)
            {
                _ServerListData.AcceptChanges();
                _ServerListData.Clear();
                _ServerListTable.Refresh();
                _ServerListData.Columns[0].AutoIncrementSeed = -1;
                _ServerListData.Columns[0].AutoIncrementStep = -1;
                _ServerListData.Columns[0].AutoIncrementSeed = 1;
                _ServerListData.Columns[0].AutoIncrementStep = 1;

                _GetListWorker.DoWork += _GetListWorker_DoWork;
                _GetListWorker.RunWorkerCompleted += _GetListWorker_RunWorkerCompleted;
                _GetListWorker.RunWorkerAsync();

                GetNewListButton.Enabled = true;
                RefreshButton.Enabled = true;
                BackGroundWorkersCheck.Enabled = false;
                _ServerListTable.Visible = true;
            }
            else
            {
                _ServerListTable.Visible = false;
                _RefreshWorker.CancelAsync();
                _GetListWorker.CancelAsync();
            }
        }

        private List<Server> GetNewList()
        {
            Query MasterQuery = new Query();
            try
            {
                MasterQuery = new Query(Dns.GetHostAddresses("master.urbanterror.info")[0], 27900);
            }
            catch (SocketException)
            {
                return null;
            }
            return MasterQuery.Master(68, true, true);
        }
        private bool RefreshServerStatus(Server server, int Attempts)
        {
            Query BackQuery = new Query();
            BackQuery.IP = server.IP;
            BackQuery.PortNumber = server.Port;
            var info = new Dictionary<string, string>();
            for (int i = 0; i < Attempts; i++)
                if (_RefreshWorker.CancellationPending)
                {
                    _RefreshWorker.CancelAsync();
                    break;
                }
                else if (info.Count == 0)
                    info = BackQuery.GetInfo();
                else
                    break;

            if (info.Count == 0)
                return false;

            AddServer(info, server);
            return true;

        }
        private void AddServer(Dictionary<string, string> info, Server server)
        {
            /*if (!info.ContainsKey("hostname"))
                info.Add("hostname", server.IP.ToString());*/

            _ServerListData.BeginInit();
            try
            {
                _ServerListData.Rows.Add(null, info["modversion"], info["hostname"], info["mapname"], info["gametype"], info["clients"], info["sv_maxclients"], server.IP.ToString(), server.Port);
            }
            /*catch (ConstraintException)
            {
                var FoundRow = _ServerListData.Rows.Find(new object[] { server.IP.ToString(), server.Port });
                if (FoundRow != null)
                {
                    if (info.ContainsKey("modversion"))
                        if (FoundRow[1].ToString() != info["modversion"])
                            FoundRow[1] = info["modversion"];
                    if (info.ContainsKey("hostname"))
                        if (FoundRow[2].ToString() != info["hostname"])
                            FoundRow[2] = info["hostname"];
                    if (info.ContainsKey("mapname"))
                        if (FoundRow[3].ToString() != info["mapname"])
                            FoundRow[3] = info["mapname"];
                    if (info.ContainsKey("gametype"))
                        if (FoundRow[4].ToString() != info["gametype"])
                            FoundRow[4] = info["gametype"];
                    if (info.ContainsKey("clients"))
                        if (FoundRow[5].ToString() != info["clients"])
                            FoundRow[5] = info["clients"];
                    if (info.ContainsKey("sv_maxclients"))
                        if (FoundRow[6].ToString() != info["sv_maxclients"])
                            FoundRow[6] = info["sv_maxclients"];
                }
            }*/
            catch
            {
            }
            if (_ServerListTable.IsAccessible)
            {
                _ServerListTable.PerformLayout();
                _ServerListData.AcceptChanges();
                _ServerListTable.Update();
            }
            _ServerListData.EndInit();
        }
    }
}
