﻿using System;
using System.Collections.Generic;

namespace Yunt.TaskManager.Model
{
    public partial class TbLog : BaseModel
    {
        public string Msg { get; set; }
        public byte Logtype { get; set; }
        public DateTime Logcreatetime { get; set; }
        public int Taskid { get; set; }
        public int Nodeid { get; set; }
    }
}
