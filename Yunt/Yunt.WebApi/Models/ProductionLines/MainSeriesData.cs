using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yunt.WebApi.Models.ProductionLines
{
    /// <summary>
    /// 成品皮带历史数据
    /// </summary>
    public class MainSeriesData
    {
        public MainSeriesData()
        {
            SeriesDatas = new List<SeriesDatas>();
            Total = new List<Total>();
        }
        public string MotorID { set; get; }
        public string MotorName { get; set; }
        public List<SeriesDatas> SeriesDatas { get; set; }

        public List<Total> Total { get; set; }
        

    }
    public class Total
    {
        /// <summary>
        /// 总运行时间
        /// </summary>
        public float SumRunningTime { get; set; }

        /// <summary>
        /// 总产量
        /// </summary>
        public float SumOutPut { get; set; }

        /// <summary>
        /// 每吨耗电量
        /// </summary>
        public float AvgActivePower { get; set; }      
    }

    public class SeriesDatas
    {
        /// <summary>
        /// 时间
        /// </summary>
        public long UnixTime { get; set; }

        /// <summary>
        /// 运行时间
        /// </summary>
        public float RunningTime { get; set; }

        /// <summary>
        /// 产量
        /// </summary>
        public float Output { get; set; }
    }

}
