﻿using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.models
{
    public partial class TbVersion
    {
        public int Id { get; set; }
        public int Taskid { get; set; }
        public int Version { get; set; }
        public DateTime Versioncreatetime { get; set; }
        public byte[] Zipfile { get; set; }
        public string Zipfilename { get; set; }
    }
}
