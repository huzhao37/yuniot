using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Yunt.Xml.Domain.BaseModel;

namespace Yunt.Xml.Domain.Model
{
    public partial class Motortype : AggregateRoot
    {
        [DataMember]
        [ProtoMember(1)]
        public string MotorTypeName { get; set; }
        [DataMember]
        [ProtoMember(2)]
        public string MotorTypeId { get; set; }
        [DataMember]
        [ProtoMember(3)]
        public DateTime? Time { get; set; }
    }
}
