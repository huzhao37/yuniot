using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.doubletoothrollcrusher2
{
    public partial class DoubleToothRollCrusherByHour
    {
        public int Id { get; set; }
        public double Current { get; set; }
        public double Current2 { get; set; }
        public double RunningTime { get; set; }
        public double LoadStall { get; set; }
        public int MotorId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Time { get; set; }
    }
}
