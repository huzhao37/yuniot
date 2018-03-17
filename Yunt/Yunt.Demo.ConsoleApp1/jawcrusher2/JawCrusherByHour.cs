using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.jawcrusher2
{
    public partial class JawCrusherByHour
    {
        public int Id { get; set; }
        public double AverageCurrent { get; set; }
        public double AverageVoltage { get; set; }
        public double AveragePowerFactor { get; set; }
        public double AverageReactivePower { get; set; }
        public double AverageTotalPower { get; set; }
        public double AverageRackSpindleTemperature1 { get; set; }
        public double AverageRackSpindleTemperature2 { get; set; }
        public double AverageMotiveSpindleTemperature1 { get; set; }
        public double AverageMotiveSpindleTemperature2 { get; set; }
        public double RunningTime { get; set; }
        public double LoadStall { get; set; }
        public int MotorId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Time { get; set; }
    }
}
