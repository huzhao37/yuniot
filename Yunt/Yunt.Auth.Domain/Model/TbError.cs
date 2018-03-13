using System;
using System.Collections.Generic;
using Yunt.Auth.Domain.BaseModel;

namespace Yunt.Auth.Domain.Model
{
    public partial class TbError : AggregateRoot
    {
        public string Msg { get; set; }
        public byte Errortype { get; set; }
        public DateTime Errorcreatetime { get; set; }
        public int Taskid { get; set; }
        public int Nodeid { get; set; }
    }
}
