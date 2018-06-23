using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;

namespace Yunt.Analysis.Repository.EF.Models
{
    /// <summary>
    /// 报警
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public  class AlarmInfo : BaseModel
    {
        /// <summary>
        /// 内容
        /// </summary> 
        [DataMember]
        [DisplayName("内容")]
        [ProtoMember(1)]
        public string Content { get; set; }

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
        /// 备注
        /// </summary>
        [DataMember]
        [DisplayName("备注")]
        [ProtoMember(4)]
        public string Remark { get; set; }

    }
}
