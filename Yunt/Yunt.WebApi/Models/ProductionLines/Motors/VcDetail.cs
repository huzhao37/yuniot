using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yunt.WebApi.Models.ProductionLines
{
    public class VcDetail
    {

        public VcDetail()
        {
            SeriesData = new List<dynamic>();
        }
        /// <summary>
        /// 运行时间
        /// </summary>
        public float RunningTime { get; set; }
        /// <summary>
        /// 负荷
        /// </summary>
        public float LoadStall { get; set; }
        public float AvgVibrate1 { get; set; }
        public float AvgVibrate2 { get; set; }
        public float WearValue1 { get; set; }
        public float WearValue2 { get; set; }
        /// <summary>
        /// 平均电流
        /// </summary>
        public float AvgCurrent { get; set; }

        public IEnumerable<dynamic> SeriesData { get; set; }

        
    }
}
