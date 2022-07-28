using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static reIMSAP.SQL;
using static reIMSAP.Util;

namespace reIMSAP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Dictionary<string, string> db = new();
        public MainWindow()
        {
            InitializeComponent();
            if (!IsCurrentProcessAdmin()) if (!Auth())
                {
                    MessageBox.Show("You are not authorized to use this application.", "reIMS - Admin Panel", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown();
                }
            login.Content = $"Logged in as {Environment.UserName}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.db.Add("host", host.Text);
            this.db.Add("user", dbuser.Text);
            this.db.Add("db", dbname.Text);
            ShowData(this.db, dbgrid);
            additem.IsEnabled = true;
            exportdb.IsEnabled = true;
            connect.IsEnabled = false;
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
                Window edit = new EditWindow(this.db, SelectedRow);
                edit.ShowDialog();
                ShowData(this.db, dbgrid);
            }
        }

        private void Additem_Click(object sender, RoutedEventArgs e)
        {
            Window add = new AddWindow(this.db, dbgrid);
            add.ShowDialog();
            ShowData(this.db, dbgrid);
        }

        private void Exportdb_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "CSV files (*.csv)|*.csv"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                DataTable dt = ((DataView)dbgrid.ItemsSource).ToTable();
                dt.ToCSV(saveFileDialog.FileName);
            }
        }
    }
}
