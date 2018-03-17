using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.jawcrusher2
{
    public partial class JawCrusher
    {
        public int Id { get; set; }
        public double Voltage { get; set; }
        public double PowerFactor { get; set; }
        public double ReactivePower { get; set; }
        public double TotalPower { get; set; }
        public double RackSpindleTemperature1 { get; set; }
        public double RackSpindleTemperature2 { get; set; }
        public double MotiveSpindleTemperature1 { get; set; }
        public double MotiveSpindleTemperature2 { get; set; }
        public int MotorId { get; set; }
        public double Current { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Time { get; set; }
    }
}
