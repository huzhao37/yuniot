using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Yunt.Device.Repository.EF.Models
{
    /// <summary>
    /// 圆锥破碎机按小时统计数据;
    /// </summary>
     [Serializable]
   [ProtoContract(SkipConstructor = true)]
    public class ConeCrusherByHour : BaseModel
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [ProtoMember(9)]
        public string MotorId { get; set; }
        [ProtoMember(10)]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 电流;
        /// </summary>
        [ProtoMember(1)]
        public double AverageCurrent { get; set; }

        /// <summary>
        /// 主轴行程;
        /// </summary>
        [ProtoMember(2)]
        public double AverageSpindleTravel { get; set; }

        /// <summary>
        /// 圆锥压力;
        /// </summary>
        [ProtoMember(3)]
        public double AverageMovaStress { get; set; }

        /// <summary>
        /// 油箱温度;
        /// </summary>
        [ProtoMember(4)]
        public double AverageTankTemperature { get; set; }

        /// <summary>
        /// 供油温度;
        /// </summary>
        [ProtoMember(5)]
        public double AverageOilFeedTempreature { get; set; }

        /// <summary>
        /// 回油温度;
        /// </summary>
        [ProtoMember(6)]
        public double AverageOilReturnTempreature { get; set; }

        /// <summary>
        /// 开机时间
        /// </summary>
        [ProtoMember(7)]
        public double RunningTime { get; set; }

        /// <summary>
        /// 负荷
        /// </summary>
        [ProtoMember(8)]
        public double LoadStall { get; set; }
        //[ProtoMember(9)]


        //public DateTimeOffset Time { get; set; }

        ///// <summary>
        ///// 设备ID;
        ///// </summary>
        //[ProtoMember(10)]
        //public string MotorId { get; set; }
    }
}
