using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.Model
{
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public class MotorParams : AggregateRoot
    {
        [DataMember]
        [DisplayName("英文参数")]
        [ProtoMember(1)]
        public string Param { get; set; }
        [DataMember]
        [DisplayName("中文描述")]
        [ProtoMember(2)]
        public string Description { get; set; }
        [DataMember]
        [DisplayName("电机设备类型")]
        [ProtoMember(3)]
        public string MotorTypeId { get; set; }
    }
}
