using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1
{
    public partial class TbLog
    {
        public int Id { get; set; }
        public string Msg { get; set; }
        public byte Logtype { get; set; }
        public DateTime Logcreatetime { get; set; }
        public int Taskid { get; set; }
        public int Nodeid { get; set; }
    }
}
