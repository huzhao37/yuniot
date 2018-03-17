using System;
using System.Collections.Generic;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.Model
{
    public partial class ConeCrusherByHour:AggregateRoot
    {
        public double AverageCurrent { get; set; }
        public double AverageSpindleTravel { get; set; }
        public double AverageMovaStress { get; set; }
        public double AverageTankTemperature { get; set; }
        public double AverageOilFeedTempreature { get; set; }
        public double AverageOilReturnTempreature { get; set; }
        public double RunningTime { get; set; }
        public double LoadStall { get; set; }
        public int MotorId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
