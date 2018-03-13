using System;
using System.Collections.Generic;
using Yunt.Auth.Domain.BaseModel;

namespace Yunt.Auth.Domain.Model
{
    public partial class TbNode : AggregateRoot
    {
        public string Nodename { get; set; }
        public DateTime Nodecreatetime { get; set; }
        public string Nodeip { get; set; }
        public DateTime Nodelastupdatetime { get; set; }
        public bool Ifcheckstate { get; set; }
    }
}
