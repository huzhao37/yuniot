using System;
using System.Collections.Generic;

namespace Yunt.Auth.Repository.EF.Models
{
    public partial class TbUser : BaseModel
    {
        public string Userstaffno { get; set; }
        public string Username { get; set; }
        public byte Userrole { get; set; }
        public DateTime Usercreatetime { get; set; }
        public string Usertel { get; set; }
        public string Useremail { get; set; }
    }
}
