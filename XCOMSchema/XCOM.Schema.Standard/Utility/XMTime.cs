using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCOM.Schema.Standard.Utility
{
    public class XMTime
    {

        /// <summary>
        /// javascript所使用的时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime JavascriptToDateTime(long timeStamp)
        {
            DateTime startTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local); // 当地时区
            DateTime dt = startTime.AddMilliseconds(timeStamp);
            return dt;
        }

        /// <summary>
        /// 把时间转换为javascript所使用的时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long DateTimeToJavascript(DateTime dt)
        {
            DateTime startTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local); // 当地时区
            long timeStamp = (long)(dt - startTime).TotalMilliseconds; // 相差毫秒数
            return timeStamp;
        }

        /// <summary>
        /// 把时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long DateTimeByUnix(DateTime dt)
        {
            DateTime startTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local); // 当地时区
            long timeStamp = (long)(dt - startTime).TotalSeconds; // 相差秒数
            return timeStamp;
        }

        /// <summary>
        /// 把Unix时间戳转化为时间
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime DateTimeToUnix(long unixTimeStamp)
        {
            DateTime startTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local); // 当地时区
            DateTime dt = startTime.AddSeconds(unixTimeStamp);
            return dt;
        }

        /// <summary>
        /// 获取今天开始
        /// </summary>
        /// <returns></returns>
        public static DateTime GetTodayStart()
        {
            var today = DateTime.Now;
            return new DateTime(today.Year, today.Month, today.Day);
        }
        /// <summary>
        /// 获取今天结束时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetTodayEnd()
        {
            var today = DateTime.Now;
            return new DateTime(today.Year, today.Month, today.Day, 23, 59, 59);
        }

        /// <summary>
        /// 两个时间的秒数
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns></returns>
        public static long TimeDifferenceSecond(DateTime startTime, DateTime endTime)
        {
            long timeStamp = (long)(endTime - startTime).TotalSeconds; // 相差秒数
            return timeStamp;
        }


    }
}
