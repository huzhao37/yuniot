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
    public partial class Dataconfig : AggregateRoot
    {
  
        [DataMember]
        [ProtoMember(1)]
        public int Datatype_Id { get; set; }
        [DataMember]
        [ProtoMember(2)]
        public int Count { get; set; }
        [DataMember]
        [ProtoMember(3)]
        public string Collectdevice_Index { get; set; }
    }
}
