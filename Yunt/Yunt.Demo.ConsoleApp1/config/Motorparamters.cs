using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.config
{
    public partial class Motorparamters
    {
        public long MotorParamterId { get; set; }
        public int? Index { get; set; }
        public string MotorSerialnum { get; set; }
        public string CabinetParamterSerialnum { get; set; }
        public string EmbeddedDeviceSerialnum { get; set; }
        public string AlarmTypeSerialnum { get; set; }
        public float? DataAccruacy { get; set; }
        public string MotorParamterSerialnum { get; set; }
        public long? Time { get; set; }
    }
}
