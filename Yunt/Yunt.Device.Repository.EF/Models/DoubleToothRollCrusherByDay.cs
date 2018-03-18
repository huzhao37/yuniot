using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yunt.Device.Repository.EF.Models
{
     [Serializable][ProtoContract(SkipConstructor = true)]
    public class DoubleToothRollCrusherByDay : BaseModel
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [ProtoMember(4)]
        public string MotorId { get; set; }
        [ProtoMember(5)]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 主机1电流
        /// </summary>
        [ProtoMember(1)]
        public double Current { get; set; }
        /// <summary>
        /// 主机2电流
        /// </summary>
        [ProtoMember(2)]
        public double Current2 { get; set; }

        /// <summary>
        /// 开机时间
        /// </summary>
        [ProtoMember(3)]
        public double RunningTime { get; set; }
        /// <summary>
        /// 负荷
        /// </summary>
        [ProtoMember(4)]
        public double LoadStall { get; set; }
       
    }
}
