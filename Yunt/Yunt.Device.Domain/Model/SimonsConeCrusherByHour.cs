using System;
using System.Collections.Generic;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.Model
{
    public partial class SimonsConeCrusherByHour : AggregateRoot
    {
        public float AverageTankTemperature { get; set; }
        public float AverageOilFeedTempreature { get; set; }
        public float AverageOilReturnTempreature { get; set; }
        public float AverageCurrent { get; set; }
        public float RunningTime { get; set; }
        public float LoadStall { get; set; }
        public string MotorId { get; set; }
  
    }
}
