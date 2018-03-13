using System;
using System.Collections.Generic;
using Yunt.Auth.Domain.BaseModel;

namespace Yunt.Auth.Domain.Model
{
    public partial class TbVersion : AggregateRoot
    {
        public int Taskid { get; set; }
        public int Version { get; set; }
        public DateTime Versioncreatetime { get; set; }
        public byte[] Zipfile { get; set; }
        public string Zipfilename { get; set; }
    }
}
