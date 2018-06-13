using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yunt.WebApi.Models.ProductionLines
{
    public class SeriesData
    {
        public long UnixTime { get; set; }

        public float RunningTime { get; set; }

        public float Output { get; set; }
        public float Current { get; set; }

        public float Frequency { get; set; }
        public float AvgMotiveSpindleTemperature1 { get; set; }
        public float AvgMotiveSpindleTemperature2 { get; set; }
        public float AvgRackSpindleTemperature1 { get; set; }

        public float AvgRackSpindleTemperature2 { get; set; }
        public float AvgVibrate1 { get; set; }
        public float AvgVibrate2 { get; set; }
        public float WearValue1 { get; set; }
        public float WearValue2 { get; set; }
        public float AvgSpindleTemperature4 { get; set; }
        public float AvgSpindleTemperature2 { get; set; }
        public float AvgSpindleTemperature3 { get; set; }

        public float AvgSpindleTemperature1 { get; set; }
        public float AvgOilReturnStress { get; set; }
        public float AvgOilFeedStress { get; set; }

        public float AvgMovaStress { get; set; }
        public float AvgOilReturnTempreatur { get; set; }
        public float AvgOilFeedTempreature { get; set; }
        public float AvgTankTemperature { get; set; }

        public float AvgSpindleTravel { get; set; }

        public float AvgMotor2Voltage_B { get; set; }
        public float AvgMotor1Voltage_B { get; set; }
    }
}
