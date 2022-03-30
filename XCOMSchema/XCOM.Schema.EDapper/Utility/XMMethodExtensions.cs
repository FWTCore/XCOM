using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.EDapper.Utility
{
    internal static class XMMethodExtensions
    {
#pragma warning disable IDE0060 // 删除未使用的参数
        public static bool XMNotIn<T>(this T t, IEnumerable<T> obj)
#pragma warning restore IDE0060 // 删除未使用的参数
        {
            return true;
        }

#pragma warning disable IDE0060 // 删除未使用的参数
        public static bool XMNotNullOrEmpty<T>(this T t)
#pragma warning restore IDE0060 // 删除未使用的参数
        {
            return true;
        }

#pragma warning disable IDE0060 // 删除未使用的参数
        public static bool XMNotLike<T>(this T t, T obj)
#pragma warning restore IDE0060 // 删除未使用的参数
        {
            return true;
        }

#pragma warning disable IDE0060 // 删除未使用的参数
        public static bool XMNotStartsLike<T>(this T t, T obj)
#pragma warning restore IDE0060 // 删除未使用的参数
        {
            return true;
        }

#pragma warning disable IDE0060 // 删除未使用的参数
        public static bool XMNotEndsLike<T>(this T t, T obj)
#pragma warning restore IDE0060 // 删除未使用的参数
        {
            return true;
        }

    }
}
