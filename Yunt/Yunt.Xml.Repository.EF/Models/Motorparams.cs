using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Yunt.Xml.Repository.EF.Models
{
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public partial class Motorparams : BaseModel
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
    }
}
