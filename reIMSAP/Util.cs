using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace reIMSAP
{
    internal class Util
    {
        private static String[] users = { "e2711bfd2da9999831dc1cbf539829692ac8a135ad58a7e4783091a609be4a31" };
        public static String sha256_hash(string value)
        {
            StringBuilder Sb = new StringBuilder();

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
            if (users.Contains(sha256_hash(Environment.UserName))) return true;
            else return false;
        }

        public static bool IsCurrentProcessAdmin()
        {
            using var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var principal = new System.Security.Principal.WindowsPrincipal(identity);
            return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }
    }
}
