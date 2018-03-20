﻿using System;
using System.Collections.Generic;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.Model
{
    public partial class Vibrosieve : AggregateRoot
    {
        public bool IsDeleted { get; set; }
        public string MotorId { get; set; }
        public float? Current { get; set; }
        public float? Voltage { get; set; }
        public float? PowerFactor { get; set; }
        public float? ReactivePower { get; set; }
        public float? TotalPower { get; set; }
    }
}