using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.Utility
{
    public class UNCHelper
    {
        /// <summary>
        /// 将数据库中变量名改为驼峰命名
        /// 如 user_name 改为 UserName
        /// </summary>
        /// <param name="name">变量名</param>
        /// <returns></returns>
        public static string GenVarName(string name)
        {
            string first = name.Substring(0, 1);
            name = name.Substring(1, name.Length - 1);
            name = first.ToUpper() + name;

            int index = name.IndexOf("_");
            while (index != -1)
            {
                if (name.Length >= index + 2)
                {
                    first = name.Substring(index + 1, 1);
                    string start = name.Substring(0, index);
                    string end = name.Substring(index + 2, name.Length - index - 2);
                    name = start + first.ToUpper() + end;

                    index = name.IndexOf("_");
                }
            }
            name = name.Replace("_", "");
            return name;
        }

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string FirstLetter(string name)
        {
            var first = name.Substring(0, 1);
            var end = name.Substring(1, name.Length - 1);
            return $"{first.ToLower()}{end}";
        }
    }
}
