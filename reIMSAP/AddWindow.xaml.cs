using System.Data;
using System.Windows;
using System.Windows.Controls;
using static reIMSAP.SQL;



namespace reIMSAP
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private string host;

        public AddWindow(string host, DataGrid maingrid)
        {
            InitializeComponent();
            this.host = host;
            ScrollViewer.SetCanContentScroll(this, false);
            DataTable dt = ((DataView)maingrid.ItemsSource).ToTable();
            dt.Rows.Clear();
            grid.ItemsSource = dt.DefaultView;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult add = MessageBox.Show("Do you wish to add this entry?", "reIMS - Admin Panel", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (add != MessageBoxResult.Yes) return;
            DataRowView drv = (DataRowView)grid.Items.GetItemAt(0);
            AddWindow edit = (AddWindow)GetWindow(sender as DependencyObject);
            InsertRow(edit.host, drv);
            this.Close();
        }
    }
}
