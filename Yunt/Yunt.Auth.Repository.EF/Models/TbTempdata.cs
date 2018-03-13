using System;
using System.Collections.Generic;

namespace Yunt.Auth.Repository.EF.Models
{
    public partial class TbTempdata : BaseModel
    {
        public int Taskid { get; set; }
        public string Tempdatajson { get; set; }
        public DateTime Tempdatalastupdatetime { get; set; }
    }
}
