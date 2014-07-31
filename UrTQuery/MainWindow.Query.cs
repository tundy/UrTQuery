using QuakeQueryDll;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UrTQueryWpf
{
    public partial class MainWindow
    {
        private QuakeQuery _MainQuery = new QuakeQuery();
        private IPAddress _ip;
        private ushort _port;

        void _MainQuery_serverResponseEvent(Server sender)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                if (sender.IP.Equals(_ip) && sender.Port.Equals(_port))
                {
                    Output.Focus();
                    Output.ScrollToEnd();
                }
                var FoundRow = _ServerListDataTable.Rows.Find(new object[] { sender.IP.ToString(), sender.Port });
                if (FoundRow != null)
                {
                    FoundRow[9] = (ushort)((sender.LastRecvTime - sender.LastSendTime).TotalMilliseconds);
                }
            }));
        }
        void _MainQuery_printResponseEvent(Server sender)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                if (sender.IP.Equals(_ip.ToString()) && sender.Port.Equals(_port))
                {
                    Output.AppendText(Environment.NewLine);
                    Output.AppendText(sender.ToString() + Environment.NewLine);
                    Output.AppendText(sender.Response);
                    Output.AppendText(Environment.NewLine);
                }
            }));
        }
        void _MainQuery_statusResponseEvent(Server sender)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                if (sender.IP.Equals(_ip.ToString()) && sender.Port.Equals(_port))
                {
                    Output.AppendText(Environment.NewLine);
                    Output.AppendText(sender.ToString() + Environment.NewLine);
                    foreach (var data in sender.Status.ToList())
                    {
                        Output.AppendText(data.Key + ": " + data.Value);
                        Output.AppendText(Environment.NewLine);
                    }
                    Output.AppendText(Environment.NewLine);
                }
            }));
        }
        void _MainQuery_infoResponseEvent(Server sender)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                if (sender.IP.Equals(_ip.ToString()) && sender.Port.Equals(_port))
                {
                    Output.AppendText(Environment.NewLine);
                    Output.AppendText(sender.ToString() + Environment.NewLine);
                    foreach (var data in sender.Info.ToList())
                    {
                        Output.AppendText(data.Key + ": " + data.Value);
                        Output.AppendText(Environment.NewLine);
                    }
                    Output.AppendText(Environment.NewLine);
                }

                if (!sender.Info.ContainsKey("hostname"))
                    sender.Info["hostname"] = sender.IP.ToString();

                try
                {
                    var tmp = GameModes.LongNames[sender.Info["gametype"]];
                    sender.Info["gametype"] = tmp;
                }
                catch
                {
                }
                
                if (sender.Info["game"].Equals("q3ut4"))
                {
                    try
                    {
                        _ServerListDataTable.Rows.Add(
                            null,
                            sender.Info["modversion"],
                            sender.Info["hostname"],
                            sender.Info["mapname"],
                            sender.Info["gametype"],
                            sender.Info["clients"],
                            sender.Info["sv_maxclients"],
                            sender.IP.ToString(),
                            sender.Port,
                            (ushort)((sender.LastRecvTime - sender.LastSendTime).TotalMilliseconds)
                            );
                    }
                    catch (ConstraintException)
                    {
                        var FoundRow = _ServerListDataTable.Rows.Find(new object[] { sender.IP.ToString(), sender.Port });
                        if (FoundRow != null)
                        {
                            FoundRow[1] = sender.Info["modversion"];
                            FoundRow[2] = sender.Info["hostname"];
                            FoundRow[3] = sender.Info["mapname"];
                            FoundRow[4] = sender.Info["gametype"];
                            FoundRow[5] = sender.Info["clients"];
                            FoundRow[6] = sender.Info["sv_maxclients"];
                            FoundRow[9] = (ushort)((sender.LastRecvTime - sender.LastSendTime).TotalMilliseconds);
                        }
                    }
                }
                _tmpServers.Remove(sender.ToString());

                UpdateStatus();
            }));
        }


        private void SetLocalhost(string Message)
        {
            Address.Text = "127.0.0.1";
            _ip = IPAddress.Parse("127.0.0.1");
            Message += Environment.NewLine + "Address is set to the Default value 127.0.0.1";
            MessageBox.Show(Message, "Default IP Adress", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        private void CheckIP()
        {
            if (Address.Text.Length == 0)
            {
                SetLocalhost("Server Address was not set");
            }
            else
            {
                try
                {
                    _ip = IPAddress.Parse(Address.Text);
                }
                catch (FormatException)
                {
                    string Message = "Wrong Address format" + Environment.NewLine;
                    Message += "Attempt to get IP from Domain" + Environment.NewLine;
                    try
                    {
                        _ip = Dns.GetHostAddresses(Address.Text)[0];
                        if (_ip.AddressFamily == AddressFamily.InterNetworkV6)
                        {
                            _ip = _ip.MapToIPv4();
                        }
                    }
                    catch
                    {
                        Message += "Unable to get IP Address from Domain";
                        SetLocalhost(Message);
                    }
                }
            }
        }
        private void SetLocalPort(string Message)
        {
            Port.Text = "27960";
            _port = 27960;
            Message += Environment.NewLine + "Port is set to the Default value 27960";
            MessageBox.Show(Message, "Default Port Number", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        private void CheckPort()
        {
            if (Port.Text.Length != 0)
            {
                try
                {
                    _port = ushort.Parse(Port.Text);
                }
                catch (OverflowException)
                {
                    SetLocalPort("Port value has to be between 0 and 65536");
                }
                catch (FormatException)
                {
                    SetLocalPort("Port has to be Number");
                }
            }
            else
            {
                SetLocalPort("Port number was not set");
            }
        }
        private void CheckIpPort()
        {
            CheckIP();
            CheckPort();
        }
        private void AppendText(string text)
        {
            Output.AppendText(text + Environment.NewLine);
            Output.Focus();
            Output.CaretIndex = Output.Text.Length;
            Output.ScrollToEnd();
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            CheckIpPort();
            switch (Combo.SelectedIndex)
            {
                case (0):
                    _MainQuery.Rcon(Rcon.Password, Input.Text, _ip, _port);
                    break;
                case (1):
                    _MainQuery.Print(Rcon.Password, Input.Text, _ip, _port);
                    break;
                case (2):
                    _MainQuery.Say(Rcon.Password, Input.Text, _ip, _port);
                    break;
                case (3):
                    _MainQuery.Send(Input.Text, _ip, _port);
                    break;
                case (4):
                    _MainQuery.BigText(Rcon.Password, Input.Text, _ip, _port);
                    break;
                case (5):
                    _MainQuery.PM(Rcon.Password, ID.Text, Input.Text, _ip, _port);
                    break;
                /*case (6):
                    _MainQuery.GetCvar(Rcon.Password, Input.Text, _ip, _port);
                    break;*/
                default:
                    _MainQuery.Send(Input.Text, _ip, _port);
                    break;
            }
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Output.Text = "";
        }
        private void GetStatus_Click(object sender, RoutedEventArgs e)
        {
            CheckIpPort();
            _MainQuery.GetStatus(_ip, _port);
        }
        private void GetInfo_Click(object sender, RoutedEventArgs e)
        {
            CheckIpPort();
            _MainQuery.GetInfo(_ip, _port);
        }
        private void RconStatus_Click(object sender, RoutedEventArgs e)
        {
            CheckIpPort();
            _MainQuery.Rcon(Rcon.Password, "status", _ip, _port);
        }
        private void TestPassword_Click(object sender, RoutedEventArgs e)
        {

            CheckIpPort();
            _MainQuery.Rcon(Rcon.Password, "echo \"Good rconpassword.\"", _ip, _port);
        }

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Combo.SelectedIndex == 5)
            {
                ID.Visibility = Visibility.Visible;
                Input.Width = 180;
            }
            else if (Combo.SelectedIndex != 5)
            {
                ID.Visibility = Visibility.Collapsed;
                Input.Width = 205;
            }
        }
        private void ShowRcon_Checked(object sender, EventArgs e)
        {
            Rcon.ShowPassword = true;
        }
        private void ShowRcon_Unchecked(object sender, EventArgs e)
        {
            Rcon.ShowPassword = false;
        }
        private void NoSpaces_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Text = ((TextBox)sender).Text.Replace(" ", "");
            ((TextBox)sender).Text = ((TextBox)sender).Text.Replace("\t", "");
        }

        private void Input_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Send_Click(sender, e);
        }

        private void Port_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Text = new string(((TextBox)sender).Text.Where(c => char.IsDigit(c)).ToArray());
        }
    }
}