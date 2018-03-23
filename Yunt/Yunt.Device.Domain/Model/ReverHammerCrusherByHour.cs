using System;
using System.Collections.Generic;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.Model
{
    public partial class ReverHammerCrusherByHour : AggregateRoot
    {
        public float Current { get; set; }
        public float SpindleTemperature1 { get; set; }
        public float SpindleTemperature2 { get; set; }
        public float BearingSpeed { get; set; }
        public float RunningTime { get; set; }
        public float LoadStall { get; set; }
        public string MotorId { get; set; }
  
    }
}
