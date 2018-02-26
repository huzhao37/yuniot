using System;
using System.Collections.Generic;

namespace Yunt.TaskManager.Model
{
    public partial class TbCommand
    {
        public int Id { get; set; }
        public string Command { get; set; }
        public string Commandname { get; set; }
        public byte Commandstate { get; set; }
        public int Taskid { get; set; }
        public int Nodeid { get; set; }
        public DateTime Commandcreatetime { get; set; }
    }
}
