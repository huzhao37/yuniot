using System;
using System.Collections.Generic;

namespace Yunt.Device.Domain.Model
{
    public partial class Motor
    {
        public long MotorId { get; set; }
        public string Name { get; set; }
        public string TypeNo { get; set; }
        public long? BuildTime { get; set; }
        public long? LatestMaintainTime { get; set; }
        public string MotorTypeSerialnum { get; set; }
        public float? Capicity { get; set; }
        public float? FeedSize { get; set; }
        public float? MotorPower { get; set; }
        public string ImageUrl { get; set; }
        public sbyte? IsBeltWeight { get; set; }
        public sbyte? IsDisplay { get; set; }
        public sbyte? IsMainBeltWeight { get; set; }
        public string ControlDeviceId { get; set; }
        public sbyte? IsOutConveyor { get; set; }
        public string EmbeddedDeviceSerialnum { get; set; }
        public string MotorSerialnum { get; set; }
    }
}
