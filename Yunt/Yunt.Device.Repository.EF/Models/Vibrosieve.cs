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
    /// 振动筛原始数据;
    /// </summary>
    [DataContract]
    [Serializable][ProtoContract(SkipConstructor = true)]
    public class Vibrosieve : BaseModel
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [ProtoMember(6)]
        public string MotorId { get; set; }
        [ProtoMember(7)]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 电流;      
        /// </summary>
        [DisplayName("主机电流")]
        ////[MotorConfig(IsAlarmProperty = true)]
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
        //[ProtoMember(6)]
        //public DateTimeOffset Time { get; set; }
        ///// <summary>
        ///// 设备ID
        ///// </summary>
        //[ProtoMember(7)]
        //public string MotorId { get; set; }
    }
}
