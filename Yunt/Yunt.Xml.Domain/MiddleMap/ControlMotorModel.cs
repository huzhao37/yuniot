using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yunt.Xml.Domain.MiddleMap
{
    public class ControlMotorModel
    {
        /// <summary>
        /// 控制设备ID;
        /// </summary>
        public string ControlDeviceId { get; set; }
        /// <summary>
        /// 逾期时间;
        /// </summary>
        public DateTime ExpiredTime { get; set; }
    }
}
