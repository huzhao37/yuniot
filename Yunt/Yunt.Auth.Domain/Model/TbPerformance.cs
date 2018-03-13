using System;
using System.Collections.Generic;
using Yunt.Auth.Domain.BaseModel;

namespace Yunt.Auth.Domain.Model
{
    public partial class TbPerformance : AggregateRoot
    {
        public int Nodeid { get; set; }
        public int Taskid { get; set; }
        public double Cpu { get; set; }
        public double Memory { get; set; }
        public double Installdirsize { get; set; }
        public DateTime Lastupdatetime { get; set; }
    }
}
