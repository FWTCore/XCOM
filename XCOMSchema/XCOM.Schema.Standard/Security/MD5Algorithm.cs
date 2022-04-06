using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.Security
{
    public class MD5Algorithm : ICrypto
    {
        private readonly MD5 md5;

        public MD5Algorithm()
        {
            md5 = MD5.Create();
        }

        public string Decrypt(string encryptedBase64String)
        {
            throw new ApplicationException("MD5不可逆!");
        }

        public string Encrypt(string plainString)
        {
            byte[] inArray = md5.ComputeHash(Encoding.UTF8.GetBytes(plainString));
            return Convert.ToBase64String(inArray);
        }

        public string Encrypt(string plainString, byte[] saltValue)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(plainString);
            byte[] array = new byte[bytes.Length + saltValue.Length];
            bytes.CopyTo(array, 0);
            saltValue.CopyTo(array, bytes.Length);
            byte[] array2 = md5.ComputeHash(array);
            byte[] array3 = new byte[array2.Length + saltValue.Length];
            array2.CopyTo(array3, 0);
            saltValue.CopyTo(array3, array2.Length);
            return Convert.ToBase64String(array3);
        }

    }
}