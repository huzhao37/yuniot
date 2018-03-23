using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using ProtoBuf;

namespace Yunt.Device.Repository.EF.Models 
{
     /// <summary>
   /// CC
   /// </summary>
   [DataContract]
   [Serializable]
   [ProtoContract(SkipConstructor = true)]
 public class ConeCrusher:BaseModel
   {
      /// <summary>
      /// 磨损2
      /// </summary>
      [DataMember]
      [DisplayName("磨损2")]
      [ProtoMember(1)]
      public float WearValue2{get;set;}
      /// <summary>
      /// 磨损1
      /// </summary>
      [DataMember]
      [DisplayName("磨损1")]
      [ProtoMember(2)]
      public float WearValue1{get;set;}
      /// <summary>
      /// 绝对主轴行程
      /// </summary>
      [DataMember]
      [DisplayName("绝对主轴行程")]
      [ProtoMember(3)]
      public float AbsSpindleTravel{get;set;}
      /// <summary>
      /// 实时主轴行程
      /// </summary>
      [DataMember]
      [DisplayName("实时主轴行程")]
      [ProtoMember(4)]
      public float SpindleTravel{get;set;}
      /// <summary>
      /// 实时动锥压力
      /// </summary>
      [DataMember]
      [DisplayName("实时动锥压力")]
      [ProtoMember(5)]
      public float MovaStress{get;set;}
      /// <summary>
      /// 实时回油温度
      /// </summary>
      [DataMember]
      [DisplayName("实时回油温度")]
      [ProtoMember(6)]
      public float OilReturnTempreatur{get;set;}
      /// <summary>
      /// 实时供油温度
      /// </summary>
      [DataMember]
      [DisplayName("实时供油温度")]
      [ProtoMember(7)]
      public float OilFeedTempreature{get;set;}
      /// <summary>
      /// 实时油箱温度
      /// </summary>
      [DataMember]
      [DisplayName("实时油箱温度")]
      [ProtoMember(8)]
      public float TankTemperature{get;set;}
      /// <summary>
      /// 振动2
      /// </summary>
      [DataMember]
      [DisplayName("振动2")]
      [ProtoMember(9)]
      public float Vibrate2{get;set;}
      /// <summary>
      /// 振动1
      /// </summary>
      [DataMember]
      [DisplayName("振动1")]
      [ProtoMember(10)]
      public float Vibrate1{get;set;}
      /// <summary>
      /// 功率因素
      /// </summary>
      [DataMember]
      [DisplayName("功率因素")]
      [ProtoMember(11)]
      public float PowerFactor{get;set;}
      /// <summary>
      /// C相电流
      /// </summary>
      [DataMember]
      [DisplayName("C相电流")]
      [ProtoMember(12)]
      public float Current_C{get;set;}
      /// <summary>
      /// B相电流
      /// </summary>
      [DataMember]
      [DisplayName("B相电流")]
      [ProtoMember(13)]
      public float Current_B{get;set;}
      /// <summary>
      /// A相电流
      /// </summary>
      [DataMember]
      [DisplayName("A相电流")]
      [ProtoMember(14)]
      public float Current_A{get;set;}
      /// <summary>
      /// C相电压
      /// </summary>
      [DataMember]
      [DisplayName("C相电压")]
      [ProtoMember(15)]
      public float Voltage_C{get;set;}
      /// <summary>
      /// B相电压
      /// </summary>
      [DataMember]
      [DisplayName("B相电压")]
      [ProtoMember(16)]
      public float Voltage_B{get;set;}
      /// <summary>
      /// A相电压
      /// </summary>
      [DataMember]
      [DisplayName("A相电压")]
      [ProtoMember(17)]
      public float Voltage_A{get;set;}
      /// <summary>
      /// 有功电能
      /// </summary>
      [DataMember]
      [DisplayName("有功电能")]
      [ProtoMember(18)]
      public float ActivePower{get;set;}
     /// <summary>
     /// 电机设备编号
     /// </summary>
     [ProtoMember(19)]
      public string MotorId { get; set; }

   }
}
