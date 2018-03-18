using System;
using System.Collections.Generic;
using System.Text;

namespace Yunt.Device.Domain.BaseModel
{
    /// <summary>
    /// 聚合根的抽象实现类，定义聚合根的公共属性和行为
    /// </summary>
    public abstract class AggregateRoot : IAggregateRoot
    {
        public long Id { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
