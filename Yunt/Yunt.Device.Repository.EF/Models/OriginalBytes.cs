using System;
using System.Runtime.Serialization;
using ProtoBuf;

namespace Yunt.Device.Repository.EF.Models
{
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public  class OriginalBytes : BaseModel
    {   
        [ProtoMember(1)]
        public string Bytes { get; set; }
        [ProtoMember(2)]
        public string ProductionLineId { get; set; }
        [ProtoMember(3)]
        public int EmbeddedDeviceId { get; set; }
    }
}
