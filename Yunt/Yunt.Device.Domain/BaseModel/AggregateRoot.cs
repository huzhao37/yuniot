using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;
using Yunt.Device.Domain.Model;

namespace Yunt.Device.Domain.BaseModel
{
    /// <summary>
    /// 聚合根的抽象实现类，定义聚合根的公共属性和行为
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [ProtoInclude(11, typeof(ConeCrusher))]
    [ProtoInclude(12, typeof(Conveyor))]
    [ProtoInclude(13, typeof(DoubleToothRollCrusher))]
    [ProtoInclude(14, typeof(ImpactCrusher))]
    [ProtoInclude(15, typeof(JawCrusher))]
    [ProtoInclude(16, typeof(MaterialFeeder))]
    [ProtoInclude(18, typeof(Pulverizer))]
    [ProtoInclude(19, typeof(ReverHammerCrusher))]
    [ProtoInclude(20, typeof(SimonsConeCrusher))]
    [ProtoInclude(21, typeof(VerticalCrusher))]
    [ProtoInclude(22, typeof(Vibrosieve))]

    [ProtoInclude(23, typeof(ConeCrusherByDay))]
    [ProtoInclude(24, typeof(ConeCrusherByHour))]
    [ProtoInclude(25, typeof(ConveyorByDay))]
    [ProtoInclude(26, typeof(ConveyorByHour))]
    [ProtoInclude(27, typeof(DoubleToothRollCrusherByDay))]
    [ProtoInclude(28, typeof(DoubleToothRollCrusherByHour))]
    [ProtoInclude(29, typeof(ImpactCrusherByDay))]
    [ProtoInclude(30, typeof(ImpactCrusherByHour))]
    [ProtoInclude(31, typeof(JawCrusherByDay))]
    [ProtoInclude(32, typeof(JawCrusherByHour))]
    [ProtoInclude(33, typeof(MaterialFeederByDay))]
    [ProtoInclude(34, typeof(MaterialFeederByHour))]
    [ProtoInclude(35, typeof(PulverizerByDay))]
    [ProtoInclude(36, typeof(PulverizerByHour))]
    [ProtoInclude(37, typeof(ReverHammerCrusherByDay))]
    [ProtoInclude(38, typeof(ReverHammerCrusherByHour))]


    [ProtoInclude(39, typeof(SimonsConeCrusherByDay))]
    [ProtoInclude(40, typeof(SimonsConeCrusherByHour))]
    [ProtoInclude(41, typeof(VerticalCrusherByDay))]
    [ProtoInclude(42, typeof(VerticalCrusherByHour))]
    [ProtoInclude(43, typeof(VibrosieveByDay))]
    [ProtoInclude(44, typeof(VibrosieveByHour))]
    [ProtoInclude(45, typeof(MotorType))]
    [ProtoInclude(46, typeof(Motor))]
    [ProtoInclude(47, typeof(ProductionLine))]
    [ProtoInclude(48, typeof(OriginalBytes))]

    [ProtoInclude(49, typeof(HVib))]
    [ProtoInclude(50, typeof(HVibByHour))]
    [ProtoInclude(51, typeof(HVibByDay))]
    public abstract class AggregateRoot : IAggregateRoot
    {
        [DataMember]
        [ProtoMember(52)]
        public long Id { get; set; }
        [DataMember]
        [ProtoMember(53)]
        public long Time { get; set; }
    }
}
