using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.config
{
    public partial class Productionline
    {
        public long ProductionLineId { get; set; }
        public long? BuildTime { get; set; }
        public long? Time { get; set; }
        public string Name { get; set; }
        public string ProductionLineSerialnum { get; set; }
        public float? Output { get; set; }
        public string Location { get; set; }
        public string PropertyRightPerson { get; set; }
        public string ResponsiblePerson { get; set; }
        public string ResponsiblePersonContact { get; set; }
        public float? Longitude { get; set; }
        public float? Latitude { get; set; }
        public string OreType { get; set; }
        public long? ExpiredTime { get; set; }
        public string Production { get; set; }
        public long? LatestDataTime { get; set; }
    }
}
