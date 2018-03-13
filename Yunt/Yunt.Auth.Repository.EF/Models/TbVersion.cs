using System;
using System.Collections.Generic;

namespace Yunt.Auth.Repository.EF.Models
{
    public partial class TbVersion : BaseModel
    {
        public int Taskid { get; set; }
        public int Version { get; set; }
        public DateTime Versioncreatetime { get; set; }
        public byte[] Zipfile { get; set; }
        public string Zipfilename { get; set; }
    }
}
