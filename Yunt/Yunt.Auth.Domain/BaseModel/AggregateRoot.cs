using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;
using Yunt.Auth.Domain.Model;

namespace Yunt.Auth.Domain.BaseModel
{
    /// <summary>
    /// 聚合根的抽象实现类，定义聚合根的公共属性和行为
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [ProtoInclude(11, typeof(UserRole))]
    [ProtoInclude(12, typeof(User))]
    public abstract class AggregateRoot : IAggregateRoot
    {
        [DataMember]
        [DisplayName("序号")]
        [ProtoMember(1)]
        public int Id { get; set; }
        [DataMember]
        [DisplayName("时间")]
        [ProtoMember(2)]
        public long Time { get; set; }
    }
}
