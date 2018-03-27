using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;
using Yunt.Inventory.Domain.Model.IdModel;

namespace Yunt.Inventory.Repository.EF.Models
{
    [DataContract]
    [Serializable]
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [ProtoInclude(11, typeof(Domain.Model.SpareParts))]
    [ProtoInclude(12, typeof(Domain.Model.SparePartsType))]
    [ProtoInclude(13, typeof(Domain.Model.WareHouses))]
    [ProtoInclude(14, typeof(SparePartsIdFactories))]
    [ProtoInclude(15, typeof(Domain.Model.InventoryAlarmInfo))]

    public class BaseModel
    {
        [DataMember]
        [ProtoMember(20)]
        public long Id { get; set; }
    }
}
