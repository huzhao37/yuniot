using System;
using System.Collections.Generic;
using System.Text;

namespace Yunt.Common.Shift
{
   public class ShiftCalc
    {
        /// <summary>
        /// 班次必须是24小时一个
        /// </summary>
        /// <param name="start">精确至小时</param>
        /// <param name="end">精确至小时(包含)</param>
        /// <param name="shiftStartHour">班次起始小时时间</param>
        /// <param name="shiftEndHour">班次结束小时时间</param>
        /// <returns>起始和结束时间</returns>
        public static List<Tuple<long, long>> GetTimesByShift(DateTime start,DateTime end,int shiftStartHour,int shiftEndHour)
        {
            var res = new List<Tuple<long, long>>();
            if (end.Subtract(start).TotalHours <= 24 && start.Hour >= shiftStartHour && end.Hour < shiftEndHour
                     || ((start.Hour >= shiftStartHour && end.Hour >= shiftEndHour
                     || start.Hour < shiftStartHour && end.Hour < shiftEndHour) && start.Date == end.Date))
                return res;           
            var hours =(int)end.Subtract(start).TotalHours/1+1;          
            var endTime = start.Hour >= shiftStartHour ? start.Date.AddDays(1).AddHours(shiftStartHour):start.Date.AddHours(shiftStartHour);
            var startTime =end.Hour>= shiftEndHour?end.Date.AddHours(shiftEndHour):end.Date.AddDays(-1).AddHours(shiftEndHour);
            res.Add(new Tuple<long, long>(start.TimeSpan(), endTime.TimeSpan()));     
            var remainsHours = hours - (int)endTime.Subtract(start).TotalHours/1 - (int)end.Subtract(startTime).TotalHours/1;
            var len = 0;
            if (remainsHours > 0)
                len = remainsHours / 24;
            var s = endTime;
            for (int i = 0; i < len; i++)
            {
                var e = s.AddDays(1);//.AddHours(shiftStartHour);
                res.Add(new Tuple<long, long>(s.TimeSpan(), e.TimeSpan()));
                s = e;
            }
            res.Add(new Tuple<long, long>(startTime.TimeSpan(), end.TimeSpan()));
            return res;
        }
    }
}
