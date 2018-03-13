using System;
using System.Collections.Generic;
using Yunt.Auth.Domain.BaseModel;

namespace Yunt.Auth.Domain.Model
{
    public partial class TbCategory:AggregateRoot
    {
      
        public string Categoryname { get; set; }
        public DateTime Categorycreatetime { get; set; }
    }
}
