using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yunt.WebApiV2.Models.ProductionLines
{
    /// <summary>
    /// 主皮带机历史数据
    /// </summary>
    public class MainConveyorHository
    {
        public MainConveyorHository()
        {
            SeriesData=new List<SeriesData>();
            FinishCy= new List<FinishCy>();
        }
        /// <summary>
        /// 产量
        /// </summary>
        public float Output { get; set; }
        /// <summary>
        /// 瞬时称重
        /// </summary>
        public float AvgInstantWeight { get; set; }
        /// <summary>
        /// 运行时间
        /// </summary>
        public float RunningTime { get; set; }
        /// <summary>
        /// 负荷
        /// </summary>
        public float AvgLoadStall { get; set; }

       public List<SeriesData> SeriesData { get; set; }

        public List<FinishCy> FinishCy { get; set; }
    }

    /// <summary>
    /// 成品皮带
    /// </summary>
    public class SeriesData
    {
        /// <summary>
        /// 时间
        /// </summary>
        public long UnixTime { get; set; }

        public float RunningTime { get; set; }

        public float Output { get; set; }
    }
    /// <summary>
    /// 成品皮带
    /// </summary>
    public class FinishCy
    {
        public string MotorName { get; set; }

        public string MotorId { get; set; }

        public float Output { get; set; }
    }
}
