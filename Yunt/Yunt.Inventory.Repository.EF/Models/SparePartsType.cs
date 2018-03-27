using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;

namespace Yunt.Inventory.Repository.EF.Models
{
    /// <summary>
    /// 备件类型
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public class SparePartsType : BaseModel
    {
        /// <summary>
        /// 备件类型ID
        /// </summary>
        [DataMember]
        [DisplayName("备件类型ID")]
        [ProtoMember(1)]
        public string SparePartsTypeId { get; set; }
        /// <summary>
        /// 备件类型名称
        /// </summary>
        [DataMember]
        [DisplayName("备件类型名称")]
        [ProtoMember(2)]
        public string Name { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [DataMember]
        [DisplayName("创建日期")]
        [ProtoMember(3)]
        public long  CreateTime { get; set; }

        /// <summary>
        /// 库存报警界限值（低于多少开始报警，不包含）
        /// </summary>
        [DataMember]
        [DisplayName("库存报警界限值")]
        [ProtoMember(4)]
        public int InventoryAlarmLimits { get; set; }
    }
}
