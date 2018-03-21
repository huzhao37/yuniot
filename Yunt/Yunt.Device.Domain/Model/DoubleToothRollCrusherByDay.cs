using System;
using System.Collections.Generic;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.Model
{
    public partial class DoubleToothRollCrusherByDay:AggregateRoot
    {
        public double Current { get; set; }
        public double Current2 { get; set; }
        public double RunningTime { get; set; }
        public double LoadStall { get; set; }
        public string MotorId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
