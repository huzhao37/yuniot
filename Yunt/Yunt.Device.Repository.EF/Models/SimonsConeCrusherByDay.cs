﻿using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yunt.Device.Repository.EF.Models
{
     [Serializable][ProtoContract(SkipConstructor = true)]
    public class SimonsConeCrusherByDay : BaseModel
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [ProtoMember(7)]
        public string MotorId { get; set; }
      
  
        [ProtoMember(1)]
        public float AvgTankTemperature { get; set; }
        [ProtoMember(2)]
        public float AvgOilFeedTempreature { get; set; }
        [ProtoMember(3)]
        public float AvgOilReturnTempreatur { get; set; }
        [ProtoMember(4)]
        public float AvgCurrent_B { get; set; }
        /// <summary>
        /// 开机时间
        /// </summary>
        [ProtoMember(5)]
        public float RunningTime { get; set; }
        /// <summary>
        /// 负荷
        /// </summary>
        [ProtoMember(6)]
        public float LoadStall { get; set; }
        //[ProtoMember(7)]
        //public DateTime Time { get; set; }
        ///// <summary>
        ///// 设备ID
        ///// </summary>
        //[ProtoMember(8)]
        //public string MotorId { get; set; }
    }
}
