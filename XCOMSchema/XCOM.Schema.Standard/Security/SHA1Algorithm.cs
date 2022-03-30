using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.Security
{
    public class SHA1Algorithm : ICrypto
    {
        private readonly SHA1 sha1;

        public SHA1Algorithm()
        {
            sha1 = SHA1.Create();
        }

        public string Decrypt(string encryptedBase64String)
        {
            throw new ApplicationException("SHA1不可逆!");
        }

        public string Encrypt(string plainString)
        {
            byte[] inArray = sha1.ComputeHash(Encoding.UTF8.GetBytes(plainString));
            return Convert.ToBase64String(inArray);
        }

        public string Encrypt(string plainString, byte[] saltValue)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(plainString);
            byte[] array = new byte[bytes.Length + saltValue.Length];
            bytes.CopyTo(array, 0);
            saltValue.CopyTo(array, bytes.Length);
            byte[] array2 = sha1.ComputeHash(array);
            byte[] array3 = new byte[array2.Length + saltValue.Length];
            array2.CopyTo(array3, 0);
            saltValue.CopyTo(array3, array2.Length);
            return Convert.ToBase64String(array3);
        }

        public string Encrypt(string plainString, int saltLength)
        {
            byte[] array = new byte[saltLength];
            RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
            rNGCryptoServiceProvider.GetBytes(array);
            return Encrypt(plainString, array);
        }

    }
}
