using System;
using System.Collections.Generic;

namespace Yunt.TaskManager.Model
{
    public partial class TbCategory:BaseModel
    {
      
        public string Categoryname { get; set; }
        public DateTime Categorycreatetime { get; set; }
    }
}
