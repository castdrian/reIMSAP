using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;
using static System.Linq.Enumerable;

namespace reIMSAP
{
    internal class SQL
    {
        public static void ShowData(Dictionary<string, string> db, DataGrid dbgrid)
        {
            var cs = $"Host={db["host"]};Username={db["user"]};Database={db["db"]}";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            NpgsqlCommand getAll = new NpgsqlCommand("select * from cords", con);
            DataTable dt = new DataTable();
            NpgsqlDataAdapter a = new NpgsqlDataAdapter(getAll);

            a.Fill(dt);
            dbgrid.ItemsSource = dt.DefaultView;
            con.Close();
        }

        public static void UpdateRow(Dictionary<string, string> db, DataRowView row)
        {
            var cs = $"Host={db["host"]};Username={db["user"]};Database={db["db"]}";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            string columns = "";
            string data = "";
            foreach (int i in Range(1, row.Row.Table.Columns.Count - 1))
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

        public static void InsertRow(Dictionary<string, string> db, DataRowView row)
        {
            var cs = $"Host={db["host"]};Username={db["user"]};Database={db["db"]}";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            string columns = "";
            string data = "";
            foreach (int i in Range(0, row.Row.Table.Columns.Count))
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

            NpgsqlCommand updateRow = new NpgsqlCommand($"insert into cords ({columns}) values({data})", con);
            updateRow.ExecuteNonQuery();
            con.Close();
        }

        public static void DeleteRow(Dictionary<string, string> db, DataRowView row)
        {
            var cs = $"Host={db["host"]};Username={db["user"]};Database={db["db"]}";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            NpgsqlCommand updateRow = new NpgsqlCommand($"delete from cords where {row.Row.Table.Columns[0].ColumnName}='{row[0]}'", con);
            updateRow.ExecuteNonQuery();
            con.Close();
        }
    }
}
