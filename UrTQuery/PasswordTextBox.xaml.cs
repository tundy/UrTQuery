using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UrTQueryWpf
{
    /// <summary>
    /// Interaction logic for PasswordTextBox.xaml
    /// </summary>
    public partial class PasswordTextBox : UserControl
    {
        //public delegate void EventHandler(object sender, EventArgs e);
        public event EventHandler PasswordChanged;

        private bool _showpassword = false;
        private string _password = string.Empty;
        public string Password
        {
            get { return _password; }
            set
            {
                if (value != _password)
                {
                    _password = value; PasswordShowed.Text = value; PasswordHidden.Password = value;
                }
            }
        }
        public Brush BoxBackground
        {
            get { return PasswordHidden.Background; }
            set { PasswordHidden.Background = value; PasswordShowed.Background = value; }
        }
        public Brush BoxBorderBrush
        {
            get { return PasswordHidden.BorderBrush; }
            set { PasswordHidden.BorderBrush = value; PasswordShowed.BorderBrush = value; }
        }
        public string Text
        {
            get { return _password; }
            set { Password = value; }
        }
        public bool ShowPassword
        {
            get { return _showpassword; }
            set
            {
                _showpassword = value;
                if (value)
                {
                    PasswordHidden.Visibility = Visibility.Collapsed;
                    PasswordShowed.Visibility = Visibility.Visible;
                    PasswordShowed.Text = _password;               
                }
                else
                {
                    PasswordShowed.Visibility = Visibility.Collapsed;
                    PasswordHidden.Visibility = Visibility.Visible;
                    PasswordHidden.Password = _password;
                }
            }
        }

        public PasswordTextBox()
        {
            InitializeComponent();
        }

        private void PasswordShowed_TextChanged(object sender, TextChangedEventArgs e)
        {
            _password = PasswordShowed.Text;
            OnPasswordChange(e);
        }
        private void PasswordHidden_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _password = PasswordHidden.Password;
            OnPasswordChange(e);
        }

        protected virtual void OnPasswordChange(EventArgs e)
        {
            EventHandler handler = PasswordChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
