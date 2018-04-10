using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;
using Yunt.Device.Domain.BaseModel;

namespace Yunt.Device.Domain.Model
{   /// <summary>
    /// ProductionLine
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public class ProductionLine : AggregateRoot
    {
        /// <summary>
        /// 产线Id
        /// </summary>
        [DataMember]
        [DisplayName("产线Id")]
        [ProtoMember(1)]
        public string ProductionLineId { get; set; }
        /// <summary>
        /// 产线名称
        /// </summary>
        [DataMember]
        [DisplayName("产线名称")]
        [ProtoMember(2)]
        public string Name { get; set; }

    }
}
