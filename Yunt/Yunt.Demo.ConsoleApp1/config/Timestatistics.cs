using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.config
{
    public partial class Timestatistics
    {
        public long TimeStatisticId { get; set; }
        public long? LastTime { get; set; }
        public long? NextTime { get; set; }
    }
}
