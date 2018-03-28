using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using ProtoBuf;
using Yunt.Common;

namespace Yunt.Inventory.Repository.EF.Models
{
    /// <summary>
    /// 备件
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public partial class SpareParts:BaseModel
    {
        /// <summary>
        /// 备件编号
        /// </summary> 
        [DataMember]
        [DisplayName("备件编号")]
        [ProtoMember(1)]
        public string SparePartsId { get; set; }
        /// <summary>
        /// 备件名称
        /// </summary> 
        [DataMember]
        [DisplayName("备件名称")]
        [ProtoMember(2)]
        public string SparePartsName { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        [DataMember]
        [DisplayName("入库时间")]
        [ProtoMember(3)]
        public long InTime { get; set; }
        /// <summary>
        /// 出库时间
        /// </summary>
        [DataMember]
        [DisplayName("出库时间")]
        [ProtoMember(4)]
        public long OutTime { get; set; }

        /// <summary>
        /// 入库
        /// </summary>
        [DataMember]
        [DisplayName("入库操作员")]
        [ProtoMember(5)]
        public string InOperator { get; set; }

        /// <summary>
        /// 入库
        /// </summary>
        [DataMember]
        [DisplayName("出库操作员")]
        [ProtoMember(6)]
        public string OutOperator { get; set; }

        /// <summary>
        /// 报废时间
        /// </summary>
        [DataMember]
        [DisplayName("报废时间")]
        [ProtoMember(7)]
        public long UselessTime { get; set; }
        /// <summary>
        /// 备件类型编号
        /// </summary>
        [DataMember]
        [DisplayName("备件类型编号")]
        [ProtoMember(8)]
        public string SparePartsTypeId { get; set; }
        /// <summary>
        /// 备件状态
        /// </summary>
        [DataMember]
        [DisplayName("备件状态")]
        [ProtoMember(9)]
        public SparePartsStatus SparePartsStatus { get; set; }
        /// <summary>
        /// 出厂信息
        /// </summary>
        [DataMember]
        [DisplayName("出厂信息")]
        [ProtoMember(10)]
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
        [ProtoMember(12)]
        public string WareHousesId { get; set; }

        /// <summary>
        /// 电机设备编号
        /// </summary>
        [DataMember]
        [DisplayName("电机设备编号")]
        [ProtoMember(13)]
        public string MotorId { get; set; } = "Invalid";

        /// <summary>
        /// 是否删除
        /// </summary>
        [DataMember]
        [DisplayName("是否删除")]
        [ProtoMember(14)]
        public bool IsDelete { get; set; } 
    }
}
