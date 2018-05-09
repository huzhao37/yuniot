using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;

namespace Yunt.Xml.Repository.EF.Models
{
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

    public abstract class BaseModel
    {
        [DataMember]
        [ProtoMember(20)]
        public int Id { get; set; }
    }
}
