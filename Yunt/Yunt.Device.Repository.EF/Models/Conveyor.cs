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
    /// 皮带机原始数据;
    /// </summary>
    [DataContract]
    [Serializable][ProtoContract(SkipConstructor = true)]
    public class Conveyor : BaseModel
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [ProtoMember(13)]
        public int MotorId { get; set; }
        [ProtoMember(14)]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 电流;
        /// </summary>
        [DisplayName("电流")]
        //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(1)]
        public double Current { get; set; }
        /// <summary>
        /// 电压;
        /// </summary>
        [DataMember]
        [DisplayName("电压")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(2)]
        public double Voltage { get; set; }
        /// <summary>
        /// 功率因子;
        /// </summary>
        [DataMember]
        [DisplayName("功率因子")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(3)]
        public double PowerFactor { get; set; }
        /// <summary>
        /// 无功功率;
        /// </summary>
        [DataMember]
        [DisplayName("无功功率")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(4)]
        public double ReactivePower { get; set; }
        /// <summary>
        /// 总功率;
        /// </summary>
        [DataMember]
        [DisplayName("总功率")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(5)]
        public double TotalPower { get; set; }
        /// <summary>
        /// 瞬时称重;
        /// </summary>
        [DataMember]
        [DisplayName("瞬时称重")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(6)]
        public double InstantWeight { get; set; }
        /// <summary>
        /// 累计称重;
        /// </summary>
        [DataMember]
        [DisplayName("累计称重")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(7)]
        public double AccumulativeWeight { get; set; }
        /// <summary>
        /// 速度;        
        /// </summary>
        [DataMember]
        [DisplayName("速度")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(8)]
        public double Velocity { get; set; }
        /// <summary>
        /// 频率;
        /// </summary>
        [DataMember]
        [DisplayName("频率")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(9)]
        public double Frequency { get; set; }
        /// <summary>
        /// 单位(千克KG/吨T);
        /// </summary>
        [DataMember]
        [DisplayName("单位(千克KG/吨T)")]
        [ProtoMember(10)]
        public int Unit { get; set; }

        /// <summary>
        /// 开机标志位;
        /// </summary>
        [DataMember]
        [DisplayName("开机标志位")]
        [ProtoMember(11)]
        public int BootFlagBit { get; set; }

        /// <summary>
        /// 校零;
        /// </summary>
        [DataMember]
        [DisplayName("校零")]
        [ProtoMember(12)]
        public int ZeroCalibration { get; set; }
        //[ProtoMember(13)]
        //public DateTimeOffset Time { get; set; }
        ///// <summary>
        ///// 设备ID;
        ///// </summary>
        //[ProtoMember(14)]
        //public int MotorId { get; set; }
    }
}
