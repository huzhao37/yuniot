using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using ProtoBuf;
using Yunt.Common.ObjectExtentsions;
using Yunt.Inventory.Domain.IRepository;
using Yunt.Inventory.Domain.Model;

namespace Yunt.WebApi.Models.Inventories
{
    /// <summary>
    /// 入库
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public class InHouse
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
        /// 入库时间
        /// </summary>
        [DataMember]
        [DisplayName("入库时间")]
        public long InTime { get; set; }

        /// <summary>
        /// 入库
        /// </summary>
        [DataMember]
        [DisplayName("入库操作员")]
        public string InOperator { get; set; }

        /// <summary>
        /// 备件类型编号
        /// </summary>
        [DataMember]
        [DisplayName("备件类型编号")]
        public int SparePartsTypeId { get; set; }

        [DataMember]
        public string SparePartsTypeName { get; set; }

        /// <summary>
        /// 出厂信息
        /// </summary>
        [DataMember]
        [DisplayName("出厂信息")]
        public string FactoryInfo { get; set; }

        /// <summary>
        /// 备件说明
        /// </summary>
        [DataMember]
        [DisplayName("备件说明")]
        public string Description { get; set; }

        /// <summary>
        /// 仓库编号
        /// </summary>
        [DataMember]
        [DisplayName("仓库编号")]
        public int WareHousesId { get; set; }

        [DataMember]
        public string WareHousesName { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DataMember]
        [DisplayName("是否删除")]
        public bool IsDelete { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        [DisplayName("数量")]
        public int Count { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        [DataMember]
        [DisplayName("单价")]
        public float UnitPrice { get; set; }


        public static InHouse From(Inventory.Domain.Model.InHouse t, IEnumerable<SparePartsType> sRepo,
            IEnumerable<Inventory.Domain.Model.WareHouses> wRepo)
        {

            if (!(t.Copy() is InHouse entity))
                return new InHouse();
            entity.SparePartsTypeName = sRepo.FirstOrDefault(e => e.Id==t.SparePartsTypeId)?.Name ?? "";
                entity.WareHousesName =
                    wRepo.FirstOrDefault(e => e.Id==t.WareHousesId)?.Name ??
                    "";
                return entity;
            
        }

        public static IEnumerable<InHouse> Froms(IEnumerable<Inventory.Domain.Model.InHouse> ts, 
            IEnumerable<SparePartsType> sRepo,
           IEnumerable<Inventory.Domain.Model.WareHouses> wRepo)
        {
            return ts.Select(t => From(t, sRepo, wRepo)).ToList();
        }
    }
}
