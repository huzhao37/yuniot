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
    /// 立轴破碎机原始数据;
    /// </summary>
    [DataContract]
    [Serializable][ProtoContract(SkipConstructor = true)]
    public class VerticalCrusher : BaseModel
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [ProtoMember(12)]
        public int MotorId { get; set; }
        [ProtoMember(13)]
        public bool IsDeleted { get; set; }
        #region Original Param
        /// <summary>
        /// 振动
        /// </summary>
        [DataMember]
        [DisplayName("振动")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(1)]
        public double Oscillation { get; set; }
        /// <summary>
        /// 电流
        /// </summary>
        [DisplayName("主机电机电流1")]
        ////[MotorConfig(IsAlarmProperty = true)]
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
        /// 功率因子
        /// </summary>
        [DataMember]
        [DisplayName("功率因子")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(4)]
        public double PowerFactor { get; set; }
        /// <summary>
        /// 无功功率
        /// </summary>
        [DataMember]
        [DisplayName("无功功率")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(5)]
        public double ReactivePower { get; set; }
        /// <summary>
        /// 总功率
        /// </summary>
        [DataMember]
        [DisplayName("总功率")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(6)]
        public double TotalPower { get; set; }
        #endregion

        [DataMember]
        [DisplayName("主机电机电流2")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(7)]
        public double Current2 { get; set; }

        [DataMember]
        [DisplayName("润滑油压力")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(8)]
        public double LubricatingOilPressure { get; set; }

        [DataMember]
        [DisplayName("回油温度")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(9)]
        public double OilReturnTempreature { get; set; }

        [DataMember]
        [DisplayName("油箱温度")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(10)]
        public double TankTemperature { get; set; }

        [DataMember]
        [DisplayName("轴承温度")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(11)]
        public double BearingTempreature { get; set; }
        //[ProtoMember(12)]
        //public DateTimeOffset Time { get; set; }
        //[ProtoMember(13)]
        //public int MotorId { get; set; }
    }
}
