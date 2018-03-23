using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using ProtoBuf;

namespace Yunt.Device.Repository.EF.Models 
{
     /// <summary>
   /// CY
   /// </summary>
   [DataContract]
   [Serializable]
   [ProtoContract(SkipConstructor = true)]
 public class ConveyorByDay:BaseModel
   {
      /// <summary>
      /// 功率因素
      /// </summary>
      [DataMember]
      [DisplayName("功率因素")]
      [ProtoMember(1)]
      public float AvgPowerFactor{get;set;}
      /// <summary>
      /// 每秒脉冲数
      /// </summary>
      [DataMember]
      [DisplayName("每秒脉冲数")]
      [ProtoMember(2)]
      public float AvgPulsesSecond{get;set;}
      /// <summary>
      /// 输入毫安数4-20mA
      /// </summary>
      [DataMember]
      [DisplayName("输入毫安数4-20mA")]
      [ProtoMember(3)]
      public float AvgMS420mA{get;set;}
      /// <summary>
      /// 重力传感器毫伏数
      /// </summary>
      [DataMember]
      [DisplayName("重力传感器毫伏数")]
      [ProtoMember(4)]
      public float AvgGravitySensorMill{get;set;}
      /// <summary>
      /// 重量单位
      /// </summary>
      [DataMember]
      [DisplayName("重量单位")]
      [ProtoMember(5)]
      public float AvgWeightUnit{get;set;}
      /// <summary>
      /// 瞬时产量
      /// </summary>
      [DataMember]
      [DisplayName("瞬时产量")]
      [ProtoMember(6)]
      public float AvgInstantWeight{get;set;}
      /// <summary>
      /// 累计产量
      /// </summary>
      [DataMember]
      [DisplayName("累计产量")]
      [ProtoMember(7)]
      public float AccumulativeWeight{get;set;}
      /// <summary>
      /// C相电流
      /// </summary>
      [DataMember]
      [DisplayName("C相电流")]
      [ProtoMember(8)]
      public float AvgCurrent_C{get;set;}
      /// <summary>
      /// B相电流
      /// </summary>
      [DataMember]
      [DisplayName("B相电流")]
      [ProtoMember(9)]
      public float AvgCurrent_B{get;set;}
      /// <summary>
      /// A相电流
      /// </summary>
      [DataMember]
      [DisplayName("A相电流")]
      [ProtoMember(10)]
      public float AvgCurrent_A{get;set;}
      /// <summary>
      /// C相电压
      /// </summary>
      [DataMember]
      [DisplayName("C相电压")]
      [ProtoMember(11)]
      public float AvgVoltage_C{get;set;}
      /// <summary>
      /// B相电压
      /// </summary>
      [DataMember]
      [DisplayName("B相电压")]
      [ProtoMember(12)]
      public float AvgVoltage_B{get;set;}
      /// <summary>
      /// A相电压
      /// </summary>
      [DataMember]
      [DisplayName("A相电压")]
      [ProtoMember(13)]
      public float AvgVoltage_A{get;set;}
      /// <summary>
      /// 有功电能
      /// </summary>
      [DataMember]
      [DisplayName("有功电能")]
      [ProtoMember(14)]
      public float ActivePower{get;set;}
     /// <summary>
     /// 电机设备编号
     /// </summary>
     [ProtoMember(15)]
      public string MotorId { get; set; }
     /// <summary>
     /// 开机时间
     /// </summary>
     [ProtoMember(17)]
      public float RunningTime { get; set; }
     /// <summary>
     /// 负荷
     /// </summary>
     [ProtoMember(18)]
      public float LoadStall { get; set; }

   }
}
