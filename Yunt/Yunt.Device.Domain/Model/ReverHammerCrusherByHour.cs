using System;
using System.Collections.Generic;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.Model
{
    public partial class ReverHammerCrusherByHour : AggregateRoot
    {
        public double Current { get; set; }
        public double SpindleTemperature1 { get; set; }
        public double SpindleTemperature2 { get; set; }
        public double BearingSpeed { get; set; }
        public double RunningTime { get; set; }
        public double LoadStall { get; set; }
        public int MotorId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
