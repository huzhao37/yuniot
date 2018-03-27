﻿using System;
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
        public string FinalSize { get; set; }    

        [ProtoMember(11)]
        public int EmbeddedDeviceId { get; set; }


    }
}
