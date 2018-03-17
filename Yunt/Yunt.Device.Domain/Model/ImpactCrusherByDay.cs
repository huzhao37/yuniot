using System;
using System.Collections.Generic;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.Model
{
    public partial class ImpactCrusherByDay:AggregateRoot
    {
        public double AverageSpindleTemperature1 { get; set; }
        public double AverageSpindleTemperature2 { get; set; }
        public double AverageCurrent { get; set; }
        public double AverageCurrent2 { get; set; }
        public double RunningTime { get; set; }
        public double LoadStall { get; set; }
        public int OnOffCounts { get; set; }
        public int MotorId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
