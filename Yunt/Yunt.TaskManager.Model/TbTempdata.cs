using System;
using System.Collections.Generic;

namespace Yunt.TaskManager.Model
{
    public partial class TbTempdata : BaseModel
    {
        public int Taskid { get; set; }
        public string Tempdatajson { get; set; }
        public DateTime Tempdatalastupdatetime { get; set; }
    }
}
