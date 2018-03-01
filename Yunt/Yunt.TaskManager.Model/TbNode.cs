using System;
using System.Collections.Generic;

namespace Yunt.TaskManager.Model
{
    public partial class TbNode : BaseModel
    {
        public string Nodename { get; set; }
        public DateTime Nodecreatetime { get; set; }
        public string Nodeip { get; set; }
        public DateTime Nodelastupdatetime { get; set; }
        public bool Ifcheckstate { get; set; }
    }
}
