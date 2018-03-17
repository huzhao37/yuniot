using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.conecrusher2
{
    public partial class ConeCrusher
    {
        public int Id { get; set; }
        public double Voltage { get; set; }
        public double PowerFactor { get; set; }
        public double ReactivePower { get; set; }
        public double TotalPower { get; set; }
        public double SpindleTravel { get; set; }
        public double MovaStress { get; set; }
        public double TankTemperature { get; set; }
        public double OilFeedTempreature { get; set; }
        public double OilReturnTempreature { get; set; }
        public int MotorId { get; set; }
        public double Current { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Time { get; set; }
    }
}
