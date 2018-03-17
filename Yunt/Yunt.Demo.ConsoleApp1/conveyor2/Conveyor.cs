using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.conveyor2
{
    public partial class Conveyor
    {
        public int Id { get; set; }
        public double Voltage { get; set; }
        public double PowerFactor { get; set; }
        public double ReactivePower { get; set; }
        public double TotalPower { get; set; }
        public double InstantWeight { get; set; }
        public double AccumulativeWeight { get; set; }
        public double Velocity { get; set; }
        public double Frequency { get; set; }
        public int Unit { get; set; }
        public int BootFlagBit { get; set; }
        public int ZeroCalibration { get; set; }
        public int MotorId { get; set; }
        public double Current { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Time { get; set; }
    }
}
