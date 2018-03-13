using System;
using System.Collections.Generic;
using Yunt.Auth.Domain.BaseModel;

namespace Yunt.Auth.Domain.Model
{
    public partial class TbLog : AggregateRoot
    {
        public string Msg { get; set; }
        public byte Logtype { get; set; }
        public DateTime Logcreatetime { get; set; }
        public int Taskid { get; set; }
        public int Nodeid { get; set; }
    }
}
