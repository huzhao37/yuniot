using System;
using System.Collections.Generic;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.Model
{
    public partial class Motor : AggregateRoot
    {
        public float StandValue { get; set; }
        public string ProductionLineId { get; set; }
        public string MotorId { get; set; }
        public string Name { get; set; }
        public string TypeNo { get; set; }
        public DateTimeOffset BuildTime { get; set; }
        public DateTimeOffset LatestMaintainTime { get; set; }
        public string MotorTypeId { get; set; }
        public float Capicity { get; set; }
        public float FeedSize { get; set; }
        public float MotorPower { get; set; }
        public string ImageUrl { get; set; }
        public sbyte? IsBeltWeight { get; set; }
        public sbyte? IsDisplay { get; set; }
        public sbyte? IsMainBeltWeight { get; set; }
        public string ControlDeviceId { get; set; }
        public sbyte? IsOutConveyor { get; set; }
        public string EmbeddedDeviceSerialnum { get; set; }
        public string MotorSerialnum { get; set; }
        public bool IsDeleted { get; set; }
        public string SerialNumber { get; set; }
        public DateTimeOffset LatestDataTime { get; set; }
 
        public int Status { get; set; }
        public string Image { get; set; }

        public string FinalSize { get; set; }
        public int EmbeddedDeviceId { get; set; }
    }
}
