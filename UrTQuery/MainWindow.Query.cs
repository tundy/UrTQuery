using System.Globalization;
using QuakeQueryDll;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UrTQueryWpf
{
    public partial class MainWindow
    {
        private readonly QuakeQuery _mainQuery = new QuakeQuery();

        internal IPAddress Ip
        {
            get
            {
                if (Address.Text.Length == 0)
                {
                    SetLocalAddress("Server Address was not set");
                    return Ip;
                }
                try
                {
                    return IPAddress.Parse(Address.Text);
                }
                catch (FormatException)
                {
                    var message = "Wrong Address format" + Environment.NewLine;
                    message += "Attempt to get IP from Domain" + Environment.NewLine;
                    try
                    {
                        var tmp = Dns.GetHostAddresses(Address.Text)[0];
                        if (tmp.AddressFamily == AddressFamily.InterNetworkV6)
                            tmp = tmp.MapToIPv4();
                        return tmp;
                    }
                    catch
                    {
                        message += "Unable to get IP Address from Domain";
                        SetLocalAddress(message);
                        return Ip;
                    }
                }
            }
            set { Address.Text = value.ToString(); }
        }
        internal ushort Port
        {
            get
            {
                if (TextBoxPort.Text.Length == 0)
                {
                    SetLocalPort("Port number was not set");
                    return Port;
                }
                try
                {
                    return ushort.Parse(TextBoxPort.Text);
                }
                catch (OverflowException)
                {
                    SetLocalPort("Port value has to be between 0 and 65536");
                    return Port;
                }
                catch (FormatException)
                {
                    SetLocalPort("Port has to be Number");
                    return Port;
                }
            }
            set { TextBoxPort.Text = value.ToString(CultureInfo.InvariantCulture); }
        }

        private void SetLocalAddress(string message)
        {
            Ip = IPAddress.Parse("127.0.0.1");
            message += Environment.NewLine + "Address is set to the Default value 127.0.0.1";
            MessageBox.Show(message, "Default IP Adress", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        private void SetLocalPort(string message)
        {
            Port = 27960;
            message += Environment.NewLine + "Port is set to the Default value 27960";
            MessageBox.Show(message, "Default Port Number", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        # region Query Event Handlers
        private void _MainQuery_serverResponseEvent(Server sender)
        {
            Dispatcher.Invoke(() =>
            {
                var foundRow = _serverListDataTable.Rows.Find(new object[] { sender.Ip, sender.Port });
                if (foundRow != null)
                {
                    foundRow[9] = (ushort)((sender.LastRecvTime - sender.LastSendTime).TotalMilliseconds);
                }
            });
        }
        private void _MainQuery_printResponseEvent(Server sender)
        {
            Dispatcher.Invoke(() =>
            {
                if (!sender.Ip.Equals(Ip.ToString()) || !sender.Port.Equals(Port)) return;
                var lastFocusedItem = FocusManager.GetFocusedElement(this);
                var wasScrolledToEnd = Output.VerticalOffset + Output.ViewportHeight >= Output.ExtentHeight;
                Output.Focus();
                Output.AppendText(Environment.NewLine);
                Output.AppendText(sender.ToString() + Environment.NewLine);
                Output.AppendText(sender.Response);
                Output.AppendText(Environment.NewLine);

                /*foreach (var cvar in sender.Cvars.ToList())
                {
                    Output.AppendText(cvar.Key + ": " + cvar.Value);
                    Output.AppendText(Environment.NewLine);
                }
                Output.AppendText(Environment.NewLine);*/

                if (wasScrolledToEnd) Output.ScrollToEnd();
                if (lastFocusedItem != null) lastFocusedItem.Focus();
            });
        }
        private void _MainQuery_statusResponseEvent(Server sender)
        {
            Dispatcher.Invoke(() =>
            {
                if (!sender.Ip.Equals(Ip.ToString()) || !sender.Port.Equals(Port)) return;
                var lastFocusedItem = FocusManager.GetFocusedElement(this);
                var wasScrolledToEnd = Output.VerticalOffset + Output.ViewportHeight >= Output.ExtentHeight;
                Output.Focus();
                Output.AppendText(Environment.NewLine);
                Output.AppendText(sender.ToString() + Environment.NewLine);
                foreach (var data in sender.Status.ToList())
                {
                    Output.AppendText(data.Key + ": " + data.Value);
                    Output.AppendText(Environment.NewLine);
                }
                Output.AppendText(Environment.NewLine);
                if (wasScrolledToEnd) Output.ScrollToEnd();
                if (lastFocusedItem != null) lastFocusedItem.Focus();
            });
        }
        private void _MainQuery_infoResponseEvent(Server sender)
        {
            Dispatcher.Invoke(() =>
            {
                if (sender.Ip.Equals(Ip.ToString()) && sender.Port.Equals(Port))
                {
                    var lastFocusedItem = FocusManager.GetFocusedElement(this);
                    var wasScrolledToEnd = Output.VerticalOffset + Output.ViewportHeight >= Output.ExtentHeight;
                    Output.Focus();
                    Output.AppendText(Environment.NewLine);
                    Output.AppendText(sender.ToString() + Environment.NewLine);
                    foreach (var data in sender.Info.ToList())
                    {
                        Output.AppendText(data.Key + ": " + data.Value);
                        Output.AppendText(Environment.NewLine);
                    }
                    Output.AppendText(Environment.NewLine);
                    if (wasScrolledToEnd) Output.ScrollToEnd();
                    if (lastFocusedItem != null) lastFocusedItem.Focus();
                }

                if (!sender.Info.ContainsKey("hostname"))
                    sender.Info["hostname"] = sender.Ip;

                if (GameModes.LongNames.ContainsKey(sender.Info["gametype"]))
                {
                    var tmp = GameModes.LongNames[sender.Info["gametype"]];
                    sender.Info["gametype"] = tmp;
                }
                
                if (sender.Info["game"].Equals("q3ut4"))
                {
                    try
                    {
                        _serverListDataTable.Rows.Add(
                            null,
                            sender.Info["modversion"],
                            sender.Info["hostname"],
                            sender.Info["mapname"],
                            sender.Info["gametype"],
                            sender.Info["clients"],
                            sender.Info["sv_maxclients"],
                            sender.Ip,
                            sender.Port,
                            (ushort)((sender.LastRecvTime - sender.LastSendTime).TotalMilliseconds)
                            );
                    }
                    catch (ConstraintException)
                    {
                        var foundRow = _serverListDataTable.Rows.Find(new object[] { sender.Ip, sender.Port });
                        if (foundRow != null)
                        {
                            foundRow[1] = sender.Info["modversion"];
                            foundRow[2] = sender.Info["hostname"];
                            foundRow[3] = sender.Info["mapname"];
                            foundRow[4] = sender.Info["gametype"];
                            foundRow[5] = sender.Info["clients"];
                            foundRow[6] = sender.Info["sv_maxclients"];
                            foundRow[9] = (ushort)((sender.LastRecvTime - sender.LastSendTime).TotalMilliseconds);
                        }
                    }
                }
                _tmpServers.Remove(sender.ToString());

                UpdateStatus();
            });
        }
        # endregion

        # region Button_Clicks
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            switch (Combo.SelectedIndex)
            {
                case (0):
                    _mainQuery.Rcon(Rcon.Password, Input.Text, Ip, Port);
                    break;
                case (1):
                    _mainQuery.Print(Rcon.Password, Input.Text, Ip, Port);
                    break;
                case (2):
                    _mainQuery.Say(Rcon.Password, Input.Text, Ip, Port);
                    break;
                case (3):
                    _mainQuery.Send(Input.Text, Ip, Port);
                    break;
                case (4):
                    _mainQuery.BigText(Rcon.Password, Input.Text, Ip, Port);
                    break;
                case (5):
                    _mainQuery.PM(Rcon.Password, ID.Text, Input.Text, Ip, Port);
                    break;
                /*case (6):
                    _MainQuery.GetCvar(Rcon.Password, Input.Text, Ip, Port);
                    break;*/
                default:
                    _mainQuery.Send(Input.Text, Ip, Port);
                    break;
            }
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Output.Text = "";
        }
        private void GetStatus_Click(object sender, RoutedEventArgs e)
        {
            _mainQuery.GetStatus(Ip, Port);
        }
        private void GetInfo_Click(object sender, RoutedEventArgs e)
        {
            _mainQuery.GetInfo(Ip, Port);
        }
        private void RconStatus_Click(object sender, RoutedEventArgs e)
        {
            _mainQuery.Rcon(Rcon.Password, "status", Ip, Port);
        }
        private void TestPassword_Click(object sender, RoutedEventArgs e)
        {
            _mainQuery.Rcon(Rcon.Password, "echo \"Good rconpassword.\"", Ip, Port);
        }
        #  endregion

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
        private void NumbersOnly_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Text = new string(((TextBox)sender).Text.Where(char.IsDigit).ToArray());
        }

        private void Input_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Send_Click(sender, e);
        }
    }
}