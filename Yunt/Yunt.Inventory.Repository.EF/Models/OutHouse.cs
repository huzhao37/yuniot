using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;
using Yunt.Common;

namespace Yunt.Inventory.Repository.EF.Models
{
    /// <summary>
    /// 出库
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public partial class OutHouse : BaseModel
    {
        /// <summary>
        /// 备件编号
        /// </summary> 
        [DataMember]
        [DisplayName("批次")]
        [ProtoMember(1)]
        public string BatchNo { get; set; }
   
        /// <summary>
        /// 出库时间
        /// </summary>
        [DataMember]
        [DisplayName("出库时间")]
        [ProtoMember(2)]
        public long OutTime { get; set; }

        /// <summary>
        /// 出库
        /// </summary>
        [DataMember]
        [DisplayName("出库操作员")]
        [ProtoMember(3)]
        public string OutOperator { get; set; }

        /// <summary>
        /// 报废时间
        /// </summary>
        [DataMember]
        [DisplayName("报废时间")]
        [ProtoMember(4)]
        public long UselessTime { get; set; }
        /// <summary>
        /// 备件类型编号
        /// </summary>
        [DataMember]
        [DisplayName("备件类型编号")]
        [ProtoMember(5)]
        public int SparePartsTypeId { get; set; }
        /// <summary>
        /// 备件状态
        /// </summary>
        [DataMember]
        [DisplayName("备件类型状态")]
        [ProtoMember(6)]
        public SparePartsTypeStatus SparePartsStatus { get; set; }      
        /// <summary>
        /// 电机设备编号
        /// </summary>
        [DataMember]
        [DisplayName("电机设备编号")]
        [ProtoMember(7)]
        public string MotorId { get; set; } = "Invalid";
        /// <summary>
        /// 是否删除
        /// </summary>
        [DataMember]
        [DisplayName("是否删除")]
        [ProtoMember(8)]
        public bool IsDelete { get; set; }   
        /// <summary>
        /// 单价
        /// </summary>
        [DataMember]
        [DisplayName("单价")]
        [ProtoMember(9)]
        public float UnitPrice { get; set; }
    }
}
