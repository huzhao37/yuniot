using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;
using Yunt.Inventory.Domain.BaseModel;

namespace Yunt.Inventory.Domain.Model
{
    /// <summary>
    /// 库存报警记录
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public  class InventoryAlarmInfo:AggregateRoot
    {

        /// <summary>
        /// 备件编号
        /// </summary> 
        [DataMember]
        [DisplayName("备件编号")]
        [ProtoMember(1)]
        public string SparePartsId { get; set; }

        /// <summary>
        /// 仓库余量
        /// </summary> 
        [DataMember]
        [DisplayName("仓库余量")]
        [ProtoMember(2)]
        public int InventoryBalance { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [DataMember]
        [DisplayName("创建日期")]
        [ProtoMember(3)]
        public long CreateTime { get; set; }

    }
}
