using System;
using System.Collections.Generic;
using Yunt.Auth.Domain.BaseModel;

namespace Yunt.Auth.Domain.Model
{
    public partial class TbUser : AggregateRoot
    {
        public string Userstaffno { get; set; }
        public string Username { get; set; }
        public byte Userrole { get; set; }
        public DateTime Usercreatetime { get; set; }
        public string Usertel { get; set; }
        public string Useremail { get; set; }
    }
}
