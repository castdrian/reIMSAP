using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace reIMSAP
{
    internal static class Util
    {
        private static readonly string[] users = { 
            "e2711bfd2da9999831dc1cbf539829692ac8a135ad58a7e4783091a609be4a31", 
            "84a76f17c77c9fd7c8d41a3ab9770ba48aba96afcc8ca201f8c44d39979d559f",
            "16641c2790ac690dd7698dbb4e3dbe6fd633c0eeaf4c386471fd05dcea94d698",
            "595ca859c43032b1e4d9a87a40c554f3f165edbf3069ca792c15dd84c87e631e",
            "6b4c90e8b669058db41fa2d19c9b6860f67a8096bc398fd5e85cf66388446e18"
        };
        public static string Sha256_hash(string value)
        {
            StringBuilder Sb = new();

            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        public static bool Auth()
        {
            if (users.Contains(Sha256_hash(Environment.UserName))) return true;
            else return false;
        }

        public static bool IsCurrentProcessAdmin()
        {
            using var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var principal = new System.Security.Principal.WindowsPrincipal(identity);
            return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }

        public static string UrlEncode(this string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        public static void ToCSV(this DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new(strFilePath, false);
            //headers    
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                        string value = dr[i].ToString();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        if (value.Contains(','))
                        {
                            value = string.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

        public static void GenBarcode(DataRowView row, string filename)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            string url = $"https://barcode.tec-it.com/barcode.ashx?data={row[0].ToString().UrlEncode()}&code=Code128&translate-esc=off&imagetype=Jpg";
#pragma warning restore CS8604 // Possible null reference argument.

            using var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = httpClient.Send(request);
            Image img = Image.FromStream(response.Content.ReadAsStream());
            img.Save(filename);
        }
    }
}
