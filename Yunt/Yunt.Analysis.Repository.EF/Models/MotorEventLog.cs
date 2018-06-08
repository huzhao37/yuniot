using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;

namespace Yunt.Analysis.Repository.EF.Models
{
    /// <summary>
    /// 事件类型
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public  class MotorEventLog : BaseModel
    {
        /// <summary>
        /// 编码
        /// </summary> 
        [DataMember]
        [DisplayName("编码")]
        [ProtoMember(1)]
        public string EventCode { get; set; }

        /// <summary>
        /// 电机设备编号
        /// </summary> 
        [DataMember]
        [DisplayName("电机设备编号")]
        [ProtoMember(2)]
        public string MotorId { get; set; }
        /// <summary>
        /// 电机设备名称
        /// </summary>
        [DataMember]
        [DisplayName("电机设备名称")]
        [ProtoMember(3)]
        public string MotorName { get; set; }

        /// <summary>
        /// 产线编号
        /// </summary>
        [DataMember]
        [DisplayName("产线编号")]
        [ProtoMember(4)]
        public string ProductionLineId { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [DataMember]
        [DisplayName("说明")]
        [ProtoMember(5)]
        public string Description { get; set; }

    }
}
