﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.Security
{
   public class RC2Algorithm: ICrypto
    {
		private static byte[] s_DesIV = new byte[8] { 29, 135, 52, 9, 65, 3, 97, 98 };

		private static byte[] s_DesKey = new byte[8] { 1, 77, 84, 34, 69, 90, 23, 44 };

		public string Decrypt(string encryptedBase64ConnectString)
		{
			MemoryStream memoryStream = new MemoryStream(200);
			memoryStream.SetLength(0L);
			byte[] array = Convert.FromBase64String(encryptedBase64ConnectString);
			RC2 rC = new RC2CryptoServiceProvider();
			rC.KeySize = 64;
			CryptoStream cryptoStream = new CryptoStream(memoryStream, rC.CreateDecryptor(s_DesKey, s_DesIV), CryptoStreamMode.Write);
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
			RC2 rC = new RC2CryptoServiceProvider();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, rC.CreateEncryptor(s_DesKey, s_DesIV), CryptoStreamMode.Write);
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
