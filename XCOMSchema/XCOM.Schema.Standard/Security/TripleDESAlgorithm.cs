using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.Security
{
    public class TripleDESAlgorithm : ICrypto
    {
        private static byte[] s_DesIV = new byte[8] { 29, 135, 52, 9, 65, 3, 97, 98 };

        private static byte[] s_DesKey = new byte[24]
        {
            185, 233, 114, 248, 40, 85, 215, 64, 161, 252,
            94, 142, 93, 21, 164, 232, 167, 132, 188, 227,
            154, 105, 222, 99
        };

        public string Decrypt(string encryptedBase64ConnectString)
        {
            MemoryStream memoryStream = new MemoryStream(200);
            memoryStream.SetLength(0L);
            byte[] array = Convert.FromBase64String(encryptedBase64ConnectString);
            TripleDES tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.KeySize = 192;
            CryptoStream cryptoStream = new CryptoStream(memoryStream, tripleDES.CreateDecryptor(s_DesKey, s_DesIV), CryptoStreamMode.Write);
            cryptoStream.Write(array, 0, array.Length);
            cryptoStream.FlushFinalBlock();
            memoryStream.Flush();
            memoryStream.Seek(0L, SeekOrigin.Begin);
            byte[] array2 = new byte[memoryStream.Length];
            memoryStream.Read(array2, 0, array2.Length);
            cryptoStream.Close();
            memoryStream.Close();
            return Encoding.Unicode.GetString(array2);
        }

        public string Encrypt(string plainConnectString)
        {
            MemoryStream memoryStream = new MemoryStream(200);
            memoryStream.SetLength(0L);
            byte[] bytes = Encoding.Unicode.GetBytes(plainConnectString);
            TripleDES tripleDES = new TripleDESCryptoServiceProvider();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, tripleDES.CreateEncryptor(s_DesKey, s_DesIV), CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            memoryStream.Flush();
            memoryStream.Seek(0L, SeekOrigin.Begin);
            byte[] array = new byte[memoryStream.Length];
            memoryStream.Read(array, 0, array.Length);
            cryptoStream.Close();
            memoryStream.Close();
            return Convert.ToBase64String(array, 0, array.Length);
        }

    }
}
