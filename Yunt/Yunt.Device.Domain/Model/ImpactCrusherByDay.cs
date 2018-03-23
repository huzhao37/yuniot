using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using ProtoBuf;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.Model 
{
     /// <summary>
   /// IC
   /// </summary>
   [DataContract]
   [Serializable]
   [ProtoContract(SkipConstructor = true)]
 public class ImpactCrusherByDay:AggregateRoot
   {
      /// <summary>
      /// 功率因素-电机2
      /// </summary>
      [DataMember]
      [DisplayName("功率因素-电机2")]
      [ProtoMember(1)]
      public float AvgMotor2PowerFactor{get;set;}
      /// <summary>
      /// C相电流-电机2
      /// </summary>
      [DataMember]
      [DisplayName("C相电流-电机2")]
      [ProtoMember(2)]
      public float AvgMotor2Current_C{get;set;}
      /// <summary>
      /// B相电流-电机2
      /// </summary>
      [DataMember]
      [DisplayName("B相电流-电机2")]
      [ProtoMember(3)]
      public float AvgMotor2Current_B{get;set;}
      /// <summary>
      /// A相电流-电机2
      /// </summary>
      [DataMember]
      [DisplayName("A相电流-电机2")]
      [ProtoMember(4)]
      public float AvgMotor2Current_A{get;set;}
      /// <summary>
      /// C相电压-电机2
      /// </summary>
      [DataMember]
      [DisplayName("C相电压-电机2")]
      [ProtoMember(5)]
      public float AvgMotor2Voltage_C{get;set;}
      /// <summary>
      /// B相电压-电机2
      /// </summary>
      [DataMember]
      [DisplayName("B相电压-电机2")]
      [ProtoMember(6)]
      public float AvgMotor2Voltage_B{get;set;}
      /// <summary>
      /// A相电压-电机2
      /// </summary>
      [DataMember]
      [DisplayName("A相电压-电机2")]
      [ProtoMember(7)]
      public float AvgMotor2Voltage_A{get;set;}
      /// <summary>
      /// 有功电能-电机2
      /// </summary>
      [DataMember]
      [DisplayName("有功电能-电机2")]
      [ProtoMember(8)]
      public float Motor2ActivePower{get;set;}
      /// <summary>
      /// 功率因素-电机1
      /// </summary>
      [DataMember]
      [DisplayName("功率因素-电机1")]
      [ProtoMember(9)]
      public float AvgMotor1PowerFactor{get;set;}
      /// <summary>
      /// C相电流-电机1
      /// </summary>
      [DataMember]
      [DisplayName("C相电流-电机1")]
      [ProtoMember(10)]
      public float AvgMotor1Current_C{get;set;}
      /// <summary>
      /// B相电流-电机1
      /// </summary>
      [DataMember]
      [DisplayName("B相电流-电机1")]
      [ProtoMember(11)]
      public float AvgMotor1Current_B{get;set;}
      /// <summary>
      /// A相电流-电机1
      /// </summary>
      [DataMember]
      [DisplayName("A相电流-电机1")]
      [ProtoMember(12)]
      public float AvgMotor1Current_A{get;set;}
      /// <summary>
      /// C相电压-电机1
      /// </summary>
      [DataMember]
      [DisplayName("C相电压-电机1")]
      [ProtoMember(13)]
      public float AvgMotor1Voltage_C{get;set;}
      /// <summary>
      /// B相电压-电机1
      /// </summary>
      [DataMember]
      [DisplayName("B相电压-电机1")]
      [ProtoMember(14)]
      public float AvgMotor1Voltage_B{get;set;}
      /// <summary>
      /// A相电压-电机1
      /// </summary>
      [DataMember]
      [DisplayName("A相电压-电机1")]
      [ProtoMember(15)]
      public float AvgMotor1Voltage_A{get;set;}
      /// <summary>
      /// 有功电能-电机1
      /// </summary>
      [DataMember]
      [DisplayName("有功电能-电机1")]
      [ProtoMember(16)]
      public float Motor1ActivePower{get;set;}
      /// <summary>
      /// 振动2
      /// </summary>
      [DataMember]
      [DisplayName("振动2")]
      [ProtoMember(17)]
      public float AvgVibrate2{get;set;}
      /// <summary>
      /// 振动1
      /// </summary>
      [DataMember]
      [DisplayName("振动1")]
      [ProtoMember(18)]
      public float AvgVibrate1{get;set;}
      /// <summary>
      /// 磨损2
      /// </summary>
      [DataMember]
      [DisplayName("磨损2")]
      [ProtoMember(19)]
      public float WearValue2{get;set;}
      /// <summary>
      /// 磨损1
      /// </summary>
      [DataMember]
      [DisplayName("磨损1")]
      [ProtoMember(20)]
      public float WearValue1{get;set;}
      /// <summary>
      /// 轴承温度2
      /// </summary>
      [DataMember]
      [DisplayName("轴承温度2")]
      [ProtoMember(21)]
      public float AvgSpindleTemperature2{get;set;}
      /// <summary>
      /// 轴承温度1
      /// </summary>
      [DataMember]
      [DisplayName("轴承温度1")]
      [ProtoMember(22)]
      public float AvgSpindleTemperature1{get;set;}
     /// <summary>
     /// 电机设备编号
     /// </summary>
     [ProtoMember(23)]
      public string MotorId { get; set; }
     /// <summary>
     /// 开机时间
     /// </summary>
     [ProtoMember(25)]
      public float RunningTime { get; set; }
     /// <summary>
     /// 负荷
     /// </summary>
     [ProtoMember(26)]
      public float LoadStall { get; set; }

   }
}
