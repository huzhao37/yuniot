using System;
using System.Collections.Generic;

namespace Yunt.Auth.Repository.EF.Models
{
    public partial class TbCommand : BaseModel
    {
        public string Command { get; set; }
        public string Commandname { get; set; }
        public byte Commandstate { get; set; }
        public int Taskid { get; set; }
        public int Nodeid { get; set; }
        public DateTime Commandcreatetime { get; set; }
    }
}
