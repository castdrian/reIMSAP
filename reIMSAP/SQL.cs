using System;
using System.Data;
using System.Diagnostics;
using static System.Linq.Enumerable;
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

            NpgsqlCommand getAll = new NpgsqlCommand("select * from cords", con);
            DataTable dt = new DataTable();
            NpgsqlDataAdapter a = new NpgsqlDataAdapter(getAll);

            a.Fill(dt);
            dbgrid.ItemsSource = dt.DefaultView;
            con.Close();
        }

        public static void UpdateRow(String host, DataRowView row)
        {
            var cs = $"Host={host};Username=pi;Database=reims";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            string columns = "";
            string data = "";
            foreach (int i in Range(1, row.Row.Table.Columns.Count-1))
            {
                columns += $"{row.Row.Table.Columns[i].ColumnName},";
                if (row[i].GetType() == typeof(System.Int32)) 
                {
                    data += $"{row[i]},";
                }
                if (row[i].GetType() == typeof(System.String)) 
                {
                    data += $"'{row[i]}',";
                }
                if (row[i].GetType() == typeof(System.DBNull)) 
                {
                    data += $"NULL,";
                }
                
            }
            columns = columns.Remove(columns.Length - 1, 1);
            data = data.Remove(data.Length - 1, 1);
       
            NpgsqlCommand updateRow = new NpgsqlCommand($"update cords set ({columns}) = ({data}) where {row.Row.Table.Columns[0].ColumnName}='{row[0]}'", con);
            updateRow.ExecuteNonQuery();
            con.Close();
        }
    }
}
