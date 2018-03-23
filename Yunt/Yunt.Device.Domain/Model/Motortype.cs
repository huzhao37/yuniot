using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ProtoBuf;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.Model
{
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public partial class MotorType:AggregateRoot
    {
        [ProtoMember(1)]
        [DataMember]
        public string MotorTypeId { get; set; }
        [ProtoMember(2)]
        [DataMember]
        public string Name { get; set; }
    }
}
