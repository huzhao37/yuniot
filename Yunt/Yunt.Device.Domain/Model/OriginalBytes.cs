using System;
using System.Runtime.Serialization;
using ProtoBuf;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.Model
{
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public  class OriginalBytes : AggregateRoot
    {   
        [ProtoMember(1)]
        public string Bytes { get; set; }
        [ProtoMember(2)]
        public string ProductionLineId { get; set; }
        [ProtoMember(3)]
        public int EmbeddedDeviceId { get; set; }
    }
}
