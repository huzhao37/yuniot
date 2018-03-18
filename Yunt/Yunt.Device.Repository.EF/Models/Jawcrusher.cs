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
    /// 颚式破碎机原始数据;
    /// </summary>
    [DataContract]
    [Serializable][ProtoContract(SkipConstructor = true)]
    public class JawCrusher : BaseModel
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [ProtoMember(10)]
        public string MotorId { get; set; }
        [ProtoMember(11)]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 电流
        /// </summary>
        [DisplayName("主机电流")]
        //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(1)]
        public double Current { get; set; }

        /// <summary>
        /// 电压
        /// </summary>
        [DataMember]
        [DisplayName("电压")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(2)]
        public double Voltage { get; set; }
        /// <summary>
        /// 功率因子
        /// </summary>
        [DataMember]
        [DisplayName("功率因子")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(3)]
        public double PowerFactor { get; set; }
        /// <summary>
        /// 无功功率
        /// </summary>
        [DataMember]
        [DisplayName("无功功率")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(4)]
        public double ReactivePower { get; set; }
        /// <summary>
        /// 总功率
        /// </summary>
        [DataMember]
        [DisplayName("总功率")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(5)]
        public double TotalPower { get; set; }

        //add by deniel @2015-11-18
        [DataMember]
        [DisplayName("实时机架轴承1温度")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(6)]
        public double RackSpindleTemperature1 { get; set; }

        [DataMember]
        [DisplayName("实时机架轴承2温度")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(7)]
        public double RackSpindleTemperature2 { get; set; }
        [DataMember]
        [DisplayName("实时动颚轴承1温度")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(8)]
        public double MotiveSpindleTemperature1 { get; set; }

        [DataMember]
        [DisplayName("实时动颚轴承2温度")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(9)]
        public double MotiveSpindleTemperature2 { get; set; }
        //[ProtoMember(10)]
        //public DateTimeOffset Time { get; set; }

        ///// <summary>
        ///// 设备ID
        ///// </summary>
        //[ProtoMember(11)]
        //public string MotorId { get; set; }
    }
}
