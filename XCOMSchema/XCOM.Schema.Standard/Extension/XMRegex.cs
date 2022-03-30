using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.Extension
{
    public static class XMRegex
    {
        /// <summary>
        /// 正则表达式替换内容
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string RegexReplace(this string str, string pattern, string replacement, RegexOptions options = RegexOptions.IgnoreCase)
        {
            return string.IsNullOrWhiteSpace(str) ? str : Regex.Replace(str, pattern, replacement, options);
        }

        /// <summary>
        /// 验证输入字符串是否与模式字符串匹配，匹配返回true
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">模式字符串</param>        
        public static bool IsMatch(this string input, string pattern)
        {
            return input.IsMatch(pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证输入字符串是否与模式字符串匹配，匹配返回true
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="options">筛选条件</param>
        public static bool IsMatch(this string input, string pattern, RegexOptions options)
        {
            return Regex.IsMatch(input, pattern, options);
        }


        /// <summary>
        /// 搜索匹配的值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static MatchCollection Matches(this string str, string pattern)
        {
            Regex reg = new(pattern, RegexOptions.IgnoreCase); // 搜索匹配的字符串
            var list = reg.Matches(str);
            return list;
        }

        /// <summary>
        /// 搜索匹配的值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static Match MatchSingle(this string str, string pattern)
        {
            Regex reg = new(pattern, RegexOptions.IgnoreCase); // 搜索匹配的字符串
            var result = reg.Match(str);
            return result;
        }

        /// <summary>
        /// 正在表达式 拆分字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string[] Split(this string str, string pattern)
        {
            var items = Regex.Split(str, pattern);
            return items;
        }

    }
}
