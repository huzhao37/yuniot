using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.pulverizer2
{
    public partial class PulverizerByHour
    {
        public int Id { get; set; }
        public double AverageCurrent { get; set; }
        public double AverageFanCurrent { get; set; }
        public double AverageGraderCurrent { get; set; }
        public double AverageGraderRotateSpeed { get; set; }
        public double RunningTime { get; set; }
        public double LoadStall { get; set; }
        public int MotorId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Time { get; set; }
    }
}
