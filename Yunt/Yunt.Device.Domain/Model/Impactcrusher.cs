using System;
using System.Collections.Generic;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.Model
{
    public partial class ImpactCrusher : AggregateRoot
    {
        public bool IsDeleted { get; set; }
        public string MotorId { get; set; }
        public float Current { get; set; }
        public float Current2 { get; set; }
        public float SpindleTemperature1 { get; set; }
        public float SpindleTemperature2 { get; set; }
    }
}
