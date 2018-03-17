using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.verticalcrusher2
{
    public partial class VerticalCrusher
    {
        public int Id { get; set; }
        public double Oscillation { get; set; }
        public double Voltage { get; set; }
        public double PowerFactor { get; set; }
        public double ReactivePower { get; set; }
        public double TotalPower { get; set; }
        public double Current2 { get; set; }
        public double LubricatingOilPressure { get; set; }
        public double OilReturnTempreature { get; set; }
        public double TankTemperature { get; set; }
        public double BearingTempreature { get; set; }
        public int MotorId { get; set; }
        public double Current { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Time { get; set; }
    }
}
