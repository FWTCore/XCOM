using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace XCOM.Schema.Standard.Utility
{
    public class XMHttpUtility
    {
        /// <summary>
        /// Url编码
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string UrlEncode(string content)
        {
            return HttpUtility.UrlEncode(content);
        }

        /// <summary>
        /// Url解码
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string UrlDecode(string content)
        {
            return HttpUtility.UrlDecode(content);
        }

        /// <summary>
        /// Html编码
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string HtmlEncode(string content)
        {
            return HttpUtility.HtmlEncode(content);
        }


        /// <summary>
        /// Html解码
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string HtmlDecode(string content)
        {
            return HttpUtility.HtmlDecode(content);
        }


    }
}
