using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;

namespace Yunt.Auth.Repository.EF.Models
{
    /// <summary>
    /// 聚合根的抽象实现类，定义聚合根的公共属性和行为
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [ProtoInclude(11, typeof(UserRole))]
    [ProtoInclude(12, typeof(User))]
    public abstract class BaseModel
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
