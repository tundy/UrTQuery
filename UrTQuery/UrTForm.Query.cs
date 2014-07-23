using QuakeQueryDll;
using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using UrTQuery.Properties;

namespace UrTQuery
{
    partial class MainForm
    {
        private Query _MainQuery = new Query();

        private void InitializeQuery()
        {
            Type.SelectedIndex = 0;
            Address.Text = Settings.Default.LastIP;
            Port.Text = Settings.Default.LastPort;
        }

        private void SetLocalhost(ref Query q, string Message)
        {
            Address.Text = "127.0.0.1";
            q.IP = IPAddress.Parse("127.0.0.1");
            Message += Environment.NewLine + "Address is set to the Default value 127.0.0.1";
            MessageBox.Show(Message, "Default IP Adress", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void CheckIP(ref Query q)
        {
            if (Address.Text.Length == 0)
            {
                SetLocalhost(ref q, "Server Address was not set");
            }
            else
            {
                try
                {
                    q.IP = IPAddress.Parse(Address.Text);
                }
                catch (FormatException)
                {
                    string Message = "Wrong Address format" + Environment.NewLine;
                    Message += "Attempt to get IP from Domain" + Environment.NewLine;
                    try
                    {
                        q.IP = Dns.GetHostAddresses(Address.Text)[0];
                    }
                    catch
                    {
                        Message += "Unable to get IP Address from Domain";
                        SetLocalhost(ref q, Message);
                    }
                }
            }
        }
        private void SetLocalPort(ref Query q, string Message)
        {
            Port.Text = "27960";
            q.PortNumber = 27960;
            Message += Environment.NewLine + "Port is set to the Default value 27960";
            MessageBox.Show(Message, "Default Port Number", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void CheckPort(ref Query q)
        {
            if (Port.Text.Length != 0)
            {
                try
                {
                    q.PortNumber = ushort.Parse(Port.Text);
                }
                catch (OverflowException)
                {
                    SetLocalPort(ref q, "Port value has to be between 0 and 65536");
                }
                catch (FormatException)
                {
                    SetLocalPort(ref q, "Port has to be Number");
                }
            }
            else
            {
                SetLocalPort(ref q, "Port number was not set");
            }
        }
        private void CheckIpPort(ref Query q)
        {
            CheckIP(ref q);
            CheckPort(ref q);
        }

        private void Send_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            CheckIpPort(ref _MainQuery);
            try
            {
                switch (Type.SelectedIndex)
                {
                    case (0):
                        Output.AppendText(_MainQuery.Rcon(RCON.Text, Input.Text) + Environment.NewLine);
                        break;
                    case (1):
                        Output.AppendText(_MainQuery.Print(RCON.Text, Input.Text) + Environment.NewLine);
                        break;
                    case (2):
                        Output.AppendText(_MainQuery.Say(RCON.Text, Input.Text) + Environment.NewLine);
                        break;
                    case (3):
                        Output.AppendText(_MainQuery.Out(Input.Text) + Environment.NewLine);
                        break;
                    case (4):
                        Output.AppendText(_MainQuery.BigText(RCON.Text, Input.Text) + Environment.NewLine);
                        break;
                    case (5):
                        Output.AppendText(_MainQuery.PM(RCON.Text, ID.Text, Input.Text) + Environment.NewLine);
                        break;
                    case (6):
                        Output.AppendText(Input.Text + ": " + _MainQuery.GetCvar(RCON.Text, Input.Text) + Environment.NewLine);
                        break;
                    default:
                        Output.AppendText(_MainQuery.Out(Input.Text) + Environment.NewLine);
                        break;
                }
            }
            catch (Exception ex)
            {
                QueryHandle(ex);
            }
            Cursor.Current = Cursors.Default;
        }
        private void Clear_Click(object sender, EventArgs e)
        {
            Output.Text = "";
        }
        private void GetStatus_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            CheckIpPort(ref _MainQuery);
            try
            {
                var tmpDic = _MainQuery.GetStatus();
                foreach (var tmp in tmpDic)
                {
                    var key = tmp.Key;
                    var value = tmp.Value;
                    Output.AppendText(key + ": " + value + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                QueryHandle(ex);
            }
            Cursor.Current = Cursors.Default;
        }
        private void GetInfo_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            CheckIpPort(ref _MainQuery);
            try
            {
                var tmpDic = _MainQuery.GetInfo();
                foreach (var tmp in tmpDic)
                {
                    var key = tmp.Key;
                    var value = tmp.Value;
                    Output.AppendText(key + ": " + value + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                QueryHandle(ex);
            }
            Cursor.Current = Cursors.Default;
        }
        private void rconStatus_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            CheckIpPort(ref _MainQuery);
            try
            {
                Output.AppendText(_MainQuery.Rcon(RCON.Text, "status") + Environment.NewLine);
            }
            catch (Exception ex)
            {
                QueryHandle(ex);
            }
            Cursor.Current = Cursors.Default;
        }
        private void CheckPassword_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;
            CheckIpPort(ref _MainQuery);
            try
            {
                Output.AppendText(_MainQuery.Rcon(RCON.Text, "echo \"Good rconpassword.\"") + Environment.NewLine);
            }
            catch (Exception ex)
            {
                QueryHandle(ex);
            }
            Cursor.Current = Cursors.Default;
        }

        bool MOVED = false;
        private void Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Type.SelectedIndex == 5 && !MOVED)
            {
                ID.Visible = true;
                Input.Size = new Size(185, Input.Size.Height);
                Input.Location = new Point(Input.Location.X + 28, Input.Location.Y);
                MOVED = true;
            }
            else if (Type.SelectedIndex != 5 && MOVED)
            {
                ID.Visible = false;
                ID.Text = string.Empty;
                Input.Size = new Size(213, Input.Size.Height);
                Input.Location = new Point(Input.Location.X - 28, Input.Location.Y);
                MOVED = false;
            }
        }
        private void Input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Send_Click(sender, e);
            }
        }
        private void Address_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if (e.KeyChar == '.')
            {
                // Separator is OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            else if ((ModifierKeys & (Keys.Control)) != 0)
            {
                // Allow Control Shorcuts
            }
            else
            {
                e.Handled = true;
            }
        }
        private void NumbersOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            else if ((ModifierKeys & (Keys.Control)) != 0)
            {
                // Allow Control Shorcuts
            }
            else
            {
                e.Handled = true;
            }
        }
        private void ShowPassword_CheckStateChanged(object sender, EventArgs e)
        {
            if (RCON.PasswordChar == '\0')
                RCON.PasswordChar = '*';
            else
                RCON.PasswordChar = '\0';
        }
        private void NoSpaces_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ' || e.KeyChar == '\t')
            {
                e.Handled = true;
            }
        }
        private void NoSpaces_TextChanged(object sender, EventArgs e)
        {
            ((Control)sender).Text = ((Control)sender).Text.Replace(" ", "");
            ((Control)sender).Text = ((Control)sender).Text.Replace("\t", "");
        }

    }
}
