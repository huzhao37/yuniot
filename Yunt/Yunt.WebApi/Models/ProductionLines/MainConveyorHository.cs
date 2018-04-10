using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yunt.WebApi.Models.ProductionLines
{
    /// <summary>
    /// 主皮带机历史数据
    /// </summary>
    public class MainConveyorHository
    {
        /// <summary>
        /// 产量
        /// </summary>
        public float AccumulativeWeight { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public long Time { get; set; }
    }
}
