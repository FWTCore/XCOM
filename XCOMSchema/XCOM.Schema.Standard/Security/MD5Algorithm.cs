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
        public string Decrypt(string encryptedBase64String)
        {
            throw new ApplicationException("MD5不可逆!");
        }

        public static string Encrypt(string plainString)
        {
            byte[] inArray = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(plainString));
            return Convert.ToBase64String(inArray);
        }

        public static string Encrypt(string plainString, byte[] saltValue)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(plainString);
            byte[] array = new byte[bytes.Length + saltValue.Length];
            bytes.CopyTo(array, 0);
            saltValue.CopyTo(array, bytes.Length);
            byte[] array2 = MD5.Create().ComputeHash(array);
            byte[] array3 = new byte[array2.Length + saltValue.Length];
            array2.CopyTo(array3, 0);
            saltValue.CopyTo(array3, array2.Length);
            return Convert.ToBase64String(array3);
        }

    }
}