using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XCOM.Schema.Standard.Utility
{
    public static class XMSerialize
    {
        public static T LoadFromXml<T>(string filePath)
        {
            FileStream fileStream = null;
            try
            {
                XmlSerializer xmlSerializer = new(typeof(T));
                fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return (T)xmlSerializer.Deserialize(fileStream);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
        }

        public static void SaveToXml(string filePath, object data)
        {
            FileStream fileStream = null;
            try
            {
                XmlSerializer xmlSerializer = new(data.GetType());
                fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                xmlSerializer.Serialize(fileStream, data);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
        }
    }
}
