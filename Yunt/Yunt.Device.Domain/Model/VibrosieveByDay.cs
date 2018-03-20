﻿using System;
using System.Collections.Generic;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.Model
{
    public partial class VibrosieveByDay : AggregateRoot
    {
        public double AverageCurrent { get; set; }
        public double AverageVoltage { get; set; }
        public double AveragePowerFactor { get; set; }
        public double AverageReactivePower { get; set; }
        public double AverageTotalPower { get; set; }
        public double RunningTime { get; set; }
        public double LoadStall { get; set; }
        public int MotorId { get; set; }
        public bool IsDeleted { get; set; }
    }
}