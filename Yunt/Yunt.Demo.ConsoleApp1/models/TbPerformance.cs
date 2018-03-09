using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1
{
    public partial class TbPerformance
    {
        public int Id { get; set; }
        public int Nodeid { get; set; }
        public int Taskid { get; set; }
        public double Cpu { get; set; }
        public double Memory { get; set; }
        public double Installdirsize { get; set; }
        public DateTime Lastupdatetime { get; set; }
    }
}
