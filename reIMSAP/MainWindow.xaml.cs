using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Security.Principal;
using System.Security.Cryptography;
using static reIMSAP.Util;
using static reIMSAP.SQL;
using System.Diagnostics;
using Npgsql;
using System.Windows.Controls;
using System.Data;

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
            if (!IsCurrentProcessAdmin()) if (!Auth()) {
                    MessageBox.Show("You are not authorized to use this application.", "reIMS - Admin Panel", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown(); 
                }
            login.Content = $"Logged in as {Environment.UserName}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowData(host.Text,dbgrid);
            additem.IsEnabled = true;
            importdb.IsEnabled = true;
            exportdb.IsEnabled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Developed by: Adrian Fernandez Castro, reh 7667, EDU\n© 2022 - All rights reserved", "reIMS - Admin Panel", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                DataGridRow row = sender as DataGridRow;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                DataRowView SelectedRow = (DataRowView)row.Item;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                string item = SelectedRow["item"].ToString() ?? "";
                string amount = SelectedRow["amount"].ToString() ?? "";
                string connector = SelectedRow["connector"].ToString() ?? "";
                string color = SelectedRow["color"].ToString() ?? "";
                string type = SelectedRow["type"].ToString() ?? "";
                string length = SelectedRow["length"].ToString() ?? "";
                string cclass = SelectedRow["class"].ToString() ?? "";
                string mode = SelectedRow["mode"].ToString() ?? "";

                Window edit = new EditWindow();
                edit.ShowDialog();
            }
        }
    }
}
