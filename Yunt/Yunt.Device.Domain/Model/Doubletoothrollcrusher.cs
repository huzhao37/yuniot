﻿using System;
using System.Collections.Generic;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.Model
{
    public partial class DoubleToothRollCrusher : AggregateRoot
    {
  
        public string MotorId { get; set; }
        public float Current2 { get; set; }
        public float Current { get; set; }
    }
}
