using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Yunt.Xml.Repository.EF.Models
{
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public partial class Physicfeature : BaseModel
    {
        [DataMember]
        [ProtoMember(1)]
        public string PhysicType { get; set; }
        [DataMember]
        [ProtoMember(2)]
        public DateTime? Time { get; set; }
    }
}
