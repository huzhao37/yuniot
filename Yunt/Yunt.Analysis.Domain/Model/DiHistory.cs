using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;

namespace Yunt.Analysis.Domain.Model
{
    /// <summary>
    /// Di实时记录
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public class DiHistory
    {
        /// <summary>
        /// 电机ID
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        [MaxLength(20)]
        public string MotorId { get; set; }
        /// <summary>
        /// 电机名称
        /// </summary>
        [DataMember]
        [ProtoMember(2)]
        [MaxLength(20)]
        public string MotorName { get; set; }
      
        /// <summary>
        /// 参数名称
        /// </summary>
        [DataMember]
        [ProtoMember(3)]
        [MaxLength(20)]
        public string Param { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        [DataMember]
        [ProtoMember(4)]
        public float Value { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        [DataMember]
        [ProtoMember(5)]
        public long Time { get; set; }

        /// <summary>
        /// 电机类型编码
        /// </summary>
        [DataMember]
        [ProtoMember(6)]
        [MaxLength(4)]
        public string MotorTypeId { get; set; }

        /// <summary>
        /// 数据物理特性
        /// </summary>
        [DataMember]
        [ProtoMember(7)]
        [MaxLength(4)]
        public string DataPhysic { get; set; }

    }
}
