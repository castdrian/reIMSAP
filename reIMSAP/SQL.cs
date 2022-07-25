using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Npgsql;

namespace reIMSAP
{
    internal class SQL
    {
        public static void Connect(String host)
        {
            var cs = $"Host={host};Username=pi;Database=reims";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "SELECT version()";

            using var cmd = new NpgsqlCommand(sql, con);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var version = cmd.ExecuteScalar().ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            Debug.WriteLine($"PostgreSQL version: {version}");
        }
    }
}
