using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.models
{
    public partial class TbConfig
    {
        public int Id { get; set; }
        public string Configkey { get; set; }
        public string Configvalue { get; set; }
        public string Remark { get; set; }
        public DateTime Lastupdatetime { get; set; }
        public bool Istest { get; set; }
    }
}
