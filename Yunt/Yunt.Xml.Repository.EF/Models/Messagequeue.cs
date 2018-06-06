using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Yunt.Xml.Repository.EF.Models
{
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public partial class Messagequeue : BaseModel
    {
        [DataMember]
        [ProtoMember(1)]
        public string Host { get; set; }
        [DataMember]
        [ProtoMember(2)]
        public int Port { get; set; }
        [DataMember]
        [ProtoMember(3)]
        public string Route_Key { get; set; }
        [DataMember]
        [ProtoMember(4)]
        public string Name { get; set; }
        [DataMember]
        [ProtoMember(5)]
        public int Timer { get; set; }
        [DataMember]
        [ProtoMember(6)]
        public string Collectdevice_Index { get; set; }
        [DataMember]
        [ProtoMember(7)]
        public int Write_Read { get; set; }
        [DataMember]
        [ProtoMember(8)]
        public string Com_Type { get; set; }
        [DataMember]
        [ProtoMember(9)]
        public DateTime Time { get; set; }
        [DataMember]
        [ProtoMember(10)]
        public string Remark { get; set; }
        [DataMember]
        [ProtoMember(11)]
        public string Username { get; set; }
        [DataMember]
        [ProtoMember(12)]
        public string Pwd { get; set; }
    }
}
