﻿using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yunt.Device.Repository.EF.Models
{
    /// <summary>
    /// 颚式破碎机按天统计数据;
    /// </summary>
     [Serializable][ProtoContract(SkipConstructor = true)]
    public class JawCrusherByDay : BaseModel
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [ProtoMember(12)]
        public string MotorId { get; set; }
        [ProtoMember(13)]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 平均电流;
        /// </summary>
        [ProtoMember(1)]
        public double AverageCurrent { get; set; }
        /// <summary>
        /// 平均电压;
        /// </summary>
        [ProtoMember(2)]
        public double AverageVoltage { get; set; }
        /// <summary>
        /// 功率因子;
        /// </summary>
        [ProtoMember(3)]
        public double AveragePowerFactor { get; set; }
        /// <summary>
        /// 无功功率;
        /// </summary>
        [ProtoMember(4)]
        public double AverageReactivePower { get; set; }
        /// <summary>
        /// 总功率;
        /// </summary>
        [ProtoMember(5)]
        public double AverageTotalPower { get; set; }
        [ProtoMember(6)]
        public double AverageRackSpindleTemperature1 { get; set; }
        [ProtoMember(7)]
        public double AverageRackSpindleTemperature2 { get; set; }
        [ProtoMember(8)]
        public double AverageMotiveSpindleTemperature1 { get; set; }
        [ProtoMember(9)]
        public double AverageMotiveSpindleTemperature2 { get; set; }

        /// <summary>
        /// 开机时间
        /// </summary>
        [ProtoMember(10)]
        public double RunningTime { get; set; }
        /// <summary>
        /// 负荷
        /// </summary>
        [ProtoMember(11)]
        public double LoadStall { get; set; }
        //[ProtoMember(12)]
        //public DateTimeOffset Time { get; set; }
        //[ProtoMember(13)]
        //public string MotorId { get; set; }
    }
}
