using System;
using System.Collections.Generic;

namespace Yunt.TaskManager.Model
{
    public partial class TbConfig : BaseModel
    {
        public string Configkey { get; set; }
        public string Configvalue { get; set; }
        public string Remark { get; set; }
        public DateTime Lastupdatetime { get; set; }
        public bool Istest { get; set; }
    }
}
