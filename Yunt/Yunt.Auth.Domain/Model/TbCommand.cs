using System;
using System.Collections.Generic;
using Yunt.Auth.Domain.BaseModel;

namespace Yunt.Auth.Domain.Model
{
    public partial class TbCommand : AggregateRoot
    {
        public string Command { get; set; }
        public string Commandname { get; set; }
        public byte Commandstate { get; set; }
        public int Taskid { get; set; }
        public int Nodeid { get; set; }
        public DateTime Commandcreatetime { get; set; }
    }
}
