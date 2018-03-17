
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Yunt.Device.Domain.BaseModel;


namespace Yunt.Device.Domain.Model
{
    /// <summary>
    /// 磨粉机
    /// </summary>
    [DataContract]
   
    public class Pulverizer : AggregateRoot
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public int MotorId { get; set; }
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 主机电流
        /// </summary>
        [DisplayName("主机电流")]
        ////[MotorConfig(IsAlarmProperty = true)]
     
        public double Current { get; set; }
        /// <summary>
        /// 鼓风机电流
        /// </summary>
        [DataMember]
        [DisplayName("鼓风机电流")]
       // //[MotorConfig(IsAlarmProperty = true)]
       
        public double FanCurrent { get; set; }
        /// <summary>
        /// 分级机电流
        /// </summary>
        [DataMember]
        [DisplayName("分级机电流")]
       // //[MotorConfig(IsAlarmProperty = true)]
        
        public double GraderCurrent { get; set; }
        /// <summary>
        /// 分级机转速
        /// </summary>
        [DataMember]
        [DisplayName("分级机转速")]
       // //[MotorConfig(IsAlarmProperty = true)]
       
        public double GraderRotateSpeed { get; set; }
        //[ProtoMember(5)]
        //public DateTimeOffset Time { get; set; }
        //[ProtoMember(6)]
        //public int MotorId { get; set; }
    }
}
