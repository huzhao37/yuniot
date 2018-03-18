using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yunt.Device.Repository.EF.Models
{
    /// <summary>
    /// 反击破碎机
    /// </summary>
     [Serializable][ProtoContract(SkipConstructor = true)]
    public class ImpactCrusherByHour : BaseModel
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [ProtoMember(8)]
        public string MotorId { get; set; }
        [ProtoMember(9)]
        public bool IsDeleted { get; set; }
        [ProtoMember(1)]
        public double AverageSpindleTemperature1 { get; set; }
        [ProtoMember(2)]
        public double AverageSpindleTemperature2 { get; set; }
        [ProtoMember(3)]
        public double AverageCurrent { get; set; }
        [ProtoMember(4)]
        public double AverageCurrent2 { get; set; }

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

        /// <summary>
        /// 开关机次数
        /// </summary>
        [ProtoMember(7)]
        public int OnOffCounts { get; set; }
        //[ProtoMember(8)]
        //public DateTimeOffset Time { get; set; }
        ///// <summary>
        ///// 设备ID
        ///// </summary>
        //[ProtoMember(9)]
        //public string MotorId { get; set; }
    }
}
