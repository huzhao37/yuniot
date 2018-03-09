using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1
{
    public partial class TbError
    {
        public int Id { get; set; }
        public string Msg { get; set; }
        public byte Errortype { get; set; }
        public DateTime Errorcreatetime { get; set; }
        public int Taskid { get; set; }
        public int Nodeid { get; set; }
    }
}
