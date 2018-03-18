using System;
using System.Collections.Generic;
using System.Text;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.Model.IdModel
{
   public class MotorIdFactories:AggregateRoot
    {
        /// <summary>
        /// 产线Id
        /// </summary>
        public string ProductionLineId { get; set; }
        /// <summary>
        /// 电机设备类型Id
        /// </summary>

        public string MotorTypeId { get; set; }
        /// <summary>
        /// 电机设备序号
        /// </summary>

        public int MotorIndex { get; set; }
    }
}
