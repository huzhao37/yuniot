using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yunt.WebApiV2.Models.ProductionLines
{
    /// <summary>
    /// 产线状态
    /// </summary>
    public class ProductionLineStatus
    {
        /// <summary>
        /// 产线GPRS状态
        /// </summary>
        public bool Gprs { get; set; }
        /// <summary>
        /// 产线开关机状态
        /// </summary>
        public bool LineStatus { get; set; }
        /// <summary>
        /// 失联电机数目
        /// </summary>
        public int LoseMotors { get; set; }
        /// <summary>
        /// 停止电机数目
        /// </summary>
        public int StopMotors { get; set; }
        /// <summary>
        /// 运行电机数目
        /// </summary>
        public int RunMotors { get; set; }

        /// <summary>
        /// 报警电机数目
        /// </summary>
        public int AlarmMotors { get; set; }

    }
}
