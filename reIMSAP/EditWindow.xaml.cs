using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using static reIMSAP.SQL;



namespace reIMSAP
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private string host;

        public EditWindow(DataRowView row, string host)
        {
            InitializeComponent();
            this.host = host;
            ScrollViewer.SetCanContentScroll(this, false);
            DataTable dt = row.DataView.ToTable();
            dt.Rows.Clear();
            dt.ImportRow(row.Row);
            gridrow.ItemsSource = dt.DefaultView;
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult update = MessageBox.Show("Do you wish to update the selected entry?", "reIMS - Admin Panel", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (update != MessageBoxResult.Yes) return;
            DataRowView drv = (DataRowView)gridrow.Items.GetItemAt(0);
            EditWindow edit = (EditWindow)GetWindow(sender as DependencyObject);
            UpdateRow(edit.host, drv);
            this.Close();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult delete = MessageBox.Show("Do you wish to delete the selected entry?", "reIMS - Admin Panel", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (delete != MessageBoxResult.Yes) return;
        }
    }
}
