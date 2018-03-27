using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yunt.Device.Repository.EF.Models
{
     [Serializable][ProtoContract(SkipConstructor = true)]
    public class ReverHammerCrusherByDay : BaseModel
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [ProtoMember(7)]
        public string MotorId { get; set; }

  
        /// <summary>
        /// 电流
        /// </summary>
        [ProtoMember(1)]
        public float Current { get; set; }
        /// <summary>
        /// 轴承1温度
        /// </summary>
        [ProtoMember(2)]
        public float SpindleTemperature1 { get; set; }
        /// <summary>
        /// 轴承2温度
        /// </summary>
        [ProtoMember(3)]
        public float SpindleTemperature2 { get; set; }
        /// <summary>
        /// 轴承速度  rPM
        /// </summary>
        [ProtoMember(4)]
        public float BearingSpeed { get; set; }

        /// <summary>
        /// 开机时间
        /// </summary>
        [ProtoMember(5)]
        public float RunningTime { get; set; }
        /// <summary>
        /// 负荷
        /// </summary>
        [ProtoMember(6)]
        public float LoadStall { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        //[ProtoMember(7)]
        //public DateTime Time { get; set; }
        ///// <summary>
        ///// 设备ID
        ///// </summary>
        //[ProtoMember(8)]
        //public string MotorId { get; set; }
    }
}
