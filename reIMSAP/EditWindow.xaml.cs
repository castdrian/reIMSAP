using Microsoft.Win32;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using static reIMSAP.SQL;
using static reIMSAP.Util;



namespace reIMSAP
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private Dictionary<string, string> db = new Dictionary<string, string>();

        public EditWindow(Dictionary<string, string> db, DataRowView row)
        {
            InitializeComponent();
            this.db = db;
            ScrollViewer.SetCanContentScroll(this, false);
            DataTable dt = row.DataView.ToTable();
            dt.Rows.Clear();
            dt.ImportRow(row.Row);
            grid.ItemsSource = dt.DefaultView;
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult update = MessageBox.Show("Do you wish to update the selected entry?", "reIMS - Admin Panel", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (update != MessageBoxResult.Yes) return;
            DataRowView drv = (DataRowView)grid.Items.GetItemAt(0);
            EditWindow edit = (EditWindow)GetWindow(sender as DependencyObject);
            UpdateRow(edit.db, drv);
            this.Close();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult delete = MessageBox.Show("Do you wish to delete the selected entry?", "reIMS - Admin Panel", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (delete != MessageBoxResult.Yes) return;
            DataRowView drv = (DataRowView)grid.Items.GetItemAt(0);
            EditWindow edit = (EditWindow)GetWindow(sender as DependencyObject);
            DeleteRow(edit.db, drv);
            this.Close();
        }

        private void barcode_Click(object sender, RoutedEventArgs e)
        {
            DataRowView drv = (DataRowView)grid.Items.GetItemAt(0);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG files (*.png)|*.png";
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            saveFileDialog.FileName = $"{drv[0].ToString().Replace('/', '-')}.png";
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (saveFileDialog.ShowDialog() == true)
            {
                GenBarcode(drv, saveFileDialog.FileName);
                MessageBox.Show($"Barcode saved as {saveFileDialog.FileName}.", "reIMS - Admin Panel", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
