using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.cone
{
    public partial class Conecrusher
    {
        public long ConeCrusherId { get; set; }
        public float? Voltage { get; set; }
        public float? PowerFactor { get; set; }
        public float? ReactivePower { get; set; }
        public float? TotalPower { get; set; }
        public float? SpindleTravel { get; set; }
        public float? MovaStress { get; set; }
        public float? TankTemperature { get; set; }
        public float? OilFeedTempreature { get; set; }
        public float? OilReturnTempreature { get; set; }
        public long? MotorId { get; set; }
        public float? Current { get; set; }
        public long? Time { get; set; }
    }
}
