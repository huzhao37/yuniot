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
    public partial class Datatype : AggregateRoot
    {
        [DataMember]
        [ProtoMember(1)]
        public string Description { get; set; }
        [DataMember]
        [ProtoMember(2)]
        public int Bit { get; set; }
        [DataMember]
        [ProtoMember(3)]
        public int InByte { get; set; }
        [DataMember]
        [ProtoMember(4)]
        public int OutIntArray { get; set; }
    }
}
