using System.Collections.Generic;
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
        private readonly Dictionary<string, string> db = new();

        public AddWindow(Dictionary<string, string> db, DataGrid maingrid)
        {
            InitializeComponent();
            this.db = db;
            ScrollViewer.SetCanContentScroll(this, false);
            DataTable dt = ((DataView)maingrid.ItemsSource).ToTable();
            dt.Rows.Clear();
            grid.ItemsSource = dt.DefaultView;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult insert = MessageBox.Show("Do you wish to add this entry?", "reIMS - Admin Panel", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (insert != MessageBoxResult.Yes) return;
            DataRowView drv = (DataRowView)grid.Items.GetItemAt(0);
            AddWindow add = (AddWindow)GetWindow(sender as DependencyObject);
            InsertRow(add.db, drv);
            this.Close();
        }
    }
}
