using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yunt.Xml.Domain.MiddleMap
{
    /// <summary>
    /// 电机模型
    /// </summary>
    public class MachineModel
    {
        /// <summary>
        /// 电机ID嵌入式所使用(!=MotorId)
        /// </summary>
        public string MachineId { get; set; }
        /// <summary>
        /// 电机名称
        /// </summary>
        public string MachineName { get; set; }
        /// <summary>
        /// 电机类型
        /// </summary>
        public string MachineType { get; set; }
        /// <summary>
        /// 电机设备ID(或为SimCardId)
        /// </summary>
        public int MotorId { get; set; }
        /// <summary>
        /// 电机类型
        /// </summary>
        public string MachineTypeId { get; set; }
    }
}
