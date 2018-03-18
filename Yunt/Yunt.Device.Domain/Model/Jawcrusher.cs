using System;
using System.Collections.Generic;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.Model
{
    public partial class Jawcrusher : AggregateRoot
    {
        public bool IsDeleted { get; set; }
        public string MotorId { get; set; }
        public float? Voltage { get; set; }
        public float? PowerFactor { get; set; }
        public float? ReactivePower { get; set; }
        public float? TotalPower { get; set; }
        public float? RackSpindleTemperature1 { get; set; }
        public float? RackSpindleTemperature2 { get; set; }
        public float? MotiveSpindleTemperature1 { get; set; }
        public float? MotiveSpindleTemperature2 { get; set; }
        public float? Current { get; set; }
    }
}
