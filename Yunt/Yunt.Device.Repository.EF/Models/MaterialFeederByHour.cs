using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yunt.Device.Repository.EF.Models
{
    /// <summary>
    /// 给料机按小时统计数据;
    /// </summary>
     [Serializable][ProtoContract(SkipConstructor = true)]
    public class MaterialFeederByHour : BaseModel
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [ProtoMember(7)]
        public int MotorId { get; set; }
        [ProtoMember(8)]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 平均频率;      
        /// </summary>
        [ProtoMember(1)]
        public double AverageFrequency { get; set; }
        /// <summary>
        /// 平均电流;        
        /// </summary>
        [ProtoMember(2)]
        public double AverageCurrent { get; set; }
        /// <summary>
        /// 平均电压;
        /// </summary>
        [ProtoMember(3)]
        public double AverageVoltage { get; set; }
        /// <summary>
        /// 平均速度;        
        /// </summary>
        [ProtoMember(4)]
        public double AverageVelocity { get; set; }

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
