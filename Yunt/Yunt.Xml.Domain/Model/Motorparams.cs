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
    public partial class Motorparams : AggregateRoot
    {
        [DataMember]
        [ProtoMember(1)]
        public string Param { get; set; }
        [DataMember]
        [ProtoMember(2)]
        public string Description { get; set; }
        [DataMember]
        [ProtoMember(3)]
        public string MotorTypeId { get; set; }
        [DataMember]
        [ProtoMember(4)]
        public DateTime? Time { get; set; }

        [DataMember]
        [ProtoMember(5)]
        public int PhysicId { get; set; }
    }
}
