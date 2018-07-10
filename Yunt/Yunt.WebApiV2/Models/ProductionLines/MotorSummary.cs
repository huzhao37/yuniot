using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yunt.Common;

namespace Yunt.WebApiV2.Models.ProductionLines
{
    /// <summary>
    /// 电机设备摘要
    /// </summary>
    public class MotorSummary
    {
        /// <summary>
        /// 电机设备Id
        /// </summary>
        public string MotorId { get; set; }
        /// <summary>
        /// 电机设备名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 电机设备类型ID
        /// </summary>
        public string MotorTypeId { get; set; }
        ///// <summary>
        ///// 电机 设备负荷
        ///// </summary>
        public float LoadStall { get; set; }
        /// <summary>
        ///电机 设备状态
        /// </summary>
        public MotorStatus MotorStatus { get; set; }
        ///// <summary>
        ///// 运行时间
        ///// </summary>
       public float RunningTime { get; set; }

        ///// <summary>
        ///// 瞬时值
        ///// </summary>
        //public float InstantValue { get; set; }
    }
}
