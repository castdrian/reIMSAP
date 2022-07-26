using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Npgsql;

namespace reIMSAP
{
    internal class SQL
    {
        public static void ShowData(String host, DataGrid dbgrid)
        {
            var cs = $"Host={host};Username=pi;Database=reims";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            NpgsqlCommand myTableCommand = new NpgsqlCommand("select * from cords", con);
            DataTable dt = new DataTable();
            NpgsqlDataAdapter a = new NpgsqlDataAdapter(myTableCommand);

            a.Fill(dt);
            dbgrid.ItemsSource = dt.DefaultView;
            con.Close();
        }
    }
}
