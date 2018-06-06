using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Yunt.Xml.Repository.EF.Models
{
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public partial class Collectdevice : BaseModel
    {
        [DataMember]
        [ProtoMember(1)]
        public string Index { get; set; }
        [DataMember]
        [ProtoMember(2)]
        public string Productionline_Id { get; set; }
        [DataMember]
        [ProtoMember(3)]
        public DateTime Time { get; set; }
        [DataMember]
        [ProtoMember(4)]
        public string Remark { get; set; }
    }
}
