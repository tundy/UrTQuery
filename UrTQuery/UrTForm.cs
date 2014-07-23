using System.Windows.Forms;

namespace UrTQuery
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeServerList();
            InitializeQuery();

            this.FormClosing += MainForm_FormClosing;
            this.TabPage.SelectedIndexChanged += TabPage_SelectedIndexChanged;
        }

        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _RefreshWorker.CancelAsync();
            _GetListWorker.CancelAsync();
            if (Address.Text != null)
                Properties.Settings.Default.LastIP = Address.Text;
            if (Port.Text != null)
                Properties.Settings.Default.LastPort = Port.Text;
            if (_ServerList != null)
                ServerListSettings.SaveServerList(_ServerList);
            Properties.Settings.Default.Save();
        }

        void TabPage_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.TabPage.SelectedIndex == 1)
            {
                _ServerListTable.Refresh();
                _ServerListTable.PerformLayout();
                _ServerListData.BeginInit();
                _ServerListData.AcceptChanges();
                _ServerListData.EndInit();
                _ServerListTable.Visible = true;
            }
            else
            {
                _ServerListTable.Visible = false;
            }
        }
    }
}
