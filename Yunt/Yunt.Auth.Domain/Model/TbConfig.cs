using System;
using System.Collections.Generic;
using Yunt.Auth.Domain.BaseModel;

namespace Yunt.Auth.Domain.Model
{
    public partial class TbConfig : AggregateRoot
    {
        public string Configkey { get; set; }
        public string Configvalue { get; set; }
        public string Remark { get; set; }
        public DateTime Lastupdatetime { get; set; }
        public bool Istest { get; set; }
    }
}
