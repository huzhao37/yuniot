using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using ProtoBuf;


namespace Yunt.Device.Repository.EF.Models
{
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public partial class MotorType : BaseModel
    {
        [DataMember]
        [ProtoMember(1)]
        public string MotorTypeId { get; set; }       
        [ProtoMember(2)]
        [DisplayName("中文名")]
        public string Name { get; set; }
       
    }
}
