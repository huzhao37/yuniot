using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.pulverizer2
{
    public partial class Pulverizer
    {
        public int Id { get; set; }
        public double FanCurrent { get; set; }
        public double GraderCurrent { get; set; }
        public double GraderRotateSpeed { get; set; }
        public int MotorId { get; set; }
        public double Current { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Time { get; set; }
    }
}
