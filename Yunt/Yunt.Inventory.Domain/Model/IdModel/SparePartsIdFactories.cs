using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;
using Yunt.Inventory.Domain.BaseModel;

namespace Yunt.Inventory.Domain.Model.IdModel
{
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public class SparePartsIdFactories : AggregateRoot
    {
        /// <summary>
        /// 备件类型Id
        /// </summary>

        [DataMember]
        [ProtoMember(1)]
        public string SparePartsTypeId { get; set; }
        /// <summary>
        /// 备件序号
        /// </summary>

        [DataMember]
        [ProtoMember(2)]
        public int SparePartsIndex { get; set; }
        /// <summary>
        /// 时间
        /// </summary>

        [DataMember]
        [ProtoMember(3)]
        public int Time { get; set; }
    }
}
