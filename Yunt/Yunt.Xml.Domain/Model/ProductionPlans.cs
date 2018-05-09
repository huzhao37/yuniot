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
    public partial class ProductionPlans : AggregateRoot
    {
        [DataMember]
        [ProtoMember(1)]
        public int Start { get; set; }
        [DataMember]
        [ProtoMember(2)]
        public int End { get; set; }
        [DataMember]
        [ProtoMember(3)]
        public int MainCy { get; set; }
        [DataMember]
        [ProtoMember(4)]
        public int FinishCy1 { get; set; }
        [DataMember]
        [ProtoMember(5)]
        public int FinishCy2 { get; set; }
        [DataMember]
        [ProtoMember(6)]
        public int FinishCy3 { get; set; }
        [DataMember]
        [ProtoMember(7)]
        public int FinishCy4 { get; set; }
        [DataMember]
        [ProtoMember(8)]
        public DateTime Time { get; set; }
        [DataMember]
        [ProtoMember(9)]
        public string Remark { get; set; }
        [DataMember]
        [ProtoMember(10)]
        public string ProductionlineId { get; set; }
    }
}
