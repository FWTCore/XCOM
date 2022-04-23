using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.Utility
{
    public class XMMoney
    {
        /// <summary>
        /// 显示金额格式
        /// </summary>
        /// <param name="money"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static string FormatMoney(decimal money, int decimals)
        {
            return String.Format($"{{0:N{decimals}}}", money);
        }

    }
}

