using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using ProtoBuf;
using Yunt.Common;
using Yunt.Common.ObjectExtentsions;
using Yunt.Device.Domain.Model;
using Yunt.Inventory.Domain.Model;

namespace Yunt.WebApiV2.Models.Inventories
{
    /// <summary>
    /// 出库
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public  class OutHouse
    {
        [DataMember]
        public long Id { get; set; }
        /// <summary>
        /// 备件编号
        /// </summary> 
        [DataMember]
        [DisplayName("批次")]
        public string BatchNo { get; set; }
   
        /// <summary>
        /// 出库时间
        /// </summary>
        [DataMember]
        [DisplayName("出库时间")]
        public long OutTime { get; set; }

        /// <summary>
        /// 出库
        /// </summary>
        [DataMember]
        [DisplayName("出库操作员")]
        public string OutOperator { get; set; }

        /// <summary>
        /// 报废时间
        /// </summary>
        [DataMember]
        [DisplayName("报废时间")]
        public long UselessTime { get; set; }
        /// <summary>
        /// 备件类型编号
        /// </summary>
        [DataMember]
        [DisplayName("备件类型编号")]
        public string SparePartsTypeId { get; set; }
        [DataMember]
        public string SparePartsTypeName { get; set; }
        /// <summary>
        /// 备件状态
        /// </summary>
        [DataMember]
        [DisplayName("备件类型状态")]
        public SparePartsTypeStatus SparePartsStatus { get; set; }      
        /// <summary>
        /// 电机设备编号
        /// </summary>
        [DataMember]
        [DisplayName("电机设备编号")]
        public string MotorId { get; set; } = "Invalid";
        [DataMember]
        public string MotorName { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [DataMember]
        [DisplayName("是否删除")]
        public bool IsDelete { get; set; }   
        /// <summary>
        /// 单价
        /// </summary>
        [DataMember]
        [DisplayName("单价")]
        public float UnitPrice { get; set; }

        public static OutHouse From(Inventory.Domain.Model.OutHouse t, IEnumerable<SparePartsType> sRepo,
           IEnumerable<Motor> mRepo)
        {

            if (!(t.Copy() is OutHouse entity))
                return new OutHouse();
            entity.SparePartsTypeName = sRepo.FirstOrDefault(e => e.Id == t.SparePartsTypeId)?.Name ?? "";
            entity.MotorName =
                mRepo.FirstOrDefault(e => e.MotorId.Equals(t.MotorId))?.Name ??
                "";
            return entity;

        }

        public static IEnumerable<OutHouse> Froms(IEnumerable<Inventory.Domain.Model.OutHouse> ts, IEnumerable<SparePartsType> sRepo,
           IEnumerable<Motor> mRepo)
        {
            return ts.Select(t => From(t, sRepo, mRepo)).ToList();
        }
    }
}
