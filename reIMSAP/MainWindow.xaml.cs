using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Security.Principal;
using System.Security.Cryptography;
using static reIMSAP.Util;
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
        }
    }
}
