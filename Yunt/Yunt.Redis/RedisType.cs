using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yunt.Redis
{
   public enum RedisType
    {
        /// <summary>
        /// 常用类型表的存储
        /// </summary>
        GeneralType=0,
        /// <summary>
        /// 瞬时统计的存储
        /// </summary>
        InstantStatistics=1,
        /// <summary>
        /// 小时统计的存储
        /// </summary>
        HourStatistics,
        /// <summary>
        /// 自然日统计的存储
        /// </summary>
        DayStatistics,
        /// <summary>
        /// 工作班次统计的存储
        /// </summary>
        WorkShiftStatistics,
       
        /// <summary>
        /// 不存储
        /// </summary>
        NoRedis,
        /// <summary>
        /// 数字量数据的存储
        /// </summary>
        DigitalSave,
        /// <summary>
        /// 报警瞬时数据的存储
        /// </summary>
       Alarm,
    }
}
