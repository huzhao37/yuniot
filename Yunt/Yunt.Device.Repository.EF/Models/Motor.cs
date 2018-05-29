using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ProtoBuf;

namespace Yunt.Device.Repository.EF.Models
{
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public partial class Motor:BaseModel
    { 
        /// <summary>
       /// 设备ID
       /// </summary>
        [ProtoMember(1)]
        public string MotorId { get; set; }
      
        [ProtoMember(2)]
        public string ProductSpecification { get; set; }
        [ProtoMember(3)]
        public string ProductionLineId { get; set; }
        [ProtoMember(4)]
        public string Name { get; set; }
        [ProtoMember(5)]
        public string SerialNumber { get; set; }   
        [ProtoMember(6)]
        public string MotorTypeId { get; set; }
        [ProtoMember(7)]
        public float StandValue { get; set; }
        [ProtoMember(8)]
        public float FeedSize { get; set; }
        [ProtoMember(9)]
        public float MotorPower { get; set; }    
        [ProtoMember(10)]
        public float FinalSize { get; set; }    

        [ProtoMember(11)]
        public int EmbeddedDeviceId { get; set; }
        [ProtoMember(12)]
        public bool IsBeltWeight { get; set; }
        [ProtoMember(13)]
        public bool IsMainBeltWeight { get; set; }
        [ProtoMember(14)]
        public float Slope { get; set; }
        [ProtoMember(15)]
        public float OffSet { get; set; }
        [ProtoMember(16)]
        public bool UseCalc { get; set; }

    }
}
