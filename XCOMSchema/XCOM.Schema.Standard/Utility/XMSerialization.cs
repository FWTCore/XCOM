using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XCOM.Schema.Standard.Utility
{
    public class XMSerialization
    {

        #region 序列化和反序列化Xml文档

        /// <summary>
        /// 反序列化Xml
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="filename">文件路径</param>
        /// <returns></returns>
        public static object LoadXml(Type type, string filePath)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(type);
                return serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                throw new Exception("反序列化Xml异常", ex);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }


        /// <summary>
        /// 序列化Xml
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="filename">文件路径</param>
        public static void SaveXml(object obj, string filePath)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(fs, obj);
            }
            catch (Exception ex)
            {
                throw new Exception("序列化Xml异常", ex);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }

        }

        #endregion


        #region 序列化和反序列化Json文档

        /// <summary>
        /// 反序列化Json
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="filename">文件路径</param>
        /// <returns></returns>
        public static T LoadJson<T>(string filePath)
        {
            try
            {
                return XMJson.Deserialize<T>(XMFile.ReadFile(filePath));
            }
            catch (Exception ex)
            {
                throw new Exception("反序列化Json文件异常", ex);
            }
        }


        /// <summary>
        /// 序列化Json
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="filename">文件路径</param>
        public static void SaveJson(object obj, string filePath)
        {
            try
            {
                var fcontent = XMJson.Serailze(obj);
                XMFile.WriteFile(filePath, fcontent);
            }
            catch (Exception ex)
            {
                throw new Exception("序列化Json文件异常", ex);
            }
        }

        #endregion

    }
}
