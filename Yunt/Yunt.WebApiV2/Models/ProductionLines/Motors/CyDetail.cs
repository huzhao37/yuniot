using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yunt.WebApiV2.Models.ProductionLines
{
    public class CyDetail
    {

        public CyDetail()
        {
            SeriesData = new List<dynamic>();         
        }
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
        public float LoadStall { get; set; }
        /// <summary>
        /// 产量
        /// </summary>
        public float AccumulativeWeight { get; set; }

        /// <summary>
        /// 平均电流
        /// </summary>
        public float AvgCurrent { get; set; }

        public IEnumerable<dynamic> SeriesData { get; set; }

        
    }
}
