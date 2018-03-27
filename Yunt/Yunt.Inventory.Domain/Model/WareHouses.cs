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
    /// 仓库
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public class WareHouses : AggregateRoot
    {

        /// <summary>
        /// 仓库编号
        /// </summary> 
        [DataMember]
        [DisplayName("仓库编号")]
        [ProtoMember(1)]
        public string WareHousesId { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary> 
        [DataMember]
        [DisplayName("仓库名称")]
        [ProtoMember(2)]
        public string Name { get; set; }

        /// <summary>
        /// 仓库管理员
        /// </summary> 
        [DataMember]
        [DisplayName("仓库管理员")]
        [ProtoMember(3)]
        public string Keeper { get; set; }

        /// <summary>
        /// 仓库设备类型
        /// </summary> 
        [DataMember]
        [DisplayName("仓库设备类型")]
        [ProtoMember(4)]
        public string MotorTypeId { get; set; }
        /// <summary>
        /// 备注
        /// </summary> 
        [DataMember]
        [DisplayName("备注")]
        [ProtoMember(5)]
        public string Remark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary> 
        [DataMember]
        [DisplayName("创建时间")]
        [ProtoMember(6)]
        public long CreateTime { get; set; }
    }
}
