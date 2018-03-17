using System;
using System.Collections.Generic;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.Model
{
    public partial class Conveyor : AggregateRoot
    {
        public long MotorId { get; set; }
        public float? Voltage { get; set; }
        public float? PowerFactor { get; set; }
        public float? ReactivePower { get; set; }
        public float? TotalPower { get; set; }
        public float? InstantWeight { get; set; }
        public float? AccumulativeWeight { get; set; }
        public float? Velocity { get; set; }
        public float? Frequency { get; set; }
        public int? Unit { get; set; }
        public sbyte? BootFlagBit { get; set; }
        public sbyte? ZeroCalibration { get; set; }
        public float? Current { get; set; }
        public bool IsDeleted { get; set; }
    }
}
