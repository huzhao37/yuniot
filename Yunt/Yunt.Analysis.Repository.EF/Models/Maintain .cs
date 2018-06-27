using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;

namespace Yunt.Analysis.Repository.EF.Models
{
    /// <summary>
    /// 维护保养
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public  class Maintain : BaseModel
    {
        /// <summary>
        /// 时长
        /// </summary> 
        [DataMember]
        [DisplayName("时长")]
        [ProtoMember(1)]
        public float  Duration { get; set; }

        /// <summary>
        /// 电机设备编号
        /// </summary> 
        [DataMember]
        [DisplayName("电机设备编号")]
        [ProtoMember(2)]
        public string MotorId { get; set; }
        /// <summary>
        /// 维护人员
        /// </summary>
        [DataMember]
        [DisplayName("维护人员")]
        [ProtoMember(3)]
        public string Operator { get; set; }

        /// <summary>
        /// 维护记录
        /// </summary>
        [DataMember]
        [DisplayName("维护记录")]
        [ProtoMember(4)]
        public string Record { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        [DisplayName("备注")]
        [ProtoMember(5)]
        public string Remark { get; set; }

    }
}
