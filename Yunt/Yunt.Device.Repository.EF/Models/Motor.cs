using System;
using System.Collections.Generic;
using ProtoBuf;

namespace Yunt.Device.Repository.EF.Models
{
    public partial class Motor:BaseModel
    { 
        /// <summary>
       /// 设备ID
       /// </summary>
        [ProtoMember(19)]
        public string MotorId { get; set; }
        [ProtoMember(20)]
        public bool IsDeleted { get; set; }

        [ProtoMember(21)]
        public string ProductionLineId { get; set; }
        [ProtoMember(1)]
        public string Name { get; set; }
        [ProtoMember(2)]
        public string SerialNumber { get; set; }
        [ProtoMember(3)]
        public DateTimeOffset BuildTime { get; set; }
        [ProtoMember(4)]
        public DateTimeOffset LatestMaintainTime { get; set; }
        [ProtoMember(5)]
        public DateTimeOffset LatestDataTime { get; set; }
        [ProtoMember(6)]
        public int Status { get; set; }
        [ProtoMember(8)]
        public string MotorTypeId { get; set; }
        [ProtoMember(9)]
        public double Capicity { get; set; }
        [ProtoMember(10)]
        public double FeedSize { get; set; }
        [ProtoMember(11)]
        public double MotorPower { get; set; }
        [ProtoMember(12)]
        public string Image { get; set; }
        [ProtoMember(13)]
        public string FinalSize { get; set; }
        [ProtoMember(14)]
        public bool IsBeltWeight { get; set; }
        [ProtoMember(15)]
        public bool IsDisplay { get; set; }
        [ProtoMember(16)]
        public bool IsMainBeltWeight { get; set; }
        [ProtoMember(17)]
        public bool IsOutConveyor { get; set; }
        [ProtoMember(18)]
        public int EmbeddedDeviceId { get; set; }


    }
}
