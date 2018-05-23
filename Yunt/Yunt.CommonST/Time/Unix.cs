using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yunt.Common
{
    public static class Unix
    {
        /// <summary>
        /// 将Unix时间戳转换为DateTime类型时间;(本地时区时间)
        /// </summary>
        /// <param name=”d”>double 型数字</param>
        /// <returns>DateTime</returns>
        public static DateTime ConvertIntDateTime(double d)
        {
            DateTime time = DateTime.MinValue;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            //TODO:  
            time = startTime.AddSeconds(d - 8 * 60 * 60);
            return time;
        }

        /// <summary>
        /// 将Unix时间戳转化为DateTime类型时间; (UTC时间)
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static System.DateTime ConvertIntDateTimeUTC(double d)
        {
            DateTime time = DateTime.MinValue;
            DateTime startTime = TimeZone.CurrentTimeZone.ToUniversalTime(new DateTime(1970, 1, 1));
            time = startTime.AddSeconds(d);
            return time;
        }
        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>int</returns>
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            double intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            //intResult = (time - startTime).TotalSeconds;
            intResult = (time.AddHours(8) - startTime).TotalSeconds;
            return (int)intResult;
        }

        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>int</returns>
        public static long ConvertDateTimeLong(System.DateTime time)
        {
            double intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            //intResult = (time - startTime).TotalSeconds;
            intResult = (time.AddHours(8) - startTime).TotalSeconds;
            return (long)intResult;
        }

        /// <summary>
        /// 将Unix时间戳转换为DateTime类型时间;(本地时区时间)
        /// </summary>
        /// <param name=”d”>double 型数字</param>
        /// <returns>DateTime</returns>
        public static System.DateTime ConvertLongDateTime(long d)
        {
            System.DateTime time = System.DateTime.MinValue;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            //TODO:  
            time = startTime.AddSeconds(d - 8 * 60 * 60);
            return time;
        }
    }
}
