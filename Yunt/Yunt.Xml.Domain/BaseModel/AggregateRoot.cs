using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;
using Yunt.Xml.Domain.Model;

namespace Yunt.Xml.Domain.BaseModel
{
    /// <summary>
    /// 聚合根的抽象实现类，定义聚合根的公共属性和行为
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [ProtoInclude(11, typeof(Collectdevice))]
    [ProtoInclude(12, typeof(Dataconfig))]
    [ProtoInclude(13, typeof(Dataformmodel))]
    [ProtoInclude(14, typeof(Datatype))]
    [ProtoInclude(15, typeof(Messagequeue))]
    [ProtoInclude(16, typeof(Motorparams))]
    [ProtoInclude(17, typeof(Motortype))]
    [ProtoInclude(18, typeof(Physicfeature))]
    [ProtoInclude(19, typeof(ProductionPlans))]

    public abstract class AggregateRoot : IAggregateRoot
    {
        [DataMember]
        [ProtoMember(20)]
        public int Id { get; set; }
    }
}
