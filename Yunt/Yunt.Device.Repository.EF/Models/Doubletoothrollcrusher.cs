using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;


namespace Yunt.Device.Repository.EF.Models
{
    [DataContract]
    [Serializable][ProtoContract(SkipConstructor = true)]
    public class DoubleToothRollCrusher : BaseModel
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [ProtoMember(3)]
        public string MotorId { get; set; }

  
        /// <summary>
        /// 主机1电流
        /// </summary>
        [DisplayName("主机电流1")]
        [ProtoMember(1)]
        public float Current { get; set; }
        /// <summary>
        /// 主机2电流
        /// </summary>
        [DataMember]
        [DisplayName("主机电流2")]
        [ProtoMember(2)]
        public float Current2 { get; set; }
        ///// <summary>
        ///// 时间
        ///// </summary>
        //[ProtoMember(3)]
        //public DateTimeOffset Time { get; set; }
        ///// <summary>
        ///// 设备ID
        ///// </summary>
        //[ProtoMember(4)]
        //public string MotorId { get; set; }
    }
}
