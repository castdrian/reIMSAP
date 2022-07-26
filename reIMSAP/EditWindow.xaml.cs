
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;



namespace reIMSAP
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public EditWindow(DataRowView row, DataGrid dbgrid)
        {
            InitializeComponent();
            ScrollViewer.SetCanContentScroll(this, false);
            DataTable dt = row.DataView.ToTable();
            dt.Rows.Clear();
            dt.ImportRow(row.Row);
            gridrow.ItemsSource = dt.DefaultView;
        }
    }
}
