using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.impactcrusher
{
    public partial class Impactcrusher
    {
        public long ImpactCrusherId { get; set; }
        public long? MotorId { get; set; }
        public long? Time { get; set; }
        public float? Current { get; set; }
        public float? Current2 { get; set; }
        public float? SpindleTemperature1 { get; set; }
        public float? SpindleTemperature2 { get; set; }
    }
}
