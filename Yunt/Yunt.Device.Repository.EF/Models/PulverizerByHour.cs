using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yunt.Device.Repository.EF.Models
{
    /// <summary>
    /// 磨粉机
    /// </summary>
     [Serializable][ProtoContract(SkipConstructor = true)]
    public class PulverizerByHour : BaseModel
    {

        /// <summary>
        /// 设备ID
        /// </summary>
        [ProtoMember(7)]
        public int MotorId { get; set; }
        [ProtoMember(8)]
        public bool IsDeleted { get; set; }
        [ProtoMember(1)]
        public double AverageCurrent { get; set; }
        [ProtoMember(2)]
        public double AverageFanCurrent { get; set; }
        [ProtoMember(3)]
        public double AverageGraderCurrent { get; set; }
        [ProtoMember(4)]
        public double AverageGraderRotateSpeed { get; set; }
        /// <summary>
        /// 开机时间
        /// </summary>
        [ProtoMember(5)]
        public double RunningTime { get; set; }
        /// <summary>
        /// 负荷
        /// </summary>
        [ProtoMember(6)]
        public double LoadStall { get; set; }
        //[ProtoMember(7)]
        //public DateTimeOffset Time { get; set; }
        //[ProtoMember(8)]
        //public int MotorId { get; set; }
    }
}
