﻿using System;
using System.Collections.Generic;

namespace Yunt.Auth.Repository.EF.Models
{
    public partial class TbError : BaseModel
    {
        public string Msg { get; set; }
        public byte Errortype { get; set; }
        public DateTime Errorcreatetime { get; set; }
        public int Taskid { get; set; }
        public int Nodeid { get; set; }
    }
}
