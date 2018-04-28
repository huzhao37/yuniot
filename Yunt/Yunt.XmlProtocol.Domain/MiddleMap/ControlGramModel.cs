using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yunt.XmlProtocol.Domain.MiddleMap
{
    /// <summary>
    /// 控制应答协议模型;
    /// </summary>
    public class ControlGramModel
    {
        public ControlGramModel()
        {
            ControlMotors = new List<ControlMotorModel>();
        }
        /// <summary>
        /// 嵌入式设备ID;
        /// </summary>
        public string DeviceId { get; set; }
        /// <summary>
        /// 应答协议表单ID;
        /// </summary>
        public int DataConfigId { get; set; }
        /// <summary>
        /// 数据表单个数;
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 控制设备;
        /// </summary>
        public List<ControlMotorModel> ControlMotors { get; set; }
    }
}
