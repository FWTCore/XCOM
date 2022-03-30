using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.Extension
{
   public static  class XMString
    {
        /// <summary>
        /// 使用正则移除连接串中的密码，供日志打印时使用
        /// </summary>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public static string RemoveConnectionPassword(this string connStr)
        {
            return connStr.RegexReplace("password.*?=.*?;", "xxxx");
        }

        /// <summary>
        /// 去除/*   */注释
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveAnnotation(this string str)
        {
            return str.RegexReplace(@"\/\*(\s|.)*?\*\/", "");
        }

        public static byte[] SerializeUtf8(this string str)
        {
            return string.IsNullOrWhiteSpace(str) ? null : Encoding.UTF8.GetBytes(str);
        }

        public static string DeserializeUtf8(this byte[] stream)
        {
            return stream == null ? null : Encoding.UTF8.GetString(stream);
        }

        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(this string str, string delchar)
        {
            return str.Substring(0, str.LastIndexOf(delchar));
        }

        /// <summary>
        /// 得到字符串长度，一个汉字长度为2
        /// </summary>
        /// <param name="inputString">参数字符串</param>
        /// <returns></returns>
        public static int StrLength(this string inputString)
        {
            ASCIIEncoding ascii = new();
            int tempLen = 0;
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;
            }
            return tempLen;
        }

        /// <summary>
        /// 按照字节截取字符串 一个汉字算2个字节
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="length">截取数量,单位：字节</param>
        /// <param name="flag">返回字符串 是否带省略号;true 带 false 不带</param>
        /// <returns></returns>
        public static string Substring(string str, int length, bool flag = true)
        {
            byte[] bytes = Encoding.Default.GetBytes(str);
            string resultStr = str;
            if (bytes.Length > length)
            {
                resultStr = "";
                for (int i = 0; i < str.Length; i++)
                {
                    byte[] b = Encoding.Default.GetBytes(resultStr);
                    if (b.Length < length)
                    {
                        resultStr += str.Substring(i, 1);
                    }
                    else
                    {
                        if (flag)
                        {
                            resultStr += "...";
                        }
                        break;
                    }
                }
            }
            return resultStr;
        }
    }
}
