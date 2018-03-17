using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.config
{
    public partial class Cabinetparamter
    {
        public long CabinetParamterId { get; set; }
        public long? Time { get; set; }
        public string Param { get; set; }
        public string Description { get; set; }
        public int? CabinetParamterType { get; set; }
        public string CabinetParamterSerialnum { get; set; }
    }
}
