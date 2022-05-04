using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.Security
{
    public class MD5Algorithm
    {
        public static string Encrypt(string source)
        {
            MD5 md5 = MD5.Create();
            byte[] btStr = Encoding.UTF8.GetBytes(source);
            byte[] hashStr = md5.ComputeHash(btStr);
            StringBuilder pwd = new StringBuilder();
            foreach (byte bStr in hashStr) { pwd.Append(bStr.ToString("x2")); }
            return pwd.ToString();
        }

    }
}