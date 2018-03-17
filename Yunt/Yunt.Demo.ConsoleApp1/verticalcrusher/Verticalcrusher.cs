using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.verticalcrusher
{
    public partial class Verticalcrusher
    {
        public long VerticalCrusherId { get; set; }
        public float? Oscillation { get; set; }
        public float? Voltage { get; set; }
        public float? PowerFactor { get; set; }
        public float? Current { get; set; }
        public float? ReactivePower { get; set; }
        public float? TotalPower { get; set; }
        public float? Current2 { get; set; }
        public float? LubricatingOilPressure { get; set; }
        public float? OilReturnTempreature { get; set; }
        public float? TankTemperature { get; set; }
        public float? BearingTempreature { get; set; }
        public long? Time { get; set; }
        public long? MotorId { get; set; }
    }
}
