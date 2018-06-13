using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;

namespace Yunt.Inventory.Repository.EF.Models
{
    [DataContract]
    [Serializable]
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    [ProtoInclude(11, typeof(InHouse))]
    [ProtoInclude(12, typeof(SparePartsType))]
    [ProtoInclude(13, typeof(WareHouses))]
    //[ProtoInclude(14, typeof(SparePartsIdFactories))]
    [ProtoInclude(14, typeof(InventoryAlarmInfo))]
    [ProtoInclude(15, typeof(OutHouse))]

    public class BaseModel
    {
        [DataMember]
        [ProtoMember(20)]
        public long Id { get; set; }
    }
}
