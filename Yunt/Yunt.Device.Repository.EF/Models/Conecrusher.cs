using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Runtime.Serialization;
using ProtoBuf;



namespace Yunt.Device.Repository.EF.Models
{
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public  class ConeCrusher:BaseModel
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [ProtoMember(1)]
        public int MotorId { get; set; }
        [ProtoMember(12)]
        public bool IsDeleted { get; set; }

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
        /// 主轴行程;
        /// </summary>
        [DataMember]
        [DisplayName("主轴行程")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(6)]
        public double SpindleTravel { get; set; }
        /// <summary>
        /// 圆锥压力;
        /// </summary>
        [DataMember]
        [DisplayName("圆锥压力")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(7)]
        public double MovaStress { get; set; }
        /// <summary>
        /// 油箱温度;
        /// </summary>
        [DataMember]
        [DisplayName("油箱温度")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(8)]
        public double TankTemperature { get; set; }
        /// <summary>
        /// 供油温度;
        /// </summary>
        [DataMember]
        [DisplayName("供油温度")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(9)]
        public double OilFeedTempreature { get; set; }
        /// <summary>
        /// 回油温度;
        /// </summary>
        [DataMember]
        [DisplayName("回油温度")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(10)]
        public double OilReturnTempreature { get; set; }
        /// <summary>
        /// 电流;
        /// </summary>
        [DisplayName("电流")]
        [ProtoMember(11)]
        //[MotorConfig(IsAlarmProperty = true)]
        public double Current { get; set; }
      
    }
}
