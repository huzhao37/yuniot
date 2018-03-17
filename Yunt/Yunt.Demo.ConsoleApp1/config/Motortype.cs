using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.config
{
    public partial class Motortype
    {
        public int MotorTypeId { get; set; }
        public long? Time { get; set; }
        public string MotorTypeSerialnum { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
