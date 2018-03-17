using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.materialfeeder2
{
    public partial class MaterialFeederByWorkShift
    {
        public int Id { get; set; }
        public double AverageFrequency { get; set; }
        public double AverageCurrent { get; set; }
        public double AverageVoltage { get; set; }
        public double AverageVelocity { get; set; }
        public double RunningTime { get; set; }
        public double LoadStall { get; set; }
        public int WorkShiftId { get; set; }
        public int MotorId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Time { get; set; }
    }
}
