using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using ProtoBuf;

namespace Yunt.Analysis.Repository.EF.Models
{
    /// <summary>
    /// 事件类型
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public  class EventKind : BaseModel
    {
        /// <summary>
        /// 说明
        /// </summary> 
        [DataMember]
        [DisplayName("说明")]
        [ProtoMember(1)]
        public string Description { get; set; }

        /// <summary>
        /// 规则
        /// </summary> 
        [DataMember]
        [DisplayName("规则")]
        [ProtoMember(2)]
        public string Regulation { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [DataMember]
        [DisplayName("编码")]
        [ProtoMember(3)]
        public string Code { get; set; }

        /// <summary>
        /// 电机设备类型
        /// </summary>
        [DataMember]
        [DisplayName("电机设备类型")]
        [ProtoMember(4)]
        public string MotorTypeId { get; set; }

    }
}
