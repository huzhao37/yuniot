using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.impactcrusher2
{
    public partial class ImpactCrusherByWorkShift
    {
        public int Id { get; set; }
        public double AverageSpindleTemperature1 { get; set; }
        public double AverageSpindleTemperature2 { get; set; }
        public double AverageCurrent { get; set; }
        public double AverageCurrent2 { get; set; }
        public double RunningTime { get; set; }
        public double LoadStall { get; set; }
        public int OnOffCounts { get; set; }
        public int WorkShiftId { get; set; }
        public int MotorId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Time { get; set; }
    }
}
