using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.config
{
    public partial class Dvr
    {
        public long DvrId { get; set; }
        public string AccessToken { get; set; }
        public long? ExpireTime { get; set; }
        public string ProductionLineSerialnum { get; set; }
        public string Time { get; set; }
        public string DvrSerialnum { get; set; }
    }
}
