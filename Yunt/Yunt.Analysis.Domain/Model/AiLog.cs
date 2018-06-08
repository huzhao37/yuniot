using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;

namespace Yunt.Analysis.Domain.Model
{
    /// <summary>
    /// 模拟量10min实时记录
    /// </summary>
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public class AiLog
    {
        /// <summary>
        /// 电机ID
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public string MotorId { get; set; }
        /// <summary>
        /// 电机名称
        /// </summary>
        [DataMember]
        [ProtoMember(2)]
        public string MotorName { get; set; }
        /// <summary>
        /// 产线ID
        /// </summary>
        [DataMember]
        [ProtoMember(3)]
        public int ProductionLineId { get; set; }
        /// <summary>
        /// 参数名称
        /// </summary>
        [DataMember]
        [ProtoMember(4)]
        public string Param { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        [DataMember]
        [ProtoMember(5)]
        public float Value { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        [DataMember]
        [ProtoMember(6)]
        public long Time { get; set; }

    }
}
