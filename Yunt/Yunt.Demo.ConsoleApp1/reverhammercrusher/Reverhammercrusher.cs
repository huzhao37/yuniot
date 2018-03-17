using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.reverhammercrusher
{
    public partial class Reverhammercrusher
    {
        public long ReverHammerCrusherId { get; set; }
        public float? Current { get; set; }
        public float? SpindleTemperature1 { get; set; }
        public float? SpindleTemperature2 { get; set; }
        public float? BearingSpeed { get; set; }
        public long? Time { get; set; }
        public long? MotorId { get; set; }
    }
}
