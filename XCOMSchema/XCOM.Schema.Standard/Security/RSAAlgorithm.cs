using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.Security
{
    public class RSAAlgorithm : ICrypto
    {
        public string Decrypt(string encryptedBase64ConnectString)
        {
            throw new NotImplementedException();
        }

        public string Encrypt(string plainConnectString)
        {
            throw new NotImplementedException();
        }
    }
}
