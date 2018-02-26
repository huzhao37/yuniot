using System;
using System.Collections.Generic;

namespace Yunt.Demo.ConsoleApp1.models
{
    public partial class TbNode
    {
        public int Id { get; set; }
        public string Nodename { get; set; }
        public DateTime Nodecreatetime { get; set; }
        public string Nodeip { get; set; }
        public DateTime Nodelastupdatetime { get; set; }
        public bool Ifcheckstate { get; set; }
    }
}
