using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Yunt.Xml.Repository.EF.Models
{
    public partial class Motortype : BaseModel
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
