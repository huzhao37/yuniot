using System;
using System.Collections.Generic;
using System.Text;

namespace Yunt.Common
{
   public static class TimeEx
    {

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>C#格式时间</returns>
        public static DateTime Time(this long timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        #region 将Unix时间戳转为C#格式时间

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>C#格式时间</returns>
        public static DateTime Time(this string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

 

        #endregion

        #region DateTime时间格式转换为Unix时间戳格式

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"> DateTime时间格式</param>
        /// <returns>Unix时间戳格式</returns>
        public static int TimeSpan(this DateTime time)
        {
            if (time == DateTime.MinValue)
            {
                return 0;
            }
            else
            {
              //  DateTime startTime = TimeZoneInfo.ConvertTime(time, TimeZoneInfo.Local);//等价的建议写法
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                return (int)(time - startTime).TotalSeconds;
            }
        }

        #endregion

        /// <summary>
        /// redis key 过期时长（到现在为止间隔1周）
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long Expire(this long time)
        {
            return Convert.ToInt64(Math.Abs(time.Time().Date.AddDays(7).Subtract(DateTime.Now.Date).TotalSeconds));
        }

        /// <summary>
        /// redis key 过期时长（到现在为止间隔1天）
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ExpireOneDay(this long time)
        {
            return Convert.ToInt64(Math.Abs(time.Time().Date.AddDays(1).Subtract(DateTime.Now.Date).TotalSeconds));
        }
        /// <summary>
        /// redis key 过期时长（< 1 days，明天凌晨过期）
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ExpireOneDay2(this long time)
        {
            return Convert.ToInt64(Math.Abs(time.Time().Date.AddDays(1).Subtract(time.Time()).TotalSeconds));
        }

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>C#格式时间</returns>
        public static DateTime Time(this int timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
    }
}
