using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.config
{
    public partial class Alarmtype
    {
        public long AlarmTypeId { get; set; }
        public long? Time { get; set; }
        public string Description { get; set; }
        public string Maintenance { get; set; }
        public string Param { get; set; }
        public string AlarmTypeSerialnum { get; set; }
    }
}
