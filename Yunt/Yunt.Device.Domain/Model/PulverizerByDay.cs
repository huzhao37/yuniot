using System;
using System.Collections.Generic;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.Model
{
    public partial class PulverizerByDay : AggregateRoot
    {
        public double AverageCurrent { get; set; }
        public double AverageFanCurrent { get; set; }
        public double AverageGraderCurrent { get; set; }
        public double AverageGraderRotateSpeed { get; set; }
        public double RunningTime { get; set; }
        public double LoadStall { get; set; }
        public int MotorId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
