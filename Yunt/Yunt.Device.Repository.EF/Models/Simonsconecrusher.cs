
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
    /// 西蒙斯圆锥破碎机
    /// </summary>
    [DataContract]
    [Serializable][ProtoContract(SkipConstructor = true)]
    public class SimonsConeCrusher : BaseModel
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [ProtoMember(4)]
        public int MotorId { get; set; }
        [ProtoMember(5)]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 油箱温度;
        /// </summary>
        [DataMember]
        [DisplayName("油箱温度")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(1)]
        public double TankTemperature { get; set; }
        /// <summary>
        /// 供油温度;
        /// </summary>
        [DataMember]
        [DisplayName("供油温度")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(2)]
        public double OilFeedTempreature { get; set; }
        /// <summary>
        /// 回油温度;
        /// </summary>
        [DataMember]
        [DisplayName("回油温度")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(3)]
        public double OilReturnTempreature { get; set; }
        /// <summary>
        /// 主机电流;
        /// </summary>
        //[DisplayName("主机电流")]
        ////[MotorConfig(IsAlarmProperty = true)]
        //[ProtoMember(4)]
        //public double Current { get; set; }
        //[ProtoMember(5)]
        //public DateTimeOffset Time { get; set; }
        ///// <summary>
        ///// 设备ID
        ///// </summary>
        //[ProtoMember(6)]
        //public int MotorId { get; set; }
    }
}
