using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Yunt.Xml.Domain.BaseModel;

namespace Yunt.Xml.Domain.Model
{
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public partial class Dataformmodel : AggregateRoot
    {
        [DataMember]
        [ProtoMember(1)]
        public string MachineName { get; set; }
        [DataMember]
        [ProtoMember(2)]
        public int? Index { get; set; }
        [DataMember]
        [ProtoMember(3)]
        public string FieldParam { get; set; }
        [DataMember]
        [ProtoMember(4)]
        public string FieldParamEn { get; set; }
        [DataMember]
        [ProtoMember(5)]
        public string MotorTypeName { get; set; }
        [DataMember]
        [ProtoMember(6)]
        public string Unit { get; set; }
        [DataMember]
        [ProtoMember(7)]
        public string DataType { get; set; }
        [DataMember]
        [ProtoMember(8)]
        public string DataPhysicalFeature { get; set; }
        [DataMember]
        [ProtoMember(9)]
        public string DataPhysicalAccuracy { get; set; }
        [DataMember]
        [ProtoMember(10)]
        public string MachineId { get; set; }
        [DataMember]
        [ProtoMember(11)]
        public string DeviceId { get; set; }
        [DataMember]
        [ProtoMember(12)]
        public DateTime? Time { get; set; }
        [DataMember]
        [ProtoMember(13)]
        public double? Value { get; set; }
        [DataMember]
        [ProtoMember(14)]
        public int? Divalue { get; set; }
        [DataMember]
        [ProtoMember(15)]
        public int? Dovalue { get; set; }
        [DataMember]
        [ProtoMember(16)]
        public int? DebugValue { get; set; }
        [DataMember]
        [ProtoMember(17)]
        public int? WarnValue { get; set; }
        [DataMember]
        [ProtoMember(18)]
        public int? Bit { get; set; }
        [DataMember]
        [ProtoMember(19)]
        public string BitDesc { get; set; }
        [DataMember]
        [ProtoMember(20)]
        public int? LineId { get; set; }
        [DataMember]
        [ProtoMember(21)]
        public string CollectdeviceIndex { get; set; }
        [DataMember]
        [ProtoMember(22)]
        public string MotorId { get; set; }
        [DataMember]
        [ProtoMember(23)]
        public int? DataPhysicalId { get; set; }
        [DataMember]
        [ProtoMember(24)]
        public int? FormatId { get; set; }
    }
}
