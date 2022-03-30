using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.Security
{
    public static class XMCrypto
    {
        private static readonly byte[] desIV = new byte[8] { 29, 135, 52, 9, 65, 3, 97, 98 };

        private static readonly byte[] desKey = new byte[8] { 1, 77, 84, 34, 69, 90, 23, 44 };

        public static ICrypto GetCrypto(CryptoEnum algorithm)
        {
            return algorithm switch
            {
                CryptoEnum.DES => new DESAlgorithm(),
                CryptoEnum.RC2 => new RC2Algorithm(),
                CryptoEnum.Rijndael => new RijndaelAlgorithm(),
                CryptoEnum.TripleDES => new TripleDESAlgorithm(),
                CryptoEnum.RSA => new RSAAlgorithm(),
                CryptoEnum.MD5 => new MD5Algorithm(),
                CryptoEnum.SHA1 => new SHA1Algorithm(),
                _ => null,
            };
        }

        public static string Decrypt(string encryptedText)
        {
            MemoryStream memoryStream = new(200);
            memoryStream.SetLength(0L);
            byte[] array = Convert.FromBase64String(encryptedText);
            DES dES = new DESCryptoServiceProvider
            {
                KeySize = 64
            };
            CryptoStream cryptoStream = new(memoryStream, dES.CreateDecryptor(desKey, desIV), CryptoStreamMode.Write);
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

        public static string Encrypt(string plainText)
        {
            MemoryStream memoryStream = new(200);
            memoryStream.SetLength(0L);
            byte[] bytes = Encoding.Unicode.GetBytes(plainText);
            DES dES = new DESCryptoServiceProvider();
            CryptoStream cryptoStream = new(memoryStream, dES.CreateEncryptor(desKey, desIV), CryptoStreamMode.Write);
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