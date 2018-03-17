using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.verticalcrusher2
{
    public partial class VerticalCrusherByWorkShift
    {
        public int Id { get; set; }
        public double AverageOscillation { get; set; }
        public double AverageCurrent { get; set; }
        public double AverageVoltage { get; set; }
        public double AveragePowerFactor { get; set; }
        public double AverageReactivePower { get; set; }
        public double AverageTotalPower { get; set; }
        public double AverageCurrent2 { get; set; }
        public double AverageOilReturnTempreature { get; set; }
        public double AverageTankTemperature { get; set; }
        public double AverageBearingTempreature { get; set; }
        public double RunningTime { get; set; }
        public double LoadStall { get; set; }
        public int WorkShiftId { get; set; }
        public int MotorId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Time { get; set; }
    }
}
