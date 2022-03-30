using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM.Schema.Standard.Extension;

namespace XCOM.Schema.Standard.Utility
{
    public static class XMJson
    {
        /// <summary>
        /// Json格式的设定
        /// </summary>
        private static readonly JsonSerializerSettings setting = new()
        {
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
            DateFormatString = "yyyy-MM-dd HH:mm:ss",
            NullValueHandling = NullValueHandling.Ignore
        };


        #region 方法

        /// <summary>
        /// Json字符串转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="v"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return default;
            }
            if (typeof(T) == typeof(string))
            {
                return (T)(object)str;
            }
            return (T)JsonConvert.DeserializeObject(str.RemoveAnnotation(), typeof(T));
        }

        /// <summary>
        /// byte转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="v"></param>
        /// <returns></returns>
        public static T Deserialize<T>(byte[] stream)
        {
            string str;
            if (stream == null)
            {
                return default;
            }
            else
            {
                str = stream.DeserializeUtf8();
                return string.IsNullOrWhiteSpace(str) ? default : (T)JsonConvert.DeserializeObject(str.RemoveAnnotation(), typeof(T));
            }
        }


        /// <summary>
        /// Json字符串转换成对象
        /// </summary>
        /// <param name="str"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object Deserialize(string str, Type t)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return Activator.CreateInstance(t, null);
            }
            if (t == typeof(string))
            {
                return (object)str;
            }
            return JsonConvert.DeserializeObject(str.RemoveAnnotation(), t);
        }



        /// <summary>
        /// 对象转换成Json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serailze(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            return JsonConvert.SerializeObject(obj, setting);
        }

        public static byte[] SerailzeByte(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            var json = JsonConvert.SerializeObject(obj, setting);
            return string.IsNullOrWhiteSpace(json) ? null : Encoding.UTF8.GetBytes(json);
        }

        #endregion

    }
}
