using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.Security
{
    public interface ICrypto
    {
        string Decrypt(string encryptedBase64ConnectString);

        string Encrypt(string plainConnectString);
    }
}
