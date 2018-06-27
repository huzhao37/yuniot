using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;

namespace Yunt.Analysis.Repository.EF.Models
{
    /// <summary>
    /// 聚合根的抽象实现类，定义聚合根的公共属性和行为
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [ProtoInclude(11, typeof(MotorEventLog))]
    [ProtoInclude(12, typeof(EventKind))]
    [ProtoInclude(13, typeof(AlarmInfo))]
    [ProtoInclude(14, typeof(Maintain))]
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
