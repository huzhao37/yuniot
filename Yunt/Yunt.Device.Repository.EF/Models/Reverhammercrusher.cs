using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;


namespace Yunt.Device.Repository.EF.Models
{
    /// <summary>
    /// 可逆锤破
    /// </summary>
    [DataContract]
    [Serializable][ProtoContract(SkipConstructor = true)]
    public class ReverHammerCrusher : BaseModel
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [ProtoMember(5)]
        public string MotorId { get; set; }
      
  
        /// <summary>
        /// 电流
        /// </summary>
        [DisplayName("电流")]
        [ProtoMember(1)]
        public float Current { get; set; }
        /// <summary>
        /// 轴承1温度
        /// </summary>
        [DataMember]
        [DisplayName("轴承1温度")]
        [ProtoMember(2)]
        public float SpindleTemperature1 { get; set; }
        /// <summary>
        /// 轴承2温度
        /// </summary>
        [DataMember]
        [DisplayName("轴承2温度")]
        [ProtoMember(3)]
        public float SpindleTemperature2 { get; set; }
        /// <summary>
        /// 轴承速度  rPM
        /// </summary>
        [DataMember]
        [DisplayName("轴承速度")]
        [ProtoMember(4)]
        public float BearingSpeed { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        //[ProtoMember(5)]
        //public DateTime Time { get; set; }
        ///// <summary>
        ///// 设备ID
        ///// </summary>
        //[ProtoMember(6)]
        //public string MotorId { get; set; }
    }
}
