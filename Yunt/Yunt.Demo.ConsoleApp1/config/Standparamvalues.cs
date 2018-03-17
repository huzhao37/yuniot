using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.config
{
    public partial class Standparamvalues
    {
        public long StandParamValueId { get; set; }
        public string Parameter { get; set; }
        public float? Value { get; set; }
        public string Description { get; set; }
        public string MotorTypeSerialnum { get; set; }
        public string MotorSerialnum { get; set; }
        public string StandParamValueSerialnum { get; set; }
        public long? Time { get; set; }
    }
}
