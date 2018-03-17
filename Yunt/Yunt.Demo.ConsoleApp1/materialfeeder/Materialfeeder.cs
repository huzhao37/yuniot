using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.materialfeeder
{
    public partial class Materialfeeder
    {
        public long MaterialFeederId { get; set; }
        public float? Frequency { get; set; }
        public float? Current { get; set; }
        public float? Voltage { get; set; }
        public float? Velocity { get; set; }
        public float? PowerFactor { get; set; }
        public float? ReactivePower { get; set; }
        public float? TotalPower { get; set; }
        public float? InFrequency { get; set; }
        public long? Time { get; set; }
        public long? MotorId { get; set; }
    }
}
