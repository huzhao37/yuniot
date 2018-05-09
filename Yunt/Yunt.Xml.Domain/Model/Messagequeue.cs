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
    public partial class Messagequeue : AggregateRoot
    {
        [DataMember]
        [ProtoMember(1)]
        public string Host { get; set; }
        [DataMember]
        [ProtoMember(2)]
        public int Port { get; set; }
        [DataMember]
        [ProtoMember(3)]
        public string RouteKey { get; set; }
        [DataMember]
        [ProtoMember(4)]
        public string Name { get; set; }
        [DataMember]
        [ProtoMember(5)]
        public int Timer { get; set; }
        [DataMember]
        [ProtoMember(6)]
        public string CollectdeviceIndex { get; set; }
        [DataMember]
        [ProtoMember(7)]
        public int WriteRead { get; set; }
        [DataMember]
        [ProtoMember(8)]
        public string ComType { get; set; }
        [DataMember]
        [ProtoMember(9)]
        public DateTime Time { get; set; }
        [DataMember]
        [ProtoMember(10)]
        public string Remark { get; set; }
        [DataMember]
        [ProtoMember(11)]
        public string Username { get; set; }
        [DataMember]
        [ProtoMember(12)]
        public string Pwd { get; set; }
    }
}
