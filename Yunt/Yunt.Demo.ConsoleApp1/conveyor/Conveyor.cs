using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.conveyor
{
    public partial class Conveyor
    {
        public long ConveyorId { get; set; }
        public float? Voltage { get; set; }
        public float? PowerFactor { get; set; }
        public float? ReactivePower { get; set; }
        public float? TotalPower { get; set; }
        public float? InstantWeight { get; set; }
        public float? AccumulativeWeight { get; set; }
        public float? Velocity { get; set; }
        public float? Frequency { get; set; }
        public int? Unit { get; set; }
        public sbyte? BootFlagBit { get; set; }
        public sbyte? ZeroCalibration { get; set; }
        public float? Current { get; set; }
        public long? MotorId { get; set; }
        public long? Time { get; set; }
    }
}
