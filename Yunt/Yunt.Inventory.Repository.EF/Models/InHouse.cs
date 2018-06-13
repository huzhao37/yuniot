using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using ProtoBuf;
using Yunt.Common;

namespace Yunt.Inventory.Repository.EF.Models
{
    /// <summary>
    /// 入库
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public partial class InHouse : BaseModel
    {
        /// <summary>
        /// 备件编号
        /// </summary> 
        [DataMember]
        [DisplayName("批次")]
        [ProtoMember(1)]
        public string BatchNo { get; set; }     

        /// <summary>
        /// 入库时间
        /// </summary>
        [DataMember]
        [DisplayName("入库时间")]
        [ProtoMember(2)]
        public long InTime { get; set; }     

        /// <summary>
        /// 入库
        /// </summary>
        [DataMember]
        [DisplayName("入库操作员")]
        [ProtoMember(3)]
        public string InOperator { get; set; }     

        /// <summary>
        /// 备件类型编号
        /// </summary>
        [DataMember]
        [DisplayName("备件类型编号")]
        [ProtoMember(4)]
        public int SparePartsTypeId { get; set; }     
        /// <summary>
        /// 出厂信息
        /// </summary>
        [DataMember]
        [DisplayName("出厂信息")]
        [ProtoMember(5)]
        public string FactoryInfo { get; set; }
        /// <summary>
        /// 备件说明
        /// </summary>
        [DataMember]
        [DisplayName("备件说明")]
        [ProtoMember(11)]
        public string Description { get; set; }

        /// <summary>
        /// 仓库编号
        /// </summary>
        [DataMember]
        [DisplayName("仓库编号")]
        [ProtoMember(6)]
        public int WareHousesId { get; set; }
   
        /// <summary>
        /// 是否删除
        /// </summary>
        [DataMember]
        [DisplayName("是否删除")]
        [ProtoMember(7)]
        public bool IsDelete { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        [DisplayName("数量")]
        [ProtoMember(8)]
        public int Count { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        [DataMember]
        [DisplayName("单价")]
        [ProtoMember(9)]
        public float UnitPrice { get; set; }
     
    }
}
