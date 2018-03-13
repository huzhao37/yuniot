using System;
using System.Collections.Generic;

namespace Yunt.Auth.Repository.EF.Models
{
    public partial class TbPerformance : BaseModel
    {
        public int Nodeid { get; set; }
        public int Taskid { get; set; }
        public double Cpu { get; set; }
        public double Memory { get; set; }
        public double Installdirsize { get; set; }
        public DateTime Lastupdatetime { get; set; }
    }
}
