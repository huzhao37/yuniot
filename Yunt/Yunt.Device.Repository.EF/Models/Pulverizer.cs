
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;


namespace Yunt.Device.Repository.EF.Models
{
    /// <summary>
    /// 磨粉机
    /// </summary>
    [DataContract]
    [Serializable][ProtoContract(SkipConstructor = true)]
    public class Pulverizer : BaseModel
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [ProtoMember(5)]
        public string MotorId { get; set; }
        [ProtoMember(6)]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 主机电流
        /// </summary>
        [DisplayName("主机电流")]
        ////[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(1)]
        public double Current { get; set; }
        /// <summary>
        /// 鼓风机电流
        /// </summary>
        [DataMember]
        [DisplayName("鼓风机电流")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(2)]
        public double FanCurrent { get; set; }
        /// <summary>
        /// 分级机电流
        /// </summary>
        [DataMember]
        [DisplayName("分级机电流")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(3)]
        public double GraderCurrent { get; set; }
        /// <summary>
        /// 分级机转速
        /// </summary>
        [DataMember]
        [DisplayName("分级机转速")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(4)]
        public double GraderRotateSpeed { get; set; }
        //[ProtoMember(5)]
        //public DateTimeOffset Time { get; set; }
        //[ProtoMember(6)]
        //public string MotorId { get; set; }
    }
}
