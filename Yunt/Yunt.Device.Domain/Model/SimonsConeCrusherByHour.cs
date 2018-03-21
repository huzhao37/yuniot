using System;
using System.Collections.Generic;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.Model
{
    public partial class SimonsConeCrusherByHour : AggregateRoot
    {
        public double AverageTankTemperature { get; set; }
        public double AverageOilFeedTempreature { get; set; }
        public double AverageOilReturnTempreature { get; set; }
        public double AverageCurrent { get; set; }
        public double RunningTime { get; set; }
        public double LoadStall { get; set; }
        public string MotorId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
