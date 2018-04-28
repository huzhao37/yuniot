using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yunt.MQ
{
    public enum DataType
    {
        /// <summary>
        /// 锁死应答
        /// </summary>
        Locker = 0,

        /// <summary>
        /// 单缸圆锥
        /// </summary>
        ConeCrusher = 1,

        /// <summary>
        /// 西蒙斯
        /// </summary>
        SimonsConeCrusher = 2,

        /// <summary>
        /// 磨粉机
        /// </summary>
        Pulverizer = 3,

        /// <summary>
        /// 立轴破
        /// </summary>
        VerticalCrusher = 4,

        /// <summary>
        /// 反击破
        /// </summary>
        ImpactCrusher = 5,

        /// <summary>
        /// 综合
        /// </summary>
        Integrate = 9,
    }
}
