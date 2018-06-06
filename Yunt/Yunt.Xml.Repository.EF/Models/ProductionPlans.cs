using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Yunt.Xml.Repository.EF.Models
{
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public partial class ProductionPlans : BaseModel
    {
        [DataMember]
        [ProtoMember(1)]
        public int Start { get; set; }
        [DataMember]
        [ProtoMember(2)]
        public int End { get; set; }
        [DataMember]
        [ProtoMember(3)]
        public int Main_Cy { get; set; }
        [DataMember]
        [ProtoMember(4)]
        public int Finish_Cy1 { get; set; }
        [DataMember]
        [ProtoMember(5)]
        public int Finish_Cy2 { get; set; }
        [DataMember]
        [ProtoMember(6)]
        public int Finish_Cy3 { get; set; }
        [DataMember]
        [ProtoMember(7)]
        public int Finish_Cy4 { get; set; }
        [DataMember]
        [ProtoMember(8)]
        public DateTime Time { get; set; }
        [DataMember]
        [ProtoMember(9)]
        public string Remark { get; set; }
        [DataMember]
        [ProtoMember(10)]
        public string Productionline_Id { get; set; }
    }
}
