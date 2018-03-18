using System;
using System.Collections.Generic;
using System.Text;

namespace Yunt.Device.Repository.EF.Models.IdModel
{
   public class MotorIdFactories:BaseModel
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
