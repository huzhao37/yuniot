using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.jawcrusher
{
    public partial class Jawcrusher
    {
        public long JawCrusherId { get; set; }
        public float? Voltage { get; set; }
        public float? PowerFactor { get; set; }
        public float? ReactivePower { get; set; }
        public float? TotalPower { get; set; }
        public float? RackSpindleTemperature1 { get; set; }
        public float? RackSpindleTemperature2 { get; set; }
        public float? MotiveSpindleTemperature1 { get; set; }
        public float? MotiveSpindleTemperature2 { get; set; }
        public long? Time { get; set; }
        public float? Current { get; set; }
        public long? MotorId { get; set; }
    }
}
