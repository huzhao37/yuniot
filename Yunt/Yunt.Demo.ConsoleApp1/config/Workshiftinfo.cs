using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.config
{
    public partial class Workshiftinfo
    {
        public long WorkShiftInfoId { get; set; }
        public string ProductionLineSerialnum { get; set; }
        public int? Index { get; set; }
        public long? CreateTime { get; set; }
        public long? AccountId { get; set; }
        public int? StartHour { get; set; }
        public int? EndHour { get; set; }
        public string WorkShiftInfoSerialnum { get; set; }
    }
}
