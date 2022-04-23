using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.Utility
{
    public class XMMath
    {
        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="data"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static decimal Round(decimal data, int decimals)
        {
            return Math.Round(data, decimals, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 取地板值
        /// </summary>
        /// <param name="data"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static decimal Floor(decimal data, int? decimals = null)
        {
            if (decimals == null || decimals <= 0)
            {
                return Math.Floor(data);
            }
            else
            {
                var digit = (decimal)Math.Pow(10, (double)decimals);
                return Math.Floor(data * digit) / digit;
            }
        }

        /// <summary>
        /// 取天板值
        /// </summary>
        /// <param name="data"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static decimal Ceiling(decimal data, int? decimals = null)
        {
            if (decimals == null || decimals <= 0)
            {
                return Math.Ceiling(data);
            }
            else
            {
                var digit = (decimal)Math.Pow(10, (double)decimals);
                return Math.Ceiling(data * digit) / digit;
            }
        }

    }
}
