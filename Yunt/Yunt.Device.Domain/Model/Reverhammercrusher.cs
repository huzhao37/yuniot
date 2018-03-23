using System;
using System.Collections.Generic;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.Model
{
    public partial class ReverHammerCrusher : AggregateRoot
    {
  
        public string MotorId { get; set; }
        public float Current { get; set; }
        public float SpindleTemperature1 { get; set; }
        public float SpindleTemperature2 { get; set; }
        public float BearingSpeed { get; set; }
    }
}
