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
    public partial class Collectdevice : AggregateRoot
    {
        [DataMember]
        [ProtoMember(1)]
        public string Index { get; set; }
        [DataMember]
        [ProtoMember(2)]
        public string ProductionlineId { get; set; }
        [DataMember]
        [ProtoMember(3)]
        public DateTime Time { get; set; }
        [DataMember]
        [ProtoMember(4)]
        public string Remark { get; set; }
    }
}
