using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.simonsconecrusher2
{
    public partial class SimonsConeCrusher
    {
        public int Id { get; set; }
        public double TankTemperature { get; set; }
        public double OilFeedTempreature { get; set; }
        public double OilReturnTempreature { get; set; }
        public int MotorId { get; set; }
        public double Current { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Time { get; set; }
    }
}
