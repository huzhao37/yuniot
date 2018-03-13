using System;
using System.Collections.Generic;
using Yunt.Auth.Domain.BaseModel;

namespace Yunt.Auth.Domain.Model
{
    public partial class TbTempdata : AggregateRoot
    {
        public int Taskid { get; set; }
        public string Tempdatajson { get; set; }
        public DateTime Tempdatalastupdatetime { get; set; }
    }
}
