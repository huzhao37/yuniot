using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;
using Yunt.Device.Domain.Model;

namespace Yunt.Device.Repository.EF.Models
{
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
    [ProtoInclude(45, typeof(Motortype))]
    [ProtoInclude(46, typeof(Motor))]

    public class BaseModel
    {
        [DataMember]
        [ProtoMember(47)]
        public long Id { get; set; }
        [DataMember]
        [ProtoMember(48)]
        public DateTimeOffset Time { get; set; }
    }
}
