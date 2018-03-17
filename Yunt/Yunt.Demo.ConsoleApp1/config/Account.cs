using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.config
{
    public partial class Account
    {
        public long AccountId { get; set; }
        public long? Time { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public string Company { get; set; }
        public int? AccountRoleId { get; set; }
        public string ProductionLineList { get; set; }
    }
}
