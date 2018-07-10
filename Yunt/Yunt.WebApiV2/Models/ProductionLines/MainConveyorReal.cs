using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yunt.WebApiV2.Models.ProductionLines
{
    /// <summary>
    /// 主皮带实时数据
    /// </summary>
    public class MainConveyorReal
    {
        public MainConveyorReal()
        {
        }

        public bool ProductionLineStatus { get; set; }
        /// <summary>
        /// 瞬时称重
        /// </summary>
        public float InstantWeight { get; set; }
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
        
    }

}
