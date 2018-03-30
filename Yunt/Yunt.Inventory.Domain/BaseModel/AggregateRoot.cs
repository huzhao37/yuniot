﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;
using Yunt.Inventory.Domain.Model;
using Yunt.Inventory.Domain.Model.IdModel;

namespace Yunt.Inventory.Domain.BaseModel
{
    /// <summary>
    /// 聚合根的抽象实现类，定义聚合根的公共属性和行为
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [ProtoInclude(11, typeof(SpareParts))]
    [ProtoInclude(12, typeof(SparePartsType))]
    [ProtoInclude(13, typeof(WareHouses))]
    [ProtoInclude(14, typeof(SparePartsIdFactories))]
    [ProtoInclude(15, typeof(InventoryAlarmInfo))]

    public abstract class AggregateRoot : IAggregateRoot
    {
        [DataMember]
        [ProtoMember(20)]
        public long Id { get; set; }
        //[DataMember]
        //[ProtoMember(21)]
        //public long CreateTime { get; set; }
    }
}