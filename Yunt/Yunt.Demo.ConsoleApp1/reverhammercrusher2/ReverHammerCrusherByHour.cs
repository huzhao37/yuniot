using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.reverhammercrusher2
{
    public partial class ReverHammerCrusherByHour
    {
        public int Id { get; set; }
        public double Current { get; set; }
        public double SpindleTemperature1 { get; set; }
        public double SpindleTemperature2 { get; set; }
        public double BearingSpeed { get; set; }
        public double RunningTime { get; set; }
        public double LoadStall { get; set; }
        public int MotorId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Time { get; set; }
    }
}
