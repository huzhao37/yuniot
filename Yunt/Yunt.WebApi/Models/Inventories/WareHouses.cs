using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using ProtoBuf;
using Yunt.Common.ObjectExtentsions;
using Yunt.Inventory.Domain.BaseModel;
using Yunt.Xml.Domain.Model;

namespace Yunt.WebApi.Models.Inventories
{
    /// <summary>
    /// 仓库
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public class WareHouses 
    {
        [DataMember]
        public long Id { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary> 
        [DataMember]
        [DisplayName("仓库名称")]
        public string Name { get; set; }

        /// <summary>
        /// 仓库管理员
        /// </summary> 
        [DataMember]
        [DisplayName("仓库管理员")]
        public string Keeper { get; set; }

        /// <summary>
        /// 仓库设备类型
        /// </summary> 
        [DataMember]
        [DisplayName("仓库设备类型")]
        public string MotorTypeId { get; set; }
        [DataMember]
        public string MotorTypeName { get; set; }
        /// <summary>
        /// 备注
        /// </summary> 
        [DataMember]
        [DisplayName("备注")]
        public string Remark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary> 
        [DataMember]
        [DisplayName("创建时间")]
        public long CreateTime { get; set; }

        /// <summary>
        /// 产线编码
        /// </summary> 
        [DataMember]
        [DisplayName("产线编码")]
        public string ProductionLineId { get; set; }

        public static WareHouses From(Inventory.Domain.Model.WareHouses t,IEnumerable<Motortype> mRepo)
        {
            if (!(t.CopySameFieldsObject<WareHouses>() is WareHouses entity))
                return new WareHouses();
            entity.MotorTypeName =
                mRepo.FirstOrDefault(e => e.MotorTypeId.Equals(t.MotorTypeId))?.MotorTypeName ??
                "";
            return entity;

        }

        public static IEnumerable<WareHouses> Froms(IEnumerable<Inventory.Domain.Model.WareHouses> ts,
           IEnumerable<Motortype> mRepo)
        {
            return ts.Select(t => From(t, mRepo)).ToList();
        }
    }
}
