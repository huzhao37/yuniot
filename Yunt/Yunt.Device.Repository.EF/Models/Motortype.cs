using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using ProtoBuf;


namespace Yunt.Device.Repository.EF.Models
{
    public partial class MotorType : BaseModel
    {
        [DataMember]
        [ProtoMember(9)]
        public string MotorTypeId { get; set; }       
        [ProtoMember(1)]
        [DisplayName("中文名")]
        public string Name { get; set; }
        [DataMember]
        [ProtoMember(2)]
        public string Code { get; set; }
        [ProtoMember(3)]
        [DisplayName("英文名")]
        public string Description { get; set; }
        [DataMember]
        [ProtoMember(4)]
        public string MaintenancePeriod { get; set; }
        [DataMember]
        [ProtoMember(5)]
        public double Capacity { get; set; }
        [DataMember]
        [ProtoMember(6)]
        public double FeedSize { get; set; }
        [DataMember]
        [ProtoMember(7)]
        public double MotorPower { get; set; }
        [DataMember]
        [ProtoMember(8)]
        public string ImageUrl { get; set; }
    }
}
