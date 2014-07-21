using QuakeQueryDll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UrTQuery
{
    partial class MainForm
    {
        private void QueryHandle(Exception ex)
        {
            if (ex is SocketException)
            {
                if (((SocketException)ex).SocketErrorCode == SocketError.AddressFamilyNotSupported)
                {
                    if (_MainQuery.IP.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        Address.Text = _MainQuery.IP.MapToIPv4().ToString();
                        Send_Click(this, null);
                    }
                    else
                    {
#if !DEBUG
                        ErrorForm Bug = new ErrorForm(ex);
                        Bug.Show();
#else
                        throw ex;
#endif
                    }
                }
                else if (((SocketException)ex).SocketErrorCode == SocketError.NetworkUnreachable || ((SocketException)ex).SocketErrorCode == SocketError.ConnectionReset)
                    MessageBox.Show(ex.Message, "Send Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
#if !DEBUG
                    ErrorForm Bug = new ErrorForm(ex);
                    Bug.Show();
#else
                    throw ex;
#endif
                }
            }
            else if (ex is Win32Exception)
            {

                if (((Win32Exception)ex).NativeErrorCode == QueryError.NoImput || ((Win32Exception)ex).NativeErrorCode == QueryError.WrongFormat)
                    MessageBox.Show(ex.Message, "Send Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
#if !DEBUG
                ErrorForm Bug = new ErrorForm(ex);
                Bug.Show();
#else
                    throw ex;
#endif
                }
            }
            else if (ex is Exception)
            {
#if !DEBUG
                ErrorForm Bug = new ErrorForm(ex);
                Bug.Show();
#else
                throw ex;
#endif
            }
            else
                throw ex;
        }
    }
}
