using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Security.Principal;
using System.Security.Cryptography;
using static reIMSAP.Util;
using static reIMSAP.SQL;
using System.Diagnostics;

namespace reIMSAP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (!IsCurrentProcessAdmin()) if (!Auth()) Application.Current.Shutdown();
            login.Content = $"Logged in as {Environment.UserName}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Connect(host.Text);
        }
    }
}
