using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.materialfeeder2
{
    public partial class MaterialFeeder
    {
        public int Id { get; set; }
        public double Frequency { get; set; }
        public double Voltage { get; set; }
        public double Velocity { get; set; }
        public double PowerFactor { get; set; }
        public double ReactivePower { get; set; }
        public double TotalPower { get; set; }
        public double InFrequency { get; set; }
        public int MotorId { get; set; }
        public double Current { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Time { get; set; }
    }
}
