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
    /// 反击破碎机原始数据;
    /// </summary>
    [DataContract]
    [Serializable][ProtoContract(SkipConstructor = true)]
    public class ImpactCrusher : BaseModel
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [ProtoMember(5)]
        public int MotorId { get; set; }
        [ProtoMember(6)]
        public bool IsDeleted { get; set; }
        #region Original Param
        /// <summary>
        /// 电流
        /// </summary>
        [DisplayName("主机电机电流")]
        //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(1)]
        public double Current { get; set; }

        ///// <summary>
        ///// 振动
        ///// </summary>
        [DataMember]
        [DisplayName("主机电机电流2")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(2)]
        public double Current2 { get; set; }
        #endregion


        /// <summary>
        /// 电流
        /// </summary>
        [DataMember]
        [DisplayName("轴承温度1")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(3)]
        public double SpindleTemperature1 { get; set; }
        /// <summary>
        /// 电压
        /// </summary>
        [DataMember]
        [DisplayName("轴承温度2")]
       // //[MotorConfig(IsAlarmProperty = true)]
        [ProtoMember(4)]
        public double SpindleTemperature2 { get; set; }

        //[ProtoMember(5)]
        //public DateTimeOffset Time { get; set; }
        //[ProtoMember(6)]
        //public int MotorId { get; set; }
    }
}
