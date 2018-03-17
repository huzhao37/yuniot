using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yunt.Device.Repository.EF.Models
{
     [Serializable][ProtoContract(SkipConstructor = true)]
    public class SimonsConeCrusherByHour : BaseModel
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [ProtoMember(7)]
        public int MotorId { get; set; }
        [ProtoMember(8)]
        public bool IsDeleted { get; set; }
        [ProtoMember(1)]
        public double AverageTankTemperature { get; set; }
        [ProtoMember(2)]
        public double AverageOilFeedTempreature { get; set; }
        [ProtoMember(3)]
        public double AverageOilReturnTempreature { get; set; }
        [ProtoMember(4)]
        public double AverageCurrent { get; set; }
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
        ///// <summary>
        ///// 设备ID
        ///// </summary>
        //[ProtoMember(8)]
        //public int MotorId { get; set; }
    }
}
