using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using ProtoBuf;

namespace Yunt.Device.Repository.EF.Models.IdModel
{
    [DataContract]
    [Serializable]
    [ProtoContract(SkipConstructor = true)]
    public class MotorIdFactories:BaseModel
    {
        /// <summary>
        /// 产线Id
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public string ProductionLineId { get; set; }
        /// <summary>
        /// 电机设备类型Id
        /// </summary>

        [DataMember]
        [ProtoMember(2)]
        public string MotorTypeId { get; set; }
        /// <summary>
        /// 电机设备序号
        /// </summary>

        [DataMember]
        [ProtoMember(3)]
        public int MotorIndex { get; set; }
    }
}
