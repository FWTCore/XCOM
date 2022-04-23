using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XCOM.Schema.Standard.Extension;

namespace XCOM.Schema.Standard.Utility
{
    public class XMVerification
    {
        /// <summary>
        /// 是否是手机号
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static bool IsPhoneNumber(string phoneNumber)
        {
            return phoneNumber.IsMatch(@"^\s*1\d{10}\s*$");
        }

        /// <summary>
        /// 替换手机号
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static string ReplacePhoneNumber(string phoneNumber)
        {
            return Regex.Replace(phoneNumber, "(\\d{3})\\d{4}(\\d{4})", "$1****$2");
        }




    }
}
