﻿using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yunt.Device.Repository.EF.Models
{
     [Serializable][ProtoContract(SkipConstructor = true)]
    public class DoubleToothRollCrusherByHour : BaseModel
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [ProtoMember(5)]
        public string MotorId { get; set; }

  
        /// <summary>
        /// 主机1电流
        /// </summary>
        [ProtoMember(1)]
        public float Current { get; set; }
        /// <summary>
        /// 主机2电流
        /// </summary>
        [ProtoMember(2)]
        public float Current2 { get; set; }

        /// <summary>
        /// 开机时间
        /// </summary>
        [ProtoMember(3)]
        public float RunningTime { get; set; }
        /// <summary>
        /// 负荷
        /// </summary>
        [ProtoMember(4)]
        public float LoadStall { get; set; }
    
    }
}
