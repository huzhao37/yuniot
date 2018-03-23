using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using ProtoBuf;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.Model 
{
     /// <summary>
   /// HVB
   /// </summary>
   [DataContract]
   [Serializable]
   [ProtoContract(SkipConstructor = true)]
 public class HVib:AggregateRoot
   {
      /// <summary>
      /// 回油压力
      /// </summary>
      [DataMember]
      [DisplayName("回油压力")]
      [ProtoMember(1)]
      public float OilReturnStress{get;set;}
      /// <summary>
      /// 进油压力
      /// </summary>
      [DataMember]
      [DisplayName("进油压力")]
      [ProtoMember(2)]
      public float OilFeedStress{get;set;}
      /// <summary>
      /// 功率因素
      /// </summary>
      [DataMember]
      [DisplayName("功率因素")]
      [ProtoMember(3)]
      public float PowerFactor{get;set;}
      /// <summary>
      /// 轴承温度4
      /// </summary>
      [DataMember]
      [DisplayName("轴承温度4")]
      [ProtoMember(4)]
      public float SpindleTemperature4{get;set;}
      /// <summary>
      /// 轴承温度3
      /// </summary>
      [DataMember]
      [DisplayName("轴承温度3")]
      [ProtoMember(5)]
      public float SpindleTemperature3{get;set;}
      /// <summary>
      /// 轴承温度2
      /// </summary>
      [DataMember]
      [DisplayName("轴承温度2")]
      [ProtoMember(6)]
      public float SpindleTemperature2{get;set;}
      /// <summary>
      /// 轴承温度1
      /// </summary>
      [DataMember]
      [DisplayName("轴承温度1")]
      [ProtoMember(7)]
      public float SpindleTemperature1{get;set;}
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
