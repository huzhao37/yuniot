using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

using System.Runtime.Serialization;

namespace Yunt.Device.Repository.EF.Models
{
    /// <summary>
    /// 给料机原始数据
    /// </summary>
    [DataContract]
    [Serializable][ProtoContract(SkipConstructor = true)]
    public class MaterialFeeder : BaseModel
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [ProtoMember(9)]
        public string MotorId { get; set; }
        [ProtoMember(10)]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 频率        
        /// </summary>
        [DataMember]
        [DisplayName("频率")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(1)]
        public double Frequency { get; set; }
        /// <summary>
        /// 电流        
        /// </summary>
        [DisplayName("电流")]
        //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(2)]
        public double Current { get; set; }
        /// <summary>
        /// 电压
        /// </summary>
        [DataMember]
        [DisplayName("电压")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(3)]
        public double Voltage { get; set; }
        /// <summary>
        /// 速度        
        /// </summary>
        [DataMember]
        [DisplayName("速度")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(4)]
        public double Velocity { get; set; }

        /// <summary>
        /// 功率因子
        /// </summary>
        [DataMember]
        [DisplayName("功率因子")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(5)]
        public double PowerFactor { get; set; }
        /// <summary>
        /// 无功功率
        /// </summary>
        [DataMember]
        [DisplayName("无功功率")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(6)]
        public double ReactivePower { get; set; }
        /// <summary>
        /// 总功率
        /// </summary>
        [DataMember]
        [DisplayName("总功率")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(7)]
        public double TotalPower { get; set; }


        [DataMember]
        [DisplayName("输入频率")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(8)]
        public double InFrequency { get; set; }
        //[ProtoMember(9)]
        //public DateTimeOffset Time { get; set; }
        ///// <summary>
        ///// 设备ID
        ///// </summary>
        //[ProtoMember(10)]
        //public string MotorId { get; set; }
    }
}
