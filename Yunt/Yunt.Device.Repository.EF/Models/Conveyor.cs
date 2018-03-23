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
 public class Conveyor:BaseModel
   {
      /// <summary>
      /// 功率因素
      /// </summary>
      [DataMember]
      [DisplayName("功率因素")]
      [ProtoMember(1)]
      public float PowerFactor{get;set;}
      /// <summary>
      /// 每秒脉冲数
      /// </summary>
      [DataMember]
      [DisplayName("每秒脉冲数")]
      [ProtoMember(2)]
      public float PulsesSecond{get;set;}
      /// <summary>
      /// 输入毫安数4-20mA
      /// </summary>
      [DataMember]
      [DisplayName("输入毫安数4-20mA")]
      [ProtoMember(3)]
      public float MS420mA{get;set;}
      /// <summary>
      /// 重力传感器毫伏数
      /// </summary>
      [DataMember]
      [DisplayName("重力传感器毫伏数")]
      [ProtoMember(4)]
      public float GravitySensorMill{get;set;}
      /// <summary>
      /// 重量单位
      /// </summary>
      [DataMember]
      [DisplayName("重量单位")]
      [ProtoMember(5)]
      public float WeightUnit{get;set;}
      /// <summary>
      /// 瞬时产量
      /// </summary>
      [DataMember]
      [DisplayName("瞬时产量")]
      [ProtoMember(6)]
      public float InstantWeight{get;set;}
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
      public float Current_C{get;set;}
      /// <summary>
      /// B相电流
      /// </summary>
      [DataMember]
      [DisplayName("B相电流")]
      [ProtoMember(9)]
      public float Current_B{get;set;}
      /// <summary>
      /// A相电流
      /// </summary>
      [DataMember]
      [DisplayName("A相电流")]
      [ProtoMember(10)]
      public float Current_A{get;set;}
      /// <summary>
      /// C相电压
      /// </summary>
      [DataMember]
      [DisplayName("C相电压")]
      [ProtoMember(11)]
      public float Voltage_C{get;set;}
      /// <summary>
      /// B相电压
      /// </summary>
      [DataMember]
      [DisplayName("B相电压")]
      [ProtoMember(12)]
      public float Voltage_B{get;set;}
      /// <summary>
      /// A相电压
      /// </summary>
      [DataMember]
      [DisplayName("A相电压")]
      [ProtoMember(13)]
      public float Voltage_A{get;set;}
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

   }
}
