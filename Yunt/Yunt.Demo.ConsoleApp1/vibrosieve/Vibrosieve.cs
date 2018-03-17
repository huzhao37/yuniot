using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.vibrosieve
{
    public partial class Vibrosieve
    {
        public long VibrosieveId { get; set; }
        public float? Current { get; set; }
        public float? Voltage { get; set; }
        public float? PowerFactor { get; set; }
        public float? ReactivePower { get; set; }
        public float? TotalPower { get; set; }
        public long? Time { get; set; }
        public long? MotorId { get; set; }
    }
}
