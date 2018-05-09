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
    public partial class Physicfeature : AggregateRoot
    {
        [DataMember]
        [ProtoMember(1)]
        public string PhysicType { get; set; }
        [DataMember]
        [ProtoMember(2)]
        public DateTime? Time { get; set; }
    }
}
