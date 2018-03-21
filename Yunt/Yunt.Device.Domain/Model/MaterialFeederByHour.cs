using System;
using System.Collections.Generic;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.Model
{
    public partial class MaterialFeederByHour:AggregateRoot
    {
        public double AverageFrequency { get; set; }
        public double AverageCurrent { get; set; }
        public double AverageVoltage { get; set; }
        public double AverageVelocity { get; set; }
        public double RunningTime { get; set; }
        public double LoadStall { get; set; }
        public string MotorId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
