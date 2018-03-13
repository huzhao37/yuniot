using System;
using System.Collections.Generic;
using System.Text;

namespace Yunt.Auth.Domain.BaseModel
{
    /// <summary>
    /// 聚合根的抽象实现类，定义聚合根的公共属性和行为
    /// </summary>
    public abstract class AggregateRoot : IAggregateRoot
    {
        public int Id { get; set; }
    }
}
